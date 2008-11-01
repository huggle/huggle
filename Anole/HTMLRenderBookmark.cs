using System;

namespace Anole
{
	/// <summary>
	/// Summary description for HTMLRenderBookmark.
	/// </summary>
	public class HTMLRenderBookmark : HTMLRenderItem
	{
		public HTMLRenderBookmark(HTMLElement element)
		{
			this.Element=element;
		}
		public override void Paint(System.Drawing.Graphics g)
		{
			//do nothing, I'm a bookmark
		}

	}
}
