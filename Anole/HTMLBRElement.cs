using System;
using System.Drawing;
using System.Xml;

namespace Anole
{
	/// <summary>
	/// Summary description for HTMLBRElement.
	/// </summary>
	public class HTMLBRElement : HTMLElement
	{
		public HTMLBRElement(ref HTMLDocument document, HTMLElement parent, XmlNode xnode)
		{
			Initialize(ref document, parent, xnode);
		}

		public override void Layout(HTMLRenderer renderer, Graphics g)
		{
			renderer.NewBand();
			renderer.NewBand();
		}
	}
}
