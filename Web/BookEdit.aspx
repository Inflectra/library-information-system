<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BookEdit.aspx.cs" Inherits="Inflectra.SampleApplications.LibraryInformationSystem.Web.BookEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:PlaceHolder ID="plcNewBook" runat="server">
        <h2>Create New Book</h2>
        <p>
            Please enter the book information and click Insert:
        </p>    
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="plcEditBook" runat="server">
        <h2>Edit Book</h2>
        <p>
            Please edit the existing book information and click Update:
        </p>    
    </asp:PlaceHolder>
    <fieldset class="login">
        <legend>Book Information</legend>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
        <table>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" AssociatedControlID="txtName">Name:</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtName" runat="server" Width="350px" MaxLength="255" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtName" ErrorMessage="The Name field is required" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" AssociatedControlID="ddlAuthor">Author:</asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlAuthor" runat="server" Width="150px" DataTextField="Name" DataValueField="Id" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlAuthor" ErrorMessage="The Author field is required" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" AssociatedControlID="ddlGenre">Genre:</asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlGenre" runat="server" Width="150px" DataTextField="Name" DataValueField="Id" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlGenre" ErrorMessage="The Genre field is required" />
                </td>
            </tr>
        </table>
        <br />
        <asp:Button ID="btnSubmit" runat="server" CausesValidation="true" />
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" />
    </fieldset>
</asp:Content>
