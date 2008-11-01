using System;
using System.Drawing;
using System.Xml;

namespace Anole
{
	/// <summary>
	/// Summary description for HTMLAElement.
	/// </summary>
	public class HTMLAElement : HTMLElement
	{
		protected string myHREF;
		public string HREF
		{
			get{return myHREF;}
			set{myHREF=value;}
		}

		public HTMLAElement(ref HTMLDocument document, HTMLElement parent, XmlNode xnode)
		{
			Initialize(ref document, parent, xnode);
			
			//Set color to link color
			this.ForeColor=document.ALinkColor;

			//Get the HREF
			this.myHREF=this.ParseString(xnode,"href","");
	
			//Set font to underscored
			string fname=this.Font.Name;
			float fsize=this.Font.Size;
			FontStyle fs=this.Font.Style;
			fs|=FontStyle.Underline;
			this.Font=new Font(fname,fsize,fs);
			ParseChildren(xnode);
		}

		public override void Layout(HTMLRenderer renderer, Graphics g)
		{
			if(this.Name.Length>0)
			{
				renderer.CurrentBand().AddItem(new HTMLRenderBookmark(this));
			}
			base.Layout (renderer, g);
		}

	}
}
