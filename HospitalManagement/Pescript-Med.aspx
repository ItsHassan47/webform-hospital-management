<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Pescript-Med.aspx.cs" Inherits="HospitalManagement.Pescript_Med" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <label>Patient:</label>
        <asp:DropDownList ID="ddlPatients" runat="server">
            <asp:ListItem Text="Select Patient" Value=""></asp:ListItem>
        </asp:DropDownList>
    </div>
    <div>
        <label>Doctor:</label>
        <asp:DropDownList ID="ddlDoctors" runat="server">
            <asp:ListItem Text="Select Doctor" Value=""></asp:ListItem>
        </asp:DropDownList>
    </div>
    <div>
        <label>Medicine:</label>
        <asp:DropDownList ID="ddlMedicines" runat="server">
            <asp:ListItem Text="Select Medicine" Value=""></asp:ListItem>
        </asp:DropDownList>
    </div>
    <div>
        <label>Dosage:</label>
        <asp:TextBox ID="txtDosage" runat="server"></asp:TextBox>
    </div>
    <div>
        <label>Instructions:</label>
        <asp:TextBox ID="txtInstructions" runat="server"></asp:TextBox>
    </div>
    <div>
        <asp:Button ID="btnSavePrescription" runat="server" Text="Save Prescription" OnClick="btnSavePrescription_Click" />
    </div>
    <asp:Button ID="btnViewPrescription" runat="server" Text="View Prescription" OnClick="btnViewPrescription_Click" CommandArgument='<%# Eval("PrescriptionId") %>' />
    <br />
    <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
</asp:Content>
