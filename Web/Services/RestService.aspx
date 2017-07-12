<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="RestService.aspx.cs" Inherits="Inflectra.SampleApplications.LibraryInformationSystem.Web.Services.RestService1" EnableTheming="false" Theme="" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title></title>
        <meta http-equiv="X-UA-Compatible" content="IE=Edge" />
        <link rel="stylesheet" type="text/css" href="Documentation.css" />
    </head>
    <body>
        <form id="frmMain" runat="server">
            <div id="content">
                <p class="heading1">
                  <asp:Literal runat="server" ID="ltrProductName2"/>: REST Web Service
                </p>
                <h1>
                    Resources
                </h1>
                <p>
                    This table provides a list of all the resources exposed by this REST web service:
                </p>
                <asp:GridView ID="grdResources" runat="server" AutoGenerateColumns="false">
                    <Columns>
                        <asp:TemplateField HeaderText="Resource">
                            <ItemTemplate>
                                <asp:HyperLink runat="server" NavigateUrl='<%#"#" + ((KeyValuePair<string, RestResourceInfo>)(Container.DataItem)).Key %>' Text="<%#((KeyValuePair<string, RestResourceInfo>)(Container.DataItem)).Key %>" />
                            </ItemTemplate>                        
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Methods">
                            <ItemTemplate>
                                <asp:Repeater ID="rptMethods" runat="server" DataSource="<%#((KeyValuePair<string, RestResourceInfo>)(Container.DataItem)).Value.Methods %>">
                                    <ItemTemplate>
                                        <span class="fixedWidth"><%#(string)(Container.DataItem) %></span>
                                    </ItemTemplate>
                                    <SeparatorTemplate>,</SeparatorTemplate>
                                </asp:Repeater>                            
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Description">
                            <ItemTemplate>
                                <%#((KeyValuePair<string, RestResourceInfo>)(Container.DataItem)).Value.Description %>
                            </ItemTemplate>                        
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <h1>Accessing Services</h1>
                <p>
                    To access these REST web services, you need to use the following base URL:
                </p>
                <p>
                    <asp:HyperLink ID="lnkBaseUrl" runat="server" />
                </p>
                <p>
                    You can specify the format of data that will be returned (XML or JSON) by passing the following HTTP Headers:
                </p>
                <ul>
                    <li>
                        <strong>Content-Type: application/xml</strong> - Sends data in XML format
                    </li>
                    <li>
                        <strong>accept: application/xml</strong> - Returns data in XML format
                    </li>
                    <li>
                        <strong>Content-Type: application/json</strong> - Sends data in JSON format                    
                    </li>
                    <li>
                        <strong>accept: application/json</strong> - Returns data in JSON format                    
                    </li>
                </ul>
                <p>
                    To authenticate with the web service there are two methods:
                </p>
                <ul>
                    <li>
                        Pass in the <b>username</b> and <b>password</b> using the standard HTTP basic authentication header:
                        <pre class="ReturnData">
                            Authorization: Basic XXXXXXXXXXXXXXXXXXXXXXXXXX
                            where XXXXXXXXXXXXXXXXXXXXXXXXXX is username:password base64 encoded
                        </pre>
                    </li>
                    <li>
                        Append the <b>username</b> and <b>password</b> as an extra querystring parameter:                    
                        <pre class="ReturnData">
                            ?username=librarian&password=librarian
                        </pre>
                    </li>
                </ul>
                <p>
                    Finally if you need to call one of the <span class="fixedWidth">GET</span> methods from a web page in another domain
                    you need to use the special JSONP (JSON with Padding) syntax. This tells the web service to return
                    the data in the form of a JavaScript &lt;script&gt; tag so that it be can be called across web site domains safely.
                    To request the data in JSONP format, just add the following extra querystring parameter:
                </p>
                <pre class="ReturnData">
                    ?callback=[name of callback function]
                </pre>
                <asp:Repeater ID="rptResourceOperations" runat="server">
                    <ItemTemplate>
                        <h1><a runat="server" name='<%#((KeyValuePair<string, RestResourceInfo>)(Container.DataItem)).Key %>' />
                        <%#((KeyValuePair<string, RestResourceInfo>)(Container.DataItem)).Key%></h1>
                        <table>
                            <thead>
                                <tr>
                                    <th>URI</th>
                                    <th>Method</th>
                                    <th>Description</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="rptOperations" runat="server" DataSource="<%#((KeyValuePair<string, RestResourceInfo>)(Container.DataItem)).Value.Operations.OrderBy(o => o.Uri) %>">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:HyperLink runat="server" Text="<%#((RestOperationInfo)(Container.DataItem)).Uri%>" CssClass="Uri"
                                                    NavigateUrl='<%# "RestServiceOperation.aspx?uri=" + Server.UrlEncode(((RestOperationInfo)(Container.DataItem)).Uri) + "&method=" + Server.UrlEncode(((RestOperationInfo)(Container.DataItem)).Method) %>' />                                                
                                            </td>
                                            <td>
                                                <span class="fixedWidth">
                                                    <%#((RestOperationInfo)(Container.DataItem)).Method%>
                                                </span>
                                            </td>
                                            <td><%#((RestOperationInfo)(Container.DataItem)).Description%></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>                     
                        </table>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </form>
    </body>
</html>

