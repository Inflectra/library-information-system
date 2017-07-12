﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Xsl;
using System.IO;
using System.ServiceModel;
using System.Net;
using Inflectra.SampleApplications.LibraryInformationSystem.Web.Classes;

namespace Inflectra.SampleApplications.LibraryInformationSystem.Web.Services
{
    /// <summary>
    /// This web form uses XSLT to transform the Web Service WSDL into a human readable form for developers
    /// </summary>
    public partial class SoapService1 : System.Web.UI.Page
    {
        private const string CLASS_NAME = "Inflectra.SampleApplications.LibraryInformationSystem.Web.Services.SoapService1";

        /// <summary>
        /// This method generates the HTML from the passed-in XML
        /// </summary>
        /// <param name="sender">The object raising the event</param>
        /// <param name="e">The event arguments</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            const string METHOD_NAME = "Page_Load";
            try
            {
                //Get the current URL (without querystring) and replace the .ASPX with .SVC
                string currentUrl = Request.Url.GetLeftPart(UriPartial.Path);
                currentUrl = currentUrl.ToLowerInvariant().Replace(".aspx", ".svc");

                //Load in the XML WSDL document for the web service
                WebClient webClient = new WebClient();
                string wsdl = webClient.DownloadString(currentUrl + "?wsdl");
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(wsdl);

                //Now load in the appropriate XSLT stylesheet based on the passed-in querystring
                if (!String.IsNullOrEmpty(Request.QueryString["operation"]))
                {
                    //Viewing operation details
                    string operationName = Request.QueryString["operation"].Trim();
                    XsltArgumentList argsList = new XsltArgumentList();
                    argsList.AddParam("operationName", "", operationName);
                    argsList.AddParam("productName", "", "Library Information System"); 

                    XslCompiledTransform xslt = new XslCompiledTransform();
                    xslt.Load(Server.MapPath("~/Transforms/OperationDetails.xslt"));

                    //Write out the transformed file to the Response stream
                    MemoryStream memoryStream = new MemoryStream();
                    XHTMLWriter writer = new XHTMLWriter(memoryStream, System.Text.Encoding.UTF8);
                    xslt.Transform(xmlDoc, argsList, Response.Output);
                }
                else if (!String.IsNullOrEmpty(Request.QueryString["datacontract"]))
                {
                    //Viewing data-contract details
                    string datacontractName = Request.QueryString["datacontract"].Trim();
                    XsltArgumentList argsList = new XsltArgumentList();
                    argsList.AddParam("datacontractName", "", datacontractName);
                    argsList.AddParam("productName", "", "Library Information System"); 

                    string dataContractName = Request.QueryString["datacontract"].Trim();
                    XslCompiledTransform xslt = new XslCompiledTransform();
                    xslt.Load(Server.MapPath("~/Transforms/DataContractDetails.xslt"));

                    //Write out the transformed file to the Response stream
                    MemoryStream memoryStream = new MemoryStream();
                    XHTMLWriter writer = new XHTMLWriter(memoryStream, System.Text.Encoding.UTF8);
                    xslt.Transform(xmlDoc, argsList, Response.Output);
                }
                else
                {
                    //Viewing the list of operations
                    XslCompiledTransform xslt = new XslCompiledTransform();
                    xslt.Load(Server.MapPath("~/Transforms/ServiceOperations.xslt"));

                    //Pass the product name
                    XsltArgumentList argsList = new XsltArgumentList();
                    argsList.AddParam("productName", "", "Library Information System"); 

                    //Write out the transformed file to the Response stream
                    MemoryStream memoryStream = new MemoryStream();
                    XHTMLWriter writer = new XHTMLWriter(memoryStream, System.Text.Encoding.UTF8);
                    xslt.Transform(xmlDoc, argsList, Response.Output);
                }
            }
            catch (System.Exception exception)
            {
                Response.Write("Documentation Unavailable (" + exception.Message + ")!");
                Logger.LogErrorEvent(CLASS_NAME + METHOD_NAME, exception);
            }
        }
    }
}