<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" MasterPageFile="~/MasterPage.master" CodeBehind="lowstock.aspx.cs" Inherits="SSISTeam2.Views.StoreClerk.lowstock" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">
    <div class="panel panel-primary">
        <!-- Default panel contents -->
        <div class="panel-heading">Reorder Report List</div>
        <div class="panel-body">
            
            <asp:Label ID="lblResult" runat="server" class="btn btn-block alert-success" Visible="false"></asp:Label>
            <asp:Label ID="lblduplicate" runat="server" class="btn btn-block alert-danger" Visible="false"></asp:Label>             
        
        </div>

        <!-- Table -->

        <div class="table-responsive">

            <asp:GridView ID="GridView1" runat="server"
                AutoGenerateColumns="false"
                AllowPaging="true"
                PageSize="10"
                HeaderStyle-CssClass="text-center-impt"
                CssClass="table table-responsive table-striped"
                GridLines="None"
                PagerStyle-HorizontalAlign="Center" PagerSettings-Position="TopAndBottom">

                <PagerSettings Mode="NumericFirstLast" FirstPageText="First" LastPageText="Last" PreviousPageText="Previous" NextPageText="Next" />
                <PagerTemplate>
                    <asp:Button Text="First" runat="server" CommandName="Page" CommandArgument="First" CssClass="btn btn-default btn-sm" />
                    <asp:Button Text="Prev" runat="server" CommandName="Page" CommandArgument="Prev" CssClass="btn btn-default btn-sm" />
                    <asp:Button Text="Next" runat="server" CommandName="Page" CommandArgument="Next" CssClass="btn btn-default btn-sm" />
                    <asp:Button Text="Last" runat="server" CommandName="Page" CommandArgument="Last" CssClass="btn btn-default btn-sm" />
                </PagerTemplate>

                <Columns>

                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="Item Code"><%-- HeaderStyle-CssClass="text-center-impt">--%>

                        <ItemTemplate>
                            <asp:Label ID="Label_ItemCode" runat="server" Text='<%# Eval("item_code") %>' CssClass="text-bold"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="Quantity On Hand"><%-- HeaderStyle-CssClass="text-center-impt">--%>

                        <ItemTemplate>
                            <asp:Label ID="Label_CurrentQuantity" runat="server" Text='<%# Eval("current_qty") %>' CssClass="alert-danger"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="Re-order Quantity"><%-- HeaderStyle-CssClass="text-center-impt">--%>

                        <ItemTemplate>
                            <asp:Label ID="Label_ReOrderQuantity" runat="server" Text='<%# Eval("reorder_qty") %>' CssClass="text-bold"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="Re-order Level"><%-- HeaderStyle-CssClass="text-center-impt">--%>

                        <ItemTemplate>
                            <asp:Label ID="Label_ReOrderLevel" runat="server" Text='<%# Eval("reorder_level") %>' CssClass="text-bold"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="Supplier ID"><%-- HeaderStyle-CssClass="text-center-impt">--%>

                        <ItemTemplate>
                            <asp:Label ID="Label_SupplierID" runat="server" Text='<%# Eval("supplier_id") %>' CssClass="text-bold"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="Item Description"><%-- HeaderStyle-CssClass="text-center-impt">--%>

                        <ItemTemplate>
                            <asp:Label ID="Label_ItemDesc" runat="server" Text='<%# Eval("item_description") %>' CssClass="text-bold"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="Order"><%-- HeaderStyle-CssClass="text-center-impt">--%>

                        <ItemTemplate>
                            <asp:Button ID="Order" runat="server" Text="Add To Cart"
                                OnClick="MakeOrder"
                                CssClass="btn btn-primary" />
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>

        </div>

    </div>


</asp:Content>
