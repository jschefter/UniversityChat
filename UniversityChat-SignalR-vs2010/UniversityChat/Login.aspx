<%@ Page Title="Log in to University Chat" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="UniversityChat.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .invalid-message {
            color: Red;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Panel runat="server" ID="AnonPanel" Visible="false">
        <h1>University Chat Login</h1>
        <p>Log in using your username and password below, or <asp:Hyperlink runat="server" ID="LinkToRegister" NavigateUrl="~/Register.aspx" Text="Register for an account"></asp:Hyperlink>.</p>
        <div class="login-box">
            <p><span class="label">User Name:</span><asp:TextBox runat="server" ID="UserName"></asp:TextBox></p>
            <p><span class="label">Password:</span><asp:TextBox runat="server" ID="Password" TextMode="Password"></asp:TextBox></p>
            <p><asp:CheckBox runat="server" ID="RememberMe" Checked="false" Text=" Remember me" /></p>
            <p><asp:Label runat="server" ID="InvalidMessage" Visible="false" CssClass="invalid-message" Text="Invalid user credentials! Please try again!"></asp:Label></p>
            <p><button type="submit">Login</button></p>
        </div>
    </asp:Panel>
    <asp:Panel runat="server" ID="AuthPanel" Visible="false">
        <p>You are already logged in. <asp:HyperLink runat="server" ID="LinkToIndex" NavigateUrl="~/Index.aspx" Text="Return to main page"></asp:HyperLink>.</p>
    </asp:Panel>
</asp:Content>
