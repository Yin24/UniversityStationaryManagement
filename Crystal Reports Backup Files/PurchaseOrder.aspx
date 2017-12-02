<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeBehind="PurchaseOrder.aspx.cs" Inherits="SSISTeam2.Views.StoreClerk.PurchaseOrder" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">

    <div class="panel panel-default">
        <!-- Default panel contents -->
        <div class="panel-heading">Create New Purchase Order</div>
        <div class="panel-body">
            <p>Step 2 of 3: Add Items</p>
            <p>
                <asp:Label ID="LabelOrderSummary" runat="server" Text=""></asp:Label>
            </p>

            <div class="row">

                <div class="col-md-4">
                    <asp:Label ID="Label1" runat="server" Text="Deliver Date:"></asp:Label><asp:TextBox ID="deliverydate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                     <asp:RequiredFieldValidator ID="vldDate" runat="server" ErrorMessage="Date Is Required" ControlToValidate="deliverydate" ForeColor="Red">* Date is Required.</asp:RequiredFieldValidator>
                </div>

            </div>



        </div>

        <!-- Table -->

        <div class="table-responsive">

            <asp:GridView ID="GridView1" runat="server"
                AutoGenerateColumns="false"
                PageSize="10"
                HeaderStyle-CssClass="text-center-impt"
                CssClass="table table-responsive table-striped"
                GridLines="None"
                PagerStyle-HorizontalAlign="Center" PagerSettings-Position="TopAndBottom">



                <Columns>

                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="Item Description"><%-- HeaderStyle-CssClass="text-center-impt">--%>

                        <ItemTemplate>
                            <asp:Label ID="Label_ItemDesc" runat="server" Text='<%# Eval("ItemDesc") %>' CssClass="text-bold"></asp:Label>
                        </ItemTemplate>

                        <ItemStyle Width="10%"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="Supplier Name"><%-- HeaderStyle-CssClass="text-center-impt">--%>

                        <ItemTemplate>
                            <asp:Label ID="Label_supplier" runat="server" Text='<%# Eval("SupplierName") %>' CssClass="text-bold"></asp:Label>
                        </ItemTemplate>

                        <ItemStyle Width="10%"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="Price">

                        <ItemTemplate>
                            <asp:Label ID="Label_price" runat="server" Text='<%# Eval("Price") %>' CssClass="text-bold"></asp:Label>
                        </ItemTemplate>

                        <ItemStyle Width="10%"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="Quantity">

                        <ItemTemplate>
                            <asp:TextBox ID="quantity" Text='<%# Eval("Quantity") %>' CssClass="form-control" runat="server" OnTextChanged="quantity_TextChanged" AutoPostBack="True"></asp:TextBox>
                        </ItemTemplate>

                        <ItemStyle Width="10%"></ItemStyle>
                    </asp:TemplateField>






                </Columns>

                <HeaderStyle CssClass="text-center-impt"></HeaderStyle>

                <PagerSettings Position="TopAndBottom"></PagerSettings>

                <PagerStyle HorizontalAlign="Center"></PagerStyle>
            </asp:GridView>

            <div class="panel-footer">
                <div class="row">
                    <div class="col-md-4"><b>Order Total :</b> $<asp:Label ID="Lbl_total" runat="server"></asp:Label></div>
                    <div class="col-md-4 col-md-offset-4">
                        <asp:Button ID="order" runat="server" Text="Order Now"
                            OnClick="MakeOrder" Visible="false"
                            CssClass="btn btn-primary" />
                    </div>

                </div>

            </div>
        </div>

    </div>
</asp:Content>
