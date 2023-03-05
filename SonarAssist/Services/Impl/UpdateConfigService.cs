using DryIoc;
using SonarAssist.Common;
using SonarAssist.Common.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonarAssist.Services.Impl
{
	/// <summary>
	/// Update global configuration.
	/// </summary>
	public class UpdateConfigService : SonarService
	{
		public override void Execute(Dictionary<string, string>? parameters = null)
		{
			if (parameters == null)
			{
				Status = ServiceStatus.OK;
				return;
			}

				var config = Global.Container.Resolve<GlobalConfiguration>();
			if (parameters.TryGetValue("s", out string? value))
				config.Server = value;
			if (parameters.TryGetValue("t", out value))
				config.Token = value;
			if (parameters.TryGetValue("p", out value))
				config.Profile = value;

			ConfigManager.Save(config, Constants.GLOBAL_CONFIG_FILENAME);

			Status = ServiceStatus.OK;
		}

		public override Task ExecuteAsync(Dictionary<string, string>? parameters = null)
		{
			throw new NotImplementedException();
		}
	}
}
