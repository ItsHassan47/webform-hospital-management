<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Find-Doctor.aspx.cs" Inherits="HospitalManagement.Find_Doctor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div>
        <h1>Find A Doctor</h1>
        <%--<div>
            <label for="ddlSpecialization">Specialization:</label>
            <asp:DropDownList ID="ddlSpecialization" runat="server" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlSpecialization_SelectedIndexChanged">
                <asp:ListItem Text="All" Value=""></asp:ListItem>
            </asp:DropDownList>
            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
        </div>--%>
        <asp:GridView ID="GridViewDoctors" runat="server" CssClass="gridview-style" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="DoctorId" HeaderText="ID" />
                <asp:BoundField DataField="Name" HeaderText="Name" />
                <asp:BoundField DataField="Email" HeaderText="Email" />
                <asp:BoundField DataField="Specialization" HeaderText="Specialization" />
                <asp:BoundField DataField="Qualification" HeaderText="Qualification" />
                <asp:TemplateField HeaderText="Actions">
                    <ItemTemplate>
                        <asp:Button ID="ButtonChat" runat="server" Text="Chat" OnClick="ButtonChat_Click"
                            CommandName="Chat" CommandArgument='<%# Container.DataItemIndex %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Appointments">
                    <ItemTemplate>
                        <asp:Button ID="ButtonAppointments" runat="server" Text="Appointments" OnClick="ButtonAppointments_Click"
                            CommandName="Appointments" CommandArgument='<%# Container.DataItemIndex %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>


    </div>
</asp:Content>
