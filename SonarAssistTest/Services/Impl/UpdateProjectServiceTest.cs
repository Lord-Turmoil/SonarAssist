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
	public class UpdateProjectServiceTest
	{
		[TestMethod]
		public async Task UpdateProjectAsync()
		{
			Shared.Init();

			UpdateProjectService service = new UpdateProjectService();
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
				Console.WriteLine(ex.Message);
			}

			Assert.AreEqual(ServiceStatus.OK, service.Status);
		}
	}
}
