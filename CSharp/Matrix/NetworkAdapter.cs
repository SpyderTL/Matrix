using System.Collections.Generic;

namespace Matrix
{
	public class NetworkAdapter : Device
	{
		public Queue<NetworkPacket> SendPacketQueue = new Queue<NetworkPacket>();
		public Queue<NetworkPacket> ReceivePacketQueue = new Queue<NetworkPacket>();

		public NetworkAdapter()
		{
		}
	}
}