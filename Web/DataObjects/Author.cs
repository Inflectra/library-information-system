using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inflectra.SampleApplications.LibraryInformationSystem.Web.DataObjects
{
    /// <summary>
    /// Represents an author
    /// </summary>
    public class Author
    {
        /// <summary>
        /// The id of the author
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name of the author
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The age of the author
        /// </summary>
        public int Age { get; set; }

    }
}