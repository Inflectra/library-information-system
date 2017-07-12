using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Activation;
using Inflectra.SampleApplications.LibraryInformationSystem.Web.DataObjects;
using System.Threading;
using System.ServiceModel.Web;

namespace Inflectra.SampleApplications.LibraryInformationSystem.Web.Services
{
    /// <summary>
    /// The libary information system REST API
    /// </summary>
    [
    ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, Namespace = "http://www.inflectra.com/LibraryInformationSystem/Services/"),
    AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)
    ]
    public class RestService : IRestService
    {
        #region Properties

        /// <summary>
        /// Is the current user authenticated
        /// </summary>
        protected bool IsAuthenticated
        {
            get
            {
                if (Thread.CurrentPrincipal == null)
                {
                    return false;
                }
                else if (Thread.CurrentPrincipal.Identity == null)
                {
                    return false;
                }
                else
                {
                    return Thread.CurrentPrincipal.Identity.IsAuthenticated;
                }
            }
        }

        #endregion

        /// <summary>
        /// Returns the unique session id that is used to prevent different user's requests getting mixed up
        /// </summary>
        /// <returns>The unique session id that needs to be passed to all other requests</returns>
        public string Session_Retrieve()
        {
            return Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Retrieves the list of users in the system for the current user's session
        /// </summary>
        /// <param name="session_id">The unique session id</param>
        /// <returns>List of authors</returns>
        public List<DataObjects.Author> Author_Retrieve(string session_id)
        {
            //Make sure a session id is specified
            if (String.IsNullOrWhiteSpace(session_id))
            {
                throw new WebFaultException<string>("You need to pass a session_id with this request", System.Net.HttpStatusCode.BadRequest);
            }

            if (IsAuthenticated)
            {
                try
                {
                    return DataFactory.GetAuthors(session_id);
                }
                catch (Exception ex)
                {
                    throw ConvertExceptions(ex);
                }
            }
            else
            {
                throw new WebFaultException<string>("Unauthorized, please pass through the authentication information with the request", System.Net.HttpStatusCode.Unauthorized);
            }
        }

        /// <summary>
        /// Retrieves a single author by its id
        /// </summary>
        /// <param name="id">The id of the author</param>
        /// <param name="session_id">The unique session id</param>
        /// <returns>The author object</returns>
        public DataObjects.Author Author_RetrieveById(string id, string session_id)
        {
            //Make sure a session id is specified
            if (String.IsNullOrWhiteSpace(session_id))
            {
                throw new WebFaultException<string>("You need to pass a session_id with this request", System.Net.HttpStatusCode.BadRequest);
            }

            if (IsAuthenticated)
            {
                try
                {
                    return DataFactory.GetAuthors(session_id).FirstOrDefault(a => a.Id.ToString() == id);
                }
                catch (Exception ex)
                {
                    throw ConvertExceptions(ex);
                }
            }
            else
            {
                throw new WebFaultException<string>("Unauthorized, please pass through the authentication information with the request", System.Net.HttpStatusCode.Unauthorized);
            }
        }

        /// <summary>
        /// Retrieves a list of books in the system
        /// </summary>
        /// <param name="session_id">The unique session id</param>
        /// <returns>The list of books in the system</returns>
        public List<DataObjects.Book> Book_Retrieve(string session_id)
        {
            //Make sure a session id is specified
            if (String.IsNullOrWhiteSpace(session_id))
            {
                throw new WebFaultException<string>("You need to pass a session_id with this request", System.Net.HttpStatusCode.BadRequest);
            }

            if (IsAuthenticated)
            {
                try
                {
                    return DataFactory.GetBooks(session_id);
                }
                catch (Exception ex)
                {
                    throw ConvertExceptions(ex);
                }
            }
            else
            {
                throw new WebFaultException<string>("Unauthorized, please pass through the authentication information with the request", System.Net.HttpStatusCode.Unauthorized);
            }
        }

        /// <summary>
        /// Retrieves a single book by its id
        /// </summary>
        /// <param name="id">The id of the book</param>
        /// <param name="session_id">The unique session id</param>
        /// <returns>The book object</returns>
        public DataObjects.Book Book_RetrieveById(string id, string session_id)
        {
            //Make sure a session id is specified
            if (String.IsNullOrWhiteSpace(session_id))
            {
                throw new WebFaultException<string>("You need to pass a session_id with this request", System.Net.HttpStatusCode.BadRequest);
            }

            if (IsAuthenticated)
            {
                try
                {
                    return DataFactory.GetBooks(session_id).FirstOrDefault(b => b.Id.ToString() == id);
                }
                catch (Exception ex)
                {
                    throw ConvertExceptions(ex);
                }
            }
            else
            {
                throw new WebFaultException<string>("Unauthorized, please pass through the authentication information with the request", System.Net.HttpStatusCode.Unauthorized);
            }
        }

        /// <summary>
        /// Adds a new book to the system
        /// </summary>
        /// <param name="book">The book data</param>
        /// <param name="session_id">The unique session id</param>
        /// <returns>The book with its id populated</returns>
        public DataObjects.Book Book_Insert(string session_id, DataObjects.Book book)
        {
            //Make sure a session id is specified
            if (String.IsNullOrWhiteSpace(session_id))
            {
                throw new WebFaultException<string>("You need to pass a session_id with this request", System.Net.HttpStatusCode.BadRequest);
            }

            if (IsAuthenticated)
            {
                try
                {
                    //Need to find the book with the maximum ID currently and increment
                    List<Book> books = DataFactory.GetBooks(session_id);
                    int maxIdBook = books.OrderByDescending(b => b.Id).First().Id + 1;
                    book.Id = maxIdBook;
                    books.Add(book);
                    return book;
                }
                catch (Exception ex)
                {
                    throw ConvertExceptions(ex);
                }
            }
            else
            {
                throw new WebFaultException<string>("Unauthorized, please pass through the authentication information with the request", System.Net.HttpStatusCode.Unauthorized);
            }
        }

        /// <summary>
        /// Updates an existing book in the system
        /// </summary>
        /// <param name="id">The id of the book to update</param>
        /// <param name="session_id">The unique session id</param>
        /// <param name="book">The book record</param>
        public void Book_Update(string id, string session_id, DataObjects.Book book)
        {
            //Make sure a session id is specified
            if (String.IsNullOrWhiteSpace(session_id))
            {
                throw new WebFaultException<string>("You need to pass a session_id with this request", System.Net.HttpStatusCode.BadRequest);
            }

            if (IsAuthenticated)
            {
                try
                {
                    Book bookRef = DataFactory.GetBooks(session_id).FirstOrDefault(b => b.Id == book.Id);
                    if (bookRef == null)
                    {
                        throw new WebFaultException<string>(String.Format("Book with id={0} not found", book.Id), System.Net.HttpStatusCode.NotFound);
                    }
                    bookRef.AuthorId = book.AuthorId;
                    bookRef.GenreId = book.GenreId;
                    bookRef.Name = book.Name;
                }
                catch (Exception ex)
                {
                    throw ConvertExceptions(ex);
                }
            }
            else
            {
                throw new WebFaultException<string>("Unauthorized, please pass through the authentication information with the request", System.Net.HttpStatusCode.Unauthorized);
            }
        }

        /// <summary>
        /// Deletes an existing book in the system
        /// </summary>
        /// <param name="session_id">The unique session id</param>
        /// <param name="id">The id of the book to delete</param>
        public void Book_Delete(string id, string session_id)
        {
            //Make sure a session id is specified
            if (String.IsNullOrWhiteSpace(session_id))
            {
                throw new WebFaultException<string>("You need to pass a session_id with this request", System.Net.HttpStatusCode.BadRequest);
            }

            if (IsAuthenticated)
            {
                try
                {
                    Book bookRef = DataFactory.GetBooks(session_id).FirstOrDefault(b => b.Id.ToString() == id);
                    if (bookRef == null)
                    {
                        throw new WebFaultException<string>(String.Format("Book with id={0} not found", id), System.Net.HttpStatusCode.NotFound);
                    }

                    //Actually remove it
                    DataFactory.GetBooks(session_id).Remove(bookRef);
                }
                catch (Exception ex)
                {
                    throw ConvertExceptions(ex);
                }
            }
            else
            {
                throw new WebFaultException<string>("Unauthorized, please pass through the authentication information with the request", System.Net.HttpStatusCode.Unauthorized);
            }
        }

        /// <summary>
        /// Retrieves a list of books by a date range
        /// </summary>
        /// <param name="session_id">The session id</param>
        /// <param name="dateRange">The date range</param>
        /// <returns>The list of books that match</returns>
        public List<Book> Book_RetrieveByDateRange(string session_id, DateRange dateRange)
        {
            if (dateRange == null)
            {
                return new List<Book>();
            }
            return Book_Retrieve(session_id).Where(b => b.DateAdded >= dateRange.StartDate && b.DateAdded <= dateRange.EndDate).ToList();
        }

        /// <summary>
        /// Uploads a file and returns the byte count
        /// </summary>
        /// <param name="id">The id of the book (can be anything)</param>
        /// <param name="imageBytes">The data for the image</param>
        /// <returns>The number of bytes</returns>
        public int Book_UploadImage(string id, byte[] imageBytes)
        {
            //Simply return the byte count
            if (imageBytes == null)
            {
                return 0;
            }
            return imageBytes.Length;
        }

        #region Internal Functions

        /// <summary>
        /// Converts a normal exception into one that REST clients can handle
        /// </summary>
        /// <param name="exception">The normal exception</param>
        /// <returns>The Fault Exception</returns>
        protected FaultException ConvertExceptions(Exception exception)
        {
            //If it's a WebFaultException already, then we have nothing to do
            if (exception is WebFaultException)
            {
                return (WebFaultException)exception;
            }

            //Now return the WebFaultException that contains the details of the exception
            return new WebFaultException<string>(exception.Message, System.Net.HttpStatusCode.BadRequest);
        }

        #endregion
    }
}
