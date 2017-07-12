using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Inflectra.SampleApplications.LibraryInformationSystem.Web.Services.Wsdl.Documentation;
using Inflectra.SampleApplications.LibraryInformationSystem.Web.DataObjects;

namespace Inflectra.SampleApplications.LibraryInformationSystem.Web.Services
{
	[ServiceContract(Namespace = "http://www.inflectra.com/LibraryInformationSystem/Services/", SessionMode = SessionMode.Allowed)]
    [XmlComments(XmlCommentFormat.Default, "Inflectra.SampleApplications.LibraryInformationSystem.Web.Services.SoapService")]
    public interface ISoapService
    {
        [
        OperationContract,
        FaultContract(typeof(ServiceFaultMessage))
        ]
        bool Connection_Authenticate(string userName, string password);

        [
        OperationContract,
        FaultContract(typeof(ServiceFaultMessage))
        ]
        void Connection_Disconnect();

        [
        OperationContract,
        FaultContract(typeof(ServiceFaultMessage))
        ]
        List<Author> Author_Retrieve();

        [
        OperationContract,
        FaultContract(typeof(ServiceFaultMessage))
        ]
        Author Author_RetrieveById(int id);

        [
        OperationContract,
        FaultContract(typeof(ServiceFaultMessage))
        ]
        List<Book> Book_Retrieve();

        [
        OperationContract,
        FaultContract(typeof(ServiceFaultMessage))
        ]
        Book Book_RetrieveById(int id);

        [
        OperationContract,
        FaultContract(typeof(ServiceFaultMessage))
        ]
        Book Book_Insert(Book book);

        [
        OperationContract,
        FaultContract(typeof(ServiceFaultMessage))
        ]
        void Book_Update(Book book);

        [
        OperationContract,
        FaultContract(typeof(ServiceFaultMessage))
        ]
        void Book_Delete(int id);

        [
        OperationContract,
        FaultContract(typeof(ServiceFaultMessage))
        ]
        List<Book> Book_RetrieveByDateRange(DateTime startDate, DateTime endDate);

        [
        OperationContract,
        FaultContract(typeof(ServiceFaultMessage))
        ]
        int Book_UploadImage(int id, byte[] imageBytes);
    }
}
