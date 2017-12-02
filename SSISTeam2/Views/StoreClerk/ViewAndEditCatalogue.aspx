<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="ViewAndEditCatalogue.aspx.cs" Inherits="SSISTeam2.Views.StoreClerk.ViewCatalogueForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead"
    runat="server">
    <title>Edit Catalogue</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">
    <asp:Panel ID="Panel1" runat="server" DefaultButton ="Button1">
         <div class="panel panel-default">
        <!-- Default panel contents -->
        <div class="panel-heading"><h3>Maintain Store Catalogue</h3></div>
        <div class="panel-body">
           <p></p>
            <table>
                
                <tr>
                    
                    <td>
                        <div class="input-group">
                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
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
              <p></p> 
             <div class="table-responsive">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" AllowPaging="True" OnDataBound="GridView_EditBooks_DataBound" PageSize="10"
                HeaderStyle-CssClass="text-center-impt"
                CssClass="table table-responsive table-striped"
                GridLines="None"
                PagerStyle-HorizontalAlign="Center" PagerSettings-Position="TopAndBottom" OnPageIndexChanging="OnPageIndexChanging" OnRowEditing="GridView1_RowEditing" OnRowUpdating="OnRowUpdating" OnRowCancelingEdit="OnRowCancelingEdit" OnRowDeleting="OnRowDeleting">
                <emptydatarowstyle forecolor="Red" />
<HeaderStyle CssClass="text-center-impt"></HeaderStyle>

                <PagerSettings Mode="NumericFirstLast" FirstPageText="First" LastPageText="Last" PreviousPageText="Previous" NextPageText="Next"/>

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
                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="Item Number"><%-- HeaderStyle-CssClass="text-center-impt">--%>
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("itemNumber") %>' CssClass="text-bold" Width="70px"></asp:Label>
                        </ItemTemplate>

<ItemStyle Width="10%"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="Category Name"><%-- HeaderStyle-CssClass="text-center-impt">--%>
                       <EditItemTemplate>
                            <asp:DropDownList ID="DropDownList1" Visible="false" runat="server">
                            </asp:DropDownList>
                            <%--<asp:TextBox ID="TextBox1" runat="server" Text='<%# Eval("categoryName") %>' BackColor="Transparent" Height="30px" Width="100px"></asp:TextBox>--%>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Category Name is required" ControlToValidate="TextBox1" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                       </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("categoryName") %>' CssClass="text-bold" Height="20px" Width="100px"></asp:Label>
                            <%--<asp:DropDownList ID="DropDownList2" runat="server">
                            </asp:DropDownList>--%>
                        </ItemTemplate><ItemStyle Width="10%">
                        </ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="Description"><%-- HeaderStyle-CssClass="text-center-impt">--%>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Eval("Description") %>' BackColor="Transparent" Height="30px" Width="200px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Description is required" ControlToValidate="TextBox2" ForeColor="Red"></asp:RequiredFieldValidator>
                        </EditItemTemplate>
                        <ItemTemplate>                     
                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("Description") %>' CssClass="text-bold" Width="250px"></asp:Label>
                        </ItemTemplate>

<ItemStyle Width="10%"></ItemStyle>
                    </asp:TemplateField>
                     <asp:TemplateField ItemStyle-Width="10%" HeaderText="Current Quantity"><%-- HeaderStyle-CssClass="text-center-impt">--%>
                        <ItemTemplate>
                            <asp:Label ID="Label7" runat="server" Text='<%# Eval("CQ") %>' CssClass="text-bold" Width="150px"></asp:Label>
                        </ItemTemplate>
<ItemStyle Width="10%"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="Reorder Level"><%-- HeaderStyle-CssClass="text-center-impt">--%>
                        
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox3" runat="server" Text='<%# Eval("RL") %>' BackColor="Transparent" Height="30px" Width="50px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Reorder Level is required" ControlToValidate="TextBox3" ForeColor="Red"></asp:RequiredFieldValidator>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label4" runat="server" Text='<%# Eval("RL") %>' CssClass="text-bold" Width="150px"></asp:Label>
                        </ItemTemplate>

<ItemStyle Width="10%"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="Reorder Quantity"><%-- HeaderStyle-CssClass="text-center-impt">--%>
                        
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox4" runat="server" Text='<%# Eval("RQ") %>' BackColor="Transparent" Height="30px" Width="50px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Reorder Quantity is required" ControlToValidate="TextBox4" ForeColor="Red"></asp:RequiredFieldValidator>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label5" runat="server" Text='<%# Eval("RQ")%>' CssClass="text-bold"></asp:Label>
                        </ItemTemplate>
                        
<ItemStyle Width="10%"></ItemStyle>
                    </asp:TemplateField>
                     <asp:TemplateField ItemStyle-Width="10%" HeaderText="Unit of Measure"><%-- HeaderStyle-CssClass="text-center-impt">--%>
                        
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox5" runat="server" Text='<%# Eval("UoM") %>' BackColor="Transparent" Height="30px" Width="70px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Unit Of Measure is required" ControlToValidate="TextBox5" ForeColor="Red"></asp:RequiredFieldValidator>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label6" runat="server" Text='<%# Eval("UoM")%>' CssClass="text-bold"></asp:Label>
                        </ItemTemplate>

<ItemStyle Width="10%"></ItemStyle>
                    </asp:TemplateField>
                    
                     <asp:TemplateField ItemStyle-Width="50%">  
                    <ItemTemplate >  
                        <asp:Button ID="btn_Edit" runat="server" Text="Edit" CommandName="Edit" CssClass="btn btn-primary"/>
                        <asp:Button ID="btn_Delete" runat="server" Text="Delete" CommandName="Delete" CssClass="btn btn-danger"/>    
                    </ItemTemplate>  
                    <EditItemTemplate>  
                        <asp:Button ID="btn_Update" runat="server" Text="Update" CommandName="Update" CssClass="btn btn-primary"/>  
                        <asp:Button ID="btn_Cancel" runat="server" Text="Cancel" CommandName="Cancel" CssClass="btn btn-primary"/>  
                    </EditItemTemplate>  

<ItemStyle Width="50%"></ItemStyle>
                </asp:TemplateField>      
                </Columns>
                
            </asp:GridView>
                 <asp:GridView ID="GridView2" runat="server"></asp:GridView>
            </div>

        </div>
        </div>

        <!-- Table -->

       
    </asp:Panel>
   
   
</asp:Content>
