using SonarAssist.Common.Config;
using SonarAssist.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DryIoc;
using SonarAssist.Services;

namespace SonarAssistTest
{
	public class Shared
	{
		public static void InitConfig()
		{
			Directory.SetCurrentDirectory(@"E:\Program\Projects\Analyzer\SonarAssist\SonarAssist\Work");
			Console.WriteLine(Directory.GetCurrentDirectory());

			var config = ConfigManager.Load<GlobalConfiguration>(@"config\config.json");
			Global.Container.RegisterInstance(config);
		}

		public static void InitClient()
		{
			Global.Container.RegisterInstance(new SonarClient());
		}

		public static void Init()
		{
			InitConfig();

			try
			{
				InitClient();
			}
			catch (Exception ex)
			{
				Console.WriteLine("Failed to initialize client.");
				Console.WriteLine(ex.ToString());
			}
		}
	}
}
