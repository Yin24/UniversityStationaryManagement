<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeBehind="Cart.aspx.cs" enableEventValidation="false" Inherits="SSISTeam2.Views.StoreClerk.Cart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead"
    runat="server">
    <title>Your Cart</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">
    <div class="panel panel-primary">
        <!-- Default panel contents -->
        <div class="panel-heading">
            <h3>View Items Marked for Purchase</h3>
        </div>
        <div class="panel-body">
            <h4>Step 1 of 3: Add Items</h4>

            <div class="row">

                <div class="col-md-4">
                    <asp:Label ID="LabelOrderSummary" runat="server" Text=""></asp:Label><asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                </div>


            </div>
            <br />

            <%-- Cart GridView--%>

            <div class="table-responsive">

                <asp:GridView ID="GridView1" runat="server"
                    AutoGenerateColumns="false"
                    AllowPaging="true"
                    PageSize="10"
                    HeaderStyle-CssClass="text-center-impt"
                    CssClass="table table-responsive table-striped"
                    GridLines="None"
                    PagerStyle-HorizontalAlign="Center" PagerSettings-Position="TopAndBottom">



                    <Columns>

                        <asp:TemplateField ItemStyle-Width="10%" HeaderText="Tender ID"><%-- HeaderStyle-CssClass="text-center-impt">--%>

                            <ItemTemplate>
                                <asp:Label ID="Label_tenderId" runat="server" Text='<%# Eval("tender_id") %>' CssClass="text-bold"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField ItemStyle-Width="10%" HeaderText="Item Code"><%-- HeaderStyle-CssClass="text-center-impt">--%>

                            <ItemTemplate>
                                <asp:Label ID="Label_ItemCode" runat="server" Text='<%# Eval("item_code") %>' CssClass="text-bold"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField ItemStyle-Width="10%" HeaderText="Item Description"><%-- HeaderStyle-CssClass="text-center-impt">--%>

                            <ItemTemplate>
                                <asp:Label ID="Label_ItemDesc" runat="server" Text='<%# Eval("item_description") %>' CssClass="text-bold"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField ItemStyle-Width="10%" HeaderText="Supplier Name">

                            <ItemTemplate>
                                <a href="#" data-toggle="tooltip" title='<%# "Contact: " + Eval("contact_num") %>'>
                                    <asp:Label ID="Label_Supplier" runat="server" Text='<%# Eval("name") %>' CssClass="text-bold"></asp:Label>
                                </a>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField ItemStyle-Width="10%" HeaderText="Ranking">

                            <ItemTemplate>
                                <asp:Label ID="Label_rank" runat="server" Text='<%# Eval("rank") %>' CssClass="text-bold"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField ItemStyle-Width="10%" HeaderText="Unit Price">

                            <ItemTemplate>
                                <asp:Label ID="Label_price" runat="server" Text='<%# Eval("price") %>' CssClass="text-bold"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField ItemStyle-Width="10%" HeaderText="Add to Cart"><%-- HeaderStyle-CssClass="text-center-impt">--%>

                            <ItemTemplate>
                                <asp:Button ID="add" runat="server" Text="Add"
                                    OnClick="MakeOrder"
                                    CssClass="btn btn-primary" />
                            </ItemTemplate>
                        </asp:TemplateField>



                    </Columns>
                </asp:GridView>

            </div>



            <div class="panel panel-primary">
                <!-- Default panel contents -->
                <div class="panel-heading">
                    Items in Cart
                </div>
                <div class="panel-body">

                    <asp:Label ID="lblResult" runat="server" class="btn btn-block alert-success" Visible="false"></asp:Label>
                    <asp:Label ID="lblduplicate" runat="server" class="btn btn-block alert-danger" Visible="false"></asp:Label>
                </div>
                <br />

                <%-- Detail GridView--%>
                <div class="table-responsive">

                    <asp:GridView ID="GridView2" runat="server"
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
                            </asp:TemplateField>

                            <asp:TemplateField ItemStyle-Width="10%" HeaderText="Supplier Name"><%-- HeaderStyle-CssClass="text-center-impt">--%>

                                <ItemTemplate>
                                    <asp:Label ID="Label_supplier" runat="server" Text='<%# Eval("SupplierName") %>' CssClass="text-bold"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ItemStyle-Width="10%" HeaderText="Price">

                                <ItemTemplate>
                                    <asp:Label ID="Label_price" runat="server" Text='<%# Eval("Price") %>' CssClass="text-bold"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField ItemStyle-Width="10%"><%-- HeaderStyle-CssClass="text-center-impt">--%>

                                <ItemTemplate>
                                    <asp:Button ID="remove" runat="server" Text="Remove"
                                        OnClick="ButtonRemove_Click"
                                        CssClass="btn btn-danger" />
                                </ItemTemplate>
                            </asp:TemplateField>



                        </Columns>
                    </asp:GridView>

                     <div class="panel-footer"><asp:Button ID="next" runat="server" Text="Next Step" Visible="false" OnClick="Purchase"
                                      
                                        CssClass="btn btn-primary" /></div>
                    

                </div>

            </div>
        </div>
    </div>

    <script>
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
        });
    </script>


</asp:Content>
