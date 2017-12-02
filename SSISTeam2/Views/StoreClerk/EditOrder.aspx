<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditOrder.aspx.cs" MasterPageFile="~/MasterPage.master" enableEventValidation="false" Inherits="SSISTeam2.Views.StoreClerk.EditOrder" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead"
    runat="server">
    <title>Purchase Order Status</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">

    <div class="panel panel-default">
        <!-- Default panel contents -->
        <div class="panel-heading">
            <h3>Order History Detail</h3>
        </div>
        <div class="panel-body">
            <p>
             <asp:HyperLink runat="server" NavigateUrl="~/views/storeclerk/viewpendingorder.aspx"><span class="glyphicon glyphicon-circle-arrow-left" style="color: black;"></span></asp:HyperLink> Your Ref: PO Number <%= Session["order"] %>               
             <asp:Label ID="lblResult" runat="server" class="btn btn-block alert-danger" Visible="false"></asp:Label>               
            </p>
        </div>

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

                     <asp:TemplateField ItemStyle-Width="10%" HeaderText="#ID"><%-- HeaderStyle-CssClass="text-center-impt">--%>

                        <ItemTemplate>
                            <asp:Label ID="Label_OrderDetailId" runat="server" Text='<%# Eval("order_details_id") %>' CssClass="text-bold"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="Item Name"><%-- HeaderStyle-CssClass="text-center-impt">--%>

                        <ItemTemplate>
                            <asp:Label ID="Label_itemDesc" runat="server" Text='<%# Eval("item_description") %>' CssClass="text-bold"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="Expected Quantity"><%-- HeaderStyle-CssClass="text-center-impt">--%>

                        <ItemTemplate>
                            <asp:Label ID="Label_quantity" runat="server" Text='<%# Eval("quantity") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="Status"><%-- HeaderStyle-CssClass="text-center-impt">--%>

                        <ItemTemplate>
                            <asp:Label ID="Label_status" runat="server" Text='<%# Eval("status") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                   

                    <asp:TemplateField ItemStyle-Width="10%"><%-- HeaderStyle-CssClass="text-center-impt">--%>

<%--                        <ItemTemplate>
                            <asp:Button ID="delete" runat="server" Text="Delete"
                                CssClass="btn btn-danger"  OnClick="delete_OrderItem" />
                        </ItemTemplate>--%>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

        </div>

        <div class="panel-footer">

            
            <asp:Button ID="finish" runat="server" Text="Receive Delivery Orders" 
                CssClass="btn btn-primary" OnClick="ReceiveOrder" />

            <input type="button" class="btn btn-sm btn-info" onclick="print();" value="Print Page" />
        </div>


    </div>


</asp:Content>
