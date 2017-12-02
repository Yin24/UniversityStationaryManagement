<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeBehind="ViewSupplierList.aspx.cs" EnableEventValidation="false" Inherits="SSISTeam2.Views.StoreClerk.ViewSupplierList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead"
    runat="server">
    <title>Supplier Listing</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">
    <asp:Panel ID="Panel1" runat="server" DefaultButton ="Button1">
    <div class="panel panel-default">
        <!-- Default panel contents -->
        <div class="panel-heading">
            <h3>Supplier List</h3>
        </div>
        <div class="panel-body">
            
                <asp:Button ID="Button2" runat="server" Text="Add New Supplier"
                    CssClass="btn btn-primary" OnClick="Add_New_Supplier_Click" />
            <br /><br />

        <table>
            <tr>
                <td>
                    <div class="input-group">
                        <asp:TextBox ID="TextBox2" class="form-control" runat="server"></asp:TextBox>
                        <span class="input-group-addon">
                            <i class="fa fa-search"></i>
                        </span>
                    </div>
                </td>
                <td>
                    <asp:Button ID="Button1" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="Search_Click" />
                </td>
              
            </tr>
        </table>  
    </div>

        <!-- Table -->

        <div class="table-responsive">

            <asp:GridView ID="GridView1" runat="server"
                AutoGenerateColumns="false"
                AllowPaging="true"
                PageSize="3"
                HeaderStyle-CssClass="text-center-impt"
                CssClass="table table-responsive table-striped"
                GridLines="None"
                 PagerStyle-HorizontalAlign="Center" PagerSettings-Position="TopAndBottom" EmptyDataText="No records Found">
                 

                <PagerSettings Mode="NumericFirstLast" FirstPageText="First" LastPageText="Last" PreviousPageText="Previous" NextPageText="Next" />
                <PagerTemplate>
                    <asp:Button Text="First" runat="server" CommandName="Page" CommandArgument="First" CssClass="btn btn-default btn-sm" />
                    <asp:Button Text="Prev" runat="server" CommandName="Page" CommandArgument="Prev" CssClass="btn btn-default btn-sm" />
                    <asp:Button Text="Next" runat="server" CommandName="Page" CommandArgument="Next" CssClass="btn btn-default btn-sm" />
                    <asp:Button Text="Last" runat="server" CommandName="Page" CommandArgument="Last" CssClass="btn btn-default btn-sm" />
                </PagerTemplate>

                <Columns>
                     <asp:TemplateField HeaderText="No.">
                   <ItemTemplate>
                       <%#Container.DataItemIndex+1 %>
                   </ItemTemplate>
                </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="Supplier ID"><%-- HeaderStyle-CssClass="text-center-impt">--%>

                        <ItemTemplate>
                            <asp:Label ID="Label_SupplierId" runat="server" Text='<%# Eval("supplier_id") %>' CssClass="text-bold"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="Supplier Name"><%-- HeaderStyle-CssClass="text-center-impt">--%>

                        <ItemTemplate>
                            <asp:Label ID="Label_SupplierName" runat="server" Text='<%# Eval("name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="Contact Name"><%-- HeaderStyle-CssClass="text-center-impt">--%>

                        <ItemTemplate>
                            <asp:Label ID="Label_ContactName" runat="server" Text='<%# Eval("contact_name") %>' CssClass="text-bold"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="Contact Number"><%-- HeaderStyle-CssClass="text-center-impt">--%>

                        <ItemTemplate>
                            <asp:Label ID="Label_ContactNum" runat="server" Text='<%# Eval("contact_num") %>' CssClass="text-bold"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="Fax Number"><%-- HeaderStyle-CssClass="text-center-impt">--%>

                        <ItemTemplate>
                            <asp:Label ID="Label_FaxNum" runat="server" Text='<%# Eval("fax_num") %>' CssClass="text-bold"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="Address"><%-- HeaderStyle-CssClass="text-center-impt">--%>

                        <ItemTemplate>
                            <asp:Label ID="Label_Address" runat="server" Text='<%# Eval("address") %>' CssClass="text-bold"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="GST Registration Number"><%-- HeaderStyle-CssClass="text-center-impt">--%>

                        <ItemTemplate>
                            <asp:Label ID="Label_GstRegNum" runat="server" Text='<%# Eval("gst_reg_num") %>' CssClass="text-bold"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField ItemStyle-Width="10%"><%-- HeaderStyle-CssClass="text-center-impt">--%>

                        <ItemTemplate>
                            <asp:Button ID="edit" runat="server" Text="Edit"
                                CssClass="btn btn-primary" OnClick="Edit_Supplier" />
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField ItemStyle-Width="10%"><%-- HeaderStyle-CssClass="text-center-impt">--%>

                        <ItemTemplate>
                            <asp:Button ID="delete" runat="server" Text="Delete"
                                CssClass="btn btn-danger" 
                                OnClientClick="return confirm('Are you sure you want to cancel this request?');"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

        </div>

    </div>
    </asp:Panel>
</asp:Content>
