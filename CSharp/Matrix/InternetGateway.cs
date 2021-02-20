using System;
using System.Collections.Generic;

namespace Matrix
{
	public class InternetGateway
	{
		public Queue<InternetPacket> SentPackets = new Queue<InternetPacket>();
		public Queue<InternetPacket> ReceivedPackets = new Queue<InternetPacket>();

		public NetworkAdapter NetworkAdapter = new NetworkAdapter();

		public string Domain;

		public InternetGateway(string domain)
		{
			Domain = domain;
		}

		public void Wait()
		{
			while (NetworkAdapter.ReceivedPackets.Count != 0)
			{
				var packet = NetworkAdapter.ReceivedPackets.Dequeue();

				if (packet is InternetPacket internetPacket &&
					internetPacket.DestinationDomain != Domain)
						SentPackets.Enqueue(internetPacket);
			}

			while (ReceivedPackets.Count != 0)
			{
				var packet = ReceivedPackets.Dequeue();

				NetworkAdapter.SentPackets.Enqueue(packet);
			}
		}
	}
}