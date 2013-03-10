<%@ Page Title="University Chat Registration Page" Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="UniversityChat.Register" MasterPageFile="~/Site.Master"%>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .invalid {
            color: Red;
        }
    </style>
</asp:Content>

<asp:Content ID="ChatContent" ContentPlaceHolderID="MainContent" runat="server">

    
    <asp:Panel runat="server" ID="PanelRegisterUser" Visible="false">
        <h1>Create an Account</h1>
        <p>(All fields need to be 6+ letters in length)</p>
        <table>
            <tr>
                <td>
                    <asp:Label ID="UsernameLabel" runat="server">Username:</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextUsername" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="PasswordLabel" runat="server">Password:</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextPassword" runat="server" TextMode="Password"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="ConfirmPasswordLabel" runat="server">Confirm Password:</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextConfirmPassword" runat="server" TextMode="Password"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="EmailLabel" runat="server">Email:</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextEmail" runat="server" TextMode="Email"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="FirstNameLabel" runat="server">First Name:</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextFirstName" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LastNameLabel" runat="server">Last Name:</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextLastName" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
        <asp:Label runat="server" ID="InvalidMessage" Visible="false" CssClass="invalid" Text="Invalid Input! Please try again!" />
        <asp:Label runat="server" ID="UserExistsMessage" Visible="false" CssClass="invalid" Text="Username has already been registered. Try again with a different username." />
        <div>
            <button type="submit">Create Account</button>
            <button type="reset">Reset Form</button>
        </div>
    </asp:Panel>
    <asp:Panel runat="server" ID="PanelUserRegistered" Visible="false">
        <p><asp:Label runat="server" ID="Username"></asp:Label>, your account has been successfully created!</p>
        <p><asp:HyperLink runat="server" ID="LinkToIndex" NavigateUrl="~/Login.aspx" Text="Click Here"></asp:HyperLink> to login.</p>
    </asp:Panel>
    <asp:Panel runat="server" ID="PanelUserAuthenticated" Visible="false">
        <p>You have already created your account.</p>
        <p><asp:HyperLink runat="server" ID="HyperLink1" NavigateUrl="~/Index.aspx" Text="Click Here"></asp:HyperLink> to connect to the chat service.</p>
    </asp:Panel>
</asp:Content>
