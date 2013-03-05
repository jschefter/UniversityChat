<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgetPassword.aspx.cs" Inherits="UniversityChat.ForgetPassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Forget Password</title>
</head>
<body>
    <h1>Password Recovery</h1>
    <p>Enter your username below to receive a link to reset your password.</p>
    <form id="form1" runat="server">
    <div>
        <table style="width:10%;">
            <tr>
                <td>
                    <asp:Label id="UsernameLabel" runat="server">Username:</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextUsername" runat="server" placeholder="Username"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    <div>
        <br/>
        <asp:Button ID="buttonSubmit" runat="server" Text="Email me!" 
            onclick="btnSubmit_Click" />
    </div>
    </form>
</body>
</html>
