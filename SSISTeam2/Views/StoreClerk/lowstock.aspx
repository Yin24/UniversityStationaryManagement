<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" MasterPageFile="~/MasterPage.master" CodeBehind="lowstock.aspx.cs" Inherits="SSISTeam2.Views.StoreClerk.lowstock" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead"
    runat="server">
    <title>Inventory Low on Stock</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">
    <div class="panel panel-primary">
        <!-- Default panel contents -->
        <div class="panel-heading">Low Stock Ordering - Items to be Restocked</div>
        <div class="panel-body">
            
            <asp:Label ID="lblResult" runat="server" class="btn btn-block alert-success" Visible="false"></asp:Label>
            <asp:Label ID="lblduplicate" runat="server" class="btn btn-block alert-danger" Visible="false"></asp:Label>             
            <asp:Label ID="lblNoData" runat="server" Text="There are no items that have low stocks." CssClass="alert alert-warning"></asp:Label>
        </div>

        <!-- Table -->

        <div class="table-responsive">

            <asp:GridView ID="GridView1" runat="server"
                AutoGenerateColumns="false"
                AllowPaging="true"
                 OnPageIndexChanging="OnPageIndexChanging" ShowHeaderWhenEmpty="True" EmptyDataText="No records Found"
                PageSize="5"
                 OnDataBound="GridView_EditBooks_DataBound"
                HeaderStyle-CssClass="text-center-impt"
                CssClass="table table-responsive table-striped"
                GridLines="None"
                PagerStyle-HorizontalAlign="Center" PagerSettings-Position="TopAndBottom" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">

                <PagerSettings Mode="NumericFirstLast" FirstPageText="First" LastPageText="Last" PreviousPageText="Previous" NextPageText="Next" />
                <PagerTemplate>
                    <asp:Button Text="First" runat="server" CommandName="Page" CommandArgument="First" CssClass="btn btn-default btn-sm" />
                    <asp:Button Text="Prev" runat="server" CommandName="Page" CommandArgument="Prev" CssClass="btn btn-default btn-sm" />
                     <asp:DropDownList ID="DropDownList_JumpToPage" runat="server" OnSelectedIndexChanged="DropDownList_JumpToPage_SelectedIndexChanged" AutoPostBack="True" CssClass="btn btn-default btn-sm"></asp:DropDownList>

                    <asp:Label ID="Paging_CurrentPage" Text="" runat="server"><%# " / " + GridView1.PageCount %></asp:Label>
                    <asp:Button Text="Next" runat="server" CommandName="Page" CommandArgument="Next" CssClass="btn btn-default btn-sm" />
                    <asp:Button Text="Last" runat="server" CommandName="Page" CommandArgument="Last" CssClass="btn btn-default btn-sm" />
                </PagerTemplate>

                <Columns>
                    <asp:TemplateField ItemStyle-Width="5%" HeaderText="No.">
               <ItemTemplate>
                   <%#Container.DataItemIndex+1 %>
               </ItemTemplate>
            </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="Item Code"><%-- HeaderStyle-CssClass="text-center-impt">--%>

                        <ItemTemplate>
                            <asp:Label ID="Label_ItemCode" runat="server" Text='<%# Eval("ItemCode") %>' CssClass="text-bold"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="Available Quantity"><%-- HeaderStyle-CssClass="text-center-impt">--%>

                        <ItemTemplate>
                            <asp:Label ID="Label_CurrentQuantity" runat="server" Text='<%# Eval("AvailableQuantity") %>' CssClass="alert-danger"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="Re-order Quantity"><%-- HeaderStyle-CssClass="text-center-impt">--%>

                        <ItemTemplate>
                            <asp:Label ID="Label_ReOrderQuantity" runat="server" Text='<%# Eval("ReorderQuantity") %>' CssClass="text-bold"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="Re-order Level"><%-- HeaderStyle-CssClass="text-center-impt">--%>

                        <ItemTemplate>
                            <asp:Label ID="Label_ReOrderLevel" runat="server" Text='<%# Eval("ReorderLevel") %>' CssClass="text-bold"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%--<asp:TemplateField ItemStyle-Width="10%" HeaderText="Supplier ID">

                        <ItemTemplate>
                            <asp:Label ID="Label_SupplierID" runat="server" Text='<%# Eval("supplier_id") %>' CssClass="text-bold"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>

                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="Item Description"><%-- HeaderStyle-CssClass="text-center-impt">--%>

                        <ItemTemplate>
                            <asp:Label ID="Label_ItemDesc" runat="server" Text='<%# Eval("Description") %>' CssClass="text-bold"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="Order"><%-- HeaderStyle-CssClass="text-center-impt">--%>

                        <ItemTemplate>
                            <asp:Button ID="Order" runat="server" Text="Mark for Purchasing"
                                OnClick="MakeOrder"
                                CssClass="btn btn-primary" />
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>

        </div>


    </div>

        <div class="row">
            <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" CssClass="btn btn-primary" />
        </div>

</asp:Content>
