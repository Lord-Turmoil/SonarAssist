using DryIoc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonarAssist.Common
{
	/// <summary>
	/// Should be called to initialize startup path.
	/// </summary>
	public class Global
	{
		private Global() { }

		public static Container Container { get; private set; } = new Container();

		public static readonly string StartupPath = Environment.CurrentDirectory;
	}
}
