<%@ Page Title="Inflectra | Library Information System" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="Inflectra.SampleApplications.LibraryInformationSystem.Web._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Welcome to the Library Information System
    </h2>
    <div style="margin-right: 150px">
        <p>
            This sample application lets you view, create and edit <asp:HyperLink runat="server" NavigateUrl="~/Books.aspx">books</asp:HyperLink> in
            the libary catalog as well as view, create
            and edit <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Authors.aspx">authors</asp:HyperLink>.
        </p>
        <p>
           To view the library catalog or the authors list you will need to login as a borrower and to make changes to the list of books
           or authors you will need to login as a librarian.
        </p>
        <p>
            Note: This is not a real application, but is just a sample application used in the popular <b>SpiraTest</b> <a href="http://www.inflectra.com/SpiraTest" target="_blank">test management system</a>
            and <b>Rapise</b> <a href="http://www.inflectra.com/Rapise" target="_blank">test automation system</a>.
            Both of these products are marketed by <a href="http://www.inflectra.com" target="_blank">Inflectra Corporation</a>.
        </p>
        <br />
        <table>
            <tr>
                <td style="padding-right: 50px">
                    <a href="http://www.inflectra.com/SpiraTest" target="_blank">
                        <img src="Images/SpiraTestLogo.gif" alt="SpiraTest" />
                    </a>
                </td>                
                <td style="padding-right: 50px">
                    <a href="http://www.inflectra.com/Rapise" target="_blank">
                        <img src="Images/RapiseLogo_Small.gif" alt="Rapise" />
                    </a>
                </td>
                <td>
                    <a href="http://www.inflectra.com" target="_blank">
                        <img src="Images/Inflectra Logo-Medium.gif" alt="Inflectra" />
                    </a>
                </td>
            </tr>        
            <tr>
                <td>
                    Test Management
                </td>
                <td>
                    Test Automation
                </td>
                <td>
                    Inflectra Corporation
                </td>
            </tr>
        </table>
        <p>
            This sample application has <a href="Services/SoapService.aspx">SOAP</a> and <a href="Services/RestService.aspx">REST</a> web service APIs that can be tested by Rapise.
        </p>
        <h2>Other Samples</h2>
        <table>
            <tr>
                <td style="vertical-align:top">
                    <p>
                        &gt; <a href="Html5/AUTHTML5.htm">View the sample HTML5 Application</a>
                    </p>
                    <p>
                        &gt; <a href="Flex3/AUTFlexFP.html">View the sample Flex 3 Application</a>
                    </p>
                    <p>
                        &gt; <a href="Flex4/AUTFlexFP.html">View the sample Flex 4 Application</a>
                    </p>
                    <p>
                        &gt; <a href="Java/AUTJava.htm">View the sample Java Application</a>
                    </p>
                    <p>
                        &gt; <a href="Silverlight/SilverlightDemo.html">View the sample Silverlight Application</a>
                    </p>
                    <p>
                        &gt; <a href="HtmlTest.htm">View the HTML control list sample</a>
                    </p>
                    <p>
                        &gt; <a href="popup/parent.html">Simple HTML Popup Sample</a>
                    </p>
                </td>
                <td style="vertical-align:top">
                    <p>
                        &gt; <a href="jQuery/jQueryUi.htm">View the jQuery UI control list sample</a>
                    </p>
                    <p>
                        &gt; <a href="gwt/showcase/Showcase.html">View the GWT control list sample</a>
                    </p>
                    <p>
                        &gt; <a href="gwt2.2/showcase/Showcase.html">View the GWT v2.2 control list sample</a>
                    </p>
                    <p>
                        &gt; <a href="gwt-ext/Showcase2.html">View the GWT-Ext control list sample</a>
                    </p>
                    <p>
                        &gt; <a href="smartgwt/Index.html">View the SmartGWT control list sample</a>
                    </p>
                    <p>
                        &gt; <a href="yui3/Index.html">View the YUI control list samples</a> (<a href="yui2/Index.html">2.0</a>, <a href="yui2.9/examples/Index.html">2.9</a>, <a href="yui3/Index.html">3.0</a>)
                    </p>
                    <p>
                        &gt; <a href="TwoTabs/Page1.html">Two tabs sample</a> (<a href="TwoTabs/Page1.html">page 1</a> | <a href="TwoTabs/Page2.html">page 2</a>)
                    </p>
                </td>
            </tr>
        </table>
        <br />
    </div>
</asp:Content>
