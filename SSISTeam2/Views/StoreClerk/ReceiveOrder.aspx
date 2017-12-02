<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReceiveOrder.aspx.cs" MasterPageFile="~/MasterPage.master" Inherits="SSISTeam2.Views.StoreClerk.ReceiveOrder" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead"
    runat="server">
    <title>Purchase Order Details</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">

    <div class="panel panel-default">
        <!-- Default panel contents -->
        <div class="panel-heading">
            <h3>Delivery Order</h3>
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-md-4">
                    <h5>
                        <asp:HyperLink runat="server" NavigateUrl="~/views/storeclerk/editorder.aspx"><span class="glyphicon glyphicon-circle-arrow-left" style="color: black;"></span></asp:HyperLink>
                        Purchase Order Number : <%= Session["order"] %>

                    </h5>
                </div>
                <div class="col-md-4  col-md-offset-4">
                    <h5>Supplier Name : <%= Session["suppliername"] %> </h5>
                </div>
            </div>

            <div class="row">

                <div class="col-md-4 col-md-offset-8">
                    <asp:Label ID="Label1" runat="server" Text="Order Receive Date:"></asp:Label><asp:TextBox ID="deliverydate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="vldDate" runat="server" ErrorMessage="Date Is Required" ControlToValidate="deliverydate" ForeColor="Red">* Date is Required.</asp:RequiredFieldValidator>
                </div>

            </div>

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

                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="#Order Detail ID"><%-- HeaderStyle-CssClass="text-center-impt">--%>

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

                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="Actual Quantity"><%-- HeaderStyle-CssClass="text-center-impt">--%>

                        <ItemTemplate>
                            <asp:TextBox ID="quantity" Text='<%# Eval("quantity") %>' CssClass="form-control" Width="50%" runat="server"></asp:TextBox>
                            <asp:RangeValidator ID="RangeValidator1" runat="server"
                                ErrorMessage="RangeValidator" ControlToValidate="quantity"
                                ForeColor="Red" Type="Integer"
                                MaximumValue='<%# Eval("quantity") %>' MinimumValue="1">Invalid Amount</asp:RangeValidator>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="Remarks"><%-- HeaderStyle-CssClass="text-center-impt">--%>

                        <ItemTemplate>
                            <asp:TextBox ID="remark" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>



                 

                </Columns>
            </asp:GridView>

        </div>

        <div class="panel-footer">

            <!-- Button trigger modal -->
            <asp:Button ID="confirm" runat="server" Text="Confirm"
                CssClass="btn btn-primary" OnClick="Confirm" />

        </div>

    </div>

</asp:Content>
