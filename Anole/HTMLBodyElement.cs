using System;
using System.Xml;
using System.Windows.Forms;
using System.Drawing;
namespace Anole
{
	/// <summary>
	/// Summary description for HTMLBodyElement.
	/// </summary>
	public class HTMLBodyElement : HTMLElement
	{
		public HTMLBodyElement(ref HTMLDocument document, HTMLElement parent, XmlNode xnode)
		{
			Initialize(ref document, parent, xnode);
			ParseBackColor(xnode);
			document.BackColor=this.BackColor;
			this.ForeColor=this.ParseColor(xnode,"text",Color.Black);
			ParseChildren(xnode);
		}
	}
}
