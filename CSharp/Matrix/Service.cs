using System.Collections.Generic;

namespace Matrix
{
	public class Service
	{
		public Queue<ServiceRequest> Requests = new Queue<ServiceRequest>();
		public Queue<ServiceResponse> Responses = new Queue<ServiceResponse>();

		public Dictionary<string, string> Properties = new Dictionary<string, string>();
	}
}