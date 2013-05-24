using System;
using System.Collections.Generic;

namespace huggle3.Controls
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class QueuePanel : Gtk.Bin
	{
		public List<EditItem> List = new List<EditItem>();
		private Queue _Queue = null;
		/// <summary>
		/// The queue which is connected to this panel
		/// </summary>
		public Queue queue
		{
			set
			{
				if (_Queue != null)
				{
					_Queue.Panel = null;
				}
				_Queue = value;
				if (value != null)
				{
					_Queue.Panel = this;
					// $fixme$
					//Rebuild();
				}
			}
			get
			{
				return _Queue;
			}
		}
		private int OffsetX = 0;
		public QueuePanel ()
		{
			this.Build ();
		}
	}
}

