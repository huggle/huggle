using System;
using System.Xml;
using System.Collections;
using System.Drawing;
using System.Text;

namespace Anole
{
	/// <summary>
	/// Base class for all HTML Elements.
	/// </summary>
	public class HTMLElement
	{
		//Properties

		protected HTMLDocument myHTMLDocument;
		public HTMLDocument HTMLDocument
		{
			get{return myHTMLDocument;}
			set{myHTMLDocument=value;}
		}

		protected Color myBackColor;
		public Color BackColor
		{	
			get{return myBackColor;}
			set{myBackColor=value;}
		}

		protected Color myForeColor;
		public Color ForeColor
		{	
			get{return myForeColor;}
			set{myForeColor=value;}
		}

		protected int myWidth;
		public int Width
		{
			get{return myWidth;}
			set{myWidth=value;}
		}

		protected int myHeight;
		public int Height
		{
			get{return myHeight;}
			set{myHeight=value;}
		}

		protected Font myFont;
		public Font Font
		{
			get{return myFont;}
			set{myFont=value;}
		}

		protected HTMLElement myParent;
		public HTMLElement Parent
		{
			get{return myParent;}
			set{myParent=value;}
		}

		protected ArrayList myChildren;
		public ArrayList Children
		{
			get{return myChildren;}
			set{myChildren=value;}
		}

		protected string myType;
		public string Type
		{
			get{return myType;}
			set{myType=value.ToUpper();}
		}

		protected string myText;
		public string Text
		{
			get{return myText;}
			set{myText=value;}
		}

		protected XmlNode myNode;
		public XmlNode XmlNode
		{
			get{return myNode;}
			set{myNode=value;}
		}

		protected HTMLParser myParser;
		public HTMLParser Parser
		{
			get{return myParser;}
			set{myParser=value;}
		}

		protected HTMLAlignment myAlignment;
		public HTMLAlignment Alignment
		{
			get{return myAlignment;}
			set{myAlignment=value;}
		}

		protected HTMLVAlignment myVAlignment;
		public HTMLVAlignment VAlignment
		{
			get{return myVAlignment;}
			set{myVAlignment=value;}
		}

		protected string myName;
		public string Name
		{
			get{return myName;}
			set{myName=value;}
		}

		protected void Initialize(ref HTMLDocument document, HTMLElement parent, XmlNode xnode)
		{
			this.myHTMLDocument=document;
			this.XmlNode=xnode;
			this.Children=new ArrayList();
			this.Type=xnode.Name.ToUpper();
			this.myParent=parent;

			if(myParent!=null)
			{
				this.Parent=parent;
				this.Font=parent.Font;
				this.Alignment=parent.Alignment;
				this.VAlignment=parent.VAlignment;
				this.BackColor=parent.BackColor;
				this.ForeColor=parent.ForeColor;
			}
			else
			{
				//If no parent, start with default settings
				this.Font=new Font("Arial",10,FontStyle.Regular);
				this.Alignment=HTMLAlignment.Left;
				this.VAlignment=HTMLVAlignment.Bottom;
				this.BackColor=Color.White;
				this.ForeColor=Color.Black;
			}

			this.Text="";

			//Parse common attributes
			ParseCommonAttributes(xnode);
		}

		public HTMLElement()
		{
		}

		public HTMLElement(ref HTMLDocument document, HTMLElement parent, XmlNode xnode)
		{
			Initialize(ref document,parent,xnode);

			if(xnode.HasChildNodes)
				ParseChildren(xnode);
			else
				this.Text=xnode.InnerText.Trim();
		}

