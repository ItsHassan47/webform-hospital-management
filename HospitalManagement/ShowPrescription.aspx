<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ShowPrescription.aspx.cs" Inherits="HospitalManagement.ShowPrescription" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Prescription Information</h1>

    <div>
        <asp:Label ID="lblPatient" runat="server" Text="Patient:"></asp:Label>
        <asp:Label ID="lblPatientName" runat="server"></asp:Label>
    </div>

    <div>
        <asp:Label ID="lblDoctor" runat="server" Text="Doctor:"></asp:Label>
        <asp:Label ID="lblDoctorName" runat="server"></asp:Label>
    </div>

    <div>
        <asp:Label ID="lblMedicine" runat="server" Text="Medicine:"></asp:Label>
        <asp:Label ID="lblMedicineName" runat="server"></asp:Label>
    </div>

    <div>
        <asp:Label ID="lblDosage" runat="server" Text="Dosage:"></asp:Label>
        <asp:Label ID="lblDosageValue" runat="server"></asp:Label>
    </div>

    <div>
        <asp:Label ID="lblInstructions" runat="server" Text="Instructions:"></asp:Label>
        <asp:Label ID="lblInstructionsValue" runat="server"></asp:Label>
    </div>
</asp:Content>
