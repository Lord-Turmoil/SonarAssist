using DryIoc;
using Newtonsoft.Json.Linq;
using SonarAssist.Common;
using SonarAssist.Common.Config;
using SonarAssist.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace SonarAssist.Services.Impl
{
	/// <summary>
	/// Initialize a single project.
	/// </summary>
	public class InitProjectService : SonarService
	{
		private bool _force = false;
		private string _root = "";

		private LocalConfiguration _config;

		/// <summary>
		/// Initialize a single project.
		/// </summary>
		/// <param name="parameters">
		///		 *root: root folder
		///		 force: force initialize
		///		  name: custom name
		///		   src: java source folder
		///		output: java output folder
		/// </param>
		/// <returns></returns>
		public override async Task ExecuteAsync(Dictionary<string, string>? parameters = null)
		{
			if (parameters == null || !_ParseParameters(parameters))
			{
				Status = ServiceStatus.Error;
				throw new ArgumentException(ExceptionMessage.BadArguments);
			}

			Directory.SetCurrentDirectory(Path.GetFullPath(_root));

			if (!_CheckPrerequisites())
			{
				Status = ServiceStatus.Error;
				throw new ServiceErrorException($"\"{_root}\" is not a Java project");
			}

			_InitConfig();
			_UpdateConfig(parameters);

			_GenerateSonarProperties();

			Status = ServiceStatus.OK;

			await Task.Run(() => { });
		}

		private bool _CheckPrerequisites()
		{
			return Directory.Exists(Path.GetFullPath(Constants.LOCAL_SRC_PATH));
		}

		private bool _ParseParameters(Dictionary<string, string> parameters)
		{
			if (!parameters.ContainsKey("root"))
				return false;
			_root = parameters["root"];

			if (parameters.TryGetValue("force", out _))
				_force = true;

			return true;
		}

		private void _InitConfig()
		{
			Directory.CreateDirectory(Constants.LOCAL_PATH);
			if (!File.Exists(Constants.LOCAL_CONFIG_FILENAME))
			{
				// Err... It not only create, but also opens...
				File.Create(Constants.LOCAL_CONFIG_FILENAME).Close();
			}

			_config = ConfigManager.Load<LocalConfiguration>(Constants.LOCAL_CONFIG_FILENAME) ?? new LocalConfiguration();
		}

		private void _UpdateConfig(Dictionary<string, string> parameters)
		{
			string? value;
			if (parameters.TryGetValue("name", out value))
				_config.name = value;
			if (parameters.TryGetValue("src", out value))
				_config.src = value;
			if (parameters.TryGetValue("output", out value))
				_config.output = value;

			// This is the unique project key.
			if (_force || string.IsNullOrEmpty(_config.key))
				_config.key = "bot-" + Guid.NewGuid().ToString();
			if (string.IsNullOrEmpty(_config.name))
				_config.name = _config.key;

			ConfigManager.Save(_config, Constants.LOCAL_CONFIG_FILENAME);
		}

		private async void _GenerateSonarProperties()
		{
			string[] lines = {
				"# Must be unique in a given SonarQube instance",
				$"sonar.projectKey={_config.key}",
				"",
				"# this is the name displayed in the SonarQube UI",
				$"sonar.projectName={_config.name}",
				"",
				@"# Path is relative to the sonar-project.properties file",
				@"# Replace ""\"" by ""/"" on Windows",
				"# If not set, SonarQube starts looking for source code from the",
				"#   directory containing sonar-project.properties file",
				$"sonar.sources={_config.src.Replace('\\', '/')}",
				"",
				"# Path for classes",
				$"sonar.java.binaries={_config.output.Replace('\\', '/')}",
				"",
				"sonar.language=java",
				"sonar.sourceEncoding=UTF-8",
				"",
				$"sonar.login={Global.Container.Resolve<GlobalConfiguration>().Token}"
			};

			await File.WriteAllLinesAsync(Constants.SONAR_FILENAME, lines);
		}

		public override void Execute(Dictionary<string, string>? parameters = null)
		{
			throw new NotImplementedException();
		}
	}
}
