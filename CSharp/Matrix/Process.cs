using System;
using System.Collections.Generic;

namespace Matrix
{
	public class Process
	{
		public Computer Computer;
		public Program Program;

		public List<Console> Consoles = new List<Console>();
		public List<Window> Windows = new List<Window>();
		public List<Service> Services = new List<Service>();

		public Dictionary<string, string> Properties = new Dictionary<string, string>();

		public bool Started;
		public bool Running;
		public bool Stopped;

		public Process(Program program, Computer computer)
		{
			Program = program;
			Computer = computer;
		}

		public void Wait()
		{
			if (!Running)
				return;

			Program.Wait(this);
		}

		public void Start()
		{
			Started = true;

			Program.Start(this);

			Running = true;
		}
	}
}