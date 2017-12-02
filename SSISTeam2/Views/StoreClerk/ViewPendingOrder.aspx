<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeBehind="ViewPendingOrder.aspx.cs" enableEventValidation="false" Inherits="SSISTeam2.Views.StoreClerk.ViewPendingOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead"
    runat="server">
    <title>View Order History</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">
    <div class="panel panel-default">
        <!-- Default panel contents -->
        <div class="panel-heading">
            <h3>Order History List</h3>
        </div>
        <div class="panel-body">
            <p>
                Your Orders               
            </p>
            <asp:Panel ID="panelNoData" runat="server">
                <br />
                <p>
                    <asp:Label ID="lblNoData" runat="server" Text="There are no previous orders to view." CssClass="alert alert-warning"></asp:Label>
                </p>
            </asp:Panel>
        </div>


        <div class="table-responsive">

            <asp:GridView ID="GridView1" runat="server"
                AutoGenerateColumns="false"
               AllowPaging="True" PageSize="3"
                OnPageIndexChanging="OnPageIndexChanging" ShowHeaderWhenEmpty="True" EmptyDataText="No records Found"
                HeaderStyle-CssClass="text-center-impt"
                CssClass="table table-responsive table-striped"
                 OnDataBound="GridView_EditBooks_DataBound"
                GridLines="None"
                PagerStyle-HorizontalAlign="Center" PagerSettings-Position="TopAndBottom">

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

                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="Order ID"><%-- HeaderStyle-CssClass="text-center-impt">--%>

                        <ItemTemplate>
                            <asp:Label ID="Label_OrderId" runat="server" Text='<%# Eval("order_id") %>' CssClass="text-bold"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="Order By"><%-- HeaderStyle-CssClass="text-center-impt">--%>

                        <ItemTemplate>
                            <asp:Label ID="Label_ClerkName" runat="server" Text='<%# Eval("clerk_user") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                     <asp:TemplateField ItemStyle-Width="10%" HeaderText="Order Date"><%-- HeaderStyle-CssClass="text-center-impt">--%>

                        <ItemTemplate>
                            <asp:Label ID="Label_OrderDate" runat="server" Text='<%# Eval("order_date") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="Supplier ID"><%-- HeaderStyle-CssClass="text-center-impt">--%>

                        <ItemTemplate>
                            <asp:Label ID="Label_SupplierId" runat="server" Text='<%# Eval("supplier_id") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="Status"><%-- HeaderStyle-CssClass="text-center-impt">--%>

                        <ItemTemplate>
                            <asp:Label ID="Label_Status" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                

                    <asp:TemplateField ItemStyle-Width="10%"><%-- HeaderStyle-CssClass="text-center-impt">--%>

                        <ItemTemplate>
                            <asp:Button ID="detail" runat="server" Text="Update Status/View Details"
                                CssClass="btn btn-primary" OnClick="Edit_Order"  />
                        </ItemTemplate>
                    </asp:TemplateField>


<%--                    <asp:TemplateField ItemStyle-Width="10%"><HeaderStyle-CssClass="text-center-impt">

                        <ItemTemplate>
                            <asp:Button ID="delete" runat="server" Text="Cancel"
                                CssClass="btn btn-danger" OnClick="Delete_Order" OnClientClick="return confirm('Are you sure you want to cancel this order?');" />
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                </Columns>
            </asp:GridView>

        </div>

    </div>
</asp:Content>
