using DryIoc;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using SonarAssist.Common;
using SonarAssist.Common.Config;
using SonarAssist.Common.Exceptions;
using SonarAssist.Services.Requests;
using SonarAssist.Services.Responses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonarAssist.Services
{
	/// <summary>
	/// Ensure configs are OK before you create SonarClient.
	/// </summary>
	public class SonarClient
	{
		private RestClient _client;

		public SonarClient()
		{
			var config = Global.Container.Resolve<GlobalConfiguration>();
			var options = new RestClientOptions(config.Server)
			{
				MaxTimeout = 5 * 1000,
				Encoding = Encoding.UTF8,
			};
			_client = new RestClient(options);
			_client.Authenticator = new HttpBasicAuthenticator(config.Token, "");
		}

		/// <summary>
		/// Execute specified sonar request.
		/// </summary>
		/// <typeparam name="TDto">Response JSON DTO type.</typeparam>
		/// <param name="request">Specified request.</param>
		/// <returns>Return SonarResponse.</returns>
		/// <exception cref="RequestFailedException"></exception>
		public async Task<SonarResponse<TDto>> ExecuteAsync<TDto>(SonarRequest request) where TDto : ISonarResponseDto
		{
			var restRequest = new RestRequest(request.Route, request.Method);
			if (request.Parameters != null)
			{
				foreach (var param in request.Parameters)
					restRequest.AddQueryParameter(param.Key, param.Value);
			}

			RestResponse response = await _client.ExecuteAsync(restRequest);
			if (_IsGoodResponse(response))
				return new SonarResponse<TDto>(response);
			throw new RequestFailedException(response);
		}

		private bool _IsGoodResponse(RestResponse response)
		{
			return (response.StatusCode == System.Net.HttpStatusCode.OK)
				|| (response.StatusCode == System.Net.HttpStatusCode.NoContent);
		}
	}
}
