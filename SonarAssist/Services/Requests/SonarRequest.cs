using RestSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonarAssist.Services.Requests
{
	public abstract class SonarRequest
	{
		protected SonarRequest() { }

		public Method Method { get; set; }

		/// <summary>
		/// e.g. api/authentication/validate
		/// </summary>
		public string Route { get; set; } = "";

		/// <summary>
		/// SonarQube api only support inline parameters.
		/// </summary>
		public Dictionary<string, string> Parameters { get; set; }

		public SonarRequest AddParameter(string name, string value)
		{
			if (Parameters == null)
				Parameters = new Dictionary<string, string>();
			Parameters[name] = value;
			return this;
		}

		public SonarRequest AddParameter(string name, int value)
		{
			return AddParameter(name, value.ToString());
		}

		public SonarRequest RemoveParameter(string name)
		{
			if (Parameters != null)
				Parameters.Remove(name);
			return this;
		}
	}
}
