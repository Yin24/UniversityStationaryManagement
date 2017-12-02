<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeBehind="ViewCat.aspx.cs" Inherits="SSISTeam2.Views.Employee.ViewCat" %>

<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1"
    runat="server">
    
<div class="panel panel-default">
        <!-- Default panel contents -->
        <div class="panel-heading"><h3>View Catalogue</h3></div>
        <div class="panel-body">
            <p>
                <form class="form-inline">
                    <div class="form-group">
                         <label for="search">Search</label>
                         <input type="search" class="form-control" id="txt_search">
                    </div>
               </form>
            </p>
        </div>

        <!-- Table -->

        <div class="table-responsive">

            <asp:GridView ID="GridView1" runat="server"
                AutoGenerateColumns="False"
                AllowPaging="True"
                HeaderStyle-CssClass="text-center-impt"
                CssClass="table table-responsive table-striped"
                GridLines="None"
                PagerStyle-HorizontalAlign="Center" PagerSettings-Position="TopAndBottom" DataSourceID="SqlDataSource1">

<HeaderStyle CssClass="text-center-impt"></HeaderStyle>

                <PagerSettings Mode="NumericFirstLast" FirstPageText="First" LastPageText="Last" PreviousPageText="Previous" NextPageText="Next" />

<PagerStyle HorizontalAlign="Center"></PagerStyle>
                <PagerTemplate>
                    <asp:Button Text="First" runat="server" CommandName="Page" CommandArgument="First" CssClass="btn btn-default btn-sm" />
                    <asp:Button Text="Prev" runat="server" CommandName="Page" CommandArgument="Prev" CssClass="btn btn-default btn-sm" />
                    <asp:Button Text="Next" runat="server" CommandName="Page" CommandArgument="Next" CssClass="btn btn-default btn-sm" />
                    <asp:Button Text="Last" runat="server" CommandName="Page" CommandArgument="Last" CssClass="btn btn-default btn-sm" />
                </PagerTemplate>

                <Columns>

                    

                    <asp:BoundField DataField="cat_name" HeaderText="cat_name" SortExpression="cat_name" />
                    <asp:BoundField DataField="item_description" HeaderText="item_description" SortExpression="item_description" />
                </Columns>
            </asp:GridView>

            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SSISConnectionString %>" SelectCommand="SELECT Category.cat_name, Stock_Inventory.item_description FROM Category INNER JOIN Stock_Inventory ON Category.cat_id = Stock_Inventory.cat_id"></asp:SqlDataSource>

        </div>

    </div>

</asp:content>
