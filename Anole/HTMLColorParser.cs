using System;
using System.Drawing;
using System.Globalization;

namespace Anole
{
	/// <summary>
	/// Summary description for HTMLColorParser.
	/// </summary>
	public class HTMLColorParser
	{
		public HTMLColorParser()
		{
		}

		/// <summary>
		/// Given a color in text-name format (like red) or a color
		/// in HTML color code format (like #ff0011) returns
		/// a Color or Color.White
		/// </summary>
		/// <param name="scolor"></param>
		/// <returns></returns>
		public static Color ParseColor(string scolor)
		{
			//Is it an HTML color?
			if(scolor.StartsWith("#")) return HTMLColorParser.ColorFromHTMLColor(scolor);

			//A "standard" color?
			switch(scolor.ToLower())
			{
				case "red": return Color.Red;
				case "green": return Color.Green;
				case "blue": return Color.Blue;
				case "purple": return Color.Purple;
				case "yellow": return Color.Yellow;
				case "pink": return Color.Pink;
				case "black": return Color.Black;
				case "gray": return Color.Gray;
				case "brown": return Color.Brown;
				case "orange": return Color.Orange;
			}
			return Color.White;

		}

		/// <summary>
		/// Given a color in this format "#FF0001" returns
		/// Color(255,0,1);
		/// </summary>
		/// <param name="scolor"></param>
		/// <returns></returns>
		protected static Color ColorFromHTMLColor(string scolor)
		{
			
			if(scolor.Length!=7) return Color.White;
			return Color.FromArgb(
				Int32.Parse(scolor.Substring(1,2),NumberStyles.HexNumber),
				Int32.Parse(scolor.Substring(3,2),NumberStyles.HexNumber),
				Int32.Parse(scolor.Substring(5,2),NumberStyles.HexNumber)
				);
		}
	}
}
