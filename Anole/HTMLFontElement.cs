using System;
using System.Xml;
using System.Drawing;
using System.Windows.Forms;

namespace Anole
{
	/// <summary>
	/// Summary description for HTMLFontElement.
	/// </summary>
	public class HTMLFontElement : HTMLElement
	{
		public HTMLFontElement(ref HTMLDocument document,HTMLElement parent,XmlNode xnode)
		{
			Initialize(ref document, parent, xnode);

			ParseForeColor(xnode);

			string fname=this.ParseString(xnode,"face",this.Font.Name);
			float fsize=this.ParseFloat(xnode,"size",this.Font.Size);
			
			if(
				(fname.CompareTo(this.Font.Name)!=0)||
				(fsize!=Font.Size)
				)
			{
				//Create a new font
				this.Font=new Font(fname,fsize,this.Font.Style);
			}

			ParseChildren(xnode);
		}
	}

	public class HTMLBoldElement : HTMLElement
	{
		public HTMLBoldElement(ref HTMLDocument document, HTMLElement parent, XmlNode xnode)
		{
			Initialize(ref document, parent, xnode);
			string fname=this.Font.Name;
			float fsize=this.Font.Size;
			FontStyle fs=this.Font.Style;
			fs|=FontStyle.Bold;
			this.Font=new Font(fname,fsize,fs);
			ParseChildren(xnode);
		}
	}

	public class HTMLUnderscoreElement : HTMLElement
	{
		public HTMLUnderscoreElement(ref HTMLDocument document, HTMLElement parent, XmlNode xnode)
		{
			Initialize(ref document, parent, xnode);
			string fname=this.Font.Name;
			float fsize=this.Font.Size;
			FontStyle fs=this.Font.Style;
			fs|=FontStyle.Underline;
			this.Font=new Font(fname,fsize,fs);
			ParseChildren(xnode);
		}
	}

	public class HTMLItalicElement : HTMLElement
	{
		public HTMLItalicElement(ref HTMLDocument document, HTMLElement parent, XmlNode xnode)
		{
			Initialize(ref document, parent, xnode);
			string fname=this.Font.Name;
			float fsize=this.Font.Size;
			FontStyle fs=this.Font.Style;
			fs|=FontStyle.Italic;
			this.Font=new Font(fname,fsize,fs);
			ParseChildren(xnode);
		}
	}
}
