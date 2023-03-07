using DryIoc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonarAssist.Services.Impl
{
	public class ScanProjectService : SonarService
	{
		private string _root = "";

		public override void Execute(Dictionary<string, string>? parameters = null)
		{
			if (parameters == null || !_ParseParameters(parameters))
			{
				Status = ServiceStatus.Error;
				throw new ArgumentException("Arguments illegal!");
			}

			Directory.SetCurrentDirectory(Path.GetFullPath(_root));


		}

		private bool _ParseParameters(Dictionary<string, string> parameters)
		{
			return parameters.TryGetValue("root", out _root);
		}

		private bool _CheckPrerequisites()
		{

			return true;
		}

		public override Task ExecuteAsync(Dictionary<string, string>? parameters = null)
		{
			throw new NotImplementedException();
		}
	}
}
