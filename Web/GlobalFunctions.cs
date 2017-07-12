using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inflectra.SampleApplications.LibraryInformationSystem.Web
{
    /// <summary>
    /// General utility functions
    /// </summary>
    public static class GlobalFunctions
    {
        /// <summary>
        /// Constructs a querystring from a namevalue collection
        /// </summary>
        /// <param name="Params"></param>
        /// <returns></returns>
        public static string ConstructQueryString(this System.Collections.Specialized.NameValueCollection Params)
        {
            List<string> items = new List<string>();
            foreach (string name in Params)
                items.Add(String.Concat(name, "=", System.Web.HttpUtility.UrlEncode(Params[name])));
            return string.Join("&", items.ToArray());
        }
    }

    /// <summary>
    /// Fake logger class to avoid having to remove from SpiraTeam derived web service code
    /// </summary>
    public static class Logger
    {
        public static void LogErrorEvent(string p1, string p2)
        {
        }

        public static void LogErrorEvent(string p1, Exception ex)
        {
        }

        public static void LogWarningEvent(string p1, string p2)
        {
        }

        public static void LogEnteringEvent(string p1)
        {
        }

        public static void LogExitingEvent(string p1)
        {
        }

        public static void LogTraceEvent(string p1, string p2)
        {
        }

        public static void Flush()
        {
        }
    }
}