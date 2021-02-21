using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Matrix.UnitTest
{
	[TestClass]
	public class WindowUnitTest
	{
		[TestMethod]
		public void ButtonTest()
		{
			var computer = new Computer { Firmware = new TestProgram() };

			computer.Start();

			var window = computer.Processes[0].Windows[0];

			window.Events.Enqueue(new WindowEvent { Type = WindowEvent.EventType.Click, Control = window.Controls.First(x => x.Properties["Name"] == "ShowDialogButton") });

			computer.Wait();

			Assert.AreEqual(2, computer.Processes[0].Windows.Count);

			Assert.AreEqual("PopUpDialog", computer.Processes[0].Windows[1].Properties["Name"]);

			var dialog = computer.Processes[0].Windows[1];

			dialog.Events.Enqueue(new WindowEvent { Type = WindowEvent.EventType.Click, Control = dialog.Controls.First(x => x.Properties["Name"] == "CloseButton") });

			computer.Wait();

			Assert.AreEqual(1, computer.Processes[0].Windows.Count);
		}

		public class TestProgram : Program
		{
			public override void Start(Process process)
			{
				var window = new Window { Properties = { { "Name", "MainWindow" } } };

				window.Controls.Add(new WindowControl { Properties = { { "Name", "ShowDialogButton" }, { "Text", "Click Me" } } });

				process.Windows.Add(window);

				base.Start(process);
			}

			public override void Wait(Process process)
			{
				var window = process.Windows.First(x => x.Properties["Name"] == "MainWindow");
				var dialog = process.Windows.FirstOrDefault(x => x.Properties["Name"] == "PopUpDialog");

				while (window.Events.Count != 0)
				{
					var item = window.Events.Dequeue();

					if (item.Type == WindowEvent.EventType.Click &&
						item.Control.Properties["Name"] == "ShowDialogButton")
					{
						if (dialog != null)
							continue;

						dialog = new Window { Properties = { { "Name", "PopUpDialog" } } };

						dialog.Controls.Add(new WindowControl { Properties = { { "Name", "CloseButton" }, { "Text", "Close Me" } } });

						process.Windows.Add(dialog);
					}
				}

				if (dialog != null)
				{
					while (dialog.Events.Count != 0)
					{
						var item = dialog.Events.Dequeue();

						if (item.Type == WindowEvent.EventType.Click &&
							item.Control.Properties["Name"] == "CloseButton")
						{
							process.Windows.Remove(dialog);
						}
					}
				}

				base.Wait(process);
			}
		}
	}
}