		/// <summary>
		/// Layout is called by HTMLRenderer to instruct HTMLElements to
		/// create Render items in bands owned by the calling renderer.
		/// Different HTML elements (i.e. Table cells, images etc.) should
		/// override this function as needed for their specific layout requiremtns.
		/// This base class does text layout only.
		/// </summary>
		/// <param name="renderer"></param>
		/// <param name="g"></param>
		public virtual void Layout(HTMLRenderer renderer, Graphics g)
		{
			if(this.Text.Length!=0)
			{
				int i; //loop counter
				//Split text into substrings delimited by spaces
				//and line feeds
				string[] sSubStrings;
				sSubStrings=this.Text.Split(new char[]{' ','\n'});


				//Measure the width of each substring based on the current font
				SizeF sf;
				int[] nSubStringWidths=new int[sSubStrings.Length];
				for(i=0; i<sSubStrings.Length; i++)
				{
					sSubStrings[i]=sSubStrings[i].Trim();
					sf=g.MeasureString(sSubStrings[i],this.Font);
					nSubStringWidths[i]=(int) sf.Width;
				}

				//Compute height of current font
				sf=g.MeasureString("W",this.Font);
				int nFontHeight=(int)sf.Height;

				//Compute the width of a space in the current font
				sf=g.MeasureString(" ", this.Font);
				int nSpaceWidth=(int) sf.Width;

				HTMLRenderBand band=renderer.CurrentBand();
				StringBuilder sb = new StringBuilder("");
				int sbwidth=0;
				for(i=0; i<sSubStrings.Length; i++)
				{
					//Build string and comput width
					sb.Append(sSubStrings[i]);
					sbwidth+=nSubStringWidths[i];

					//If this is the last substring just go ahead and add it
					//to the current band
					if(i==sSubStrings.Length-1)
					{
						if((band.WidthRemaining()<sbwidth)&&(band.Width!=0))
							band=renderer.NewBand();
						band.AddItem(new HTMLRenderItem(sb.ToString(),
							sbwidth+nSpaceWidth,nFontHeight,this));
						break;
					}

					//If the current band will hold the current string
					//but not the current string + space + next substring
					//then add the current string and start a new band
					if(
						(band.WidthRemaining()>=sbwidth) &&
						(band.WidthRemaining()< sbwidth+nSpaceWidth+nSubStringWidths[i+1]))
					{
						band.AddItem(new HTMLRenderItem(sb.ToString(),
							sbwidth,nFontHeight,this));
						band=renderer.NewBand();
						sb=new StringBuilder();
						sbwidth=0;
					}
						//else if the current band is too short to hold the current
						//create a new band and add it
					else if(band.WidthRemaining()<sbwidth)
					{
						band=renderer.NewBand();
						band.AddItem(new HTMLRenderItem(sb.ToString(),
							sbwidth,nFontHeight,this));
						sb=new StringBuilder();
						sbwidth=0;
					}
						//add a space and the space's with
					else
					{
						sb.Append(" ");
						sbwidth+=nSpaceWidth;
					}
				
				}
			}

			//Layout the children
			foreach(HTMLElement childelement in Children)
			{
				childelement.Layout(renderer,g);
			}
		}

		protected void ParseChildren(XmlNode xnode)
		{
			if (!xnode.HasChildNodes) return;
			foreach(XmlNode childnode in xnode.ChildNodes)
			{
				switch(childnode.Name.ToUpper())
				{
					case "BODY":
						Children.Add(new HTMLBodyElement(ref this.myHTMLDocument,this, childnode));
						break;
					case "FONT":
						Children.Add(new HTMLFontElement(ref this.myHTMLDocument,this, childnode));
						break;
					case "B":
						Children.Add(new HTMLBoldElement(ref this.myHTMLDocument,this, childnode));
						break;
					case "U":
						Children.Add(new HTMLUnderscoreElement(ref this.myHTMLDocument,this, childnode));
						break;
					case "I":
						Children.Add(new HTMLItalicElement(ref this.myHTMLDocument,this, childnode));
						break;
					case "BR":
						Children.Add(new HTMLBRElement(ref this.myHTMLDocument,this,childnode));
						break;
					case "HR":
						Children.Add(new HTMLHRElement(ref this.myHTMLDocument,this, childnode));
						break;
					case "A":
						Children.Add(new HTMLAElement(ref this.myHTMLDocument,this, childnode));
						break;
					case "CENTER":
						Children.Add(new HTMLCenterElement(ref this.myHTMLDocument,this,childnode));
						break;
					case "IMG":
						Children.Add(new HTMLImageElement(ref this.myHTMLDocument,this, childnode));
						break;
					case "TABLE":
						Children.Add(new HTMLTableElement(ref this.myHTMLDocument, this, childnode));
						break;
					case "TR":
						Children.Add(new HTMLTRElement(ref this.myHTMLDocument,this, childnode));
						break;
					case "TD":
						Children.Add(new HTMLTDElement(ref this.myHTMLDocument,this, childnode));
						break;
					default:
						Children.Add(new HTMLElement(ref this.myHTMLDocument,this,childnode));
						break;
				}
			}
		}

