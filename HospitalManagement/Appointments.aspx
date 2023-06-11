<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Appointments.aspx.cs" Inherits="HospitalManagement.Appointments" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Appointments</h1>
    <asp:GridView ID="GridView1" runat="server" CssClass="gridview-style" AutoGenerateColumns="False" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDeleting="GridView1_RowDeleting" DataKeyNames="AppointmentId">
        <Columns>
            <asp:BoundField DataField="AppointmentId" HeaderText="Appointment ID" ReadOnly="True" />
            <asp:BoundField DataField="PatientId" HeaderText="Patient ID" ReadOnly="True" />
            <asp:BoundField DataField="DoctorId" HeaderText="Doctor ID" ReadOnly="True" />
            <asp:BoundField DataField="AppointmentDateTime" HeaderText="Appointment Date" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
            <asp:TemplateField HeaderText="Status">
                <ItemTemplate>
                    <%# GetAppointmentStatus(Eval("Status")) %>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="ddlStatus" runat="server">
                        <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                        <asp:ListItem Text="Confirmed" Value="Confirmed"></asp:ListItem>
                        <asp:ListItem Text="Cancelled" Value="Cancelled"></asp:ListItem>
                    </asp:DropDownList>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
        </Columns>
    </asp:GridView>

</asp:Content>
