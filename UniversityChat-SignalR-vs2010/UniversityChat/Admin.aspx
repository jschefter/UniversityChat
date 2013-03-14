<%@ Page Title="University Chat Admin Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="UniversityChat.Admin" %>
<%@ Import Namespace="UniversityChat" %>

<asp:Content ID="ChatContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h1>Admin Page</h1>
        <br />
        
        <h3>Class Settings</h3>
        <asp:DropDownList id="roomName" runat="server"/>
        <asp:Button ID="Button2" runat="server" Text="Remove Channel" OnClick="RemoveChannel_Click"/> <br /> <br />

        <h3>User Settings</h3>
        <asp:DropDownList id="userName" runat="server"/>
        <asp:Button ID="Button1" runat="server" Text="Delete User" OnClick="RemoveUser_Click"/> <br /> <br />
        <asp:DropDownList ID="roleID" runat="server"/>
        <asp:Button runat="server" Text="Change Role ID for User" OnClick="ChangeRoleId_Click"/> <br /> <br />

        <asp:Label ID="resultLabel" runat="server"></asp:Label>
    </div>
</asp:Content>

