using System;
using System.Drawing;
using System.Xml;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace Anole
{
	/// <summary>
	/// Summary description for HTMLImageElement.
	/// </summary>
	public class HTMLImageElement : HTMLElement
	{
		protected string mySource;
		public string Source
		{
			get{return mySource;}
			set{mySource=value;}
		}

		protected Bitmap myBitmap;
		public Bitmap Bitmap
		{
			get{return myBitmap;}
			set{myBitmap=value;}
		}

		public HTMLImageElement(ref HTMLDocument document, HTMLElement parent, XmlNode xnode)
		{
			Initialize(ref document, parent, xnode);

			//Parse the image name
			mySource=ParseString(xnode,"src","");

			//See if file exists
			if(!System.IO.File.Exists(mySource))
			{
				//See if the file exists in the current directory
				string sPath=Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName) + "\\";
				if(System.IO.File.Exists(sPath+mySource))
				{
					mySource=sPath+mySource;
				}
			}
			if(!System.IO.File.Exists(mySource))
				return;

			//Load the bitmap
			try
			{
				myBitmap=new Bitmap(mySource);
				this.Width=myBitmap.Width;
				this.Height=myBitmap.Height;
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
				myBitmap=null;
			}
		}

		public override void Layout(HTMLRenderer renderer, Graphics g)
		{
			if(myBitmap==null) return;
			HTMLRenderBand band=renderer.CurrentBand();
			if(band.WidthRemaining()<myBitmap.Width)
			{
				band=renderer.NewBand();
			}

			band.AddItem(new HTMLRenderImage(this));
			
		}
	}
}
