<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Make-Appointment.aspx.cs" Inherits="HospitalManagement.Make_Appointment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container2">
        <h1 class="hAppoint">Make an Appointment</h1>
        <hr />        
        <label>Patient ID:</label>
        <asp:TextBox ID="txtPatientId" runat="server"></asp:TextBox>
        <br />
        <label>Appointment Date and Time:</label>
        <asp:TextBox ID="txtAppointmentDateTime" runat="server"></asp:TextBox>
        <br />
        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
    </div>
</asp:Content>
