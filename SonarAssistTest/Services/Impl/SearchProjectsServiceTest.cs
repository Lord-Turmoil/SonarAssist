using DryIoc;
using SonarAssist.Common;
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
    public class SearchProjectsServiceTest
    {
        [TestMethod]
        public async Task SearchAsync()
        {
            Shared.Init();

            SearchProjectsService service = new SearchProjectsService();

            await service.ExecuteAsync();

            Assert.AreEqual(ServiceStatus.OK, service.Status);
        }

        [TestMethod]
        public async Task PagedSearchAsync()
        {
            Shared.Init();

            SearchProjectsService service = new SearchProjectsService();

            Dictionary<string, string> parameters = new Dictionary<string, string>
            {
                { "ps", "5" }
            };
            await service.ExecuteAsync(parameters);

            Assert.AreEqual(ServiceStatus.OK, service.Status);
        }
    }
}
