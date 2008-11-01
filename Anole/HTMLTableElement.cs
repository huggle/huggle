using System;
using System.Xml;
using System.Drawing;

namespace Anole
{
	/// <summary>
	/// Summary description for HTMLTableElement.
	/// </summary>
	public class HTMLTableElement : HTMLElement
	{
		protected int myTargetWidth;
		public int TargetWidth
		{
			get{return myTargetWidth;}
			set{myTargetWidth=value;}
		}

		protected int myBorderSize;
		public int BorderSize
		{
			get{return myBorderSize;}
			set{myBorderSize=value;}
		}

		protected int myCellSpacing;
		public int CellSpacing
		{
			get{return myCellSpacing;}
			set{myCellSpacing=value;}
		}

		protected int myCellPadding;
		public int CellPadding
		{
			get{return myCellPadding;}
			set{myCellPadding=value;}
		}

		public HTMLTableElement(ref HTMLDocument document, HTMLElement parent, XmlNode xnode)
		{
			Initialize(ref document, parent, xnode);

			//Get backcolor or use document default color
			this.BackColor=this.ParseColor(xnode,"bgcolor",document.BackColor);
			this.myTargetWidth=this.ParseInt(xnode,"width",0);
			this.myBorderSize=this.ParseInt(xnode,"borders",1);
			this.myCellPadding=this.ParseInt(xnode,"cellpadding",0);
			this.myCellSpacing=this.ParseInt(xnode,"cellspacing",0);
			ParseChildren(xnode);
		}

		public override void Layout(HTMLRenderer renderer, System.Drawing.Graphics g)
		{
			//Determine number of rows and columns
			int nRowCount=0;
			int nColCount=0;
			nRowCount=this.Children.Count;
			HTMLTRElement firstrow=this.Children[0] as HTMLTRElement;
			if(nRowCount>0)
			{
				nColCount=firstrow.Children.Count;
			}
			else
			{
				System.Windows.Forms.MessageBox.Show("No rows in table");
				return;
			}

			//If the table width is not set, assume
			//that the width is the width of the renderer
			if(myTargetWidth==0)
			{
				myTargetWidth=renderer.TargetWidth;
			}

			//Determine an initial column count
			int nTargetColumnWidth=myTargetWidth/nColCount;

			//Create an array of column widths and
			//set to the intial column width
			int[] nColumnWidths=new int[nColCount];
			for(int i=0; i<nColCount; i++)
			{
				nColumnWidths[i]=nTargetColumnWidth;
			}
			//TBD: loop thru the first tr and see if any
			//td elements override the default column width

			//Create an array to track row-heights
			int[] nRowHeights=new int[nRowCount];


			//Give each row the column widths
			HTMLTRElement trelement;
			for(int i=0; i<this.Children.Count; i++)
			{
				trelement=Children[i] as HTMLTRElement;
				nRowHeights[i]=trelement.ComputeRowDimensions(ref nColumnWidths, g);
				
			}

			//Compute table height
			int tableheight=0;
			for(int i=0; i<nRowHeights.Length; i++)
			{
				nRowHeights[i]=10;
				tableheight+=nRowHeights[i];
			}

			//Compute table width
			int tablewidth=0;
			for(int i=0; i<nColumnWidths.Length; i++)
			{
				tablewidth+=nColumnWidths[i];
			}


			//Start a new band
			HTMLRenderBand band=renderer.NewBand();

			band.AddItem(new HTMLRenderTableItem(nRowCount,nColCount,
				tablewidth,tableheight,this,nColumnWidths,nRowHeights));

			//Add a final band
			renderer.NewBand();

		}

	}

	public class HTMLTRElement : HTMLElement
	{
		protected int[] myColumnWidths;

		public HTMLTRElement(ref HTMLDocument document, HTMLElement parent, XmlNode xnode)
		{
			Initialize(ref document, parent, xnode);
			this.BackColor=this.ParseColor(xnode,"bgcolor",parent.BackColor);
			ParseChildren(xnode);
		}


		/// <summary>
		/// Given an array of column widhts fills in the max of columnwidths
		/// Returns the height of the row.
		/// </summary>
		/// <param name="nColumnWidths"></param>
		/// <param name="g"></param>
		/// <returns></returns>
		public int ComputeRowDimensions(ref int[] nColumnWidths, Graphics g)
		{
			HTMLRenderer r;
			HTMLTDElement td;
			int rowheight=0;
			for(int i=0; i<this.Children.Count; i++)
			{
				//Create a renderer and have it render
				//our td item and all it's children
				td=this.Children[i] as HTMLTDElement;
				r=new HTMLRenderer(td,nColumnWidths[i]);

				//Store the max of either the computed with
				//or the original width

				nColumnWidths[i]=Math.Max(nColumnWidths[i],r.Width);
				
				//Store the height of row
				rowheight=Math.Max(rowheight,r.Height);

			}
			return rowheight;
		}

		

	}

	public class HTMLTDElement : HTMLElement
	{
		protected int myTargetWidth;
		public int TargetWidth
		{
			get{return myTargetWidth;}
			set{myTargetWidth=value;}
		}

		public HTMLTDElement(ref HTMLDocument document, HTMLElement parent, XmlNode xnode)
		{
			Initialize(ref document, parent, xnode);
			this.BackColor=this.ParseColor(xnode,"bgcolor",parent.BackColor);
			this.Alignment=this.ParseAlignment(xnode,HTMLAlignment.Left);
			this.myTargetWidth=this.ParseInt(xnode,"width",0);

			ParseChildren(xnode);
		}
	}
}
