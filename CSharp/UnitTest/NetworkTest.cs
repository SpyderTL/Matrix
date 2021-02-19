using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Matrix.UnitTest
{
	[TestClass]
	public class NetworkTest
	{
		[TestMethod]
		public void NetworkPackets()
		{
			var network = new Network();

			var adapter = new NetworkAdapter();
			var adapter2 = new NetworkAdapter();

			network.Adapters.Add(adapter);
			network.Adapters.Add(adapter2);

			var packet = new NetworkPacket();

			adapter.SendPacketQueue.Enqueue(packet);

			network.Wait();

			Assert.IsTrue(adapter2.ReceivePacketQueue.Any(x => x == packet));
			Assert.IsTrue(adapter.ReceivePacketQueue.Any(x => x == packet));
			Assert.IsFalse(adapter.SendPacketQueue.Any(x => x == packet));
		}

		private class TestFirmware : Program
		{
		}
	}
}
