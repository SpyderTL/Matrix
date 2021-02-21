using System;
using System.Diagnostics;
using System.Linq;
using Matrix.Reference;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Matrix.UnitTest.Reference
{
	[TestClass]
	public class FileServerUnitTest
	{
		[TestMethod]
		public void FileService()
		{
			var server = new FileServer();

			var computer = new Computer
			{
				Firmware = server
			};

			computer.Start();

			var process = computer.Processes.First(x => x.Program == server);

			var service = process.Services.OfType<FileServer.FileService>().First();

			service.Requests.Enqueue(new FileServer.CreateFileRequest { });

			computer.Wait();

			var response = (FileServer.CreateFileResponse)service.Responses.Dequeue();
		}
	}
}
