<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeBehind="GenerateDisbursement.aspx.cs" Inherits="SSISTeam2.Views.StoreClerk.GenerateDisbursement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead"
    runat="server">
    <title>Generate Disbursements</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">
    <div class="row">
        <div class="col-xs-12">
            <asp:Label Text="Generate Disbursement Forms" runat="server" CssClass="h1" />
        </div>
    </div>

    <div style="margin: 10px"></div>

    <div class="row">
        <div class="col-xs-12">
            <asp:Label Text="Select the collection point to disburse to" runat="server" CssClass="h5 grey-text" />
        </div>
    </div>

    <div style="margin: 10px"></div>

    <div class="row">
        <div class="col-xs-12">
            <asp:Label Text="Select collection point:" runat="server" CssClass="h4 grey-text" />
        </div>
    </div>

    <div class="row">
        <div class="col-md-5">
            <asp:DropDownList ID="ddlCollectionPoint" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCollectionPoint_SelectedIndexChanged" CssClass="form-control"></asp:DropDownList>
        </div>
    </div>

    <div style="margin: 10px"></div>

    <div class="row">
        <div class="col-xs-12">
            <asp:Label Text="Then choose which items to include in the disbursement" runat="server" CssClass="h5 grey-text" />
        </div>
    </div>

    <div style="margin: 10px"></div>

    <%--<asp:Label ID="lblDebug" Text="text" runat="server" />--%>

    <div class="row">
        <div class="col-md-10">

            <asp:GridView ID="gvToRetrieve" runat="server" AutoGenerateColumns="false" GridLines="None" CssClass="table table-responsive table-striped">
                <Columns>
                    <asp:TemplateField HeaderText="No.">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Item">
                        <ItemTemplate>
                            <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("ItemDescription") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Total Quantity">
                        <ItemTemplate>
                            <asp:Label ID="lblTotalQty" runat="server" Text='<%# Eval("IncludedQty") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Department">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Eval("DeptName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Quantity">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Eval("Quantity") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Include?">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkbxInclude" runat="server" Checked='<%# Eval("Include") %>' AutoPostBack="true" OnCheckedChanged="chkbxInclude_CheckedChanged" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%--<asp:ButtonField ButtonType="Button" CommandName="Select" HeaderText="View" ShowHeader="True" Text="Details" />--%>
                </Columns>
            </asp:GridView>

        </div>
    </div>

    <div class="row">
        <div class="col-md-5">
            <asp:Panel ID="panelNoItems" runat="server">

                <div class="alert alert-warning">
                    <asp:Label runat="server" Text="There are no items to disburse at this collection point."></asp:Label>

                    <br />
                    <br />

                    <span>Choose another collection point, or check if there are any <a href="GenerateRetrieval.aspx">items to retrieve</a>.</span>

                    <br />
                    <br />

                    <span>If you just generated some forms, you can <a href="ViewGeneratedDisbursements.aspx">view them here</a>.
                    </span>
                </div>

            </asp:Panel>

            <asp:Panel ID="panelNormal" runat="server">

                <asp:Button ID="btnSubmit" runat="server" Text="Generate selected disbursment forms" OnClick="btnSubmit_Click" CssClass="btn btn-success" />
                <br />
                <br />
                <asp:Button ID="btnSubmitAll" runat="server" Text="Generate selected disbursment forms for all collection points" OnClick="btnSubmitAll_Click" CssClass="btn btn-sm btn-success" />

            </asp:Panel>

        </div>
    </div>
</asp:Content>
