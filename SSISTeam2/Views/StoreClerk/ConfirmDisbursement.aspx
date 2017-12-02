<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeBehind="ConfirmDisbursement.aspx.cs" Inherits="SSISTeam2.Views.StoreClerk.ConfirmDisbursement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead"
    runat="server">
    <title>Confirm Disbursement</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">
    <div class="row">
        <div class="col-xs-12">
            <asp:Label Text="Confirm Disbursements" runat="server" CssClass="h1" />
        </div>
    </div>

    <div style="margin: 10px"></div>

    <div class="row">
        <div class="col-xs-12">
            <asp:Label Text="Select a department and enter the quantities of items passed to the representative." runat="server" CssClass="h5 grey-text" />
        </div>
    </div>    
    
    <div style="margin: 10px"></div>

    <div class="row">
        <div class="col-xs-12">
            <asp:Label Text="First, select a department:" runat="server" CssClass="h4 grey-text" />
        </div>
    </div>
        
    <div class="row">
        <div class="col-md-4">
            <asp:DropDownList ID="ddlDepartments" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartments_SelectedIndexChanged" CssClass="form-control"></asp:DropDownList>
        </div>
    </div>
    
    <div style="margin: 10px"></div>

    <div class="row">
        <div class="col-xs-12">
            <asp:Label ID="lblRepName" Text="Representative: " runat="server" CssClass="h5" />
            <br />
            <asp:Label ID="lblCollectionPtLocation" Text="Collection point: " runat="server" CssClass="h5" />
        </div>
    </div>

    <div style="margin: 10px"></div>
    
    <div class="row">
        <div class="col-xs-12">
            <asp:Label Text="Then, confirm the quantities" runat="server" CssClass="h4 grey-text" />
        </div>
    </div>    

    <div style="margin: 10px"></div>

    <div class="row">
        <div class="col-12 col-lg-12 col-md-12">
            <asp:GridView ID="gvDisbursement" runat="server" AutoGenerateColumns="false" GridLines="None" CssClass="table table-striped" style="width:700px; overflow-x:auto">
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
                    <asp:TemplateField HeaderText="Quantity" ItemStyle-Width="15%">
                        <ItemTemplate>
                            <asp:Label ID="lblQtyExpected" runat="server" Text='<%# Eval("QuantityExpected") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Actual Quantity" ItemStyle-Width="20%">
                        <ItemTemplate>
                            <div class="input-group">
                                <asp:TextBox runat="server"
                                    AutoPostBack="true"
                                    OnTextChanged="tbQtyActual_TextChanged"
                                    ID="tbQtyActual"
                                    Text='<%# Eval("QuantityActual") %>'
                                    TextMode="Number"
                                    min="0"
                                    max='<%# Eval("QuantityExpected") %>'
                                    step="1"
                                    CssClass="form-control"
                                    />
                                <span class="input-group-btn">
                                    <asp:Button ID="btnResetRowQty" Text="Reset" runat="server" OnClick="btnResetRowQty_Click" CssClass="btn btn-default" />
                                </span>

                            </div>
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

                <div class="alert alert-warning">
                    <asp:Label runat="server" Text="There are no items that were marked to be disbursed to this department by you."></asp:Label>
                    <br />
                    <span><a href="GenerateDisbursement.aspx">Generate some disbursement forms here</a>.</span>
                </div>

            </asp:Panel>

            <asp:Panel ID="panelNormal" runat="server">
                
                <asp:Button ID="btnSubmit" runat="server" Text="Confirm quantities" OnClick="btnSubmit_Click" CssClass="btn btn-success" />

            </asp:Panel>

        </div>
    </div>

</asp:Content>
