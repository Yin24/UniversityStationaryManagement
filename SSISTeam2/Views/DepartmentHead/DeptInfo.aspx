<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeptInfo.aspx.cs" Inherits="SSISTeam2.Views.DepartmentHead.DeptInfo"
    MasterPageFile="~/MasterPage.Master" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderHead"
    runat="server">
    <title>Department Listing</title>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div class="panel-heading" style="font-size: xx-large">
        <asp:Label ID="LabelDeptName" runat="server" Text="Label"></asp:Label> Department
    </div>
 
    <div >
    <asp:Table ID="Table1" runat="server" CssClass="table-responsive" Font-Size="Large">
           
           <asp:TableRow>
            <asp:TableCell>Contact Name</asp:TableCell>
            <asp:TableCell>: <asp:Label ID="LabelContactName" runat="server" Text="Label"></asp:Label></asp:TableCell>
        </asp:TableRow>

         <asp:TableRow>
            <asp:TableCell>Telephone No</asp:TableCell>
            <asp:TableCell>: <asp:Label ID="LabelPhNo" runat="server" Text="Label"></asp:Label></asp:TableCell>
        </asp:TableRow>

       <asp:TableRow>
            <asp:TableCell>Fax Number</asp:TableCell>
            <asp:TableCell>: <asp:Label ID="LabelFaxNo" runat="server" Text="Label"></asp:Label></asp:TableCell>
        </asp:TableRow>

       <%--<asp:TableRow>
            <asp:TableCell>Department Head Name</asp:TableCell>
            <asp:TableCell>: <asp:Label ID="LabelHeadName" runat="server" Text="Label"></asp:Label></asp:TableCell>
        </asp:TableRow>--%>
        
        <asp:TableRow>
            <asp:TableCell>Collection Point</asp:TableCell>
            <asp:TableCell>: <asp:TextBox ID="tbCollectP" runat="server"  ReadOnly="True" BorderStyle="Outset" Width="350"></asp:TextBox></asp:TableCell>
        </asp:TableRow>

       <asp:TableRow>
            <asp:TableCell>Representative Name</asp:TableCell>
            <asp:TableCell>: <asp:TextBox ID="tbRepName" runat="server"  ReadOnly="True" BorderStyle="Outset" Width="350"></asp:TextBox></asp:TableCell>
        </asp:TableRow>
    
    </asp:Table>

        <asp:Button ID="BtnChangeCpRn" runat="server" Text="Change" OnClick="BtnChangeCpRn_Click" CssClass="btn-primary" />
    </div>
    </asp:Content> 
