<%@ Page Title="University Chat Files" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Files.aspx.cs" Inherits="UniversityChat.Files" %>
<%@ Import Namespace="UniversityChat" %>

<asp:Content ID="ChatContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h1>Files Uploaded</h1>
        <br />
        
        <asp:Repeater ID="LogRepeater" runat="server">
        <HeaderTemplate>
            <table>
                <tr>
                <td>Files in database:</td>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr class="myRow">
                <td>
                    <%#((UploadedFile)Container.DataItem).LogTime%>
                </td>
                <td>
                    <%#((UploadedFile)Container.DataItem).FileName%>
                </td>
                <td>
                    <%#((UploadedFile)Container.DataItem).FileType%>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
    </div>
</asp:Content>
