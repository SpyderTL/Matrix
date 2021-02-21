using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Matrix.UnitTest
{
	[TestClass]
	public class ComputerUnitTest
	{
		/// <summary>
		/// https://github.com/SpyderTL/Matrix/wiki/Computer#startup
		/// </summary>
		[TestMethod]
		public void Startup()
		{
			var computer = new Computer
			{
				Firmware = new TestFirmware()
			};

			computer.Start();

			Assert.IsTrue(computer.Processes.Any(x => x.Program == computer.Firmware));
			Assert.IsTrue(TestFirmware.Started);
		}

		private class TestFirmware : Program
		{
			public static bool Started;

			public override void Start(Process process)
			{
				Started = true;

				base.Start(process);
			}
		}
	}
}
