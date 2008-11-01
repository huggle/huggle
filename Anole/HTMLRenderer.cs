using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace Anole
{
	/// <summary>
	/// Summary description for HTMLRenderer.
	/// </summary>
	public class HTMLRenderer
	{
		private ArrayList myRenderBands;
		private int myTargetWidth;

		private int myWidth;
		public int Width
		{
			get{return myWidth;}
			set{myWidth=value;}
		}

		private int myHeight;
		public int Height
		{
			get{return myHeight;}
			set{myHeight=value;}
		}

		public int TargetWidth
		{
			get{return myTargetWidth;}
			set{myTargetWidth=value;}
		}

		public HTMLRenderer(HTMLElement element, int targetwidth)
		{
            if (element == null) return;

			myWidth=0;
			myHeight=0;

			myTargetWidth=targetwidth;
			myRenderBands=new ArrayList();
			myRenderBands.Add(new HTMLRenderBand(targetwidth));
			Bitmap b=new Bitmap(1,1);
			Graphics g=Graphics.FromImage(b);
			element.Layout(this,g);

			//Calculate band tops
			int top=0;
			int width=0;
			foreach(HTMLRenderBand band in myRenderBands)
			{
				band.Top=top;
				top+=band.Height+1;
				band.ComputeItemCoords();
				width=Math.Max(width,band.Width);
			}

			this.Height=top;
			this.Width=width;

		}

		public HTMLRenderBand CurrentBand()
		{
			return myRenderBands[myRenderBands.Count-1] as HTMLRenderBand;
		}

		public HTMLRenderBand NewBand()
		{
			HTMLRenderBand b=new HTMLRenderBand(myTargetWidth);
			myRenderBands.Add(b);
			return b;
		}

		public void Paint(Graphics g)
		{
			//MessageBox.Show(myRenderBands.Count.ToString());
			foreach(HTMLRenderBand band in myRenderBands)
			{
				band.Paint(g);
			}
		}

		/// <summary>
		/// Returns the item whose
		/// bounding rectangle contains the specified point.
		/// Used for hyperlink hit-testing.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		public HTMLRenderItem LocateItem(int x, int y)
		{
			foreach(HTMLRenderBand band in this.myRenderBands)
			{
				if(y>=band.Top)
				{
					if(y<=band.Top+band.Height)
					{
						return band.LocateItem(x,y);
					}
				}
			}
			return null;
		}

		/// <summary>
		/// Returns the x,y coordinate of the upper left
		/// corner of the item with the given name
		/// </summary>
		/// <param name="aname"></param>
		/// <returns></returns>
		public Point LocateItem(string aname)
		{
			HTMLRenderItem item;
			foreach(HTMLRenderBand band in this.myRenderBands)
			{
				item=band.LocateItem(aname);
				if(item!=null)
				{
					return new Point(item.Left,item.Top);
				}
			}
			return Point.Empty;
		}
		
		public override string ToString()
		{
			string s="<HTMLRenderer ";
			foreach(HTMLRenderBand band in myRenderBands)
			{
				s+=band.ToString();
			}
			s+=" HTMLRenderer>";
			return s;
		}


	}
}
