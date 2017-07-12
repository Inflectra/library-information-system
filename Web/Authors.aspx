<%@ Page Title="Library Information System | Author Management" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Authors.aspx.cs" Inherits="Inflectra.SampleApplications.LibraryInformationSystem.Web.Authors" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Author Management</h2>
    <p>
        The following authors exist in the system:
        <a href="AuthorEdit.aspx">
            (Create new author)
        </a>
    </p>
    <asp:GridView runat="server" ID="grdAuthors" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField HeaderText="ID" DataField="Id" />
            <asp:BoundField HeaderText="Name" DataField="Name" />
            <asp:BoundField HeaderText="Age" DataField="Age" />
            <asp:HyperLinkField HeaderText="Edit" DataNavigateUrlFields="Id" DataNavigateUrlFormatString="AuthorEdit.aspx?id={0}" Text="Edit" />
        </Columns>
    </asp:GridView>
</asp:Content>
