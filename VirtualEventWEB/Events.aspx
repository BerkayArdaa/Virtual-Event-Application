<%@ Page Language="C#" Async="true" AutoEventWireup="true" CodeBehind="Events.aspx.cs" Inherits="VirtualEventWEB.Events" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Event List</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            margin: 20px;
        }
        .filter-section {
            margin-bottom: 20px;
            padding: 15px;
            background-color: #f8f9fa;
            border-radius: 5px;
            box-shadow: 0 2px 4px rgba(0,0,0,0.1);
        }
        .filter-container {
            display: flex;
            flex-wrap: wrap;
            gap: 15px;
            align-items: flex-end;
        }
        .filter-group {
            display: flex;
            flex-direction: column;
            min-width: 150px;
        }
        .date-filter-group {
            display: flex;
            flex-direction: column;
            min-width: 160px;
        }
        .filter-label {
            font-weight: bold;
            margin-bottom: 5px;
            font-size: 14px;
        }
        .filter-controls {
            display: flex;
            gap: 10px;
            margin-left: auto;
        }
        .date-checkbox {
            margin-top: 5px;
            font-size: 13px;
        }
        input[type="text"], 
        input[type="number"],
        input[type="date"] {
            width: 100%;
            padding: 8px;
            border: 1px solid #ddd;
            border-radius: 4px;
            box-sizing: border-box;
        }
        .btn {
            padding: 8px 12px;
            border-radius: 4px;
            cursor: pointer;
            border: none;
            font-size: 14px;
            min-width: 80px;
        }
        .btn-primary {
            background-color: #007bff;
            color: white;
        }
        .btn-secondary {
            background-color: #6c757d;
            color: white;
        }
        .btn-success {
            background-color: #28a745;
            color: white;
        }
        .grid-style {
            width: 100%;
            margin-top: 20px;
            border-collapse: collapse;
        }
        .grid-style th {
            background-color: #f2f2f2;
            padding: 10px;
            text-align: left;
            font-size: 14px;
        }
        .grid-style td {
            padding: 10px;
            border-bottom: 1px solid #ddd;
            font-size: 14px;
        }
        .message {
            margin-top: 15px;
            padding: 10px;
            border-radius: 4px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="filter-section">
            <h2 style="margin-top: 0;">Event Filters</h2>
            <div class="filter-container">
                <div class="filter-group">
                    <asp:Label CssClass="filter-label" runat="server" Text="Title"></asp:Label>
                    <asp:TextBox ID="txtTitle" runat="server"></asp:TextBox>
                </div>
                <div class="filter-group">
                    <asp:Label CssClass="filter-label" runat="server" Text="Category"></asp:Label>
                    <asp:TextBox ID="txtCategory" runat="server"></asp:TextBox>
                </div>
                <div class="date-filter-group">
                    <asp:Label CssClass="filter-label" runat="server" Text="Start Date"></asp:Label>
                    <asp:CheckBox ID="chkStartDate" runat="server" Text="Filter by date" CssClass="date-checkbox" 
                    AutoPostBack="true" OnCheckedChanged="chkStartDate_CheckedChanged" />
                    <asp:TextBox ID="txtStartDate" runat="server" TextMode="Date" Enabled="false"></asp:TextBox>
                </div>
                <div class="filter-group">
                    <asp:Label CssClass="filter-label" runat="server" Text="Duration (min)"></asp:Label>
                    <asp:TextBox ID="txtDuration" runat="server" TextMode="Number"></asp:TextBox>
                </div>
                <div class="filter-group">
                    <asp:Label CssClass="filter-label" runat="server" Text="Available Slots"></asp:Label>
                    <asp:TextBox ID="txtSlots" runat="server" TextMode="Number"></asp:TextBox>
                </div>
                <div class="filter-controls">
                    <asp:Button ID="btnFilter" runat="server" Text="Apply" OnClick="btnFilter_Click" CssClass="btn btn-primary" />
                    <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" CssClass="btn btn-secondary" />
                    <asp:Button ID="btnMyEvents" runat="server" Text="My Events" OnClick="btnMyEvents_Click" CssClass="btn btn-success" />
                     <asp:Button ID="btnNotifications" runat="server" Text="Notifications" OnClick="btnNotifications_Click" CssClass="btn btn-warning" />
                    <asp:Button ID="btnChangeCounts" runat="server" Text="Change Counts" OnClick="btnChangeCounts_Click" CssClass="btn btn-warning" />
                </div>
            </div>
        </div>

        <asp:GridView ID="EventsGrid" runat="server" AutoGenerateColumns="False" DataKeyNames="EventId" 
            OnRowCommand="EventsGrid_RowCommand" CssClass="grid-style">
            <Columns>
                <asp:BoundField DataField="EventId" HeaderText="ID" />
                <asp:BoundField DataField="Title" HeaderText="Title" />
                <asp:BoundField DataField="Category" HeaderText="Category" />
                <asp:BoundField DataField="StartTime" HeaderText="Start Time" DataFormatString="{0:g}" />
                <asp:BoundField DataField="Duration" HeaderText="Duration (min)" />
                <asp:BoundField DataField="ParticipantLimit" HeaderText="Available Slots" />
                <asp:ButtonField Text="Register" CommandName="Register" ButtonType="Button" ControlStyle-CssClass="btn btn-success" />
                 <asp:ButtonField Text="Info" CommandName="Info" ButtonType="Button" ControlStyle-CssClass="btn btn-primary" />
            </Columns>
            <EmptyDataTemplate>
                <div style="padding: 20px; text-align: center;">
                    No events found matching your criteria.
                </div>
            </EmptyDataTemplate>
        </asp:GridView>

        <asp:Label ID="lblMessage" runat="server" CssClass="message"></asp:Label>
    </form>

    <script type="text/javascript">
        function toggleDatePicker(chkBoxId, datePickerId) {
            var chkBox = document.getElementById(chkBoxId);
            var datePicker = document.getElementById(datePickerId);
            datePicker.disabled = !chkBox.checked;
            if (!chkBox.checked) {
                datePicker.value = '';
            }
        }
        window.onload = function () {
            toggleDatePicker('<%= chkStartDate.ClientID %>', '<%= txtStartDate.ClientID %>');
        };
    </script>
</body>
</html>