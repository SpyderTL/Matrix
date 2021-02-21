using System;
using System.Diagnostics;
using System.Linq;
using Matrix.Reference;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Matrix.UnitTest.Reference
{
	[TestClass]
	public class PingServerUnitTest
	{
		[TestMethod]
		public void PingService()
		{
			var server = new PingServer();

			var computer = new Computer
			{
				Firmware = server
			};

			computer.Start();

			var process = computer.Processes.First(x => x.Program == server);

			var service = process.Services.OfType<PingServer.PingService>().First();

			service.Requests.Enqueue(new PingServer.PingRequest { Sent = DateTime.Now });

			computer.Wait();

			var response = (PingServer.PingResponse)service.Responses.Dequeue();

			var elapsed = DateTime.Now - response.Request.Sent;

			System.Console.WriteLine("Elapsed Time: " + elapsed.ToString());

			Assert.IsTrue(elapsed >= TimeSpan.Zero);
		}
	}
}
