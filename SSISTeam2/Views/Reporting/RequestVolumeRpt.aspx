<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="RequestVolumeRpt.aspx.cs" Inherits="SSISTeam2.Views.Reporting.Reports.RequestVolumeRpt" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderHead"
    runat="server">
    <title>Volume Report</title>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p></p>
    <asp:table runat="server" CssClass="table">
        <asp:TableRow>
            <asp:TableCell>
                <CR:crystalreportviewer id="VOLCrystal" runat="server" autodatabind="true" HasToggleGroupTreeButton="False" HasRefreshButton="True" ToolPanelView="None" HasCrystalLogo="False" HasToggleParameterPanelButton="False" />
            </asp:TableCell>
            <asp:TableCell>
                <asp:Button runat="server" CssClass="btn btn-default btn-sm" ID="genNewRep" Text="Generate New Report" OnClick="genNewRep_OnClick" />
                <asp:Button runat="server" CssClass="btn btn-danger btn-sm" ID="backBtn" Text="Back" OnClick="backBtn_OnClick" />

            </asp:TableCell>
            </asp:TableRow>
    </asp:table>
       
                    
                

    
</asp:Content>
