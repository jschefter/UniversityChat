<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="UniversityChat.Register" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
        <asp:Label ID="Label1" runat="server" Text="Username"></asp:Label>
    </div>
        <div>
        <asp:TextBox ID="txtPassword1" runat="server" TextMode="Password" ></asp:TextBox>
        <asp:Label ID="lblPW1" runat="server" Text="Password"></asp:Label>
    </div>
    <div>
        <asp:TextBox ID="txtPassword2" runat="server" TextMode="Password" ></asp:TextBox>
        <asp:Label ID="lblPW2" runat="server" Text="Password (confirm)"></asp:Label>
    </div>
        <div>
        <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
        <asp:Label ID="Label6" runat="server" Text="Email Address"></asp:Label>
    </div>
    <div>
        <asp:TextBox ID="txtFName" runat="server"></asp:TextBox>
        <asp:Label ID="Label2" runat="server" Text="First Name"></asp:Label>
    </div>
    <div>
        <asp:TextBox ID="txtLName" runat="server"></asp:TextBox>
        <asp:Label ID="Label4" runat="server" Text="Last Name"></asp:Label>
    </div>
    <div>
        <asp:Button ID="btnSubmit" runat="server" Text="Submit" 
            onclick="btnSubmit_Click" />
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" 
            onclick="btnCancel_Click" />
    </div>
    </form>
</body>
</html>
