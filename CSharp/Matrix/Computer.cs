using System;
using System.Collections.Generic;

namespace Matrix
{
	public class Computer
	{
		public List<Device> Devices = new List<Device>();
		public Program Firmware = new Reference.ReferenceFirmware();
		public List<Process> Processes = new List<Process>();
		public bool Running;

		public void Start()
		{
			if (Running)
				return;

			Processes.Add(new Process { Program = Firmware });
		}

		public void Wait()
		{
			Processes.ForEach(x => x.Wait());
		}
	}
}
