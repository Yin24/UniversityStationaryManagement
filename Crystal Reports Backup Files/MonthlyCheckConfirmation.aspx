<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="MonthlyCheckConfirmation.aspx.cs" Inherits="SSISTeam2.MonthlyCheckConfirmation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:gridview runat="server" ID="confirmationGV"></asp:gridview>
    <asp:Button ID="confirmBtn" runat="server" Text="Button" OnClick="Button1_Click" /><asp:Button ID="backBtn" runat="server" Text="Button" OnClick="backBtn_Click" />
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
</asp:Content>
