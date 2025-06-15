<%@ Page Language="C#" AutoEventWireup="true" Async="true" CodeBehind="Notification.aspx.cs" Inherits="VirtualEventWEB.Notification" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Notifications</title>
    <style>
        body { font-family: Arial; margin: 20px; }
        .card {
            border: 1px solid #ccc;
            padding: 15px;
            border-radius: 6px;
            margin-bottom: 10px;
            background-color: #f9f9f9;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <h2>Upcoming Events (1 Day Left)</h2>
        <asp:Panel ID="pnlNotifications" runat="server"></asp:Panel>
    </form>
</body>
</html>
