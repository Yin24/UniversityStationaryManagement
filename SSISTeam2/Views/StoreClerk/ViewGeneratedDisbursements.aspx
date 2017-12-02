<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeBehind="ViewGeneratedDisbursements.aspx.cs" Inherits="SSISTeam2.Views.StoreClerk.ViewGeneratedDisbursements" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead"
    runat="server">
    <title>View Generated Disbursements</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">
    <div class="row">
        <div class="col-xs-12">
            <asp:Label Text="View Generated Disbursement Forms" runat="server" CssClass="h1" />
        </div>
    </div>

    <div style="margin: 10px"></div>

    <div class="row">
        <div class="col-xs-12">
            <asp:Label Text="View a department's disbursement form to view" runat="server" CssClass="h5 grey-text" />
        </div>
    </div>

    <div style="margin: 10px"></div>

    <div class="row">
        <div class="col-xs-12">
            <asp:Label Text="First, select a collection point:" runat="server" CssClass="h4 grey-text" />
        </div>
    </div>

    <div class="row">
        <div class="col-lg-5 col-md-6">
            <asp:DropDownList ID="ddlCollectionPoints" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCollectionPoints_SelectedIndexChanged" CssClass="form-control"></asp:DropDownList>
        </div>
    </div>

    <div style="margin: 10px"></div>

    <div class="row">
        <div class="col-xs-12">
            <asp:Label Text="Then, select a department:" runat="server" CssClass="h4 grey-text" />
            <br />
            <asp:Label ID="lblNoDepartments" Text="No departments for this collection point" runat="server" CssClass="label label-info" />
        </div>
    </div>

    <div class="row">
        <div class="col-md-2">
            <asp:DropDownList ID="ddlDepartments" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartments_SelectedIndexChanged" CssClass="form-control"></asp:DropDownList>
        </div>
    </div>

    <div style="margin: 10px"></div>

    <div class="row">
        <div class="col-md-6">

            <asp:GridView ID="gvDisbursement" runat="server" AutoGenerateColumns="false" GridLines="None" CssClass="table table-responsive table-striped">
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
                    <asp:TemplateField HeaderText="Quantity">
                        <ItemTemplate>
                            <asp:Label ID="lblQtyExpected" runat="server" Text='<%# Eval("QuantityExpected") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

        </div>
    </div>

    <div class="row">
        <div class="col-md-6">

            <asp:Label ID="lblWarningInfo" runat="server" EnableViewState="false"></asp:Label>

            <asp:Panel ID="panelNoItems" runat="server">

                <div class="alert alert-info">
                    <asp:Label runat="server" Text="This department has no generated disbursement forms."></asp:Label>
                    <br />
                    <span><a href="GenerateDisbursement.aspx">Generate some here</a>.</span>
                    <br />
                    <br />
                    <span>Or <a href="ConfirmDisbursement.aspx">view all department confirmations here</a>.</span>
                </div>

            </asp:Panel>

            <asp:Panel ID="panelNormal" runat="server">

                <asp:Button ID="btnGoToConfirm" runat="server" Text="Go to confirmation" OnClick="btnGoToConfirm_Click" CssClass="btn btn-success" />
                    
            </asp:Panel>

        </div>
    </div>

</asp:Content>
