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
	public class CreateProjectTest
	{
		[TestMethod]
		public async Task CreateProjectOnceAsync()
		{
			Shared.Init();

			CreateProjectService service = new CreateProjectService();
			Dictionary<string, string> parameters = new Dictionary<string, string>()
			{
				{ "root", @"E:\Program\Projects\Analyzer\Test" }
			};

			try
			{
				await service.ExecuteAsync(parameters);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}

			Assert.AreEqual(ServiceStatus.YES, service.Status);
		}

		[TestMethod]
		public async Task CreateProjectTwiceAsync()
		{
			Shared.Init();

			CreateProjectService service = new CreateProjectService();
			Dictionary<string, string> parameters = new Dictionary<string, string>()
			{
				{ "root", @"E:\Program\Projects\Analyzer\Test" }
			};

			try
			{
				await service.ExecuteAsync(parameters);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}

			Assert.AreEqual(ServiceStatus.NO, service.Status);
		}
	}
}
