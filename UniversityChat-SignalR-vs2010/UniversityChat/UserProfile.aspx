<%@ Page Title="University Chat UserProfile Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserProfile.aspx.cs" Inherits="UniversityChat.UserProfile" %>
<%@ Import Namespace="UniversityChat" %>

<asp:Content ID="ChatContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h1>Profile Page</h1>
        <h3>Only fill in the fields which you want to change.</h3>
        <br />
        
        <form>
            <asp:Label ID="passwordLabel" runat="server" Text="New Password: "></asp:Label>
            <asp:TextBox ID="passwordBox" runat="server" TextMode="Password"></asp:TextBox> <br />
            
            <asp:Label ID="passwordConfirmLabel" runat="server" Text="Confirm Password: "></asp:Label>
            <asp:TextBox ID="passwordConfirmBox" runat="server" TextMode="Password"></asp:TextBox> <br /> <br />

            <asp:Label ID="emailLabel" runat="server" Text="Change email: "></asp:Label>
            <asp:TextBox ID="emailBox" runat="server"></asp:TextBox> <br /> <br />
            
            <!--
            <asp:Label ID="nickNameLabel" runat="server" Text="Change nick name: "></asp:Label>
            <asp:TextBox ID="nickNameBox" runat="server"></asp:TextBox> <br /> <br />
            -->

            <asp:Label ID="oldPasswordLabel" runat="server" Text="Enter your old password to make the changes: "></asp:Label>
            <asp:TextBox ID="oldPasswordBox" runat="server" TextMode="Password"></asp:TextBox> <br />
            <asp:Button ID="submitButton" runat="server" Text="Submit" OnClick="btnSubmit_Click"/> <br /> <br />
        </form>
        
        <asp:Label ID="resultLabel" runat="server"></asp:Label>
    </div>
</asp:Content>

