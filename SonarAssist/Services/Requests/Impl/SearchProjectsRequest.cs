using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonarAssist.Services.Requests.Impl
{
	/// <summary>
	/// Search projects.
	/// </summary>
	public class SearchProjectsRequest : SonarRequest
	{
		public SearchProjectsRequest()
		{
			Method = RestSharp.Method.Get;
			Route = @"api/projects/search";

			AddParameter("ps", "500");
		}
	}
}
