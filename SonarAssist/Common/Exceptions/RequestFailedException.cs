using RestSharp;
using SonarAssist.Services.Responses;
using SonarAssist.Services.Responses.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SonarAssist.Common.Exceptions
{
	public class RequestFailedException : Exception
	{
		public SonarResponse<ErrorDto> Response { get; private set; }

		public RequestFailedException(string message)
		{
			Response = new SonarResponse<ErrorDto>()
			{
				StatusCode = HttpStatusCode.BadRequest,
				Content = new ErrorDto()
				{
					errors = new Error[] {
							new Error() { msg = message },
							new Error() { msg = "Missing content" }
						}
				}
			};
		}

		public RequestFailedException(RestResponse response)
		{
			try
			{
				switch (response.StatusCode)
				{
				case HttpStatusCode.Forbidden:
				case HttpStatusCode.BadRequest:
				case HttpStatusCode.NotFound:
					Response = new SonarResponse<ErrorDto>(response);
					break;
				default:
					Response = new SonarResponse<ErrorDto>()
					{
						StatusCode = response.StatusCode,
						Content = new ErrorDto()
						{
							errors = new Error[] {
								new Error() { msg = response.StatusCode.ToString() }
							}
						}
					};
					break;
				}
			}
			catch (Exception ex)
			{
				Response = new SonarResponse<ErrorDto>()
				{
					StatusCode = response.StatusCode,
					Content = new ErrorDto()
					{
						errors = new Error[] {
							new Error() { msg = response.StatusCode.ToString() },
							new Error() { msg = ex.ToString() },
							new Error() { msg = "Unexpected content" }
						}
					}
				};
			}
		}
	}
}
