using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Description;

namespace Inflectra.SampleApplications.LibraryInformationSystem.Web.Services.Utils
{
    static class Extensions
    {
        public static string GetHeaderType(this MessageHeaderDescription header)
        {
            return (string)ReflectionUtils.GetValue(header, "BaseType");
        }
    }
}
