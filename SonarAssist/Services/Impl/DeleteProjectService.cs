using DryIoc;
using SonarAssist.Common;
using SonarAssist.Common.Exceptions;
using SonarAssist.Services.Requests.Impl;
using SonarAssist.Services.Responses.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonarAssist.Services.Impl
{
	public class DeleteProjectService : SonarService
	{
		private List<string>? _projectKeys;

		public override async Task ExecuteAsync(Dictionary<string, string>? parameters = null)
		{
			DeleteProjectRequest request = new DeleteProjectRequest();
			request.AddParameter("projects", _BuildParameter());

			var client = Global.Container.Resolve<SonarClient>();
			try
			{
				await client.ExecuteAsync<DeleteProjectDto>(request);
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

		public DeleteProjectService AddProjectKey(string key)
		{
			if (_projectKeys == null)
				_projectKeys = new List<string>();
			_projectKeys.Add(key);
			return this;
		}

		private string _BuildParameter()
		{
			if (_projectKeys == null)
				return string.Empty;
			StringBuilder builder = new StringBuilder();
			foreach (string key in _projectKeys)
			{
				if (builder.Length > 0)
					builder.Append(",");
				builder.Append(key);
			}
			return builder.ToString();
		}

		public override void Execute(Dictionary<string, string>? parameters = null)
		{
			throw new NotImplementedException();
		}
	}
}
