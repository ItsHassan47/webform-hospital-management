<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="HospitalManagement.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="chat-container">
        <div id="chatBox" runat="server">
            <!-- Messages will be dynamically added here -->
        </div>
        <div>
            <input type="text" id="messageInput" runat="server" />
            <asp:Button ID="btnSend" runat="server" Text="Send" OnClick="SendButton_Click" />
        </div>
    </div>
</asp:Content>
