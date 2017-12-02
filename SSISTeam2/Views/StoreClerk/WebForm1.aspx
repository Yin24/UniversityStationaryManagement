<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeBehind="WebForm1.aspx.cs" Inherits="SSISTeam2.Views.StoreClerk.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead"
    runat="server">
    <title>Tender List</title>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">
    <asp:Panel ID="panSearch" runat="server" DefaultButton="Button1" Width="100%">

        <div class="panel panel-default">
            <!-- Default panel contents -->
            <div class="panel-heading">
                <h3>Tender List</h3>
            </div>
            <div class="panel-body">
                <%--  <p></p>
            <table>
                
                <tr>
                    
                    <td>
                        <div class="input-group">
                            <asp:TextBox ID="TextBox1" class="form-control" runat="server"></asp:TextBox>
                            <span class="input-group-addon">
                                <i class="fa fa-search"></i>
                            </span>
                            
                        </div>  
                    </td>
                    <td>
                        <asp:Button ID="Button1" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="Search_Click" />
                    </td>
                    
                   
                    <td><div style="text-align:left;position: absolute; margin: 0 auto; left: 1000px; right: 0; bottom: 362px;"><asp:Button ID="add" runat="server" Text="Add New Tender" CssClass="btn btn-primary" OnClick="AddNewTender_Click" /> </div></td>
                </tr>
            </table>
              <p></p> --%>

                <div class="panel-body">
                    <asp:Label ID="lblNoData" runat="server" Text="There are no items that have low stocks." CssClass="alert alert-warning" Visible="false"></asp:Label>
                    <asp:Button ID="add" runat="server" Text="Add New Item"
                        CssClass="btn btn-primary" OnClick="AddNewTender_Click" />
                    <br />
                    <br />

                    <table>
                        <tr>
                            <td>
                                <div class="input-group">
                                    <asp:TextBox ID="TextBox1" class="form-control" runat="server"></asp:TextBox>
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
         
             <div class="table-responsive">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnDataBound="GridView_EditBooks_DataBound" AllowPaging="True" PageSize="10"
                HeaderStyle-CssClass="text-center-impt"
                CssClass="table table-responsive table-striped"
                GridLines="None"
                PagerStyle-HorizontalAlign="Center" 
                PagerSettings-Position="TopAndBottom" 
                OnPageIndexChanging="OnPageIndexChanging" 
                ShowHeaderWhenEmpty="True" 
                OnRowDeleting="OnRowDeleting" 
                EmptyDataText="No records Found">
                <emptydatarowstyle forecolor="Red"/>
