﻿<%@ Page Title="University Chat" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="UniversityChat.Index" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="head" runat="server">
    <script src="Scripts/jquery.signalR-1.0.1.min.js" type="text/javascript"></script>
    <script src="/signalr/hubs" type="text/javascript"></script>   <!-- autogenerated by SignalR -->

    <script src="Scripts/chat-data-source.js" type="text/javascript"></script>
    <script src="Scripts/web-client-chat-ui.js" type="text/javascript"></script>
    <script src="Scripts/web-client.js" type="text/javascript"></script>

    <style type="text/css">
        #chat-tabs .ui-tabs-nav li a { padding: 0.125em 0em 0.125em 0.5em; }
        #chat-tabs .tabs li .ui-icon-close { float: left; margin: 0.4em 0.2em 0 0; cursor: pointer; }
    </style>
</asp:Content>

<asp:Content ID="ChatContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label runat="server" ID="UserName" CssClass="username"></asp:Label>
    <div class="chat-container chat-disabled">
        
        <div class="active-chat">
                <h4>Click on a public channel to the left to join a chat room</h4>

                <div id="chat-tabs">
                    <ul class="tabs"></ul>
                    <div class="content"></div>
                </div>
                
                <div id="chat-input">
                    <div><a id="show-upload">Upload a File</a><a id="hide-upload">Close Upload</a></div>
                    <div id="upload-ui">
                        <asp:FileUpload ID="uploadFile" runat="server"/>
                        <asp:LinkButton ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click"/> <br />
                        <asp:Label runat="server" ID="fileUploadStatus"></asp:Label>
                    </div>
                    <input type="text" id="message" disabled="disabled" placeholder="Type your message here and press Enter" />
                    <input type="submit" id="sendmessage" value="Send" />
                    
                </div>
                
            </div>
        <div class="channels">
            <h4>Public Channels</h4>
            <ul class="channel-list">
            </ul>
            <button class="add-channel" disabled="disabled">Create a Channel</button>
        </div>
        <div class="users">
            <h4>Channel Users</h4>
            <div class="user-lists"></div>
        </div>
    </div>
</asp:Content>
