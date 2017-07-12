using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using Inflectra.SampleApplications.LibraryInformationSystem.Web.DataObjects;
using System.ServiceModel.Web;
using Inflectra.SampleApplications.LibraryInformationSystem.Web.Services.Rest;
using System.Globalization;

namespace Inflectra.SampleApplications.LibraryInformationSystem.Web.Services
{
    /// <summary>
    /// RESTful service interface for Library Information System
    /// </summary>
    /// <remarks>
    /// You can tell the service whether you are using XML or JSON representation
    /// by passing through the appropriate HTTP Headers:
    ///     accept: application/xml
    ///     accept: application/json
    /// </remarks>
    [
    ServiceContract(Namespace = "http://www.inflectra.com/LibraryInformationSystem/Services/", SessionMode = SessionMode.NotAllowed)
    ]
    public interface IRestService
    {
        [
        RestResource("Session", "This resource returns a unique session id that needs to get passed to the other requests"),
        OperationContract,
        WebGet(UriTemplate = "session")
        ]
        string Session_Retrieve();

        [
        RestResource("Author", "This resource contains information on authors in the library catalog"),
        OperationContract,
        WebGet(UriTemplate = "author?session_id={session_id}")
        ]
        List<Author> Author_Retrieve(string session_id);

        [
        RestResource("Author"),
        OperationContract,
        WebGet(UriTemplate = "author/{id}?session_id={session_id}")
        ]
        Author Author_RetrieveById(string id, string session_id);

        [
        RestResource("Book", "This resource contains information on books in the library catalog"),
        OperationContract,
        WebGet(UriTemplate = "book?session_id={session_id}")
        ]
        List<Book> Book_Retrieve(string session_id);

        [
        RestResource("Book"),
        OperationContract,
        WebGet(UriTemplate = "book/{id}?session_id={session_id}")
        ]
        Book Book_RetrieveById(string id, string session_id);


        [
        RestResource("Book"),
        OperationContract,
        WebInvoke(Method = "POST", UriTemplate = "book?session_id={session_id}")
        ]
        Book Book_Insert(string session_id, Book book);

        [
        RestResource("Book"),
        OperationContract,
        WebInvoke(Method = "PUT", UriTemplate = "book/{id}?session_id={session_id}")
        ]
        void Book_Update(string id, string session_id, Book book);

        [
        RestResource("Book"),
        OperationContract,
        WebInvoke(Method = "DELETE", UriTemplate = "book/{id}?session_id={session_id}")
        ]
        void Book_Delete(string id, string session_id);

        [
        RestResource("Book"),
        OperationContract,
        WebInvoke(Method = "POST", UriTemplate = "book/search?session_id={session_id}")
        ]
        List<Book> Book_RetrieveByDateRange(string session_id, DateRange dateRange);

        [
        RestResource("Book Image"),
        OperationContract,
        WebInvoke(Method = "POST", UriTemplate = "book/{id}/image")
        ]
        int Book_UploadImage(string id, byte[] imageBytes);
    }

    /// <summary>
    /// Represents a date range
    /// </summary>
    public class DateRange
    {
        /// <summary>
        /// The starting date to search by
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// The ending date to search by
        /// </summary>
        public DateTime EndDate { get; set; }

        public string StartDateIso
        {
            get
            {
                return this.StartDate.ToUniversalTime().ToString("s", CultureInfo.InvariantCulture);
            }
            set
            {
                this.StartDate = DateTime.ParseExact(value, "s", CultureInfo.InvariantCulture);
            }
        }

        public string EndDateIso
        {
            get
            {
                return this.EndDate.ToUniversalTime().ToString("s", CultureInfo.InvariantCulture);
            }
            set
            {
                this.EndDate = DateTime.ParseExact(value, "s", CultureInfo.InvariantCulture);
            }
        }
    }
}
