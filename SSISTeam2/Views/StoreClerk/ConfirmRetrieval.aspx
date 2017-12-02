<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeBehind="ConfirmRetrieval.aspx.cs" Inherits="SSISTeam2.Views.StoreClerk.ConfirmRetrieval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead"
    runat="server">
    <title>Confirm Retrievals</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">
    <div class="row">
        <div class="col-xs-12">
            <asp:Label Text="Confirm Retrievals" runat="server" CssClass="h1" />
        </div>
    </div>

    <div style="margin: 10px"></div>

    <div class="row">
        <div class="col-xs-12">
            <asp:Label Text="Confirm the quantites that were actually retrieved. If there are discrepancies, you will be directed to file them." runat="server" CssClass="h5 grey-text" />
        </div>
    </div>

    <div style="margin: 10px"></div>


    <asp:ScriptManager runat="server"></asp:ScriptManager>

    <asp:UpdatePanel runat="server">
        <ContentTemplate>

            <div class="table-responsive">


                <div class="row">
                    <div class="col-md-7">

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
                                <%--<asp:TemplateField HeaderText="Total Quantity">
                                <ItemTemplate>
                                    <asp:Label ID="lblTotalQty" runat="server" Text='<%# Eval("QuantityExpected") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Quantity">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQtyExpected" runat="server" Text='<%# Eval("QuantityExpected") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Actual Quantity" ItemStyle-Width="30%">
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
                                                CssClass="form-control" />
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

            </div>


            <div class="row">
                <div class="col-md-4">

                    <asp:Label ID="lblWarningInfo" runat="server" EnableViewState="false"></asp:Label>

                    <asp:Panel ID="panelNoItems" runat="server">

                        <div class="alert alert-warning">
                            <asp:Label runat="server" Text="There were no items marked for retrieval by you."></asp:Label>

                            <br />
                            <br />

                            <a href="GenerateRetrieval.aspx">
                                <span class="btn btn-warning">Go to generate a retrieval</span>
                            </a>

                            <br />
                            <br />

                            Or go to <a href="GenerateDisbursement.aspx">generate some disbursement forms</a>.
                        </div>

                    </asp:Panel>

                    <asp:Panel ID="panelNormal" runat="server">

                        <asp:Button ID="btnSubmit" runat="server" Text="Confirm retrieval quantities" OnClick="btnSubmit_Click" CssClass="btn btn-success" />

                    </asp:Panel>

                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script>
        function checkQty(event) {
            console.log(event)
            var tb = event.target;
            if (parseInt(tb.value) > tb.max) {
                tb.value = tb.max;
                console.log("Greater than max => curr: " + tb.value + ", max: " + tb.max);
            } else {
                tb.value = tb.value;
            }
            var isNum = /^\d+$/.test(tb.value);
            if (!isNum) {
                tb.value = tb.max;
            }
        }
    </script>

</asp:Content>
