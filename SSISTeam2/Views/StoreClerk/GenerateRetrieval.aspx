<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeBehind="GenerateRetrieval.aspx.cs" Inherits="SSISTeam2.Views.StoreClerk.GenerateRetrieval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead"
    runat="server">
    <title>Generate Retrieval</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">
    <div class="row">
        <div class="col-xs-12">
            <asp:Label Text="Generate Retrieval Form" runat="server" CssClass="h1" />
        </div>
    </div>

    <div style="margin: 10px"></div>

    <asp:Label ID="lblDebug" Text="" runat="server" />

    <%--<%= Page.User.Identity.Name %>--%>

    <div class="row">
        <div class="col-xs-12">
            <asp:Label Text="Select which items to include in the retrieval" runat="server" CssClass="h5 grey-text" />
        </div>
    </div>

    <div style="margin: 10px"></div>
    <div class="table-responsive">
        <div class="row">
            <div class="col-md-10">

                <asp:GridView ID="gvToRetrieve" runat="server" AutoGenerateColumns="false" GridLines="None" CssClass="table table-responsive table-striped"
                    AllowPaging="true"
                    OnDataBound="GridView_EditBooks_DataBound"
                    PageSize="5"
                    OnPageIndexChanging="OnPageIndexChanging" ShowHeaderWhenEmpty="True" EmptyDataText="No records Found"
                    PagerStyle-HorizontalAlign="Center" PagerSettings-Position="TopAndBottom">

                    <PagerSettings Mode="NumericFirstLast" FirstPageText="First" LastPageText="Last" PreviousPageText="Previous" NextPageText="Next" />
                    <PagerTemplate>
                        <asp:Button Text="First" runat="server" CommandName="Page" CommandArgument="First" CssClass="btn btn-default btn-sm" />
                        <asp:Button Text="Prev" runat="server" CommandName="Page" CommandArgument="Prev" CssClass="btn btn-default btn-sm" />
                        <asp:DropDownList ID="DropDownList_JumpToPage" runat="server" OnSelectedIndexChanged="DropDownList_JumpToPage_SelectedIndexChanged" AutoPostBack="True" CssClass="btn btn-default btn-sm"></asp:DropDownList>

                        <asp:Label ID="Paging_CurrentPage" Text="" runat="server"><%# " / " + gvToRetrieve.PageCount %></asp:Label>
                        <asp:Button Text="Next" runat="server" CommandName="Page" CommandArgument="Next" CssClass="btn btn-default btn-sm" />
                        <asp:Button Text="Last" runat="server" CommandName="Page" CommandArgument="Last" CssClass="btn btn-default btn-sm" />
                    </PagerTemplate>

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

    </div>


    <div class="row">
        <div class="col-md-4">

            <asp:Panel ID="panelNoItems" runat="server">

                <div class="alert alert-warning">
                    <asp:Label runat="server" Text="There are no items that need to be retrieved right now."></asp:Label>
                    <%--<asp:Button ID="btnGoToGenerate" Text="Go to generate a retrieval" runat="server" CssClass="btn btn-warning" />--%>
                </div>
                <span>If you generated a form, you can <a href="ConfirmRetrieval.aspx">confirm the quantities here</a>.
                </span>

            </asp:Panel>

            <br />

            <asp:Panel ID="panelNormal" runat="server">

                <asp:Button ID="btnSubmit" runat="server" Text="Make Retrieval Form" OnClick="btnSubmit_Click" CssClass="btn btn-success" />

            </asp:Panel>
            <br />
            
            <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" CssClass="btn btn-primary" />

        </div>
    </div>


</asp:Content>
