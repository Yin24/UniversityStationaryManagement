<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="RequestVolumeRpt.aspx.cs" Inherits="SSISTeam2.Views.Reporting.Reports.RequestVolumeRpt" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p></p>
    <CR:crystalreportviewer id="CrystalReportViewer1" runat="server" autodatabind="true" ReportSourceID="CrystalReportSource1" />
    
    <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
        <Report FileName="C:\Users\veryt\Source\Repos\SSISTeam2\SSISTeam2\Views\Reporting\Reports\Volume_Report.rpt">
        </Report>
    </CR:CrystalReportSource>
</asp:Content>
