using System;
using System.Drawing;
using System.IO;
using System.Text;

namespace Anole
{
	/// <summary>
	/// Summary description for HTMLDocument.
	/// </summary>
	/// 
	public class HTMLDocument
	{

		protected string myName;
		public string Name
		{
			get{return myName;}
			set{myName=value;}
		}

		protected string myHTML;
		public string HTML
		{
			get{return myHTML;}
			set{myHTML=value;}
		}

		protected Color myBackColor;
		public Color BackColor
		{
			get{return myBackColor;}
			set{myBackColor=value;}
		}

		protected Color myALinkColor;
		public Color ALinkColor
		{
			get{return myALinkColor;}
			set{myALinkColor=value;}
		}

		protected int myWidth;
		public int Width
		{
			get{return myWidth;}
			set{myWidth=value;}
		}

		protected HTMLElement myRootElement;
		public HTMLElement RootElement
		{
			get{return myRootElement;}
			set{myRootElement=value;}
		}

		protected HTMLParser myParser;

		public HTMLDocument()
		{
			myName="";
			myParser=new HTMLParser();
			this.BackColor=Color.White;
			this.ALinkColor=Color.Blue;
		}

		public void LoadHTML(string html)
		{
			this.HTML=html;
			myParser.Parse(this);
		}

		public void LoadHTMLFile(string sFileName)
		{
			FileStream f=new FileStream(sFileName,FileMode.Open,FileAccess.Read);
			byte[] buffer=new byte[2048];
			int bytesread=1;
			System.Text.StringBuilder sb;
			sb=new StringBuilder();
			do
			{
				bytesread=f.Read(buffer,0,2048);
				if(bytesread>0)
				{
					sb.Append(System.Text.ASCIIEncoding.ASCII.GetString(buffer,0,bytesread));
				}
			}while(bytesread!=0);
			f.Close();
			LoadHTML(sb.ToString());
		}

		

		

	}
}
