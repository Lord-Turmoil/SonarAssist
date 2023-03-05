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
		public static readonly string GLOBAL_CONFIG_FILENAME = @"config\config.json";
		
		public static readonly string GLOBAL_LOG_PATH = @"logs\";


		public static readonly string LOCAL_PATH = @"sonar-assist\";

		public static readonly string LOCAL_SRC_PATH = @"src\";

		public static readonly string LOCAL_OUT_PATH = LOCAL_PATH + @"output\";

		public static readonly string LOCAL_CONFIG_FILENAME = LOCAL_PATH + "sonar-assist.json";

		public static readonly string SONAR_FILENAME = "sonar-project.properties";

		
		public static readonly string DEFAULT_PROFILE = "Sonar way";
	}
}
