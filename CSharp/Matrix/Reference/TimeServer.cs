using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Matrix.Reference
{
	public class TimeServer : Program
	{
		public override void Start(Process process)
		{
			process.Services.Add(new TimeService());

			base.Start(process);
		}

		public override void Wait(Process process)
		{
			foreach (var service in process.Services.OfType<TimeService>())
			{
				while (service.Requests.Count != 0)
				{
					var request = service.Requests.Dequeue();

					if (request is GetTimeRequest pingRequest)
					{
						service.Responses.Enqueue(new GetTimeResponse
						{
							Request = pingRequest,
							Received = DateTime.Now
						});
					}
				}
			}

			base.Wait(process);
		}

		public class TimeService : Service
		{
			
		}

		public class GetTimeRequest : ServiceRequest
		{
			public DateTime Sent;
		}

		public class GetTimeResponse : ServiceResponse
		{
			public GetTimeRequest Request;
			public DateTime Received;
		}
	}
}
