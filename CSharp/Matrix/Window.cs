using System.Collections.Generic;

namespace Matrix
{
	public class Window
	{
		public List<WindowControl> Controls = new List<WindowControl>();
		public Queue<WindowEvent> Events = new Queue<WindowEvent>();

		public Dictionary<string, string> Properties = new Dictionary<string, string>();
	}
}