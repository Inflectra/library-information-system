using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Inflectra.SampleApplications.LibraryInformationSystem.Web.DataObjects;
using System.Xml;
using System.Xml.Serialization;
using System.Web.Hosting;

namespace Inflectra.SampleApplications.LibraryInformationSystem.Web.Services
{
    /// <summary>
    /// Responsible for retrieving data for the web services.
    /// </summary>
    public static class DataFactory
    {
        private static Dictionary<string, object> tempState = new Dictionary<string, object>();

        /// <summary>
        /// Gets the list of authors for the current session
        /// </summary>
        public static List<Author> GetAuthors(string instanceId)
        {
            if (!tempState.ContainsKey(instanceId + "_" + "Authors"))
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
                tempState[instanceId + "_" + "Authors"] = authors;
            }
            return (List<Author>)tempState[instanceId + "_" + "Authors"];
        }

        /// <summary>
        /// Gets the list of books for the current session
        /// </summary>
        public static List<Book> GetBooks(string instanceId)
        {
            if (!tempState.ContainsKey(instanceId + "_" + "Books"))
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
                tempState[instanceId + "_" + "Books"] = books;
            }

            //Make sure all navigation properties are populated
            foreach (Book book in (List<Book>)tempState[instanceId + "_" + "Books"])
            {
                if (book.Author == null)
                {
                    //Find the author
                    List<Author> authors = GetAuthors(instanceId);
                    book.Author = authors.FirstOrDefault(a => a.Id == book.AuthorId);
                }

                if (book.Genre == null)
                {
                    //Find the genre
                    List<Genre> genres = GetGenres(instanceId);
                    book.Genre = genres.FirstOrDefault(g => g.Id == book.GenreId);
                }
            }

            return (List<Book>)tempState[instanceId + "_" + "Books"];
        }

        /// <summary>
        /// Gets the list of genres for the current session
        /// </summary>
        public static List<Genre> GetGenres(string instanceId)
        {
            if (!tempState.ContainsKey(instanceId + "_" + "Genres"))
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
                tempState[instanceId + "_" + "Genres"] = genres;
            }
            return (List<Genre>)tempState[instanceId + "_" + "Genres"];
        }
    }
}