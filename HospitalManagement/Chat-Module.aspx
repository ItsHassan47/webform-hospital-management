<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Chat-Module.aspx.cs" Inherits="HospitalManagement.Chat_Module" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Chat</h1>

    <div id="chatWindow"></div>

    <input type="text" id="txtMessage" placeholder="Type your message..." />
    <button id="btnSend">Send</button>
    <script>
        $(document).ready(function () {
            // Function to load the chat history
            function loadChatHistory() {
                $.ajax({
                    type: "POST",
                    url: "Chat.aspx/LoadChatHistory",
                    data: '{}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        // Append the chat history to the chat window
                        $("#chatWindow").html(response.d);
                    }
                });
            }

            // Load chat history on page load
            loadChatHistory();

            // Function to send a message
            function sendMessage() {
                var message = $("#txtMessage").val();

                if (message.trim() !== "") {
                    $.ajax({
                        type: "POST",
                        url: "Chat.aspx/SendMessage",
                        data: JSON.stringify({ message: message }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            // Clear the message input field
                            $("#txtMessage").val("");

                            // Load updated chat history
                            loadChatHistory();
                        }
                    });
                }
            }

            // Event handler for send button click
            $("#btnSend").click(function () {
                sendMessage();
            });

            // Event handler for Enter key press in the message input field
            $("#txtMessage").keypress(function (e) {
                if (e.which === 13) {
                    sendMessage();
                    return false;
                }
            });
        });
    </script>

</asp:Content>
