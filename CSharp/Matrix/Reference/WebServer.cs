using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Matrix.Reference
{
	public class WebServer : Program
	{
		public override void Start(Process process)
		{
			process.Services.Add(new WebService());

			base.Start(process);
		}

		public override void Wait(Process process)
		{
			foreach (var service in process.Services.OfType<WebService>())
			{
				while (service.Requests.Count != 0)
				{
					var request = service.Requests.Dequeue();

					if (request is GetRequest getRequest)
					{
						service.Responses.Enqueue(new GetResponse
						{
							Request = getRequest
						});
					}
				}
			}

			base.Wait(process);
		}

		public class WebService : Service
		{
			
		}

		public class GetRequest : ServiceRequest
		{
		}

		public class GetResponse : ServiceResponse
		{
			public GetRequest Request;
		}
	}
}
