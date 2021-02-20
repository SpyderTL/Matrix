using System.Collections.Generic;

namespace Matrix
{
	public class NetworkAdapter : Device
	{
		public Queue<NetworkPacket> SentPackets = new Queue<NetworkPacket>();
		public Queue<NetworkPacket> ReceivedPackets = new Queue<NetworkPacket>();

		public NetworkAdapter()
		{
		}
	}
}