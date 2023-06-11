<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Chat.aspx.cs" Inherits="HospitalManagement.Chat1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <div class="chat-container">
            <h2>Chat</h2>
            <asp:GridView ID="gvChat" runat="server" AutoGenerateColumns="false" CssClass="chat-grid">
                <Columns>
                    <asp:BoundField DataField="Email" HeaderText="Email" />
                    <asp:BoundField DataField="SenderId" HeaderText="Sender" />
                    <asp:BoundField DataField="ReceiverId" HeaderText="Receiver" />
                    <asp:BoundField DataField="MessageText" HeaderText="Message" />
                    <asp:BoundField DataField="Timestamp" HeaderText="Timestamp" />
                </Columns>
            </asp:GridView>
            <br />
            <div>
                <asp:TextBox ID="txtPatientMessage" runat="server" placeholder="Enter your message" Width="300"></asp:TextBox>
                <asp:Button ID="btnSend" runat="server" Text="Send" OnClick="btnSend_Click" />
            </div>
        </div>

</asp:Content>
