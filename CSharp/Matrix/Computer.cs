using System;
using System.Collections.Generic;

namespace Matrix
{
	public class Computer
	{
		public List<Device> Devices = new List<Device>();
		public List<Process> Processes = new List<Process>();
		public Program Firmware = new Reference.Firmware();

		public Dictionary<string, string> Properties = new Dictionary<string, string>();

		public bool Running;

		public void Start()
		{
			if (Running)
				return;

			var process = new Process(Firmware, this);

			Processes.Add(process);

			process.Start();
		}

		public void Wait()
		{
			foreach (var process in Processes)
				process.Wait();

			Processes.RemoveAll(x => x.Stopped);
		}

		public void Stop()
		{
			if (!Running)
				return;
		}
	}
}
