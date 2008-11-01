using System;
using System.Xml;
using System.Drawing;

namespace Anole
{
	/// <summary>
	/// Summary description for HTMLCenterElement.
	/// </summary>
	public class HTMLCenterElement : HTMLElement
	{
		public HTMLCenterElement(ref HTMLDocument document, HTMLElement parent, XmlNode xnode)
		{
			Initialize(ref document, parent, xnode);
			this.Alignment=HTMLAlignment.Center;
			ParseChildren(xnode);
		}

		public override void Layout(HTMLRenderer renderer, Graphics g)
		{
			//If the current band has items,
			//then start a new band.
			if(renderer.CurrentBand().Width>0) renderer.NewBand();

			base.Layout(renderer,g);
			renderer.NewBand();
		}
	}
}
