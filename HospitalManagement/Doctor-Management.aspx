<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Doctor-Management.aspx.cs" Inherits="HospitalManagement.Doctor_Management" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:GridView ID="GridView1" runat="server" CssClass="gridview-style" AutoGenerateColumns="False" DataSourceID="SqlDataSource1">
        <Columns>
            <asp:BoundField DataField="DoctorId" HeaderText="ID" ReadOnly="True" />
            <asp:BoundField DataField="Name" HeaderText="Name" />
            <asp:BoundField DataField="Email" HeaderText="Email" />
            <asp:BoundField DataField="Password" HeaderText="Password" />
            <asp:BoundField DataField="Specialization" HeaderText="Specialization" />
            <asp:BoundField DataField="Qualification" HeaderText="Qualification" />
            <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
        </Columns>
    </asp:GridView>


    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="Data Source=C0FFEE-PC;Initial Catalog=HospitalMangement;Integrated Security=True;"
        SelectCommand="SELECT * FROM Doctors WHERE DoctorId = @DoctorID"
        UpdateCommand="UPDATE Doctors SET Name = @Name, Email = @Email, Password = @Password, Specialization = @Specialization, Qualification = @Qualification WHERE DoctorId = @DoctorID"
        DeleteCommand="DELETE FROM Doctors WHERE DoctorId = @DoctorID">

        <SelectParameters>
            <asp:SessionParameter Name="DoctorID" SessionField="DoctorID" Type="Int32" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="Name" Type="String" />
            <asp:Parameter Name="Email" Type="String" />
            <asp:Parameter Name="Password" Type="String" />
            <asp:Parameter Name="Specialization" Type="String" />
            <asp:Parameter Name="Qualification" Type="String" />
            <asp:SessionParameter Name="DoctorID" SessionField="DoctorID" Type="Int32" />
        </UpdateParameters>
        <DeleteParameters>
            <asp:Parameter Name="DoctorID" Type="Int32" />
        </DeleteParameters>
    </asp:SqlDataSource>


</asp:Content>
