<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="TrendAnalysisStaging.aspx.cs" Inherits="SSISTeam2.Views.Reporting.Reports.TrendAnalysisStaging" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderHead"
    runat="server">
    <title>Trend Analysis Report</title>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p></p>
     <asp:table runat="server" CssClass="table">
        <asp:TableRow>
            <asp:TableCell>
                <CR:crystalreportviewer id="TACrystal" CssClass="" runat="server" autodatabind="true" HasToggleGroupTreeButton="False" HasRefreshButton="True" ToolPanelView="None" HasCrystalLogo="False" HasToggleParameterPanelButton="False"/>
            </asp:TableCell>
            <asp:TableCell>
                <asp:Button runat="server" CssClass="btn btn-default btn-sm" ID="genNewRep" Text="Generate New Report" OnClick="genNewRep_OnClick" />
                <asp:Button runat="server" CssClass="btn btn-danger btn-sm" ID="backBtn" Text="Back" OnClick="backBtn_OnClick" />

            </asp:TableCell>
            </asp:TableRow>

    </asp:table>
                       
 

    <%--Department Selector--%>
<%--    <h4>Please Select Department</h4>
    <asp:SqlDataSource 
        ID="sdsDepartment" 
        runat="server" 
        DataSourceMode="DataReader" 
        ConnectionString="data source=(local);initial catalog=SSIS;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework" 
        SelectCommand="SELECT name FROM SSIS.dbo.Department" >
    </asp:SqlDataSource>--%>
    
     <%--<asp:DropDownList 
         DataSourceID="sdsDepartment" 
         runat="server" 
         DataTextField="name" 
         DataValueField="name" 
         Width="193px" 
         CssClass="form-control">
     </asp:DropDownList>--%>
    <%-- Category Selector --%>
     <%--<br />
    <h4>Select Category</h4>
    <asp:Table runat="server" CssClass="table" Width="450px">
        <asp:TableRow>
            <asp:TableCell>
                <asp:ListBox ID="CatList" runat="server" CssClass="form-control"></asp:ListBox>
                </asp:TableCell>--%>

            <%-- Selector Controls --%>
               <%-- <asp:TableCell>
                    <asp:Button runat="server" CssClass="btn btn-default btn-sm" ID="AddOneCat" OnClick="AddOneCat_Click" Text=">" Width="40px"/>
                    <br />
                    <asp:Button runat="server" CssClass="btn btn-default btn-sm" ID="AddAllCat" OnClick="AddAllCat_Click" Text=">>" Width="40px"/>
                    <br />
                    <asp:Button runat="server" CssClass="btn btn-default btn-sm" ID="RemoveOneCat" OnClick="RemoveOneCat_Click" Text="<" Width="40px"/>
                    <br />
                    <asp:Button runat="server" CssClass="btn btn-default btn-sm" ID="RemoveAllCat" Text="<<" OnClick="RemoveAllCat_Click" Width="40px"/>
                </asp:TableCell>--%>
            
             <%--Selector--%>
           <%-- <asp:TableCell>    
                <asp:ListBox
                    ID="SelectorList"
                    runat="server" 
                    CssClass="form-control">
                    </asp:ListBox>
     </asp:TableCell>
        </asp:TableRow>
            </asp:Table>--%>

    <%--<br />
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
 --%>
    <%--
        Don't forget to add validation! Fields to validate: 
        Category Selection - SelectorList MUST have at least 1 category 
        Use custom validator
        
        --%>
    



   



</asp:Content>
