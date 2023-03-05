using DryIoc;
using SonarAssist.Common;
using SonarAssist.Common.Config;
using SonarAssist.Services;
using SonarAssist.Services.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonarAssistTest.Services.Impl
{
	[TestClass]
	public class UpdateConfigServiceTest
	{
		public static readonly string BAD_SERVER = "bad server";
		public static readonly string BAD_TOKEN = "bad token";
		public static readonly string BAD_PROFILE = "bad profile";

		[TestMethod]
		public void UpdateConfig()
		{
			Shared.Init();

			UpdateConfigService service = new UpdateConfigService();

			Dictionary<string, string> parameters = new Dictionary<string, string>()
			{
				{ "s", BAD_SERVER },
				{ "t", BAD_TOKEN },
				{ "p", BAD_PROFILE }
			};

			service.Execute(parameters);

			Assert.AreEqual(ServiceStatus.OK, service.Status);
		}

		[TestMethod]
		public void CheckConfig()
		{
			Shared.Init();

			var config = Global.Container.Resolve<GlobalConfiguration>();

			Assert.AreEqual(BAD_SERVER, config.Server);
			Assert.AreEqual(BAD_TOKEN, config.Token);
			Assert.AreEqual(BAD_PROFILE, config.Profile);
		}
	}
}
