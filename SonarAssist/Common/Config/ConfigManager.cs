using DryIoc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonarAssist.Common.Config
{
	internal class ConfigManager
	{
		public static TConfig? Load<TConfig>(string filename) where TConfig : IConfiguration
		{
			string content = "";

			if (!string.IsNullOrWhiteSpace(filename))
			{
				string fullPath = Path.GetFullPath(filename);
				if (File.Exists(fullPath))
					content = File.ReadAllText(fullPath);
			}

			return JsonConvert.DeserializeObject<TConfig>(content);
		}

		public static void Save<TConfig>(TConfig config, string filename) where TConfig : IConfiguration
		{
			if (string.IsNullOrEmpty(filename))
			{
				throw new ArgumentNullException("filename");
			}

			string fullPath = Path.GetFullPath(filename);

			File.WriteAllText(fullPath, JsonConvert.SerializeObject(config, Formatting.Indented));
		}
	}
}
