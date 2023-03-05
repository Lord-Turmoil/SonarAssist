using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonarAssist.Common.Config
{
	public class LocalConfiguration : IConfiguration
	{
		/// <summary>
		/// SonarQube project key. This determines whether a project
		/// is initialized or not.
		/// </summary>
		public string key { get; set; } = "";

		/// <summary>
		/// SonarQube project name.
		/// </summary>
		public string name { get; set; } = "";

		/// <summary>
		/// Java source file directory.
		/// </summary>
		public string src { get; set; } = Constants.LOCAL_SRC_PATH;

		/// <summary>
		/// Java class file output directory.
		/// </summary>
		public string output { get; set; } = Constants.LOCAL_OUT_PATH;
	}
}
