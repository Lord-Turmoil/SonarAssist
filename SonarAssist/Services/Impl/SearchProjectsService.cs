using DryIoc;
using SonarAssist.Common;
using SonarAssist.Common.Exceptions;
using SonarAssist.Services.Requests;
using SonarAssist.Services.Requests.Impl;
using SonarAssist.Services.Responses;
using SonarAssist.Services.Responses.Dtos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonarAssist.Services.Impl
{
	public class SearchProjectsService : SonarService
	{
		private List<Component> _components = new List<Component>();

		public override async Task ExecuteAsync(Dictionary<string, string>? parameters = null)
		{
			SearchProjectsRequest request = new SearchProjectsRequest();
			if (parameters != null)
			{
				foreach (var param in parameters)
				{
					request.Parameters[param.Key] = param.Value;
				}
			}

			var client = Global.Container.Resolve<SonarClient>();
			try
			{
				var pageIndex = 1;
				while (await _GetPageAsync(pageIndex, parameters))
				{
					pageIndex++;
				}
			}
			catch (RequestFailedException ex)
			{
				_HandleError(ex);
				return;
			}
			catch (Exception ex)
			{
				Logger.LogError("Uncaught error");
				Logger.LogError(ex.ToString());
				return;
			}

			_LogResponse();
			Status = ServiceStatus.OK;
		}

		private async Task<bool> _GetPageAsync(int pageIndex, Dictionary<string, string>? parameters)
		{
			SearchProjectsRequest request = new SearchProjectsRequest();
			if (parameters != null)
			{
				foreach (var param in parameters)
					request.Parameters[param.Key] = param.Value;
			}
			request.Parameters["p"] = pageIndex.ToString();

			var client = Global.Container.Resolve<SonarClient>();
			SonarResponse<SearchProjectDto> response;

			try
			{
				response = await client.ExecuteAsync<SearchProjectDto>(request);
				_RecordResponse(response);
			}
			catch (RequestFailedException ex)
			{
				throw ex;
			}

			if (response.Content == null)
			{
				throw new RequestFailedException("Missing Search Project content");
			}

			var pageSize = response.Content.paging.pageSize;
			var total = response.Content.paging.total;

			return pageSize * pageIndex < total;
		}

		private void _RecordResponse(SonarResponse<SearchProjectDto> response)
		{
			if (response.Content == null)
				return;

			foreach (var comp in response.Content.components)
				_components.Add(comp);
		}

		private void _LogResponse()
		{
			string filename = Path.Combine(
				Path.GetFullPath(Global.StartupPath),
				Constants.GLOBAL_LOG_PATH,
				"SearchProject",
				DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss-ms") + ".json");

			SearchProjectDto logs = new SearchProjectDto()
			{
				paging = new Paging()
				{
					pageIndex = 1,                  // nonsense
					pageSize = _components.Count,   // nonsense
					total = _components.Count
				},
				components = _components.ToArray()
			};

			Logger.Dump(logs, filename);
		}

		public override void Execute(Dictionary<string, string>? parameters = null)
		{
			throw new NotImplementedException();
		}
	}
}
