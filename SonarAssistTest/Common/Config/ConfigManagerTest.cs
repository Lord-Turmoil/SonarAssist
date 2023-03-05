using SonarAssist.Common.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonarAssistTest.Common.Config
{
	[TestClass]
	public class ConfigManagerTest
	{
		[TestMethod]
		public void ParseEmptyString()
		{
			Shared.Init();

			var config = ConfigManager.Load<LocalConfiguration>(@"empty.json");
			Assert.IsNull(config);
		}
	}
}
