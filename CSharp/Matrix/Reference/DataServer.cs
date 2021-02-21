using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Matrix.Reference
{
	public class DataServer : Program
	{
		public override void Start(Process process)
		{
			process.Services.Add(new DataService());

			base.Start(process);
		}

		public override void Wait(Process process)
		{
			foreach (var service in process.Services.OfType<DataService>())
			{
				while (service.Requests.Count != 0)
				{
					var request = service.Requests.Dequeue();

					if (request is DataRequest dataRequest)
					{
						service.Responses.Enqueue(new DataResponse
						{
							Request = dataRequest
						});
					}
				}
			}

			base.Wait(process);
		}

		public class DataService : Service
		{
			
		}

		public class DataRequest : ServiceRequest
		{

		}

		public class DataResponse : ServiceResponse
		{
			public DataRequest Request;
		}
	}
}
