using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonarAssist.Common.Config
{
	public class GlobalConfiguration : IConfiguration
	{
		/// <summary>
		/// SonarQube server url, must contain protocol (http:// or https://)
		/// and port (:9000 or else).
		/// </summary>
		public string Server { get; set; } = "";

		/// <summary>
		/// SonarQube token, must have administrative permission.
		/// </summary>
		public string Token { get; set; } = "";

		/// <summary>
		/// SonarQube quality profile. Has default value.
		/// </summary>
		private string _profile = Constants.DEFAULT_PROFILE;
		public string Profile
		{
			get { return _profile; }
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					_profile = value;
				}
			}
		}
	}
}
