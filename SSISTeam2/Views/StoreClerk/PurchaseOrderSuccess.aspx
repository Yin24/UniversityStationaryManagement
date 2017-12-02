<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeBehind="PurchaseOrderSuccess.aspx.cs" Inherits="SSISTeam2.Views.StoreClerk.PurchaseOrderSuccess" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead"
    runat="server">
    <title>Purchase Order Successful</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">
    <div class="alert alert-success" role="alert">
        Purchase Orders have successfully been created. You will receive a notification through email about the Purchase Order.
    </div>
    <p>
        <a href="ViewPendingOrder.aspx"><span class="btn btn-success">Continue To Order History List</span></a>
    </p>
</asp:Content>
