<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="CreateNewCategory.aspx.cs" Inherits="SSISTeam2.CreateNewCategory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead"
    runat="server">
    <title>Create New Category</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">
    <div class="panel panel-default">
        <!-- Default panel contents -->
        <div class="panel-heading"><h3>Add New Category</h3></div>
        <div class="panel-body">
          

         <%--  <asp:Label ID="Label3" runat="server" Text="Add New Category"></asp:Label>--%>
       <p>
            <asp:Label ID="Label4" runat="server" Text="New Category Name:"></asp:Label>
            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        </p>
        <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Submit" CssClass="btn btn-primary" />
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-primary" OnClick="btnCancel_Click" />

       <%-- <asp:RequiredFieldValidator ID="vldPrice" runat="server" ErrorMessage="Category Name Is Required" ControlToValidate="cat_name" ForeColor="Red"></asp:RequiredFieldValidator>--%>

       </div>
    </div>
         
       
   </asp:Content>


