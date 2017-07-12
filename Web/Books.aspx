<%@ Page Title="Library Information System | Book Management" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Books.aspx.cs" Inherits="Inflectra.SampleApplications.LibraryInformationSystem.Web.Books" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Book Management</h2>
    <p>
        The following books exist in the system:
        <asp:PlaceHolder ID="plcCreateBook" runat="server">
            <a href="BookEdit.aspx">
                (Create new book)
            </a>
        </asp:PlaceHolder>
    </p>
    <asp:GridView runat="server" ID="grdBooks" AutoGenerateColumns="false" DataKeyNames="Id">
        <Columns>
            <asp:BoundField HeaderText="ID" DataField="Id" />
            <asp:BoundField HeaderText="Name" DataField="Name" />
            <asp:BoundField HeaderText="Author" DataField="AuthorName" />
            <asp:BoundField HeaderText="Genre" DataField="GenreName" />
            <asp:HyperLinkField HeaderText="Edit" DataNavigateUrlFields="Id" DataNavigateUrlFormatString="BookEdit.aspx?id={0}" Text="Edit" />
            <asp:BoundField HeaderText="Date Added" DataField="DateAdded" />
            <asp:BoundField HeaderText="Out of Print" DataField="IsOutOfPrint" />
        </Columns>
    </asp:GridView>
</asp:Content>
