using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SonarAssist.Services.Responses
{
	public class SonarResponse<TDto> where TDto : ISonarResponseDto
	{
		public HttpStatusCode StatusCode { get; set; }

		/// <summary>
		/// Content is serialized to class object.
		/// </summary>
		public TDto? Content { get; set; }

		public SonarResponse() { }	// Deprecated

		public SonarResponse(RestResponse response)
		{
			StatusCode = response.StatusCode;
			if (!string.IsNullOrEmpty(response.Content))
				Content = JsonConvert.DeserializeObject<TDto>(response.Content);
		}
	}
}
