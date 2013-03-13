<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="UniversityChat.Admin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin Page</title>
</head>
<body>
    <h1>Admin Page</h1>
    <form id="form1" runat="server">
    <div>
        <asp:DropDownList id="roomName" runat="server"/>
        <asp:Button runat="server" Text="Remove Channel" OnClick="RemoveChannel_Click"/> <br/>
    </div>
    
    <div>
        <h3>Class Settings</h3>
        <asp:DropDownList id="userName" runat="server"/>
        <asp:Button runat="server" Text="Delete User" OnClick="RemoveUser_Click"/> <br/>
    </div>
    </form>
    
    <br />
    <asp:Label ID="resultLabel" runat="server"></asp:Label>
</body>
</html>
