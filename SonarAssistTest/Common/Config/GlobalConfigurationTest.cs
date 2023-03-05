using dotnetCampus.Configurations;
using dotnetCampus.Configurations.Core;
using DryIoc;
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
		public void LoadAndSave()
		{
			_InitConfig();

			var config = Global.Container.Resolve<IAppConfigurator>()
				.Of<GlobalConfiguration>();

			// Profile has default value.
			Assert.AreEqual(Constants.DEFAULT_PROFILE, config.Profile, "Wrong profile");

			// Properties that doesn't have default value is null.
			Assert.IsNull(config.Server, "Wrong default value");

			config.Server = Constants.DEFAULT_SERVER;
			Assert.AreEqual(Constants.DEFAULT_SERVER, config.Server, "Wrong server");

			config.Profile = "Profile";
			Assert.AreEqual("Profile", config.Profile, "Wrong profile");

			Global.Container.Resolve<IAppConfigurator>()
		}

		[TestMethod]
		public void Reload()
		{
			_InitConfig();

			var config = Global.Container.Resolve<IAppConfigurator>()
				.Of<GlobalConfiguration>();

			Assert.AreEqual(Constants.DEFAULT_SERVER, config.Server, "Wrong server");
			Assert.AreEqual("Profile", config.Profile, "Wrong profile");
		}

		private void _InitConfig()
		{
			var configFilename = @"config\config.coin";
			var config = ConfigurationFactory.FromFile(configFilename);
			Global.Container.RegisterInstance(config.CreateAppConfigurator());
		}
	}
}
