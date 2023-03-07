using SonarAssist.Common;
using SonarAssist.Common.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonarAssist.Extensions
{
	/// <summary>
	/// This contains some common functions for SonarQube operations.
	/// </summary>
	public class SonarUtils
	{
		/// <summary>
		/// Check if specified path is a Java project or not.
		/// </summary>
		/// <param name="projectPath"></param>
		/// <returns></returns>
		public static bool IsProject(string projectPath = Constants.CURRENT_PATH)
		{
			string fullPath = Path.GetFullPath(projectPath);
			return Directory.Exists(Path.Combine(fullPath, Constants.LOCAL_SRC_PATH));
		}

		/// <summary>
		/// Check if specified path is initialized or not.
		/// </summary>
		/// <param name="projectPath"></param>
		/// <returns></returns>
		public static bool IsInitialized(string projectPath = Constants.CURRENT_PATH)
		{
			var config = GetLocalConfiguration(projectPath);
			if (config == null)
				return false;
			if (string.IsNullOrEmpty(config.key))
				return false;

			return true;
		}

		/// <summary>
		/// Get local Java project configuration.
		/// </summary>
		/// <param name="path">Java project path</param>
		/// <returns></returns>
		public static LocalConfiguration? GetLocalConfiguration(string projectPath = Constants.CURRENT_PATH)
		{
			string fullPath = Path.GetFullPath(projectPath);
			string configPath = Path.Combine(fullPath, Constants.LOCAL_CONFIG_FILENAME);

			return ConfigManager.Load<LocalConfiguration>(configPath);
		}
	}
}
