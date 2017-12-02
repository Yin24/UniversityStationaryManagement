<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeBehind="ViewDepartmentList.aspx.cs" Inherits="SSISTeam2.Views.StoreClerk.ViewDepartmentList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead"
    runat="server">
    <title>Department Listing</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">


    <div class="panel panel-default">
        <!-- Default panel contents -->
        <div class="panel-heading"><h3>Department List</h3></div>
        <div class="panel-body">
           
        <!-- Table -->

        <div class="table-responsive">

            <asp:GridView ID="GridView1" runat="server"
                AutoGenerateColumns="false"
                OnRowCommand="GridView1_RowCommand"
                AllowPaging="true"
                PageSize="10"
                HeaderStyle-CssClass="text-center-impt"
                CssClass="table table-responsive table-striped"
                GridLines="None"
                PagerStyle-HorizontalAlign="Center" 
                PagerSettings-Position="TopAndBottom">

                <PagerSettings Mode="NumericFirstLast" FirstPageText="First" LastPageText="Last" PreviousPageText="Previous" NextPageText="Next" />
                <PagerTemplate>
                    <asp:Button Text="First" runat="server" CommandName="Page" CommandArgument="First" CssClass="btn btn-default btn-sm" />
                    <asp:Button Text="Prev" runat="server" CommandName="Page" CommandArgument="Prev" CssClass="btn btn-default btn-sm" />
                    <asp:Button Text="Next" runat="server" CommandName="Page" CommandArgument="Next" CssClass="btn btn-default btn-sm" />
                    <asp:Button Text="Last" runat="server" CommandName="Page" CommandArgument="Last" CssClass="btn btn-default btn-sm" />
                </PagerTemplate>

                <Columns>
                     <asp:TemplateField   HeaderText="No.">
                   <ItemTemplate>
                       <%#Container.DataItemIndex+1 %>
                   </ItemTemplate>
                </asp:TemplateField>

                    <asp:TemplateField  HeaderText="Department Name">

                        <ItemTemplate>
                            <asp:Label ID="Label_Name" runat="server" Text='<%# Eval("name") %>' ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Representative Name">

                        <ItemTemplate>
                            <asp:Label ID="Label_RepUser" runat="server" Text='<%# Eval("fullname") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    
                    <asp:TemplateField  HeaderText="Collection Point">

                        <ItemTemplate>
                            <asp:Label ID="Label_CollectionPoint" runat="server" Text='<%# Eval("location") %>' ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                            
                <asp:TemplateField  >
                            <ItemTemplate>
                                <asp:LinkButton ID="details" runat="server" Text="Details"
                                    CommandName="view" CommandArgument='<%# Bind("dept_code") %>'
                                    CssClass="btn btn-info"/>
                            </ItemTemplate>
                </asp:TemplateField>
                  
                </Columns>
            </asp:GridView>

        </div>
             </div>
    </div>



</asp:Content>
