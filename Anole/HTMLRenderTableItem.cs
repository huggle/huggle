using System;
using System.Drawing;

namespace Anole
{
	/// <summary>
	/// Summary description for HTMLRenderTableItem.
	/// </summary>
	public class HTMLRenderTableItem : HTMLRenderItem
	{
		protected int myRowCount;
		protected int myColCount;
		protected int[] myColWidths;
		protected int[] myRowHeights;
		
		HTMLTableElement myTableElement;

		public HTMLRenderTableItem(int rowcount,
			int colcount, int width, int height, HTMLTableElement element,
			int[] colwidths, int[] rowheights)
		{
			this.myHTMLElement=element;
			this.myTableElement=element;
			this.myRowCount=rowcount;
			this.myColCount=colcount;
			this.myWidth=width;
			this.myHeight=height;
			this.myColWidths=colwidths;
			this.myRowHeights=rowheights;
		}

		public override void Paint(Graphics g)
		{
			Pen p=new Pen(Color.Red);
			g.DrawRectangle(p,Left,Top,Width,Height);

			int l=this.Left;
			int t=this.Top;

			for(int i=0; i<myRowCount; i++)
			{
				for(int j=0; j<myColCount; j++)
				{
					g.DrawRectangle(p,l,t,this.myColWidths[j],this.myRowHeights[i]);
					l+=this.myColWidths[j];
				}
				l=this.Left;
				t+=this.myRowHeights[i];
				
			}

		}

			//g.DrawLine(p,this.Left,this.Top,this.Width,this.Top);
		}
	}

