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
	public class ScanProjectServiceTest
	{
		[TestMethod]
		public void ScanProjectAsync()
		{
			Shared.Init();

			ScanProjectService service = new ScanProjectService();
			Dictionary<string, string> parameters = new Dictionary<string, string>() {
				{ "root", @"E:\Program\Projects\Analyzer\Test" },
				{ "debug", "" }
			};

			try
			{
				service.Execute(parameters);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}

			Assert.AreEqual(ServiceStatus.OK, service.Status);
		}
	}
}
