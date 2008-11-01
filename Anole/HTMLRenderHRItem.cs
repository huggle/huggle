using System;
using System.Drawing;

namespace Anole
{
	/// <summary>
	/// Summary description for HTMLRenderHRItem.
	/// </summary>
	public class HTMLRenderHRItem : HTMLRenderItem
	{
		public HTMLRenderHRItem(int width, HTMLElement element)
		{
			this.myHTMLElement=element;
			this.Width=width;
		}

		public override void Paint(Graphics g)
		{
			Pen p=new Pen(Color.Black);
			g.DrawLine(p,this.Left,this.Top,this.Width,this.Top);

			//TBD: take into account hr attributes when drawing
		}
	}
}