<HeaderStyle CssClass="text-center-impt"></HeaderStyle>

                        <PagerSettings Mode="NumericFirstLast" FirstPageText="First" LastPageText="Last" PreviousPageText="Previous" NextPageText="Next" />

                        <PagerStyle HorizontalAlign="Center"></PagerStyle>
                        <PagerTemplate>
                            <asp:Button Text="First" runat="server" CommandName="Page" CommandArgument="First" CssClass="btn btn-default btn-sm" />
                            <asp:Button Text="Prev" runat="server" CommandName="Page" CommandArgument="Prev" CssClass="btn btn-default btn-sm" />
                            <asp:DropDownList ID="DropDownList_JumpToPage" runat="server" OnSelectedIndexChanged="DropDownList_JumpToPage_SelectedIndexChanged" AutoPostBack="True" CssClass="btn btn-default btn-sm"></asp:DropDownList>
                            <asp:Label ID="Paging_CurrentPage" Text="" runat="server"><%# " / " + GridView1.PageCount %></asp:Label>
                            <asp:Button Text="Next" runat="server" CommandName="Page" CommandArgument="Next" CssClass="btn btn-default btn-sm" />
                            <asp:Button Text="Last" runat="server" CommandName="Page" CommandArgument="Last" CssClass="btn btn-default btn-sm" />
                        </PagerTemplate>
                        <Columns>
                           
                             <asp:TemplateField HeaderText="No.">
                                <ItemTemplate>
                                   <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                             </asp:TemplateField>
                             <asp:TemplateField ItemStyle-Width="10%" HeaderText="No">
                                <EditItemTemplate>
                                    <asp:Label ID="Label11" runat="server" Text='<%# Eval("tender_year_id") %>'></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label11" runat="server" Text='<%# Eval("tender_year_id") %>'></asp:Label>
                                </ItemTemplate>

                                <ItemStyle Width="10%"></ItemStyle>
                            </asp:TemplateField>

                            <asp:TemplateField ItemStyle-Width="10%" HeaderText="No">
                                <EditItemTemplate>
                                    <asp:Label ID="Label10" runat="server" Text='<%# Eval("tender_id") %>'></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label10" runat="server" Text='<%# Eval("tender_id") %>'></asp:Label>
                                </ItemTemplate>

                                <ItemStyle Width="10%"></ItemStyle>
                            </asp:TemplateField>

                            <asp:TemplateField ItemStyle-Width="10%" HeaderText="No">
                                <EditItemTemplate>
                                    <asp:Label ID="Label8" runat="server" Text='<%# Eval("item_code") %>'></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label8" runat="server" Text='<%# Eval("item_code") %>'></asp:Label>
                                </ItemTemplate>

                                <ItemStyle Width="10%"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="10%" HeaderText="No">
                                <EditItemTemplate>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label9" runat="server" Text='<%# Eval("supplier_id") %>'></asp:Label>
                                </ItemTemplate>

                                <ItemStyle Width="10%"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="10%" HeaderText="Supplier Name"><%-- HeaderStyle-CssClass="text-center-impt">--%>
                                <EditItemTemplate>
                                    <%--<asp:TextBox ID="TextBox1" runat="server" Text='<%# Eval("name") %>' BackColor="Transparent" Height="30px" Width="250px"></asp:TextBox>--%>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Supplier Name is required" ControlToValidate="TextBox1" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                   <%-- <asp:DropDownList ID="DropDownList1" runat="server" CssClass="auto-style1">
                                    </asp:DropDownList>--%>
                                    <asp:Label ID="Labe20" runat="server" Text='<%# Eval("name") %>' CssClass="text-bold"></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("name") %>' CssClass="text-bold" Width="250px"></asp:Label>
                                </ItemTemplate>

                                <ItemStyle Width="10%"></ItemStyle>
                            </asp:TemplateField>

                            <asp:TemplateField ItemStyle-Width="10%" HeaderText="Item Description"><%-- HeaderStyle-CssClass="text-center-impt">--%>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Eval("item_description") %>' BackColor="Transparent" Height="30px" Width="250px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Item Description is required" ControlToValidate="TextBox3" ForeColor="Red"></asp:RequiredFieldValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("item_description") %>' CssClass="text-bold" Height="20px" Width="250px"></asp:Label>
                                </ItemTemplate>

                                <ItemStyle Width="10%"></ItemStyle>
                            </asp:TemplateField>



                            <asp:TemplateField ItemStyle-Width="10%" HeaderText="Price"><%-- HeaderStyle-CssClass="text-center-impt">--%>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox5" runat="server" Text='<%# Eval("price") %>' BackColor="Transparent" Height="30px" Width="150px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Price is required" ControlToValidate="TextBox5" ForeColor="Red"></asp:RequiredFieldValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label5" runat="server" Text='<%# Eval("price") %>' CssClass="text-bold"></asp:Label>
                                </ItemTemplate>

                                <ItemStyle Width="10%"></ItemStyle>
                            </asp:TemplateField>

                            <asp:TemplateField ItemStyle-Width="10%" HeaderText="Tender Date"><%-- HeaderStyle-CssClass="text-center-impt">--%>
                                <EditItemTemplate>
                                    <%--<asp:TextBox ID="TextBox6" runat="server" Text='<%# Eval("tender_date") %>' BackColor="Transparent" Height="30px" Width="150px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Tender Date is required" ControlToValidate="TextBox6" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                    <asp:Label ID="Label6" runat="server" Text='<%# Eval("tender_date", "{0:dd/MM/yyyy}") %>' CssClass="text-bold"></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label6" runat="server" Text='<%# Eval("tender_date", "{0:dd/MM/yyyy}") %>' CssClass="text-bold"></asp:Label>
                                </ItemTemplate>

<ItemStyle Width="10%"></ItemStyle>
                    </asp:TemplateField>
                    
                    
                     <asp:TemplateField ItemStyle-Width="10%">  
                    <ItemTemplate >  
                        <asp:Button ID="btn_Edit" runat="server" Text="Edit" CommandName="Edit" CssClass="btn btn-primary"/>
                       <%-- <asp:Button ID="btn_Delete" runat="server" Text="Delete" CommandName="Delete" CssClass="btn btn-primary"/>    --%>
                    </ItemTemplate>  
                    <EditItemTemplate>  
                        <asp:Button ID="btn_Update" runat="server" Text="Update" CommandName="Update" CssClass="btn btn-primary"/>  
                        <asp:Button ID="btn_Cancel" runat="server" Text="Cancel" CommandName="Cancel" CssClass="btn btn-primary"/>  
                    </EditItemTemplate>  

                                <ItemStyle Width="20%"></ItemStyle>
                            </asp:TemplateField>

                            <asp:TemplateField ItemStyle-Width="10%">
                                <EditItemTemplate>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="btn_Delete" runat="server" Text="Delete"
                                        CommandName="Delete" CommandArgument='<%# Bind("tender_id") %>'
                                        CssClass="btn btn-danger"
                                        OnClientClick="return confirm('Are you sure you want to delete this record?');" />
                                </ItemTemplate>

                                <ItemStyle Width="10%"></ItemStyle>
                            </asp:TemplateField>
                        </Columns>

                    </asp:GridView>

                </div>

            </div>
        </div>
        <!-- Table -->

    </asp:Panel>


</asp:Content>