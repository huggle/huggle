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
            try
            {
                using (Stream stream = Assembly.GetExecutingAssembly()
                       .GetManifestResourceStream("huggle3.Languages." + name + ".txt"))
                    using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            } catch (Exception fail)
            {
                Core.ExceptionHandler(fail);
            }
            return "";
        }

        /// <summary>
        /// Load a default local config
        /// </summary>
        /// <returns>The cf.</returns>
        public static string LoadCf()
        {
            try
            {
                using (Stream stream = Assembly.GetExecutingAssembly()
                       .GetManifestResourceStream("huggle3.Resources.DefaultLocalConfig.txt"))
                    using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            } catch (Exception fail)
            {
                Core.ExceptionHandler(fail);
            }
            return "";
        }

        public static void Init()
        {
            Resources.ar = LoadResource("ar");
            Resources.bg = LoadResource("bg");
            Resources.de = LoadResource("de");
            Resources.en = LoadResource("en");
            Resources.es = LoadResource("es");
            Resources.fa = LoadResource("fa");
            Resources.fr = LoadResource("fr");
            Resources.hi = LoadResource("hi");
            Resources.it = LoadResource("it");
            Resources.ja = LoadResource("ja");
            Resources.ka = LoadResource("ka");
            Resources.kn = LoadResource("kn");
            Resources.ml = LoadResource("ml");
            Resources.mr = LoadResource("mr");
            Resources.nl = LoadResource("nl");
            Resources.no = LoadResource("no");
            Resources.oc = LoadResource("oc");
            Resources.or = LoadResource("or");
            Resources.pt = LoadResource("pt");
            Resources.ptb = LoadResource("ptb");
            Resources.ru = LoadResource("ru");
            Resources.sv = LoadResource("sv");
            Resources.zh = LoadResource("zh");
            Resources.DefaultLocalConfig = LoadCf();
        }
    }
}

