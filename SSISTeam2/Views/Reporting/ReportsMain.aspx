<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ReportsMain.aspx.cs" Inherits="SSISTeam2.Views.Reporting.ReportsMain" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderHead"
    runat="server">
    <title>Generate Reports</title>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 
    <h2>Reports Management</h2>
   

    <div>
        <h3>Please indicate the type of report to generate</h3>
        <asp:table runat="server" CssClass="table table-responsive table-striped">
            <asp:TableHeaderRow>
                <asp:TableHeaderCell>Report Type</asp:TableHeaderCell>
                <asp:TableHeaderCell>Action</asp:TableHeaderCell>
            </asp:TableHeaderRow>
            
            <asp:TableRow>
            <asp:TableCell>Generate Request Volume Reports</asp:TableCell>
            <asp:TableCell><asp:Button ID="GRVReportBtn" Text="Select" width="142px" runat="server" OnClick="GRVReportBtn_Click" CssClass="btn btn-default"/></asp:TableCell>
            </asp:TableRow>
           
            <asp:TableRow>
            <asp:TableCell>Generate Trend Analysis Report </asp:TableCell>
            <asp:TableCell><asp:Button ID="GTAReportBtn" Text="Select" width="142px" runat="server"  OnClick="GTAReportBtn_Click"  CssClass="btn btn-default"/></asp:TableCell>
            
            </asp:TableRow>
        </asp:table>        
    
    </div>
</asp:Content>
