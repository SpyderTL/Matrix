using System.Collections.Generic;

namespace Matrix
{
	public class WindowEvent
	{
		public WindowControl Control;
		public EventType Type;
		public Dictionary<string, string> Parameters = new Dictionary<string, string>();

		public enum EventType
		{
			Click,
			DoubleClick,
			RightClick,
			KeyPressed
		}
	}
}