		protected void ParseCommonAttributes(XmlNode xnode)
		{
			ParseName(xnode);	
		}

		protected void ParseName(XmlNode xnode)
		{
			this.Name=ParseString(xnode,"name","");
			if (this.Parent==null) this.myHTMLDocument.Name=this.Name;
		}

		protected void ParseBackColor(XmlNode xnode)
		{
			this.BackColor=HTMLColorParser.ParseColor(this.ParseString(xnode,"bgcolor","white"));
		}

		protected Color ParseColor(XmlNode xnode, Color defaultcolor)
		{
			return ParseColor(xnode,"color",defaultcolor);
		}

		protected Color ParseColor(XmlNode xnode, string attrname, Color defaultcolor)
		{
			string sColorString=this.ParseString(xnode,attrname,"");
			if(sColorString.Length==0) return defaultcolor;
			return HTMLColorParser.ParseColor(sColorString);
		}

		protected HTMLAlignment ParseAlignment(XmlNode xnode, HTMLAlignment defaultalignment)
		{
			string sAlign=this.ParseString(xnode,"align","left");
			switch(sAlign.ToUpper())
			{
				case "LEFT":
					return HTMLAlignment.Left;
				case "CENTER":
					return HTMLAlignment.Center;
				case "RIGHT":
					return HTMLAlignment.Right;
				case "JUSTIFY":
					return HTMLAlignment.Justify;
				default:
					return defaultalignment;
			}
		}


		protected void ParseForeColor(XmlNode xnode)
		{
			this.ForeColor=HTMLColorParser.ParseColor(this.ParseString(xnode,"color","black"));
		}


		protected string ParseString(XmlNode xnode, string aname, string sdefault)
		{
			try
			{
				XmlNode attr=xnode.Attributes.GetNamedItem(aname);
				if(attr!=null) return attr.Value.ToString();
				return sdefault;
			}
			catch(Exception)
			{
				return sdefault;	
			}
			
		}

		protected int ParseInt(XmlNode xnode, string aname, int ndefault)
		{
			XmlNode attr=xnode.Attributes.GetNamedItem(aname);
			if(attr!=null) return Int32.Parse(attr.Value.ToString());
			return ndefault;
		}

		protected float ParseFloat(XmlNode xnode, string aname, float ndefault)
		{
			XmlNode attr=xnode.Attributes.GetNamedItem(aname);
			if(attr!=null) return (float)Double.Parse(attr.Value.ToString());
			return ndefault;
		}

		public override string ToString()
		{
			string s;
			s="<" + this.Type + " fontsize=" + this.Font.Size.ToString()+">";
			s+=this.Text;
			foreach(HTMLElement childelement in Children)
			{
				s+=childelement.ToString();
			}
			s+="</" + this.Type + ">";
			return s;
		}

		

	}

	

	public enum HTMLAlignment
	{
		Center,
		Justify,
		Left,
		Right
	}

	public enum HTMLVAlignment
	{
		Top,
		Middle,
		Bottom
	}
}
