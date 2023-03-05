using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonarAssist.Services.Requests.Impl
{
	public class CreateProjectRequest : SonarRequest
	{
		public CreateProjectRequest() {
			Method = RestSharp.Method.Post;
			Route = @"api/projects/create";
		}
	}
}
