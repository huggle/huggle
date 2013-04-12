using System;

namespace huggle3
{
	public partial class Queue : Gtk.ActionGroup
	{
		public Queue () : 
				base("huggle3.Queue")
		{
			this.Build ();
		}
	}
}

