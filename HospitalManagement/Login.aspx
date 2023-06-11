<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="HospitalManagement.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main class="reg-container">
        <h1>Patient and Doctor Login</h1>
        <asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label>
        <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="lblPassword" runat="server" Text="Password"></asp:Label>
        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
        <br />
        <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" />
        <br />
        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>

    </main>
</asp:Content>
