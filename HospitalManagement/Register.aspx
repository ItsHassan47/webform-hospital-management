<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="HospitalManagement.Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <main class="reg-container">
        <h1>Patient Registration</h1>
        <asp:Label ID="lblName" runat="server" Text="Name"></asp:Label>
        <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label>
        <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="lblPassword" runat="server" Text="Password"></asp:Label>
        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
        <br />
        <asp:Label ID="lblConfirmPassword" runat="server" Text="Confirm Password"></asp:Label>
        <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password"></asp:TextBox>
        <br />
        <label for="ddlGender">Gender:</label>
        <select id="ddlGender" runat="server">
            <option value="Male">Male</option>
            <option value="Female">Female</option>
        </select>
        <br />
        
        <label for="txtMedicalHistory">Medical History:</label>
        <textarea id="txtMedicalHistory" name="txtMedicalHistory" rows="5" cols="50" runat="server"></textarea>
        <br />        
        <asp:Label ID="Address" runat="server" Text="Address"></asp:Label>
        <asp:TextBox ID="txtAddress" runat="server"></asp:TextBox>
        <br />
        <asp:Button ID="btnRegister" runat="server" Text="Register" OnClick="btnRegister_Click" />
        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
    </main>

</asp:Content>

