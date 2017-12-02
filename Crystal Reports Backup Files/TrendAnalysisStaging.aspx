<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="TrendAnalysisStaging.aspx.cs" Inherits="SSISTeam2.Views.Reporting.Reports.TrendAnalysisStaging" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p></p>
    <h2>Trend Analysis Report Generator</h2>
    <%--Department Selector--%>
    <h4>Please Select Department</h4>
    <asp:SqlDataSource 
        ID="sdsDepartment" 
        runat="server" 
        DataSourceMode="DataReader" 
        ConnectionString="data source=(local);initial catalog=SSIS;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework" 
        SelectCommand="SELECT name FROM SSIS.dbo.Department" >
    </asp:SqlDataSource>
    
     <asp:DropDownList 
         DataSourceID="sdsDepartment" 
         runat="server" 
         DataTextField="name" 
         DataValueField="name" 
         Width="193px" 
         CssClass="form-control">
     </asp:DropDownList>
    <%-- Category Selector --%>
     <br />
    <h4>Select Category</h4>
    <asp:Table runat="server" CssClass="table" Width="450px">
        <asp:TableRow>
            <asp:TableCell>
                <asp:ListBox ID="CatList" runat="server" CssClass="form-control"></asp:ListBox>
                </asp:TableCell>

            <%-- Selector Controls --%>
                <asp:TableCell>
                    <asp:Button runat="server" CssClass="btn btn-default btn-sm" ID="AddOneCat" OnClick="AddOneCat_Click" Text=">" Width="40px"/>
                    <br />
                    <asp:Button runat="server" CssClass="btn btn-default btn-sm" ID="AddAllCat" OnClick="AddAllCat_Click" Text=">>" Width="40px"/>
                    <br />
                    <asp:Button runat="server" CssClass="btn btn-default btn-sm" ID="RemoveOneCat" OnClick="RemoveOneCat_Click" Text="<" Width="40px"/>
                    <br />
                    <asp:Button runat="server" CssClass="btn btn-default btn-sm" ID="RemoveAllCat" Text="<<" OnClick="RemoveAllCat_Click" Width="40px"/>
                </asp:TableCell>
            
             <%--Selector--%>
            <asp:TableCell>    
                <asp:ListBox
                    ID="SelectorList"
                    runat="server" 
                    CssClass="form-control">
                    </asp:ListBox>
     </asp:TableCell>
        </asp:TableRow>
            </asp:Table>

    <br />
    <h4>Select Months and Years</h4>
    <asp:Table runat="server" CssClass="table" Width="450px" ID="MonthYearTable">
        <asp:TableHeaderRow>
            <asp:TableHeaderCell>Month</asp:TableHeaderCell>
            <asp:TableHeaderCell>Year</asp:TableHeaderCell>
        </asp:TableHeaderRow>
        <asp:TableRow>
            <asp:TableCell>
                <asp:DropDownList runat="server" CssClass="dropdown" ID="Month1"></asp:DropDownList>
            </asp:TableCell>
            <asp:TableCell>
                <asp:DropDownList runat="server" CssClass="dropdown" ID="Year1"></asp:DropDownList>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <asp:DropDownList runat="server" CssClass="dropdown" ID="Month2"></asp:DropDownList>
            </asp:TableCell>
            <asp:TableCell>
                <asp:DropDownList runat="server" CssClass="dropdown" ID="Year2"></asp:DropDownList>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <asp:DropDownList runat="server" CssClass="dropdown" ID="Month3"></asp:DropDownList>
            </asp:TableCell>
            <asp:TableCell>
                <asp:DropDownList runat="server" CssClass="dropdown" ID="Year3"></asp:DropDownList>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableFooterRow>
            <asp:TableCell><asp:Button CausesValidation="true" runat="server" ID="SetMonthYear" Text="Generate Report" OnClick="SetMonthYear_OnClick"/></asp:TableCell>
        </asp:TableFooterRow>
    </asp:Table>
    <asp:CustomValidator ID="cValidator" ErrorMessage="Please select at least ONE category" OnServerValidate="cValidator_ServerValidate" Display="Dynamic"  ForeColor="Red" SetFocusOnError="True" ControlToValidate="SelectorList" ValidateEmptyText="true" runat="server" ClientValidationFunction="ListBoxValid"></asp:CustomValidator>
   <%-- <asp:RequiredFieldValidator ValidationGroup="lstCheck" runat="server" id="reqSelectorList" controltovalidate="SelectorList" ForeColor="Red" SetFocusOnError="true" CssClass="has-error" errormessage="Please indicate at least ONE category" />--%>
    <%--
        Don't forget to add validation! Fields to validate: 
        Category Selection - SelectorList MUST have at least 1 category 
        Use custom validator
        
        --%>



    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="True"  Height="1202px" ReportSourceID="TrendRptSrc" ToolPanelView="None" ToolPanelWidth="200px" Width="1104px" />



    <CR:CrystalReportSource ID="TrendRptSrc" runat="server">
        <Report FileName="C:\Users\veryt\Source\Repos\SSISTeam2\SSISTeam2\Views\Reporting\Reports\Trend_Analysis.rpt">
        </Report>
    </CR:CrystalReportSource>



</asp:Content>
