<%@ Page Language="C#" AutoEventWireup="true" Async="true" CodeBehind="EventInfo.aspx.cs" Inherits="VirtualEventWEB.EventInfo" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Event Description</title>
    <style>
        .container {
            margin: 50px auto;
            max-width: 600px;
            font-family: Arial;
        }
        .back-btn {
            margin-top: 20px;
            background-color: #007bff;
            color: white;
            padding: 8px 15px;
            border: none;
            border-radius: 4px;
            cursor: pointer;
        }
        .description-box {
            background-color: #f2f2f2;
            padding: 20px;
            border-radius: 8px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>Event Description</h2>
            <asp:Label ID="lblDescription" runat="server" CssClass="description-box"></asp:Label><br />
            <asp:Button ID="btnBack" runat="server" Text="Back to Events" CssClass="back-btn" OnClick="btnBack_Click" />
        </div>
    </form>
</body>
</html>
