<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="SSISTeam2.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
    <title>Dashboard</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-md-13">
        <div class="panel panel-default">
            <div class="panel-heading">
                <h3 class="panel-title">

                    <asp:Label runat="server" Font-Size="XX-Large" ID="lblFullName" CssClass="modal-header"></asp:Label></h3>
                <%--<h4>Welcome to the SSIS</h4>--%>
            </div>

        </div>
    </div>

    <div class="row">
        <div class="col-md-6">

            <div class="panel panel-default">
                <div class="panel-heading">
                    <a data-toggle="collapse" href="#collapseYour3Latest">
                        <h3 class="panel-title">Your 3 Latest Request(s)<span class="glyphicon glyphicon-chevron-down" style="float: right;" /></h3>
                    </a>
                </div>
                <div id="collapseYour3Latest" class="panel-collapse collapse in">
                    <div class="panel-body">
                        <asp:GridView ID="GridView1" runat="server" GridLines="None"
                            AutoGenerateColumns="false"
                            PageSize="3"
                            HeaderStyle-CssClass="text-center-impt"
                            CssClass="table table-responsive table-striped">

                            <Columns>

                                <asp:TemplateField HeaderText="Request ID">
                                    <ItemTemplate>
                                        <asp:Label ID="lblreqid" runat="server" Text='<%# Eval("request_id") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lbldate" runat="server" Text='<%# Eval("date_time") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Reason">
                                    <ItemTemplate>
                                        <asp:Label ID="lblreason" runat="server" Text='<%# Eval("reason") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("current_status") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                    </div>

                    <div class="panel-footer">
                        <asp:Button ID="btnMakeNewReq" runat="server" Text="Make New Request" CssClass="btn btn-primary" OnClick="btnMakeNewReq_Click" />

                    </div>
                </div>
            </div>
        </div>

        <div class="col-md-6">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <a data-toggle="collapse" href="#collapseDep3Latest">
                        <h3 class="panel-title">Department Latest 3 Request(s)<span class="glyphicon glyphicon-chevron-down" style="float: right;" /></h3>
                    </a>
                </div>
                <div id="collapseDep3Latest" class="panel-collapse collapse in">

                    <div class="panel-body">
                        <asp:GridView ID="GridView2" runat="server" GridLines="None"
                            AutoGenerateColumns="false"
                            PageSize="3"
                            HeaderStyle-CssClass="text-center-impt"
                            CssClass="table table-responsive table-striped">

                            <Columns>
                                <asp:TemplateField HeaderText="Request ID">
                                    <ItemTemplate>
                                        <asp:Label ID="lblreqid" runat="server" Text='<%# Eval("request_id") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Employee">
                                    <ItemTemplate>
                                        <asp:Label ID="lblempname" runat="server" Text='<%# Eval("fullname") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lbldate" runat="server" Text='<%# Eval("date_time") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Reason">
                                    <ItemTemplate>
                                        <asp:Label ID="lblreason" runat="server" Text='<%# Eval("reason") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("current_status") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="panel-footer">
                        <asp:Button ID="btnShowHistory" runat="server" Text="Show Request History" CssClass="btn btn-primary" OnClick="btnShowHistory_Click" />

                    </div>
                </div>

            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-lg-4">
            <div class="panel-group">

                <asp:Panel ID="panelLowStocks" runat="server" CssClass="panel panel-default">
                    <div class="panel-heading">
                        <a data-toggle="collapse" href="#collapseLowStocks">
                            <h3 class="panel-title">Top 5 items below reorder level<span class="glyphicon glyphicon-chevron-down" style="float: right;" /></h3>
                        </a>
                    </div>
                    <div id="collapseLowStocks" class="panel-collapse collapse in">
                        <div class="panel-body">

                            <asp:Panel ID="panelLowStocksEmpty" runat="server">
                                No items are in need of reordering.
                            </asp:Panel>

                            <asp:Panel ID="panelLowStocksNormal" runat="server">
                                These items are running low.
                            </asp:Panel>
                        </div>

                        <asp:GridView ID="gridViewLowStocks" runat="server" GridLines="None" AutoGenerateColumns="false" CssClass="table table-striped">
                            <Columns>
                                <asp:TemplateField HeaderText="Item Code">
                                    <ItemTemplate>
                                        <asp:Label Text='<%# Eval("ItemCode")  %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item">
                                    <ItemTemplate>
                                        <asp:Label Text='<%# Eval("Description")  %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Available Qty">
                                    <ItemTemplate>
                                        <asp:Label Text='<%# Eval("AvailableQuantity")  %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Reorder Level">
                                    <ItemTemplate>
                                        <asp:Label Text='<%# Eval("ReorderLevel")  %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                        <div class="panel-footer">
                            <asp:Panel ID="panelLowStocksBtn" runat="server">
                                <asp:LinkButton ID="btnLowStock" Text="Show all" OnClick="btnLowStock_Click" runat="server" CssClass="btn btn-primary" />
                                <asp:Label ID="lblNumLowStock" runat="server" Text=""></asp:Label>
                            </asp:Panel>
                        </div>
                    </div>

                </asp:Panel>
            </div>
        </div>
        <div class="col-md-4">
            <%-- ToRetrieve --%>
            <div class="panel-group">
                <asp:Panel ID="panelToRetrieve" runat="server" CssClass="panel panel-default">
                    <div class="panel-heading">
                        <a data-toggle="collapse" href="#collapseRetrieve">
                            <h3 class="panel-title">Next 3 Retrievals<span class="glyphicon glyphicon-chevron-down" style="float: right;" /></h3>
                        </a>
                    </div>
                    <div id="collapseRetrieve" class="panel-collapse collapse in">
                        <%-- Empty --%>
                        <asp:Panel ID="panelToRetrieve_Empty" runat="server">
                            <div class="panel-body">
                                No items in need of retrieving.
                            </div>
                        </asp:Panel>

                        <%-- From Warehouse --%>
                        <asp:Panel ID="panelToRetrieve_FromWarehouse" runat="server">
                            <div class="panel-body">
                                These items need to be retrieved from the warehouse.
                            </div>
                            <asp:GridView ID="gridViewToRetrieve_FromWarehouse" runat="server" GridLines="None" AutoGenerateColumns="false" CssClass="table table-striped" HeaderStyle-CssClass="info">
                                <Columns>
                                    <asp:TemplateField HeaderText="Item Code">
                                        <ItemTemplate>
                                            <asp:Label Text='<%# Eval("ItemCode")  %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Item">
                                        <ItemTemplate>
                                            <asp:Label Text='<%# Eval("Description")  %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Quantity">
                                        <ItemTemplate>
                                            <asp:Label Text='<%# Eval("Quantity")  %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <div class="panel-footer">
                                <asp:LinkButton ID="btnShowAllToRetrieve" Text="Show all to retrieve" OnClick="btnShowAllToRetrieve_Click" runat="server" CssClass="btn btn-sm btn-primary" />
                                <asp:Label ID="lblNumToRetrieve" runat="server" Text=""></asp:Label>

                            </div>
                        </asp:Panel>

                        <%-- Confirm amount --%>
                        <asp:Panel ID="panelToRetrieve_ToConfirm" runat="server">
                            <div class="panel-body">
                                Please confirm the quantities of the items collected from the warehouse.
                            </div>
                            <asp:GridView ID="gridViewToRetrieve_ToConfirm" runat="server" GridLines="None" AutoGenerateColumns="false" CssClass="table table-striped" HeaderStyle-CssClass="info">
                                <Columns>
                                    <asp:TemplateField HeaderText="Item Code">
                                        <ItemTemplate>
                                            <asp:Label Text='<%# Eval("ItemCode")  %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Item">
                                        <ItemTemplate>
                                            <asp:Label Text='<%# Eval("Description")  %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Quantity">
                                        <ItemTemplate>
                                            <asp:Label Text='<%# Eval("Quantity")  %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <div class="panel-footer">
                                <asp:LinkButton ID="btnConfirmRetrieval" Text="Show all to confirm" OnClick="btnConfirmRetrieval_Click" runat="server" CssClass="btn btn-sm btn-primary" />
                                <asp:Label ID="lblNumToConfirm" runat="server" Text=""></asp:Label>

                            </div>
                        </asp:Panel>
                    </div>
                </asp:Panel>
            </div>

        </div>
        <div class="col-md-4">
            <%-- ToDisburse --%>
            <div class="panel-group">
                <asp:Panel ID="panelToDisburse" runat="server" CssClass="panel panel-default">
                    <div class="panel-heading">
                        <a data-toggle="collapse" href="#collapseToDisburse">
                            <h3 class="panel-title">Next 3 Disbursements<span class="glyphicon glyphicon-chevron-down" style="float: right;" /></h3>
                        </a>
                    </div>
                    <div id="collapseToDisburse" class="panel-collapse collapse in">
                        <%-- Empty --%>
                        <asp:Panel ID="panelToDisburse_Empty" runat="server">
                            <div class="panel-body">
                                No items in need of disbursement.
                            </div>
                        </asp:Panel>

                        <%-- To be disbursed --%>
                        <asp:Panel ID="panelToDisburse_ToCollectionPt" runat="server">
                            <div class="panel-body">
                                These collection points have disbursements waiting.
                            </div>
                            <asp:GridView ID="gridViewToDisburse_ToCollectionPt" runat="server" GridLines="None" AutoGenerateColumns="false" CssClass="table table-striped" HeaderStyle-CssClass="success">
                                <Columns>
                                    <asp:TemplateField HeaderText="Collection Point">
                                        <ItemTemplate>
                                            <asp:Label Text='<%# Eval("location")  %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <div class="panel-footer">
                                <asp:LinkButton ID="btnGenerateDisbursement" Text="Show all to disburse" OnClick="btnGenerateDisbursement_Click" runat="server" CssClass="btn btn-sm btn-primary" />
                                <asp:Label ID="lblNumToDisburse" runat="server" Text=""></asp:Label>

                            </div>
                        </asp:Panel>

                        <%-- Confirm amount --%>
                        <asp:Panel ID="panelToDisburse_ToConfirm" runat="server">
                            <div class="panel-body">
                                Please confirm the sign off from representatives for these departments.
                            </div>
                            <asp:GridView ID="gridViewToDisburse_ToConfirm" runat="server" GridLines="None" AutoGenerateColumns="false" CssClass="table table-striped" HeaderStyle-CssClass="success">
                                <Columns>
                                    <asp:TemplateField HeaderText="Departments to be signed-off">
                                        <ItemTemplate>
                                            <asp:Label Text='<%# Eval("DepartmentName")  %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <div class="panel-footer">
                                <asp:LinkButton ID="btnConfirmDisbursement" Text="Show all to confirm" OnClick="btnConfirmDisbursement_Click" runat="server" CssClass="btn btn-sm btn-primary" />
                                <asp:Label ID="lblNumToSignOff" runat="server" Text=""></asp:Label>

                            </div>
                        </asp:Panel>
                    </div>
                </asp:Panel>
            </div>

        </div>
    </div>
</asp:Content>
