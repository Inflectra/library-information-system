using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Channels;
using System.Collections.Specialized;

namespace Inflectra.SampleApplications.LibraryInformationSystem.Web.Services.Rest
{
    /// <summary>
    /// Modifies the standard WebHttpBehavior to allow JSONP calls even when the user is authenticated
    /// </summary>
    public class RestWebHttpBehavior : WebHttpBehavior
    {
        protected override WebHttpDispatchOperationSelector GetOperationSelector(ServiceEndpoint endpoint)
        {
            return new RestWebHttpDispatchOperationSelector(endpoint);
        }

        /// <summary>
        /// If the URI contains the api-key, it removes the "api-key" URL token before matching the UriTemplate
        /// </summary>
        class RestWebHttpDispatchOperationSelector : WebHttpDispatchOperationSelector
        {
            public RestWebHttpDispatchOperationSelector(ServiceEndpoint endpoint) : base(endpoint) { }

            protected override string SelectOperation(ref Message message, out bool uriMatched)
            {
                Uri uri =  message.Headers.To;
                string result;
                if (!String.IsNullOrWhiteSpace(uri.Query) && uri.Query.Contains(RestServiceAuthorizationManager.API_PASSWORD))
                {
                    NameValueCollection queryCollection = HttpUtility.ParseQueryString(uri.Query);
                    if (queryCollection[RestServiceAuthorizationManager.API_PASSWORD] != null)
                    {
                        queryCollection.Remove(RestServiceAuthorizationManager.API_PASSWORD);
                        UriBuilder builder = new UriBuilder(uri);
                        builder.Query = queryCollection.ConstructQueryString();
                        message.Headers.To = builder.Uri;
                        result = base.SelectOperation(ref message, out uriMatched);
                    }
                    else
                    {
                        result = base.SelectOperation(ref message, out uriMatched);
                    }
                }
                else
                {
                    result = base.SelectOperation(ref message, out uriMatched);
                }

                return result;
            }
        } 


        public override void ApplyDispatchBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.EndpointDispatcher endpointDispatcher)
        {
            base.ApplyDispatchBehavior(endpoint, endpointDispatcher);

            if (endpointDispatcher != null && endpointDispatcher.DispatchRuntime != null && endpointDispatcher.DispatchRuntime.MessageInspectors != null)
            {
                //Remove the standard JavascriptCallbackMessageInspector
                for (int i = 0; i < endpointDispatcher.DispatchRuntime.MessageInspectors.Count; i++)
                {
                    if (endpointDispatcher.DispatchRuntime.MessageInspectors[i].GetType().Name == "JavascriptCallbackMessageInspector")
                    {
                        endpointDispatcher.DispatchRuntime.MessageInspectors.Remove(endpointDispatcher.DispatchRuntime.MessageInspectors[i]);
                        //Add the replacement one instead
                        endpointDispatcher.DispatchRuntime.MessageInspectors.Add(new JavascriptCallbackMessageInspectorEx(this.JavascriptCallbackParameterName));
                    }
                }
            }
        }
    }
}