<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeBehind="DeptDetails.aspx.cs" Inherits="SSISTeam2.Views.StoreClerk.DeptDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead"
    runat="server">
<%--    <title>Your Request Details</title>--%>
</asp:Content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1"
    runat="server">
    <div class="table-responsive ">
        <div class="panel-heading"><h3>View Department Details</h3></div>
    </div>
    <br />
    Department Name: <asp:Label ID="lbldeptname" runat="server" Text='<%# Eval("name") %>' ></asp:Label>
    <br />
    <br />
    Department Head: <asp:Label ID="lblhead" runat="server" Text='<%# Eval("head_user") %>'></asp:Label>
    <br />
    <br />
    Representative: <asp:Label ID="lblrep" runat="server" Text='<%# Eval("rep_user") %>'></asp:Label>
    <br />
    <br />
    Collection Point: <asp:Label ID="lblcollpoint" runat="server" Text='<%# Eval("location") %>'></asp:Label>
    <br />
     <br />
    Contact Name: <asp:Label ID="lblconname" runat="server" Text='<%# Eval("contact_user") %>'></asp:Label>
   <br />
     <br />
    Contact Number:  <asp:Label ID="lblconnum" runat="server" Text='<%# Eval("contact_num") %>'></asp:Label>
   <br />
     <br />
    Fax Number: <asp:Label ID="lblfax" runat="server" Text='<%# Eval("fax_num") %>'></asp:Label>
   
    <br />
    <br />
     <asp:LinkButton ID="btnBack" runat="server" Text="Back"  CssClass="btn btn-primary" OnClick="btnBack_Click"/>
    <br />

</asp:content>