<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AuthorEdit.aspx.cs" Inherits="Inflectra.SampleApplications.LibraryInformationSystem.Web.AuthorEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:PlaceHolder ID="plcNewAuthor" runat="server">
        <h2>Create New Author</h2>
        <p>
            Please enter the author information and click Insert:
        </p>    
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="plcEditAuthor" runat="server">
        <h2>Edit Author</h2>
        <p>
            Please edit the existing author information and click Update:
        </p>    
    </asp:PlaceHolder>
    <fieldset class="login">
        <legend>Author Information</legend>
        <asp:ValidationSummary runat="server" />
        <table>
            <tr>
                <td>
                    <asp:Label runat="server" AssociatedControlID="txtName">Name:</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtName" runat="server" Width="250px" MaxLength="255" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtName" ErrorMessage="The Name field is required" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" AssociatedControlID="txtAge">Age:</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtAge" runat="server" Width="50px" MaxLength="3"/>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtAge" ErrorMessage="The Age field is required" />
                    <asp:RegularExpressionValidator runat="server" ControlToValidate="txtAge" ErrorMessage="The Age field is not valid" ValidationExpression="[0-9]+" />
                </td>
            </tr>
        </table>
        <br />
        <asp:Button ID="btnSubmit" runat="server" CausesValidation="true" />
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" />
    </fieldset>
</asp:Content>
