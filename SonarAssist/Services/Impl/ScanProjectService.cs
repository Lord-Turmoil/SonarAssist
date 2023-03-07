using DryIoc;
using SonarAssist.Commands;
using SonarAssist.Common;
using SonarAssist.Common.Config;
using SonarAssist.Common.Exceptions;
using SonarAssist.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonarAssist.Services.Impl
{
	public class ScanProjectService : SonarService
	{
		private string _root = "";
		private bool _debug = false;	// whether show sonar-scanner stdout
		private LocalConfiguration _config;

		public override void Execute(Dictionary<string, string>? parameters = null)
		{
			if (parameters == null || !_ParseParameters(parameters))
			{
				Status = ServiceStatus.Error;
				throw new ArgumentException(Messages.BadArguments);
			}

			Directory.SetCurrentDirectory(Path.GetFullPath(_root));

			if (!SonarUtils.IsInitialized())
			{
				Status = ServiceStatus.Error;
				throw new ServiceErrorException(Messages.NotInitialized);
			}
			_config = SonarUtils.GetLocalConfiguration();

			try
			{
				_CompileProject();
				_ScanProject();
			}
			catch (Exception)
			{
				Status = ServiceStatus.Error;
				throw;
			}

			Logger.LogMessage($"Project \"{_config.name}\" scan complete.");

			Status = ServiceStatus.OK;
		}

		private bool _ParseParameters(Dictionary<string, string> parameters)
		{
			if (parameters.TryGetValue("debug", out _))
				_debug = true;

			return parameters.TryGetValue("root", out _root);
		}

		/// <summary>
		///		dir /s /b *.java > tmp.txt 
		///		javac @tmp.txt
		///		del tmp.txt
		/// </summary>
		/// <returns></returns>
		/// <exception cref="ServiceErrorException"></exception>
		private void _CompileProject()
		{
			int ret;

			// Find all source files.
			ret = Command.Execute(new CommandOptions(), $"dir /s /b *.java > {Constants.JAVA_TEMP_FILENAME}");
			if (ret != 0)
			{
				throw new ServiceErrorException(Messages.UnexpectedError);
			}

			// Complie java files.
			ret = Command.Execute(new CommandOptions() { ShowStandardOutput = false },
				"javac -encoding utf-8", $" -d {_config.output}",
				$" @{Constants.JAVA_TEMP_FILENAME}");

			if (ret == Command.BadCommand)
			{
				throw new ServiceErrorException(Messages.BadEnvironment);
			}
			else if (ret != 0)
			{
				throw new ServiceErrorException(Messages.CompilationFailed);
			}

			// Delete temporary file
			ret = Command.Execute(new CommandOptions(), $"del {Constants.JAVA_TEMP_FILENAME}");
			if (ret != 0)
			{
				throw new ServiceErrorException(Messages.UnexpectedError);
			}
		}

		private void _ScanProject()
		{
			int ret = Command.Execute(new CommandOptions() { ShowStandardOutput = _debug },
				Constants.SONAR_SCANNER_CMD);
			if (ret == Command.BadCommand)
			{
				throw new ServiceErrorException(Messages.BadEnvironment);
			}
			else if (ret != 0)
			{
				throw new ServiceErrorException(Messages.ScanFailed);
			}
		}

		public override Task ExecuteAsync(Dictionary<string, string>? parameters = null)
		{
			throw new NotImplementedException();
		}
	}
}
