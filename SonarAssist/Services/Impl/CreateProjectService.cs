﻿using DryIoc;
using SonarAssist.Common;
using SonarAssist.Common.Config;
using SonarAssist.Common.Exceptions;
using SonarAssist.Services.Requests.Impl;
using SonarAssist.Services.Responses;
using SonarAssist.Services.Responses.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonarAssist.Services.Impl
{
	/// <summary>
	/// This will create a project on SonarQube server. The status of
	/// this service is special, it is YES or NO, both are acceptable,
	/// but NO indicates that the project is already created.
	/// </summary>
	public class CreateProjectService : SonarService
	{
		private string _root = "";
		private LocalConfiguration _config;

		public override async Task ExecuteAsync(Dictionary<string, string>? parameters = null)
		{
			if (parameters == null || !_ParseParameters(parameters))
			{
				Status = ServiceStatus.Error;
				throw new ArgumentException("Arguments illegal!");
			}

			Directory.SetCurrentDirectory(Path.GetFullPath(_root));

			if (!_CheckPrerequisites())
			{
				Status = ServiceStatus.Error;
				throw new ServiceErrorException("Project not initialized");
			}

			CreateProjectRequest request = _BuildRequest();
			var client = Global.Container.Resolve<SonarClient>();
			SonarResponse<CreateProjectDto> response;
			try
			{
				response = await client.ExecuteAsync<CreateProjectDto>(request);
			}
			catch (RequestFailedException ex)
			{
				_HandleError(ex);
				// Duplicated project.
				if (ex.Response.StatusCode == System.Net.HttpStatusCode.BadRequest)
				{
					Status = ServiceStatus.NO;
				}
				return;
			}
			catch (Exception ex)
			{
				_HandleUncaughtError(ex);
				return;
			}

			_LogResponse(response);
			Status = ServiceStatus.YES;
		}

		private bool _ParseParameters(Dictionary<string, string> parameters)
		{
			return parameters.TryGetValue("root", out _root);
		}

		private bool _CheckPrerequisites()
		{
			_config = ConfigManager.Load<LocalConfiguration>(Constants.LOCAL_CONFIG_FILENAME);
			if (_config == null)
				return false;
			if (string.IsNullOrEmpty(_config.key))
				return false;
			return true;
		}

		private CreateProjectRequest _BuildRequest()
		{
			CreateProjectRequest request = new CreateProjectRequest();
			request.AddParameter("project", _config.key);
			request.AddParameter("name", _config.name);
			request.AddParameter("visibility", "public");
			return request;
		}

		private void _LogResponse(SonarResponse<CreateProjectDto> response)
		{
			string filename = Path.Combine(
				Path.GetFullPath(Constants.GLOBAL_LOG_PATH),
				"CreateProject",
				DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss-ms") + ".json");

			Logger.Dump(response.Content, filename);
		}

		public override void Execute(Dictionary<string, string>? parameters = null)
		{
			throw new NotImplementedException();
		}
	}
}
