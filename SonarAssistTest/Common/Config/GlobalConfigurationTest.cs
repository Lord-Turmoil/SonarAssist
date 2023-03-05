using DryIoc;
using Newtonsoft.Json;
using SonarAssist.Common;
using SonarAssist.Common.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonarAssistTest.Common.Config
{
	[TestClass]
	public class GlobalConfigurationTest
	{
		[TestMethod]
		public void Load()
		{
			Shared.InitConfig();

			var config = Global.Container.Resolve<GlobalConfiguration>();

			// This is set in property file.
			// Assert.AreEqual(Constants.DEFAULT_SERVER, config.Server, "Wrong default value");
			
			// Properties that doesn't have default value is empty string
			Assert.AreEqual("", config.Token, "Wrong token");

			// Profile has default value.
			Assert.AreEqual(Constants.DEFAULT_PROFILE, config.Profile, "Wrong profile");
		}

		[TestMethod]
		public void Save()
		{
			Shared.InitConfig();

			var config = Global.Container.Resolve<GlobalConfiguration>();

			config.Token = "token";
			ConfigManager.Save(config, @"config\config.json");
		}

		[TestMethod]
		public void Reload()
		{
			Shared.InitConfig();

			var config = Global.Container.Resolve<GlobalConfiguration>();

			Assert.AreEqual("token", config.Token, "Wrong token");
		}
	}
}
