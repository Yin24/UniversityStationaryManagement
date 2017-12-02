<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Reports Main.aspx.cs" Inherits="SSISTeam2.Views.Reporting.Reports_Main" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reports Management</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>Reports Management</h1>
        
    </div>
    <div>
        <h3>Please indicate the type of report to generate</h3>
        <table>
            <tr>
                <th>Report Type</th>
                <th></th>
            </tr>
            
            <tr>
            <td>Generate Request Volume Reports</td>
            <td><asp:Button ID="GRVReportBtn" Text="Select" width="142px" runat="server" OnClick="GRVReportBtn_Click"/></td>
            </tr>
           
            <tr>
            <td>Generate Trend Analysis Report </td>
            <td><asp:Button ID="GTAReportBtn" Text="Select" width="142px" runat="server" OnClick="GTAReportBtn_Click" /></td>
            
            </tr>
        </table>        
    
    </div>
    </form>
</body>
</html>
