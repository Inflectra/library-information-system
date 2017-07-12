using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Activation;
using System.Web;
using System.Web.Security;
using Inflectra.SampleApplications.LibraryInformationSystem.Web.DataObjects;

namespace Inflectra.SampleApplications.LibraryInformationSystem.Web.Services
{
    /// <summary>
    /// The libary information system SOAP API
    /// </summary>
    [
    ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, Namespace = "http://www.inflectra.com/LibraryInformationSystem/Services/"),
    AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required),
    ]
    public class SoapService : ISoapService
    {
        #region Properties

        /// <summary>
        /// Is the current user authenticated
        /// </summary>
        protected bool IsAuthenticated
        {
            get
            {
                if (HttpContext.Current.Session["authenticated"] == null)
                {
                    return false;
                }
                else
                {
                    return (bool)HttpContext.Current.Session["authenticated"];
                }
            }
            set
            {
                //If we are setting to not authenticated - clear the user
                if (value == false)
                {
                    HttpContext.Current.Session["authenticatedUser"] = null;
                }
                HttpContext.Current.Session["authenticated"] = value;
            }
        }

        protected string SessionId
        {
            get
            {
                if (HttpContext.Current.Session == null)
                {
                    return "";
                }
                else
                {
                    return HttpContext.Current.Session.SessionID;
                }
            }
        }


        /// <summary>
        /// The username of the authenticated user
        /// </summary>
        protected string AuthenticatedUser
        {
            get
            {
                if (HttpContext.Current.Session["authenticatedUser"] == null)
                {
                    return "";
                }
                else
                {
                    return (string)HttpContext.Current.Session["authenticatedUser"];
                }
            }
            set
            {
                HttpContext.Current.Session["authenticatedUserId"] = value;
            }
        }

        #endregion

        /// <summary>
        /// Authenticates the user before using other web service functions
        /// </summary>
        /// <param name="userName">The login of the user</param>
        /// <param name="password">The password of the user</param>
        /// <returns>True if the authentication was successful</returns>
        public bool Connection_Authenticate(string userName, string password)
        {
            if (Membership.ValidateUser(userName, password))
            {
                IsAuthenticated = true;
                AuthenticatedUser = userName;
            }
            else
            {
                IsAuthenticated = false;
            }

            return IsAuthenticated;
        }

        /// <summary>
        /// Disconnects the current user's session
        /// </summary>
        public void Connection_Disconnect()
        {
            IsAuthenticated = false;
        }

        /// <summary>
        /// Retrieves the list of users in the system for the current user's session
        /// </summary>
        /// <returns>List of authors</returns>
        public List<DataObjects.Author> Author_Retrieve()
        {
            if (IsAuthenticated)
            {
                try
                {
                    return DataFactory.GetAuthors(SessionId);
                }
                catch (Exception ex)
                {
                    throw ConvertExceptions(ex);
                }
            }
            else
            {
                throw CreateFault("Unauthorized", "Unauthorized, call Connection_Authenticate first");
            }
        }

        /// <summary>
        /// Retrieves a single author by its id
        /// </summary>
        /// <param name="id">The id of the author</param>
        /// <returns>The author object</returns>
        public DataObjects.Author Author_RetrieveById(int id)
        {
            if (IsAuthenticated)
            {
                try
                {
                    return DataFactory.GetAuthors(SessionId).FirstOrDefault(a => a.Id == id);
                }
                catch (Exception ex)
                {
                    throw ConvertExceptions(ex);
                }
            }
            else
            {
                throw CreateFault("Unauthorized", "Unauthorized, call Connection_Authenticate first");
            }
        }

        /// <summary>
        /// Retrieves a list of books in the system
        /// </summary>
        /// <returns>The list of books in the system</returns>
        public List<DataObjects.Book> Book_Retrieve()
        {
            if (IsAuthenticated)
            {
                try
                {
                    return DataFactory.GetBooks(SessionId);
                }
                catch (Exception ex)
                {
                    throw ConvertExceptions(ex);
                }
            }
            else
            {
                throw CreateFault("Unauthorized", "Unauthorized, call Connection_Authenticate first");
            }
        }

        /// <summary>
        /// Retrieves a list of books by a date range
        /// </summary>
        /// <param name="startDate">The date range start date</param>
        /// <param name="endDate">The date range end date</param>
        /// <returns>The list of books that match</returns>
        public List<Book> Book_RetrieveByDateRange(DateTime startDate, DateTime endDate)
        {
            if (startDate == null)
            {
                startDate = DateTime.MinValue;
            }
            if (endDate == null)
            {
                endDate = DateTime.MaxValue;
            }
            return Book_Retrieve().Where(b => b.DateAdded >= startDate && b.DateAdded <= endDate).ToList();
        }

        /// <summary>
        /// Uploads a file and returns the byte count
        /// </summary>
        /// <param name="id">The id of the book (can be anything)</param>
        /// <param name="imageBytes">The data for the image</param>
        /// <returns>The number of bytes</returns>
        public int Book_UploadImage(int id, byte[] imageBytes)
        {
            //Simply return the byte count
            if (imageBytes == null)
            {
                return 0;
            }
            return imageBytes.Length;
        }

        /// <summary>
        /// Retrieves a single book by its id
        /// </summary>
        /// <param name="id">The id of the book</param>
        /// <returns>The book object</returns>
        public DataObjects.Book Book_RetrieveById(int id)
        {
            if (IsAuthenticated)
            {
                try
                {
                    return DataFactory.GetBooks(SessionId).FirstOrDefault(b => b.Id == id);
                }
                catch (Exception ex)
                {
                    throw ConvertExceptions(ex);
                }
            }
            else
            {
                throw CreateFault("Unauthorized", "Unauthorized, call Connection_Authenticate first");
            }
        }

        /// <summary>
        /// Adds a new book to the system
        /// </summary>
        /// <param name="book">The book data</param>
        /// <returns>The book with its id populated</returns>
        public DataObjects.Book Book_Insert(DataObjects.Book book)
        {
            if (IsAuthenticated)
            {
                try
                {
                    //Need to find the book with the maximum ID currently and increment
                    List<Book> books = DataFactory.GetBooks(SessionId);
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
                throw CreateFault("Unauthorized", "Unauthorized, call Connection_Authenticate first");
            }
        }

        /// <summary>
        /// Updates an existing book in the system
        /// </summary>
        /// <param name="book">The book record (with its id populated)</param>
        public void Book_Update(DataObjects.Book book)
        {
            if (IsAuthenticated)
            {
                try
                {
                    Book bookRef = DataFactory.GetBooks(SessionId).FirstOrDefault(b => b.Id == book.Id);
                    if (bookRef == null)
                    {
                        throw CreateFault("NotFound", String.Format("Book with id={0} not found", book.Id));
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
                throw CreateFault("Unauthorized", "Unauthorized, call Connection_Authenticate first");
            }
        }

        /// <summary>
        /// Deletes an existing book in the system
        /// </summary>
        /// <param name="id">The id of the book to delete</param>
        public void Book_Delete(int id)
        {
            if (IsAuthenticated)
            {
                try
                {
                    Book bookRef = DataFactory.GetBooks(SessionId).FirstOrDefault(b => b.Id == id);
                    if (bookRef == null)
                    {
                        throw CreateFault("NotFound", String.Format("Book with id={0} not found", id));
                    }

                    //Actually remove it
                    DataFactory.GetBooks(SessionId).Remove(bookRef);
                }
                catch (Exception ex)
                {
                    throw ConvertExceptions(ex);
                }
            }
            else
            {
                throw CreateFault("Unauthorized", "Unauthorized, call Connection_Authenticate first");
            }
        }


        #region Internal Functions

        /// <summary>
        /// Converts a normal exception into one that WCF clients can handle
        /// </summary>
        /// <param name="exception">The normal exception</param>
        /// <returns>The Fault Exception</returns>
        protected FaultException ConvertExceptions(Exception exception)
        {
            //If it's a FaultException already, then we have nothing to do
            if (exception is FaultException)
            {
                return (FaultException)exception;
            }

            //-- Build the detail element of the Fault Message
            ServiceFaultMessage serviceFaultMessage = new ServiceFaultMessage();
            serviceFaultMessage.Message = exception.Message;
            serviceFaultMessage.StackTrace = exception.StackTrace;
            serviceFaultMessage.Type = exception.GetType().Name;

            //Build the Fault Reason
            FaultReason faultReason = new FaultReason(exception.Message);

            //Now return the FaultException that contains the details of the exception
            return new FaultException<ServiceFaultMessage>(serviceFaultMessage, faultReason);
        }

        /// <summary>
        /// Creates a new WCF fault message based on the passed-in information
        /// </summary>
        /// <param name="message">The error message</param>
        /// <param name="type">The error type</param>
        /// <returns>The Fault Exception</returns>
        protected FaultException CreateFault(string type, string message)
        {
            //-- Build the detail element of the Fault Message
            ServiceFaultMessage serviceFaultMessage = new ServiceFaultMessage();
            serviceFaultMessage.Message = message;
            serviceFaultMessage.StackTrace = String.Empty;
            serviceFaultMessage.Type = type;

            //Build the Fault Reason
            FaultReason faultReason = new FaultReason(message);

            //Now return the FaultException that contains the details of the exception
            return new FaultException<ServiceFaultMessage>(serviceFaultMessage, faultReason);
        }

        #endregion
    }
}
