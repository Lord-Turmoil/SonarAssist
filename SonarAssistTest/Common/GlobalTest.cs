using DryIoc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonarAssist.Common
{
	internal class Counter
	{
		public static int Count { get; set; } = 0;
		public Counter()
		{
			Count++;
		}
	}

	[TestClass]
	public class GlobalTest
	{
		[TestMethod]
		public void RegisterAndResolve()
		{
			Global.Container.RegisterInstance<Counter>(new Counter());

			var a = Global.Container.Resolve<Counter>();
			Assert.AreEqual(1, Counter.Count, "Count error");
			var b = Global.Container.Resolve<Counter>();
			Assert.AreEqual(1, Counter.Count, "Count error");
		}
	}
}
