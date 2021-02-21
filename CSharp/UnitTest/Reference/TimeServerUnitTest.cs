using System;
using System.Diagnostics;
using System.Linq;
using Matrix.Reference;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Matrix.UnitTest.Reference
{
	[TestClass]
	public class TimeServerUnitTest
	{
		[TestMethod]
		public void TimeService()
		{
			var server = new TimeServer();

			var computer = new Computer
			{
				Firmware = server
			};

			computer.Start();

			var process = computer.Processes.First(x => x.Program == server);

			var service = process.Services.OfType<TimeServer.TimeService>().First();

			service.Requests.Enqueue(new TimeServer.GetTimeRequest { Sent = DateTime.Now });

			computer.Wait();

			var response = (TimeServer.GetTimeResponse)service.Responses.Dequeue();

			Assert.AreNotEqual(response.Received, DateTime.MinValue);

			var elapsed = DateTime.Now - response.Request.Sent;

			Assert.IsTrue(elapsed >= TimeSpan.Zero);

			System.Console.WriteLine("Time: " + response.Received.ToString());
			System.Console.WriteLine("Elapsed Time: " + elapsed.ToString());
		}
	}
}
