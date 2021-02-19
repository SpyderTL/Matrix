using System;
using System.Collections.Generic;

namespace Matrix
{
	public class Network
	{
		public List<NetworkAdapter> Adapters = new List<NetworkAdapter>();

		public Network()
		{
		}

		public void Wait()
		{
			foreach (var adapter in Adapters)
			{
				while (adapter.SendPacketQueue.Count != 0)
				{
					var packet = adapter.SendPacketQueue.Dequeue();

					foreach (var adapter2 in Adapters)
						adapter2.ReceivePacketQueue.Enqueue(packet);
				}
			}
		}
	}
}