<%@ Page Title="University Chat Files" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Files.aspx.cs" Inherits="UniversityChat.Files" %>
<%@ Import Namespace="UniversityChat" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        th, td {
            padding: 2px 4px;
            border: none;
        }
        
        table {
            border-spacing: 0px;
        }
        
        tr.myRow {
            border: none;
        }
        
        tr.myRow:nth-child(even) {
            background-color: LightGray;
        }
    </style>
</asp:Content>

<asp:Content ID="ChatContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h1>Files</h1>
        <br />
        
        <asp:Label runat="server" ID="resultLabel"></asp:Label>
        
        <asp:Repeater ID="LogRepeater" runat="server">
        <HeaderTemplate>
            <table>
                <td>
                    
                </td>
                <td>
                    <h3>Date</h3>
                </td>
                <td>
                    <h3>File Name</h3>
                </td>
                <td>
                    <h3>File type</h3>
                </td>
        </HeaderTemplate>
        <ItemTemplate>
            <tr class="myRow">
                <td>
                    <asp:LinkButton runat="server" ID="fileID" Text="Download" OnCommand="downloadLink_Click" CommandArgument="<%#((UploadedFile)Container.DataItem).ID%>"></asp:LinkButton>
                </td>
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
