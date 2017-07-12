using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Inflectra.SampleApplications.LibraryInformationSystem.Web.DataObjects
{
    /// <summary>
    /// Represents a book
    /// </summary>
    public class Book
    {
        /// <summary>
        /// The id of the book
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name of the book
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Is the book out of print
        /// </summary>
        public bool IsOutOfPrint { get; set; }

        /// <summary>
        /// The date/time the book was added
        /// </summary>
        public DateTime DateAdded { get; set; }

        /// <summary>
        /// The date/time the book was added in ISO format
        /// </summary>

        public string DateAddedIso
        {
            get
            {
                return this.DateAdded.ToUniversalTime().ToString("s", CultureInfo.InvariantCulture);
            }
            set
            {
                this.DateAdded = DateTime.ParseExact(value, "s", CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// The id of the book's author
        /// </summary>
        public int AuthorId
        {
            get
            {
                return this.authorId;
            }
            set
            {
                //Need to reset the navigation property
                this.authorId = value;
                this.Author = null;
            }
        }
        private int authorId;

        /// <summary>
        /// The id of the book's genre
        /// </summary>
        public int GenreId
        {
            get
            {
                return this.genreId;
            }
            set
            {
                //Need to reset the navigation property
                this.genreId = value;
                this.Genre = null;
            }

        }
        private int genreId;

        //Navigation properties

        /// <summary>
        /// The author of the book
        /// </summary>
        public Author Author { get; set; }

        /// <summary>
        /// The genre of the book
        /// </summary>
        public Genre Genre { get; set; }

        //Lookup properties

        /// <summary>
        /// The name of the book's author
        /// </summary>
        public string AuthorName
        {
            get
            {
                if (this.Author == null)
                {
                    return "";
                }
                else
                {
                    return this.Author.Name;
                }
            }
        }

        /// <summary>
        /// The name of the book's genre
        /// </summary>
        public string GenreName
        {
            get
            {
                if (this.Genre == null)
                {
                    return "";
                }
                else
                {
                    return this.Genre.Name;
                }
            }
        }

    }
}