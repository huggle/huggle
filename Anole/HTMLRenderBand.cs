using System;
using System.Collections;
using System.Drawing;

namespace Anole
{
	/// <summary>
	/// Summary description for HTMLRenderBand.
	/// </summary>
	public class HTMLRenderBand
	{
		protected ArrayList myRenderItems;
		protected int myTargetWidth;
		public int TargetWidth
		{
			get{return myTargetWidth;}
			set{myTargetWidth=value;}
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

		protected int myTop;
		public int Top
		{
			get{return myTop;}
			set{myTop=value;}
		}

		public HTMLRenderBand(int targetwidth)
		{
			myTargetWidth=targetwidth;
			myRenderItems=new ArrayList();
			myHeight=0;
			myWidth=0;
		}

		public int WidthRemaining()
		{
			if(myWidth<myTargetWidth)
			{
				return myTargetWidth-myWidth;
			}
			return 0;
		}

		public int GetItemCount()
		{
			return myRenderItems.Count;
		}

		public void AddItem(HTMLRenderItem item)
		{
			myHeight=Math.Max(myHeight,item.Height);
			myWidth+=item.Width;
			myRenderItems.Add(item);
		}

		/// <summary>
		/// Called by HTMLRenderer after bands are filled
		/// with RenderItems so alignment can be calculated
		/// </summary>
		public void ComputeItemCoords()
		{
			int left=1;

			//TBD: valign for table cells

			if(myRenderItems.Count==0) return;

			//If an item in the band is centered, then
			//assume all items in that band are centered
			HTMLRenderItem hri=myRenderItems[0] as HTMLRenderItem;
			if(hri.Element.Alignment==HTMLAlignment.Center)
				left=(this.TargetWidth-this.Width)/2;
			else
				left=1;
	
			foreach(HTMLRenderItem item in myRenderItems)
			{
				item.RenderBand=this;
				item.Left=left;
				item.Top=this.Top;
				left+=item.Width;
			}
			
		}

		public void Paint(Graphics g)
		{
			/*
			Pen p=new Pen(Color.Black);
			g.DrawRectangle(p,0,this.Top,this.Width,this.Height);
			g.FillRectangle(new SolidBrush(Color.White),1,this.Top+1,
				this.Width-2,this.Height-2);
				*/
			foreach(HTMLRenderItem item in myRenderItems)
			{
				item.Paint(g);
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
			foreach(HTMLRenderItem item in this.myRenderItems)
			{
				if((x>=item.Left)&&(x<=item.Left+item.Width))
				{
					if((y>=item.Top) && (y<=item.Top+item.Height))
					{
						return item;
					}
				}
				
			}
			return null;
		}

		public HTMLRenderItem LocateItem(string aname)
		{
			foreach(HTMLRenderItem item in myRenderItems)
			{
				if(item.Element.Name.Length>0)
				{
					if(item.Element.Name.CompareTo(aname)==0)
					{
						return item;
					}
				}
			}
			return null;
		}

		public override string ToString()
		{
			string s="";
			s="<HTMLRenderBand top:" + this.Top.ToString() + " width:" + this.Width.ToString()+" height:"+this.Height.ToString()+" ";
			foreach(HTMLRenderItem item in myRenderItems)
			{
				s+=item.ToString();
			}
			s+=" HTMLRenderband>";
			return s;
		}



	}
}
