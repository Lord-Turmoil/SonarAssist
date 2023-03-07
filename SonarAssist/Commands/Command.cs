using SonarAssist.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonarAssist.Commands
{
	/// <summary>
	/// For command execution.
	/// </summary>
	public class Command
	{
		private Command() { }

		public const int BadCommand = -255;

		public static int Execute(params string[]? args)
		{
			Process? process = Process.Start(new ProcessStartInfo()
			{
				FileName = "cmd.exe",
				UseShellExecute = false,
				RedirectStandardOutput = true,
				RedirectStandardError = true,
				RedirectStandardInput = false,
				CreateNoWindow = true,
				Arguments = _BuildArguments(args)
			});
			if (process == null)
			{
				Logger.LogError("Command not found.");
				return BadCommand;
			}

			process.WaitForExit();
			string content = process.StandardOutput.ReadToEnd();
			if (!string.IsNullOrEmpty(content))
				Logger.Log(content);
			content = process.StandardError.ReadToEnd();
			if (!string.IsNullOrEmpty(content))
				Logger.LogError(content);
			int ret = process.ExitCode;
			process.Close();

			return ret;
		}

		private static string _BuildArguments(string[]? args)
		{
			StringBuilder builder = new StringBuilder();

			builder.Append("/C");
			if (args != null)
			{
				foreach (string arg in args)
				{
					builder.Append(" ").Append(arg);
				}
			}

			return builder.ToString();
		}
	}
}
