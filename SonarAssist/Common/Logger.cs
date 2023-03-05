using Newtonsoft.Json;
using SonarAssist.Common.Config;
using SonarAssist.Services.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonarAssist.Common
{
	public class Logger
	{
		private Logger() { }

		public static void Log(string message, ConsoleColor color = ConsoleColor.White)
		{
			Console.WriteLine(message, color);
		}

		public static void LogError(string message)
		{
			Log(message, ConsoleColor.Red);
		}

		public static void LogMessage(string message)
		{
			Log(message, ConsoleColor.DarkYellow);
		}

		public static void Dump<TDto>(TDto? dto, string filename) where TDto : ISonarResponseDto
		{
			if (string.IsNullOrEmpty(filename))
			{
				throw new ArgumentNullException("filename");
			}

			string fullPath = Path.GetFullPath(filename);
			string? dir = Path.GetDirectoryName(fullPath);
			if (dir == null)
			{
				throw new ArgumentException("filename");
			}

			Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
			if (dto == null)
				File.WriteAllText(fullPath, "// No Content");
			else
				File.WriteAllText(fullPath, JsonConvert.SerializeObject(dto, Formatting.Indented));
		}
	}
}
