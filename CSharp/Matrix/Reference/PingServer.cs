using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Matrix.Reference
{
	public class PingServer : Program
	{
		public override void Start(Process process)
		{
			process.Services.Add(new PingService());

			base.Start(process);
		}

		public override void Wait(Process process)
		{
			foreach (var service in process.Services.OfType<PingService>())
			{
				while (service.Requests.Count != 0)
				{
					var request = service.Requests.Dequeue();

					if (request is PingRequest pingRequest)
					{
						service.Responses.Enqueue(new PingResponse
						{
							Request = pingRequest
						});
					}
				}
			}

			base.Wait(process);
		}

		public class PingService : Service
		{
			
		}

		public class PingRequest : ServiceRequest
		{
			public DateTime Sent;
		}

		public class PingResponse : ServiceResponse
		{
			public PingRequest Request;
		}
	}
}
