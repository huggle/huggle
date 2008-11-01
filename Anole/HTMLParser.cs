using System;
using System.Xml;
using System.Windows.Forms;

namespace Anole
{
	/// <summary>
	/// Summary description for HTMLParser.
	/// </summary>
	public class HTMLParser
	{

		XmlDocument myDocument;
	
		public HTMLParser()
		{
			myDocument=new XmlDocument();
		}

		public void Parse(HTMLDocument document)
		{
			try
			{
				string s=document.HTML.Replace("<br>","<br/>");
				s=s.Replace("<hr>","<hr/>");
				myDocument.LoadXml(s);
				document.RootElement=new HTMLElement(ref document, null, myDocument.FirstChild);
			}
			catch
			{
				
			}
		}
	}
}
