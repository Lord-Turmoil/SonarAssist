using dotnetCampus.Configurations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonarAssist.Common.Config
{
	internal class GlobalConfiguration : Configuration
	{
		/// <summary>
		/// SonarQube server url, must contain protocol (http:// or https://)
		/// and port (:9000 or else).
		/// </summary>
		internal string? Server { get; set; }

		/// <summary>
		/// SonarQube token, must have administrative permission.
		/// </summary>
		internal string? Token { get; set; }

		/// <summary>
		/// SonarQube quality profile. Has default value.
		/// </summary>
		internal string Profile
		{
			get => GetString() ?? Constants.DEFAULT_PROFILE;
			set => SetValue(Equals(value, Constants.DEFAULT_PROFILE) ? null : value);
		}
	}
}
