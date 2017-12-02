<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangeCollectionPoint.aspx.cs" Inherits="SSISTeam2.Views.Employee.ChangeCollectionPoint"
         MasterPageFile="~/MasterPage.Master" Debug="true" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderHead"
    runat="server">
    <title>Change Collection Point and/or Representative</title>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-default">
     <div class="panel-heading"><h2>Change Collection Point</h2></div>
        
        <div class="panel-body">

            <asp:Table ID="Table" runat="server" class="active" style="table-layout: auto; font-size: large;" CssClass="table table-responsive table-striped">
         
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

            <asp:TableRow>
                <asp:TableCell>Current Collection Point</asp:TableCell>
               <asp:TableCell>: <asp:Label ID="lbCollectP" runat="server" ReadOnly="True" Width="400"></asp:Label></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>New Collection Point</asp:TableCell>
                <asp:TableCell> <asp:DropDownList ID="ddlCollectPoint" runat="server" AutoPostBack="True" AppendDataBoundItems="true" CssClass="form-control" Width="370px">
                    <asp:ListItem Text="Select---" Value="0"></asp:ListItem>
                      </asp:DropDownList></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>Current Representative Name</asp:TableCell>
                 <asp:TableCell>: <asp:Label ID="lbRepName" runat="server" ReadOnly="True" Width="350"></asp:Label></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                       <asp:TableCell HorizontalAlign="Right">        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-warning" Width="150px"/></asp:TableCell>
                <asp:TableCell>        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="btn btn-default" Width="150px"/></asp:TableCell>
         
            </asp:TableRow>
       </asp:Table>
            <asp:Label ID="lbDDLError" runat="server" ForeColor="#FF3300"></asp:Label>
                
        </div>
        </div>
               
 </asp:Content>

