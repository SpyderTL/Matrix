using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Matrix.UnitTest
{
	[TestClass]
	public class InternetUnitTest
	{
		/// <summary>
		/// https://github.com/SpyderTL/Matrix/wiki/Internet#internet-packets
		/// </summary>
		[TestMethod]
		public void InternetPackets()
		{
			var internet = new Internet();

			var gateway = new InternetGateway();
			var gateway2 = new InternetGateway();

			internet.Gateways.Add(gateway);
			internet.Gateways.Add(gateway2);

			var packet = new InternetPacket { DestinationGateway = gateway2 };

			gateway.NetworkAdapter.ReceivedPackets.Enqueue(packet);

			gateway.Wait();
			internet.Wait();
			gateway2.Wait();

			Assert.IsTrue(gateway2.NetworkAdapter.SentPackets.Contains(packet));
		}
	}
}
