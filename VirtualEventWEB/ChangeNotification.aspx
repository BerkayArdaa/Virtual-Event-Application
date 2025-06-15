<%@ Page Language="C#" Async="true" AutoEventWireup="true" CodeBehind="ChangeNotification.aspx.cs" Inherits="VirtualEventWEB.ChangeNotification" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Event Changes</title>
    <style>
        body {
            font-family: Arial;
            padding: 20px;
        }
        h2 {
            margin-bottom: 20px;
        }
        .grid-style {
            width: 100%;
            border-collapse: collapse;
            margin-top: 10px;
        }
        .grid-style th, .grid-style td {
            padding: 10px;
            border: 1px solid #ccc;
        }
        .grid-style th {
            background-color: #f2f2f2;
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Button ID="btnBack" runat="server" Text="← Geri Dön" CssClass="btn-back" OnClick="btnBack_Click" />
        <h2>Event Change History</h2>
        <asp:GridView ID="ChangeGrid" runat="server" AutoGenerateColumns="False" CssClass="grid-style">
            <Columns>
                <asp:BoundField DataField="EventId" HeaderText="Event ID" />
                <asp:BoundField DataField="Title" HeaderText="Title" />
                <asp:BoundField DataField="FieldChanged" HeaderText="Field Changed" />
                <asp:BoundField DataField="OldValue" HeaderText="Old Value" />
                <asp:BoundField DataField="NewValue" HeaderText="New Value" />
                <asp:BoundField DataField="ChangeTime" HeaderText="Change Time" DataFormatString="{0:g}" />
            </Columns>
        </asp:GridView>
    </form>
</body>
</html>
