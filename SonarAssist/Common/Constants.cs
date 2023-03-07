using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonarAssist.Common
{
	/// <summary>
	/// For global constant strings.
	/// </summary>
	public class Constants
	{
		private Constants() { }

		public const string CURRENT_PATH = @".\";

		public const string GLOBAL_CONFIG_FILENAME = @"config\config.json";
		public const string GLOBAL_LOG_PATH = @"logs\";

		public const string LOCAL_PATH = @"sonar-assist\";
		public const string LOCAL_SRC_PATH = @"src\";
		public const string LOCAL_OUT_PATH = LOCAL_PATH + @"output\";
		public const string LOCAL_CONFIG_FILENAME = LOCAL_PATH + "sonar-assist.json";

		public const string SONAR_FILENAME = "sonar-project.properties";
		public const string DEFAULT_PROFILE = "Sonar way";

		public const string SONAR_SCANNER_CMD = "sonar-scanner";

		public const string DEFAULT_PROJECT_NAME = "Untitled Project";

		public const string JAVA_TEMP_FILENAME = "$tmp.txt";
	}
}
