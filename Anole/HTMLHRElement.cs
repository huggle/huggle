using System;
using System.Drawing;
using System.Xml;

namespace Anole
{
	/// <summary>
	/// Summary description for HTMLHRElement.
	/// </summary>
	public class HTMLHRElement : HTMLElement
	{
		public HTMLHRElement(ref HTMLDocument document, HTMLElement parent, XmlNode xnode)
		{
			Initialize(ref document, parent, xnode);
			//TBD: store information attributes about the
			//horizontal rule: line color, thickness, etc.
		}

		public override void Layout(HTMLRenderer renderer, Graphics g)
		{
			HTMLRenderBand band;
			band=renderer.NewBand();
			band.AddItem(new HTMLRenderHRItem(renderer.TargetWidth,this));
			renderer.NewBand();
		}

		
	}
}
