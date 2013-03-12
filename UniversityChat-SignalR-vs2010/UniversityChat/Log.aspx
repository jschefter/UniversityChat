<%@ Page Title="University Chat Logs" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Log.aspx.cs" Inherits="UniversityChat.Log" %>
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

    <script type="text/javascript">
        $(document).ready(function () {        
            $(".datepickerFrom").datepicker();
            $(".datepickerTo").datepicker();
        });
    </script>
</asp:Content>

<asp:Content ID="ChatContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h1>University Chat Logs</h1>
        
            <asp:Label ID="Label1" runat="server">Class name:</asp:Label>
            <asp:DropDownList id="roomName" runat="server"/> <br />
            
            <p>
                From: <asp:TextBox ID="datepickerFrom" runat="server" CssClass="datepickerFrom"></asp:Textbox>
            </p>
            <p>
                To: <asp:TextBox ID="datepickerTo" runat="server" CssClass="datepickerTo"></asp:TextBox>
            </p>
            <p>
                <asp:Button runat="server" OnClick="buttonSubmitForm_Click" Text="Submit"/>
            </p>
        
        <br />
        <asp:Label runat="server" ID="ClassName"></asp:Label>
    </div>
    
    <asp:Repeater ID="LogRepeater" runat="server">
        <HeaderTemplate>
            <table>
                <tr>
                <td>Chat Log:</td>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr class="myRow">
                <td>
                    <%#((ChatLog)Container.DataItem).LogTime%>
                </td>
                <td>
                    <%#((ChatLog)Container.DataItem).UserId%>
                </td>
                <td>
                    <%#((ChatLog)Container.DataItem).Text%>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
</asp:Content>
