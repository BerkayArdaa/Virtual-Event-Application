<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditEventForm.aspx.cs" Inherits="VirtualEventWEB.EditEventForm" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Event Change Log</title>
    <style>
        body { font-family: Arial; margin: 20px; }
        h2 { margin-bottom: 20px; }
        .grid-style {
            width: 100%;
            border-collapse: collapse;
        }
        .grid-style th, .grid-style td {
            border: 1px solid #ccc;
            padding: 10px;
        }
        .grid-style th {
            background-color: #f2f2f2;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <h2>Change History for Event</h2>
        <asp:GridView ID="GridViewChanges" runat="server" CssClass="grid-style" AutoGenerateColumns="true" />
    </form>
</body>
</html>
