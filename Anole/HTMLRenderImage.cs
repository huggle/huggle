using System;
using System.Drawing;
using System.Windows.Forms;

namespace Anole
{
	/// <summary>
	/// Summary description for HTMLRenderImage.
	/// </summary>
	public class HTMLRenderImage : HTMLRenderItem
	{
		public HTMLRenderImage(HTMLElement element)
		{
			this.Element=element;
			this.Width=element.Width;
			this.Height=element.Height;
		}

		public override void Paint(Graphics g)
		{
			HTMLImageElement ie=this.Element as HTMLImageElement;
			if(ie.Bitmap==null) return;
			g.DrawImage(ie.Bitmap,this.Left,this.Top);
		}
	}
}
