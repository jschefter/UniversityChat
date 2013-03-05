<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="UniversityChat.Register" MasterPageFile="~/Site.Master"%>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="head" runat="server">
    <title>University Chat Registration Page</title>
</asp:Content>

<asp:Content ID="ChatContent" ContentPlaceHolderID="MainContent" runat="server">

    <h1>University Chat Registration Form</h1>

    <table>
        <tr>
            <td>
                <asp:Label id="UsernameLabel" runat="server">Username:</asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextUsername" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label id="PasswordLabel" runat="server">Password:</asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextPassword" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label id="ConfirmPasswordLabel" runat="server">Confirm Password:</asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextConfirmPassword" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label id="EmailLabel" runat="server">Email:</asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextEmail" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label id="FirstNameLabel" runat="server">First Name:</asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextFirstName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label id="LastNameLabel" runat="server">Last Name:</asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextLastName" runat="server"></asp:TextBox>
            </td>
        </tr>
    </table>
    <div>
        <asp:Button ID="buttonSubmit" runat="server" Text="Submit" 
            onclick="btnSubmit_Click" />
        <asp:Button ID="buttonCancel" runat="server" Text="Cancel" 
            onclick="btnCancel_Click" />
    </div>
</asp:Content>
