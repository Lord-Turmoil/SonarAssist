using SonarAssist.Services.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonarAssistTest.Services.Impl
{
	[TestClass]
	public class DeleteProjectServiceTest
	{
		[TestMethod]
		public async Task DeleteProjectAsync()
		{
			Shared.Init();

			DeleteProjectService service = new DeleteProjectService();
			service.AddProjectKey("bot-669141c6-e47f-4d7b-b527-48570108f4bd");

			try
			{
				await service.ExecuteAsync();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}
	}
}
