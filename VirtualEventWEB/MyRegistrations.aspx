<%@ Page Language="C#" AutoEventWireup="true" Async="true" CodeBehind="MyRegistrations.aspx.cs" Inherits="VirtualEventWEB.MyRegistrations" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>My Registered Events</title>
    <style>
        .event-grid {
            width: 100%;
            margin-top: 20px;
            border-collapse: collapse;
        }
        .event-grid th {
            background-color: #f2f2f2;
            padding: 8px;
            text-align: left;
        }
        .event-grid td {
            padding: 8px;
            border-bottom: 1px solid #ddd;
        }
        .event-grid tr:hover {
            background-color: #f5f5f5;
        }
        .back-button {
            margin-top: 20px;
        }
        /* New Unregister button style */
        .btn-unregister {
            background-color: #ff4444;
            color: white;
            border: none;
            padding: 5px 10px;
            border-radius: 3px;
            cursor: pointer;
            font-size: 12px;
        }
        .btn-unregister:hover {
            background-color: #cc0000;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="padding: 20px;">
            <h2>My Registered Events</h2>
            
            <asp:GridView ID="RegisteredEventsGrid" runat="server" AutoGenerateColumns="false" CssClass="event-grid"
                OnRowCommand="RegisteredEventsGrid_RowCommand" DataKeyNames="EventId">
                <Columns>
                    <asp:TemplateField HeaderText="Action" ItemStyle-Width="100px">
                        <ItemTemplate>
                            <asp:Button ID="btnUnregister" runat="server" Text="Unregister" 
                                CommandName="Unregister" CommandArgument='<%# Container.DataItemIndex %>'
                                CssClass="btn-unregister" 
                                OnClientClick="return confirm('Are you sure you want to unregister from this event?');" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Title" HeaderText="Event Title" />
                    <asp:BoundField DataField="Category" HeaderText="Category" />
                    <asp:BoundField DataField="StartTime" HeaderText="Start Time" DataFormatString="{0:g}" />
                </Columns>
                <EmptyDataTemplate>
                    <div style="padding: 20px; text-align: center;">
                        You haven't registered for any events yet.
                    </div>
                </EmptyDataTemplate>
            </asp:GridView>
            
            <asp:Button ID="btnBack" runat="server" Text="Back to Events" OnClick="btnBack_Click" CssClass="back-button" />
            <br />
            <asp:Label ID="lblMessage" runat="server" ForeColor="Red" />
        </div>
    </form>
</body>
</html>