using DryIoc;
using SonarAssist.Common;
using SonarAssist.Common.Config;
using SonarAssist.Common.Exceptions;
using SonarAssist.Extensions;
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

		public override async Task ExecuteAsync(Dictionary<string, string>? parameters = null)
		{
			if (parameters == null || !_ParseParameters(parameters))
			{
				Status = ServiceStatus.Error;
				throw new ArgumentException(ExceptionMessage.BadArguments);
			}

			Directory.SetCurrentDirectory(Path.GetFullPath(_root));

			if (!SonarUtils.IsInitialized())
			{
				Status = ServiceStatus.Error;
				throw new ServiceErrorException(ExceptionMessage.NotInitialized);
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
			var config = SonarUtils.GetLocalConfiguration();
			request.AddParameter("project", config.key);
			return request;
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
