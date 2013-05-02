using System;

namespace huggle3
{
	public partial class About : Gtk.Window
	{
		public About () : 
				base(Gtk.WindowType.Toplevel)
		{
			this.Build ();
		}
	}
}

