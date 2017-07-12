using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Xml.Serialization;
using System.Xml;

using Inflectra.SampleApplications.LibraryInformationSystem.Web.DataObjects;
using System.Web.Hosting;

namespace Inflectra.SampleApplications.LibraryInformationSystem.Web
{
    /// <summary>
    /// The base class for all pages
    /// </summary>
    public class PageBase : Page
    {
        /// <summary>
        /// Gets the list of authors for the current session
        /// </summary>
        protected List<Author> Authors
        {
            get
            {
                if (Session["Authors"] == null)
                {
                    //Get the filename containing the XML data
                    string fullyQualifiedPath = VirtualPathUtility.Combine
                        (VirtualPathUtility.AppendTrailingSlash
                        (HttpRuntime.AppDomainAppVirtualPath), "~/App_Data/Authors.xml");
                    string xmlFileName = HostingEnvironment.MapPath(fullyQualifiedPath);

                    //Create a new list of authors from the XML data
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(xmlFileName);
                    XmlSerializer serializer = new XmlSerializer(typeof(List<Author>), new XmlRootAttribute() { ElementName = "Authors" });
                    XmlNodeReader nodeReader = new XmlNodeReader(xmlDoc);
                    List<Author> authors = (List<Author>)serializer.Deserialize(nodeReader);
                    Session["Authors"] = authors;
                }
                return (List<Author>)Session["Authors"];
            }
        }

        /// <summary>
        /// Gets the list of genres for the current session
        /// </summary>
        protected List<Genre> Genres
        {
            get
            {
                if (Session["Genres"] == null)
                {
                    //Get the filename containing the XML data
                    string fullyQualifiedPath = VirtualPathUtility.Combine
                        (VirtualPathUtility.AppendTrailingSlash
                        (HttpRuntime.AppDomainAppVirtualPath), "~/App_Data/Genres.xml");
                    string xmlFileName = HostingEnvironment.MapPath(fullyQualifiedPath);

                    //Create a new list of genres from the XML data
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(xmlFileName);
                    XmlSerializer serializer = new XmlSerializer(typeof(List<Genre>), new XmlRootAttribute() { ElementName = "Genres" });
                    XmlNodeReader nodeReader = new XmlNodeReader(xmlDoc);
                    List<Genre> genres = (List<Genre>)serializer.Deserialize(nodeReader);
                    Session["Genres"] = genres;
                }
                return (List<Genre>)Session["Genres"];
            }
        }

        /// <summary>
        /// Gets the list of books for the current session
        /// </summary>
        protected List<Book> Books
        {
            get
            {
                if (Session["Books"] == null)
                {
                    //Get the filename containing the XML data
                    string fullyQualifiedPath = VirtualPathUtility.Combine
                        (VirtualPathUtility.AppendTrailingSlash
                        (HttpRuntime.AppDomainAppVirtualPath), "~/App_Data/Books.xml");
                    string xmlFileName = HostingEnvironment.MapPath(fullyQualifiedPath);

                    //Create a new list of authors from the XML data
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(xmlFileName);
                    XmlSerializer serializer = new XmlSerializer(typeof(List<Book>), new XmlRootAttribute() { ElementName = "Books" });
                    XmlNodeReader nodeReader = new XmlNodeReader(xmlDoc);
                    List<Book> books = (List<Book>)serializer.Deserialize(nodeReader);
                    Session["Books"] = books;
                }

                //Make sure all navigation properties are populated
                foreach (Book book in (List<Book>)Session["Books"])
                {
                    if (book.Author == null)
                    {
                        //Find the author
                        List<Author> authors = Authors;
                        book.Author = authors.FirstOrDefault(a => a.Id == book.AuthorId);
                    }

                    if (book.Genre == null)
                    {
                        //Find the genre
                        List<Genre> genres = Genres;
                        book.Genre = genres.FirstOrDefault(g => g.Id == book.GenreId);
                    }
                }

                return (List<Book>)Session["Books"];
            }
        }
    }
}