using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Collections.Specialized;
using System.Threading;
using System.Security.Principal;
using System.ServiceModel.Web;
using Inflectra.SampleApplications.LibraryInformationSystem.Web.Classes;
using System.Web.Security;
using System.Text;

namespace Inflectra.SampleApplications.LibraryInformationSystem.Web.Services.Rest
{
    /// <summary>
    /// Custom service authorization manager that lets you provide the login name and password
    /// either in the HTTP Basic Authentication Header or as a custom URL token "?username=x&password=y"
    /// </summary>
    public class RestServiceAuthorizationManager : ServiceAuthorizationManager
    {
        private const string CLASS_NAME = "Inflectra.SampleApplications.LibraryInformationSystem.Web.Services.Rest.RestServiceAuthorizationManager::";

        public const string API_PASSWORD = "password";
        public const string API_USERNAME = "username";
        public const string HEADER_AUTHORIZATION = "Authorization";

        protected override bool CheckAccessCore(OperationContext operationContext)
        {
            const string METHOD_NAME = "CheckAccessCore";
            Logger.LogEnteringEvent(CLASS_NAME + METHOD_NAME);

            bool authenticated = false;

            try
            {
                //If the querystring contains the callback parameter this is a JSONP call, so force the content-type to JSON
                //since JSONP calls cannot specify it in the HTTP Header themselves
                //For now this only works when the callback parameter is called "callback". Ideally we should get the configured
                //callback name from the WebHttpBehavior, but that's unnecessarily complex for now!
                if (HttpContext.Current.Request.QueryString["callback"] != null)
                {
                    HttpContext.Current.Request.Headers.Add("accept", "application/json");
                }

                string basicAuth = HttpContext.Current.Request.Headers[HEADER_AUTHORIZATION];
                if (!String.IsNullOrWhiteSpace(basicAuth) && basicAuth.StartsWith("Basic "))
                {
                    //Decode the header
                    string header = basicAuth.Substring("Basic ".Length);
                    byte[] decodedHeader = Convert.FromBase64String(header);
                    if (decodedHeader != null && decodedHeader.Length > 0)
                    {
                        string[] usernamePassword = new UTF8Encoding().GetString(decodedHeader).Split(':');
                        string username = usernamePassword[0];
                        string password = "";
                        if (usernamePassword.Length > 1)
                        {
                            password = usernamePassword[1];
                        }

                        if (String.IsNullOrWhiteSpace(username))
                        {
                            authenticated = false;
                        }
                        else
                        {
                            //Validate the user based in username and password combination.
                            authenticated = Membership.ValidateUser(username, password);
                            if (authenticated)
                            {
                                GenericIdentity identity = new GenericIdentity(username);
                                Thread.CurrentPrincipal = new GenericPrincipal(identity, new string[] { });
                                operationContext.ServiceSecurityContext.AuthorizationContext.Properties["Principal"] = new GenericPrincipal(identity, new string[] { });
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Logger.LogErrorEvent(CLASS_NAME + METHOD_NAME, exception);
            }

            //See if we have a URL token instead
            if (!authenticated)
            {
                try
                {
                    Uri uri = new Uri(operationContext.IncomingMessageProperties.Via.OriginalString);
                    NameValueCollection query = HttpUtility.ParseQueryString(uri.Query);
                    if (!String.IsNullOrWhiteSpace(query[API_PASSWORD]))
                    {
                        string password = query[API_PASSWORD].Trim();
                        string username2 = query[API_USERNAME];
                        if (String.IsNullOrWhiteSpace(username2))
                        {
                            authenticated = false;
                        }
                        else
                        {
                            username2 = username2.Trim();
                            //Validate the user based in username and password combination
                            authenticated = Membership.ValidateUser(username2, password);
                            if (authenticated)
                            {
                                GenericIdentity identity = new GenericIdentity(username2);
                                Thread.CurrentPrincipal = new GenericPrincipal(identity, new string[] { });
                                operationContext.ServiceSecurityContext.AuthorizationContext.Properties["Principal"] = new GenericPrincipal(identity, new string[] { });
                            }
                            WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters.Remove(API_USERNAME);
                        }
                        WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters.Remove(API_PASSWORD);
                    }
                }
                catch (UriFormatException)
                {
                    authenticated = false;
                }
                catch (ArgumentNullException)
                {
                    authenticated = false;
                }
                catch (Exception exception)
                {
                    Logger.LogErrorEvent(CLASS_NAME + METHOD_NAME, exception);
                }
            }

            //Return the authentication status
            Logger.LogTraceEvent(CLASS_NAME + METHOD_NAME, "Authenticated=" + authenticated);
            Logger.LogExitingEvent(CLASS_NAME + METHOD_NAME);
            return authenticated;
        }
    }
}