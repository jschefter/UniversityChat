<%@ Page Title="University Chat - Forgot Password" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ForgetPassword.aspx.cs" Inherits="UniversityChat.ForgetPassword" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="ChatContent" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Password Recovery</h1>
    <p>Enter your username below to receive a link to reset your password.</p>
    <div>
        <table>
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
        <asp:Button ID="buttonSubmit" runat="server" Text="Email me!" onclick="btnSubmit_Click" />
    </div>
</asp:Content>
