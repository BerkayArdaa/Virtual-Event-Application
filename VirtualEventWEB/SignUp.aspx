<%@ Page Language="C#" Async="true" AutoEventWireup="true" CodeBehind="SignUp.aspx.cs" Inherits="VirtualEventWEB.SignUp" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sign Up</title>
    <style>
        body {
            font-family: Arial;
            background-color: #f4f4f4;
            padding: 50px;
        }

        .signup-container {
            width: 300px;
            margin: auto;
            background: #fff;
            padding: 25px;
            border-radius: 8px;
            box-shadow: 0 0 10px rgba(0,0,0,0.2);
        }

        .signup-container h2 {
            text-align: center;
        }

        .signup-container input[type="text"],
        .signup-container input[type="password"] {
            width: 100%;
            padding: 8px;
            margin: 10px 0;
        }

        .signup-container input[type="submit"] {
            width: 100%;
            padding: 8px;
            background-color: #28a745;
            color: white;
            border: none;
            cursor: pointer;
        }

        .signup-container input[type="submit"]:hover {
            background-color: #218838;
        }

        .message {
            color: red;
            text-align: center;
            margin-top: 10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="signup-container">
            <h2>Sign Up</h2>
            <asp:TextBox ID="txtUsername" runat="server" placeholder="Username" />
            <asp:TextBox ID="txtEmail" runat="server" placeholder="Email" />
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" placeholder="Password" />
            <asp:Button ID="btnSignUp" runat="server" Text="Create Account" OnClick="btnSignUp_Click" />
            <asp:Label ID="lblMessage" runat="server" CssClass="message" />
        </div>
    </form>
</body>
</html>
