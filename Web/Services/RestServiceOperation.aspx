<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="RestServiceOperation.aspx.cs" Inherits="Inflectra.SampleApplications.LibraryInformationSystem.Web.Services.RestServiceOperation" EnableTheming="false" Theme="" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
            <br/>
            <p class="intro">
                Click <a href="RestService.aspx">here</a> for a complete list of operations. 
            </p>
            <h1>
                 <asp:Literal runat="server" ID="ltrMethodName" />: <asp:Literal runat="server" ID="ltrOperationUri" />
            </h1>
            <h3>Description</h3>
            <p class="intro">
               <asp:Literal runat="server" ID="ltrSummary" />
            </p>
            <p class="intro">
                <asp:Literal runat="server" ID="ltrRemarks" />
            </p>
            <asp:PlaceHolder ID="plcExample" runat="server">
                <h3>Example(s)</h3>
                <pre>
                  <asp:Literal runat="server" ID="ltrExample" />
                </pre>
            </asp:PlaceHolder>
            <h3>How to Execute</h3>
            <p class="intro">
                To access this REST web service, you need to use the following URL:            
            </p>
            <p>
                <asp:HyperLink ID="lnkFullUrl" runat="server" />
            </p>
            <asp:PlaceHolder ID="plcParameters" runat="server">
              <h3>Request Parameters</h3>
              <table>
                <thead>
                    <tr>
                      <th>
                        Name
                      </th>
                      <th>
                        Description
                      </th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptParameters" runat="server">
                        <ItemTemplate>
                            <tr>
                              <td>
                                <%#((System.Reflection.ParameterInfo)(Container.DataItem)).Name %>
                              </td>
                              <td>
                                <%#GetParameterDescription(((System.Reflection.ParameterInfo)(Container.DataItem)).Name) %>
                              </td>
                            </tr>                        
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
               </table>                      
            </asp:PlaceHolder>
            <h3>Request Body</h3>
            <p>
                <input type="button" id="btnXML2" name="btnXML2" value="XML" class="Selected" onclick="display2('xml')" />
                <input type="button" id="btnJSON2" name="btnJSON2" value="JSON" onclick="display2('json')" />
            </p>
            <div id="divXml2">
<pre class="ReturnData">
<asp:Literal ID="ltrXml2" runat="server" />
</pre>
            </div>
            <div id="divJson2" style="display:none">
<pre class="ReturnData">
<asp:Literal ID="ltrJson2" runat="server" />
</pre>            
            </div>

            <h3>Return Data</h3>
            <p>
                <input type="button" id="btnXML" name="btnXML" value="XML" class="Selected" onclick="display('xml')" />
                <input type="button" id="btnJSON" name="btnJSON" value="JSON" onclick="display('json')" />
            </p>
            <div id="divXml">
<pre class="ReturnData">
<asp:Literal ID="ltrXml" runat="server" />
</pre>
            </div>
            <div id="divJson" style="display:none">
<pre class="ReturnData">
<asp:Literal ID="ltrJson" runat="server" />
</pre>            
            </div>
        </div>
        <script type="text/javascript">
            function display(type)
            {
                if (type == 'json')
                {
                    document.getElementById('btnXML').className = '';
                    document.getElementById('btnJSON').className = 'Selected';
                    document.getElementById('divXml').style.display = 'none';
                    document.getElementById('divJson').style.display = 'block';
                }
                else
                {
                    document.getElementById('btnXML').className = 'Selected';
                    document.getElementById('btnJSON').className = '';
                    document.getElementById('divXml').style.display = 'block';
                    document.getElementById('divJson').style.display = 'none';
                }
            }

            function display2(type)
            {
                if (type == 'json')
                {
                    document.getElementById('btnXML2').className = '';
                    document.getElementById('btnJSON2').className = 'Selected';
                    document.getElementById('divXml2').style.display = 'none';
                    document.getElementById('divJson2').style.display = 'block';
                }
                else
                {
                    document.getElementById('btnXML2').className = 'Selected';
                    document.getElementById('btnJSON2').className = '';
                    document.getElementById('divXml2').style.display = 'block';
                    document.getElementById('divJson2').style.display = 'none';
                }
            }
        </script>
    </form>
</body>
</html>
