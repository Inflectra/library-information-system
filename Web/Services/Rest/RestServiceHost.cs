using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using System.ServiceModel.Description;
using Inflectra.SampleApplications.LibraryInformationSystem.Web.Services.Wsdl;
using Inflectra.SampleApplications.LibraryInformationSystem.Web.Services.Utils;

namespace Inflectra.SampleApplications.LibraryInformationSystem.Web.Services.Rest
{
    /// <summary>
    /// WCF Service Host that dynamically creates the appropriate endpoints
    /// </summary>
    /// <remarks>
    /// Reduces the amount of configuration needed in Web.Config and avoids need to manually
    /// create separate HTTP and HTTPS binding endpoints for each service
    /// </remarks>
    public class RestServiceHost : ServiceHost
    {
        public RestServiceHost()
        {
        }

        public RestServiceHost(Type serviceType, params Uri[] baseAddresses)
            : base(serviceType, baseAddresses)
        {
        }

        public RestServiceHost(object singeltonInstance, params Uri[] baseAddresses)
            : base(singeltonInstance, baseAddresses)
        {
        }

        /// <summary>
        /// Responsible for actually provisioning the end points
        /// </summary>
        protected override void ApplyConfiguration()
        {
            base.ApplyConfiguration();

            //Create the endpoint behavior for the RESTful web services
            //Also specify that it can dynamically use either XML or JSON depending on the Content-Type HTTP Header
            RestWebHttpBehavior restWebHttpBehavior = new RestWebHttpBehavior();
            restWebHttpBehavior.AutomaticFormatSelectionEnabled = true;

            // Create the endpoint based on the service name and the binding derived from the scheme name
            ContractDescription contract = ContractDescription.GetContract(this.Description.ServiceType);
            bool httpBaseAddressAvailable = false;
            bool httpsBaseAddressAvailable = false;
            foreach (Uri address in this.BaseAddresses)
            {
                //Create the appropriate web binding for the URL scheme (http/https)
                //Allow cross domain script access (for use in AJAX API web calls)
                //All REST services are stateless so no need to use cookies
                WebHttpBinding binding = new WebHttpBinding();
                binding.MaxReceivedMessageSize = Int32.MaxValue;
                binding.CrossDomainScriptAccessEnabled = true;
                binding.AllowCookies = false;
                if (address.Scheme.ToLowerInvariant() == "https")
                {
                    binding.Security.Mode = WebHttpSecurityMode.Transport;
                    binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;
                    httpsBaseAddressAvailable = true;
                }
                else
                {
                    binding.Security.Mode = WebHttpSecurityMode.None;
                    httpBaseAddressAvailable = true;
                }

                //Set the reader quotas to unlimited
                binding.ReaderQuotas.MaxArrayLength = Int32.MaxValue;
                binding.ReaderQuotas.MaxBytesPerRead = Int32.MaxValue;
                binding.ReaderQuotas.MaxDepth = Int32.MaxValue;
                binding.ReaderQuotas.MaxNameTableCharCount = Int32.MaxValue;
                binding.ReaderQuotas.MaxStringContentLength = Int32.MaxValue;

                //Create the endpoint and specify its endpoint behavior
                ServiceEndpoint serviceEndpoint = new ServiceEndpoint(contract, binding, new EndpointAddress(address));
                serviceEndpoint.Behaviors.Add(restWebHttpBehavior);

                //Add the endpoint
                this.Description.Endpoints.Add(serviceEndpoint);
            }

            //Specify the service behavior. Easier to do it here than in the web.config file

            //<dataContractSerializer maxItemsInObjectGraph="2147483647" />
            foreach (OperationDescription operationDescription in contract.Operations)
            {
                DataContractSerializerOperationBehavior dataContractSerializer = operationDescription.Behaviors.Find<DataContractSerializerOperationBehavior>();
                if (dataContractSerializer == null)
                {
                    dataContractSerializer = new DataContractSerializerOperationBehavior(operationDescription);
                    operationDescription.Behaviors.Add(dataContractSerializer);
                }
                dataContractSerializer.MaxItemsInObjectGraph = Int32.MaxValue;
            }

            //<serviceMetadata httpGetEnabled="false" httpsGetEnabled="false" />
            ServiceMetadataBehavior serviceMetaData = this.Description.Behaviors.Find<ServiceMetadataBehavior>();
            if (serviceMetaData == null)
            {
                serviceMetaData = new ServiceMetadataBehavior();
                this.Description.Behaviors.Add(serviceMetaData);
            }
            //We dynamically set these based on the available base addresses
            serviceMetaData.HttpGetEnabled = httpBaseAddressAvailable;
            serviceMetaData.HttpsGetEnabled = httpsBaseAddressAvailable;

            //<serviceDebug includeExceptionDetailInFaults="true" />
            ServiceDebugBehavior serviceDebug = this.Description.Behaviors.Find<ServiceDebugBehavior>();
            if (serviceDebug == null)
            {
                serviceDebug = new ServiceDebugBehavior();
                this.Description.Behaviors.Add(serviceDebug);
            }
            serviceDebug.IncludeExceptionDetailInFaults = true;

            //Add the WCF extras
            InjectWsdlExtensions();
        }

               /// <summary>
        /// Configure the WSDL generation for the endpoints
        /// </summary>
        private void InjectWsdlExtensions()
        {
            foreach (ServiceEndpoint endpoint in this.Description.Endpoints)
            {
                WsdlExtensionsConfig config = new WsdlExtensionsConfig();
                //See if we have an override URL to use in the WSDL
                string customBaseUrl = Properties.Settings.Default.WcfBaseUrl;
                if (!String.IsNullOrEmpty(customBaseUrl))
                {
                    //Add on the service URL to the base URI
                    //We need to get the part of the service afer the host name and app-path
                    string vdir = HttpContext.Current.Request.ApplicationPath;
                    string serviceUrl;
                    if (vdir == "/")
                    {
                        serviceUrl = customBaseUrl + endpoint.Address.Uri.PathAndQuery;
                    }
                    else
                    {
                        serviceUrl = endpoint.Address.Uri.PathAndQuery.Replace(vdir, customBaseUrl);
                    }
                    config.Location = new Uri(serviceUrl);
                }
                endpoint.Behaviors.Add(new WsdlExtensions(config));
            }
        }
    }
}