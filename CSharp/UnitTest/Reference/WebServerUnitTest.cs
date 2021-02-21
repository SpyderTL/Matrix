using System;
using System.Diagnostics;
using System.Linq;
using Matrix.Reference;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Matrix.UnitTest.Reference
{
	[TestClass]
	public class WebServerUnitTest
	{
		[TestMethod]
		public void WebService()
		{
			var server = new WebServer();

			var computer = new Computer
			{
				Firmware = server
			};

			computer.Start();

			var process = computer.Processes.First(x => x.Program == server);

			var service = process.Services.OfType<WebServer.WebService>().First();

			service.Requests.Enqueue(new WebServer.GetRequest { });

			computer.Wait();

			var response = (WebServer.GetResponse)service.Responses.Dequeue();
		}
	}
}
