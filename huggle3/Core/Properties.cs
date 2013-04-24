using System;
using System.Reflection;
using System.IO;

namespace huggle3
{
	public class Properties
	{
		public class Resources
		{
			public static string bg;
			public static string de;
			public static string en;
			public static string es;
			public static string fa;
			public static string fr;
			public static string hi;
			public static string it;
			public static string ja;
			public static string ka;
			public static string kn;
			public static string ml;
			public static string mr;
			public static string nl;
			public static string no;
			public static string oc;
			public static string or;
			public static string pt;
			public static string ptb;
			public static string ru;
			public static string sv;
			public static string zh;
			public static string ar;
			public static string DefaultLocalConfig;
		}

		public static string LoadResource(string name)
		{
			using (Stream stream = Assembly.GetExecutingAssembly()
			       .GetManifestResourceStream("huggle3.Languages." + name + ".txt"))
				using (StreamReader reader = new StreamReader(stream))
			{
				return reader.ReadToEnd();
			}
		}

		public static void Init()
		{
			Resources.ar = LoadResource("ar");
			Resources.bg = LoadResource("bg");
			Resources.de = LoadResource("de");
			Resources.en = LoadResource("en");
			Resources.es = LoadResource("es");
		}
	
	}
}

