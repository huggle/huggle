using System;

namespace huggle3
{
	public partial class Space : Gtk.ActionGroup
	{
		public Space () : 
				base("huggle3.Space")
		{
			this.Build ();
		}
	}
}

