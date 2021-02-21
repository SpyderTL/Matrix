using System;
using System.Collections.Generic;

namespace Matrix
{
	public class Internet
	{
		public List<InternetGateway> Gateways = new List<InternetGateway>();

		public Internet()
		{
		}

		public void Wait()
		{
			foreach (var gateway in Gateways)
			{
				while (gateway.SentPackets.Count != 0)
				{
					var packet = gateway.SentPackets.Dequeue();

					foreach (var gateway2 in Gateways)
						if (gateway2 == packet.DestinationGateway)
							gateway2.ReceivedPackets.Enqueue(packet);
				}
			}
		}
	}
}