<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Log.aspx.cs" Inherits="UniversityChat.Log" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>University Chat Logs</title>
    <style type="text/css">
        th, td {
            padding: 2px 4px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>University Chat Logs</h1>
        
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:ucdatabaseConnectionString2 %>" 
            SelectCommand="SELECT * FROM [ucdatabase].[UniversityChat].[Log] ORDER BY [LogTime]">
        </asp:SqlDataSource>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            DataKeyNames="LogNumber" DataSourceID="SqlDataSource1">
            <Columns>
                <asp:BoundField DataField="LogTime" HeaderText="Time" 
                    SortExpression="LogTime" />
                <asp:BoundField DataField="ConnectionId" HeaderText="ConnectionId" 
                    SortExpression="ConnectionId" />
                <asp:BoundField DataField="UserId" HeaderText="UserId" 
                    SortExpression="UserId" />
                <asp:BoundField DataField="Event" HeaderText="Event" SortExpression="Event" />
                <asp:BoundField DataField="LogNumber" HeaderText="LogNumber" 
                    InsertVisible="False" ReadOnly="True" SortExpression="LogNumber" />
            </Columns>
        </asp:GridView>
        <br />
        
    </div>
    </form>
</body>
</html>
