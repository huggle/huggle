using System;
using System.Drawing;

namespace Anole
{
	/// <summary>
	/// Summary description for HTMLRenderItem.
	/// </summary>
	public class HTMLRenderItem
	{
	
		protected int myTop;
		public int Top
		{
			get{return myTop;}
			set{myTop=value;}
		}

		protected int myLeft;
		public int Left
		{
			get{return myLeft;}
			set{myLeft=value;}
		}

		protected int myWidth;
		public int Width
		{
			get{return myWidth;}
			set{myWidth=value;}
		}

		protected int myHeight;
		public int Height
		{
			get{return myHeight;}
			set{myHeight=value;}
		}

		protected HTMLRenderBand myBand;
		public HTMLRenderBand RenderBand
		{
			get{return myBand;}
			set{myBand=value;}
		}

		protected HTMLElement myHTMLElement;
		public HTMLElement Element
		{
			get{return myHTMLElement;}
			set{myHTMLElement=value;}
		}

		protected string myText;
		public string Text
		{
			get{return myText;}
			set{myText=value;}
		}

		public HTMLRenderItem()
		{
		}

		public HTMLRenderItem(string text, int width, int height, HTMLElement element)
		{
			myText=text;
			myWidth=width;
			myHeight=height;
			myHTMLElement=element;
		}

		public virtual void Paint(Graphics g)
		{
			//Pen p=new Pen(Color.Blue);
			//g.DrawRectangle(p,this.Left,myBand.Top+myBand.Height-this.Height,this.Width,this.Height);
			g.DrawString(this.Text,this.myHTMLElement.Font,
				new SolidBrush(this.myHTMLElement.ForeColor),
				this.Left,myBand.Top+myBand.Height-this.Height);
		}

	}
}
