using DryIoc;
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
	/// Update project quality profile.
	/// </summary>
	public class UpdateProjectService : SonarService
	{
		private string _root = "";
		private string _profile = "";
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

			UpdateProjectRequest request = _BuildRequest();
			var client = Global.Container.Resolve<SonarClient>();
			SonarResponse<UpdateProjectDto> response;
			try
			{
				response = await client.ExecuteAsync<UpdateProjectDto>(request);
			}
			catch (RequestFailedException ex)
			{
				_HandleError(ex);
				return;
			}
			catch (Exception ex)
			{
				_HandleUncaughtError(ex);
				return;
			}

			Status = ServiceStatus.OK;
		}

		private UpdateProjectRequest _BuildRequest()
		{
			UpdateProjectRequest request = new UpdateProjectRequest();
			request.AddParameter("project", _config.key);
			return request;
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

		private bool _ParseParameters(Dictionary<string, string> parameters)
		{
			return parameters.TryGetValue("root", out _root);
		}

		public override void Execute(Dictionary<string, string>? parameters = null)
		{
			throw new NotImplementedException();
		}
	}
}
