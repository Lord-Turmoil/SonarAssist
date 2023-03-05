using SonarAssist.Common;
using SonarAssist.Common.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonarAssist.Services
{
	public abstract class SonarService
	{
		protected SonarService() { }

		public ServiceStatus Status { get; set; }

		public abstract Task ExecuteAsync(Dictionary<string, string>? parameters = null);
		public abstract void Execute(Dictionary<string, string>? parameters = null);

		protected void _HandleError(RequestFailedException ex)
		{
			switch (ex.Response.StatusCode)
			{
			case System.Net.HttpStatusCode.NotFound:
				Status = ServiceStatus.NotFound;
				break;
			case System.Net.HttpStatusCode.Unauthorized:
				Status = ServiceStatus.Unauthorized;
				break;
			case System.Net.HttpStatusCode.Forbidden:
				Status = ServiceStatus.Forbidden;
				break;
			case System.Net.HttpStatusCode.BadRequest:
				Status = ServiceStatus.BadRequest;
				break;
			default:
				Status = ServiceStatus.Error;
				break;
			}

			if (ex.Response.Content != null)
			{
				foreach (var err in ex.Response.Content.errors)
				{
					Logger.LogError(err.msg);
				}
			}
		}
	}
}
