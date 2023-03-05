using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonarAssist.Common.Exceptions
{
	public class ServiceErrorException : Exception
	{
		public ServiceErrorException(string message) : base(message) { }
	}
}
