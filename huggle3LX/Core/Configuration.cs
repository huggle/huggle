using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Huggle3LX
{
    public class Configuration
    {
        public class Core
        {
            public static int RingSize = 200;
        }

        public class SiteInfo
        {
            /// <summary>
            /// List of projects which are available
            /// </summary>
            public static Dictionary<string, string> Projects = new Dictionary<string, string>(); //  wikis
        }

        public class LoginData
        {
            public static string Username = null;

            public static string Password = null;

            public static bool UsingSSL = false;

            public static string Project = null;
        }

        public class Debugging
        {
            /// <summary>
            /// If this is true the development warnings will be displayed
            /// </summary>
            public static bool DevelopmentWarnings = true;
            /// <summary>
            /// Verbosity of debugging
            /// </summary>
            public static int Verbosity = 0;
            /// <summary>
            /// If true the application is considered to be a beta
            /// </summary>
            public static bool IsBeta = true;
        }

        public class LanguageData
        {
            /// <summary>
            /// Content of languages
            /// </summary>
            public static Dictionary<string, Dictionary<string, string>> Messages = new Dictionary<string, Dictionary<string, string>>();

            /// <summary>
            /// Languages info
            /// </summary>
            public static List<string> Languages = new List<string>();

            /// <summary>
            /// Default language
            /// </summary>
            public static string DefaultLanguage = "en";

            /// <summary>
            /// Language to be used by huggle
            /// </summary>
            public static string Language = "en";
        }
    }
}
