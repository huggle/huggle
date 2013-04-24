using System;
using System.Collections.Generic;

namespace huggle3.Controls
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class QueuePanel : Gtk.Bin
	{
		public List<EditItem> List = new List<EditItem>();
		private Queue _Queue = null;
		public QueuePanel ()
		{
			this.Build ();
		}
	}
}

