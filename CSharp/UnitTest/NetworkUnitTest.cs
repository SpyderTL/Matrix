using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Matrix.UnitTest
{
	[TestClass]
	public class NetworkUnitTest
	{
		/// <summary>
		/// https://github.com/SpyderTL/Matrix/wiki/Network#network-packets
		/// </summary>
		[TestMethod]
		public void NetworkPackets()
		{
			var network = new Network();

			var adapter = new NetworkAdapter();
			var adapter2 = new NetworkAdapter();

			network.Adapters.Add(adapter);
			network.Adapters.Add(adapter2);

			var packet = new NetworkPacket();

			adapter.SentPackets.Enqueue(packet);

			network.Wait();

			Assert.IsTrue(adapter2.ReceivedPackets.Any(x => x == packet));
			Assert.IsTrue(adapter.ReceivedPackets.Any(x => x == packet));
			Assert.IsFalse(adapter.SentPackets.Any(x => x == packet));
		}
	}
}
