using DryIoc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonarAssist.Common
{
	public class Global
	{
		private Global() { }

		public static Container Container { get; private set; } = new Container();
	}
}
