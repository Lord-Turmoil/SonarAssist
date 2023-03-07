using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonarAssist.Services.Requests.Impl
{
	/// <summary>
	/// Just use bulk delete all the time. This one is a little different,
	/// because it needs multiple project keys as one parameter.
	/// </summary>
	public class DeleteProjectRequest : SonarRequest
	{	
		public DeleteProjectRequest()
		{
			Method = RestSharp.Method.Post;
			Route = @"api/projects/bulk_delete";
		}
	}
}
