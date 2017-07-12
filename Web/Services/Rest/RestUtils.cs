using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ServiceModel.Web;
using System.Globalization;

namespace Inflectra.SampleApplications.LibraryInformationSystem.Web.Services.Rest
{
    /// <summary>
    /// Contains some static utility functions useful when working with REST web services
    /// </summary>
    public static class RestUtils
    {
        private const string CLASS_NAME = "Inflectra.SampleApplications.LibraryInformationSystem.Web.Services.Rest.RestUtils";

        public const string DATETIME_FORMAT = "yyyy-MM-ddTHH:mm:ss.fff";

        /// <summary>
        /// Converts a string that contains an integer to an integer. It it cannot be converted, it throws a meaningful exception
        /// </summary>
        /// <param name="parameterName">The name of the parameter</param>
        /// <param name="parameterValue">The string parameter</param>
        /// <returns>The integer value</returns>
        public static int ConvertToInt32(string parameterValue, string parameterName)
        {
            const string METHOD_NAME = "ConvertToInt32";

            int returnValue;
            if (Int32.TryParse(parameterValue, out returnValue))
            {
                return returnValue;
            }
            else
            {
                Logger.LogWarningEvent(CLASS_NAME + METHOD_NAME, "The passed in '" + parameterName + "' needs to be an integer");
                Logger.Flush();
                throw new WebFaultException<string>("The passed in '" + parameterName + "' needs to be an integer", System.Net.HttpStatusCode.NotAcceptable);
            }
        }

        /// <summary>
        /// Converts a string that contains a datetime to DateTime object. It it cannot be converted, it throws a meaningful exception
        /// </summary>
        /// <param name="parameterName">The name of the parameter</param>
        /// <param name="parameterValue">The string parameter</param>
        /// <returns>The DateTime value</returns>
        public static DateTime ConvertToDateTime(string parameterValue, string parameterName)
        {
            const string METHOD_NAME = "ConvertToDateTime";

            DateTime returnValue;
            if (DateTime.TryParseExact(parameterValue, DATETIME_FORMAT, CultureInfo.InvariantCulture, DateTimeStyles.None , out returnValue))
            {
                return returnValue;
            }
            else
            {
                Logger.LogWarningEvent(CLASS_NAME + METHOD_NAME, "The passed in '" + parameterName + "' needs to be a DateTime in the format: " + DATETIME_FORMAT);
                Logger.Flush();
                throw new WebFaultException<string>("The passed in '" + parameterName + "' needs to be a DateTime in the format: " + DATETIME_FORMAT, System.Net.HttpStatusCode.NotAcceptable);
            }
        }

        /// <summary>
        /// Converts a string that contains an integer to an integer. It it cannot be converted, it throws a meaningful exception
        /// </summary>
        /// <param name="parameterName">The name of the parameter</param>
        /// <param name="parameterValue">The string parameter</param>
        /// <returns>The integer value</returns>
        public static bool ConvertToBoolean(string parameterValue, string parameterName)
        {
            const string METHOD_NAME = "ConvertToBoolean";

            bool returnValue;
            if (Boolean.TryParse(parameterValue, out returnValue))
            {
                return returnValue;
            }
            else
            {
                Logger.LogWarningEvent(CLASS_NAME + METHOD_NAME, "The passed in '" + parameterName + "' needs to be either 'true' or 'false'");
                Logger.Flush();
                throw new WebFaultException<string>("The passed in '" + parameterName + "' needs to be either 'true' or 'false'", System.Net.HttpStatusCode.NotAcceptable);
            }
        }
    }
}