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
	public class InitProjectServiceTest
	{
		[TestMethod]
		public async Task InitProjectAsync()
		{
			Shared.Init();

			InitProjectService service = new InitProjectService();
			Dictionary<string, string> parameters = new Dictionary<string, string>
			{
				{ "root", @"E:\Program\Projects\Analyzer\Test" },
				{ "name", "Bot-Test" }
			};

			await service.ExecuteAsync(parameters);

			Assert.AreEqual(ServiceStatus.OK, service.Status);
		}
	}
}
