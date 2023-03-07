using SonarAssist.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonarAssistTest.Commands
{
	[TestClass]
	public class CommandTest
	{
		[TestMethod]
		public void ExecuteCommand()
		{
			Command.Execute(new CommandOptions(), "dir");
		}
	}
}
