using DryIoc;
using SonarAssist.Common;
using SonarAssist.Common.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonarAssist.Services.Requests.Impl
{
	/// <summary>
	/// Its response is 204 No Content
	/// Requires external assigned project key.
	/// </summary>
	public class UpdateProjectRequest : SonarRequest
	{
        public UpdateProjectRequest()
        {
			Method = RestSharp.Method.Post;
			Route = @"api/qualityprofiles/add_project";

			var config = Global.Container.Resolve<GlobalConfiguration>();

			AddParameter("language", "java");
			AddParameter("qualityProfile", config.Profile);
		}
    }
}
