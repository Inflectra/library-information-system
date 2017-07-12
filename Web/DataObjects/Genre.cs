using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inflectra.SampleApplications.LibraryInformationSystem.Web.DataObjects
{
    /// <summary>
    /// Represents a genre
    /// </summary>
    public class Genre
    {
        /// <summary>
        /// The id of the genre
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name of the genre
        /// </summary>
        public string Name { get; set; }
    }
}