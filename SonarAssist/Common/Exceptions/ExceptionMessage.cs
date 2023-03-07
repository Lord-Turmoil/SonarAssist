using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonarAssist.Common.Exceptions
{
	public class ExceptionMessage
	{
		private ExceptionMessage() { }

		public const string BadArguments = "Arguments illegal";
		public const string NotInitialized = "Project not initialized";
	}
}
