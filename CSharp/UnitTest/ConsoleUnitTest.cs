using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Matrix.UnitTest
{
	[TestClass]
	public class ConsoleUnitTest
	{
		[TestMethod]
		public void InputOutputTest()
		{
			var computer = new Computer { Firmware = new TestProgram() };

			computer.Start();

			var console = computer.Processes[0].Consoles[0];

			Assert.AreEqual("Enter first value:", console.Output.Dequeue());
			console.Input.Enqueue("1");
			computer.Wait();
			Assert.AreEqual("Enter second value:", console.Output.Dequeue());
			console.Input.Enqueue("2");
			computer.Wait();
			Assert.AreEqual("Sum:3", console.Output.Dequeue());
		}

		public class TestProgram : Program
		{
			public override void Start(Process process)
			{
				var console = new Console();

				process.Consoles.Add(console);

				console.Output.Enqueue("Enter first value:");
				process.Properties["State"] = "Waiting";

				base.Start(process);
			}

			public override void Wait(Process process)
			{
				var console = process.Consoles[0];

				switch (process.Properties["State"])
				{
					case "Waiting":
						if (console.Input.Count != 0)
						{
							var value = console.Input.Dequeue();
							process.Properties["Value"] = value;
							console.Output.Enqueue("Enter second value:");
							process.Properties["State"] = "Waiting2";
						}
						break;

					case "Waiting2":
						if (console.Input.Count != 0)
						{
							var value2 = console.Input.Dequeue();
							var value = process.Properties["Value"];

							if (int.TryParse(value, out int value3) &&
								int.TryParse(value2, out int value4))
								console.Output.Enqueue("Sum:" + (value3 + value4));
							else
								console.Output.Enqueue("Invalid Value!");

							console.Output.Enqueue("Enter first value:");
							process.Properties["State"] = "Waiting";
						}
						break;
				}

				base.Wait(process);
			}
		}
	}
}
