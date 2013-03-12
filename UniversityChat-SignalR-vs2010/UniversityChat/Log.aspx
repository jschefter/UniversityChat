<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Log.aspx.cs" Inherits="UniversityChat.Log" %>
<%@ Import Namespace="UniversityChat" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>University Chat Logs</title>
    <link href="Content/south-street/jquery-ui-1.10.0.custom.min.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-1.9.1.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery-ui-1.10.0.custom.min.js" type="text/javascript"></script>

    <style type="text/css">
        th, td {
            padding: 2px 4px;
        }
    </style>

    <script language="javascript">
        $(document).ready(function () {
            $("tr.myRow:even").css("color", "red");
            $("tr.myRow:odd").css("color", "blue");
        });
        $(function () {
            $("#datepickerFrom").datepicker();
            $("#datepickerTo").datepicker();
        });
      </script>
</head>
<body>
    <div>
        <h1>University Chat Logs</h1>
        
        <form id="Form2" runat="server">
            <asp:Label ID="Label1" runat="server">Class name:</asp:Label>
            <asp:DropDownList id="roomName" runat="server"/> <br />
            
            <p>
                From: <input runat="server" type="text" id="datepickerFrom" /> <br />
            </p>
            <p>
                To: <input runat="server" type="text" id="datepickerTo" /> <br />
            </p>
            <p>
                <asp:Button runat="server" OnClick="buttonSubmitForm_Click" Text="Submit"/>
            </p>
        </form>
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
            <tr id="myRow">
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
</body>
</html>
