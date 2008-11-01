using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;

namespace Anole
{
	/// <summary>
	/// Summary description for HTMLPanel.
	/// </summary>
	public class HTMLPanel : System.Windows.Forms.Panel
	{
		protected HTMLDocument myDocument;
		protected HTMLRenderer myRenderer;
		protected HScrollBar myHScrollBar;
		protected VScrollBar myVScrollBar;
		protected bool bIgnoreScrolling;
		protected Bitmap myBitmap;
		protected PictureBox myPictureBox;
		protected Button myButton;


		/// <summary>
		/// Delegate subscribers must implement to listen to hyperlink clicks
		/// </summary>
		/// 
		public delegate void LinkClickHandler(object HTMLPanel, HTMLEventArgs htmlEventInfo);

		//Publish OnLinkClick event
		public event LinkClickHandler OnLinkClick;


		public HTMLPanel()
		{
			InitializeComponent();
			myDocument=new HTMLDocument();
			bIgnoreScrolling=false;
		}

		public string HTML
		{
			get{return myDocument.HTML;}
			set{this.LoadHTML(value);}
		}

		private void InitializeComponent()
		{
			myHScrollBar=new HScrollBar();
			this.Controls.Add(myHScrollBar);
			this.myHScrollBar.ValueChanged+=new EventHandler(myHScrollBar_ValueChanged);
		
			myVScrollBar=new VScrollBar();
			this.Controls.Add(myVScrollBar);
			this.myVScrollBar.ValueChanged+=new EventHandler(myVScrollBar_ValueChanged);

			//Add event handlers
			this.Resize+=new EventHandler(HTMLPanel_Resize);
			
			myPictureBox=new PictureBox();
			myPictureBox.Location=new Point(0,0);
			myPictureBox.MouseDown+=new MouseEventHandler(myPictureBox_MouseDown);
			this.Controls.Add(myPictureBox);

			SizeScrollBars();
			
		}

		public void LoadHTML(string sHTML)
		{
			myDocument=new HTMLDocument();
			myDocument.LoadHTML(sHTML);
			Render();
		}

		public void LoadHTMLFile(string sFileName)
		{
			myDocument=new HTMLDocument();
			myDocument.LoadHTMLFile(sFileName);
			Render();
		}

		protected void Render()
		{
			myRenderer=new HTMLRenderer(myDocument.RootElement,this.Width-19);

			if((myRenderer.Width==0)||(myRenderer.Height==0))return;

			myBitmap=new Bitmap(myRenderer.Width,myRenderer.Height);
			Graphics g=Graphics.FromImage(myBitmap);
			g.Clear(myDocument.BackColor);
			myRenderer.Paint(g);

			this.myPictureBox.Width=myBitmap.Width;
			this.myPictureBox.Height=myBitmap.Height;
			this.myPictureBox.Image=myBitmap;
			this.myPictureBox.Top=0;
			this.myPictureBox.Left=0;

			SizeScrollBars();

		}

		public override string ToString()
		{
			return myDocument.RootElement.ToString();
		}

		public string GetRendererInfo()
		{
			return myRenderer.ToString();
		}

		private void HTMLPanel_Resize(object sender, EventArgs e)
		{
			SizeScrollBars();
		}

		public void SizeScrollBars()
		{
			bIgnoreScrolling=true;

			if(this.myRenderer==null)
			{
				myHScrollBar.Visible=false;
				myVScrollBar.Visible=false;
			}

			this.myHScrollBar.Value=0;
			this.myVScrollBar.Value=0;

			if(this.myRenderer!=null)
			{

				if(myRenderer.Height>this.Height)
				{
					myVScrollBar.Location=new System.Drawing.Point(this.Width-16,0);
					myVScrollBar.Size=new System.Drawing.Size(16,this.Height);

					//if(myHScrollBar.Visible)
					//	myVScrollBar.Size=new System.Drawing.Size(16,this.Height-16);
					//else
						
					myVScrollBar.Value=0;
					myVScrollBar.Maximum=myRenderer.Height;
					myVScrollBar.SmallChange=10;
					myVScrollBar.LargeChange=myRenderer.Height/10;
					myVScrollBar.Visible=true;
				}
				else
				{
					myVScrollBar.Visible=false;
				}

				int hWidth=this.Width;
				if(myVScrollBar.Visible) hWidth-=16;

				if(myRenderer.Width>hWidth)
				{
					myHScrollBar.Location=new System.Drawing.Point(0,this.Height-16);
					if(this.myVScrollBar.Visible)
					{
						myHScrollBar.Size=new System.Drawing.Size(this.Width-16,16);
						myVScrollBar.Height-=16;
					}
					else
						myHScrollBar.Size=new System.Drawing.Size(this.Width,16);

					myHScrollBar.Value=0;
					myHScrollBar.Maximum=myRenderer.Width-this.Width+16+16+16;
					myHScrollBar.SmallChange=10;
					myHScrollBar.LargeChange=myRenderer.Width/10;
					myHScrollBar.Visible=true;

				}
				else
				{
					myHScrollBar.Visible=false;
				}

			}

			if(myHScrollBar.Visible && myVScrollBar.Visible)
			{
				myButton.Visible=true;
				myButton.Left=this.Width-16;
				myButton.Top=this.Height-16;
				myButton.BringToFront();
			}

			bIgnoreScrolling=false;
		}

		private void myHScrollBar_ValueChanged(object sender, EventArgs e)
		{
			if(bIgnoreScrolling) return;
			this.myPictureBox.Left=-myHScrollBar.Value;
			//this.Invalidate();
		}

		private void myVScrollBar_ValueChanged(object sender, EventArgs e)
		{
			if(bIgnoreScrolling) return;
			this.myPictureBox.Top=-myVScrollBar.Value;
			//this.myPictureBox.Update();
			//this.myPictureBox.Image=this.myBitmap;
			//this.Invalidate();
		}

		private void myPictureBox_MouseDown(object sender, MouseEventArgs e)
		{
			//Check to see if mouse was pressed down on
			//a hyperlink
			int docx=e.X;
			int docy=e.Y;
			HTMLRenderItem item=this.myRenderer.LocateItem(docx,docy);
			if(item!=null)
			{
				HTMLElement element=item.Element.Parent;
				if(element is HTMLAElement)
				{
					HTMLAElement aelement=element as HTMLAElement;
		
					//If anyone has subscribed to
					//link click events, notify them
					if(OnLinkClick!=null)
						OnLinkClick(this,new HTMLEventArgs(this.myDocument.Name,
							aelement.Name,aelement.HREF));

					//If the href is a bookmark
					//find it's coordinates and scroll to it
					if(aelement.HREF.Trim().StartsWith("#"))
					{
						string name=aelement.HREF.Substring(1,aelement.HREF.Length-1);
						Point p=this.myRenderer.LocateItem(name);
						if(p!=Point.Empty)
						{
							this.myVScrollBar.Value=p.Y;
						}
					}

				}
			}

		}
	}


	/// <summary>
	/// Passed to subscribers of OnLinkClick. Details information about the anchor
	/// that was clicked.
	/// </summary>
	public class HTMLEventArgs : EventArgs
	{
		
		public readonly string documentname;
		public readonly string elementname;
		public readonly string href;

		public HTMLEventArgs(string documentname,
			string elementname, string href)
		{
			this.documentname=documentname;
			this.elementname=elementname;
			this.href=href;
		}

		public override string ToString()
		{
			return this.documentname+":"+this.elementname+":"+this.href;
		}

	}
}