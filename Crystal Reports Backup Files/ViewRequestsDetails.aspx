<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/MasterPage.master" CodeBehind="ViewRequestsDetails.aspx.cs" Inherits="SSISTeam2.Views.Employee.ViewRequestsDetails" %>

<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1"
    runat="server">
    
<div class="panel panel-default">
        <!-- Default panel contents -->
        <div class="panel-heading"><h3>View Details of Requests</h3></div>
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

                    

                    <asp:BoundField DataField="username" HeaderText="username" SortExpression="username" />
                    <asp:BoundField DataField="reason" HeaderText="reason" SortExpression="reason" />
                    <asp:BoundField DataField="quantity" HeaderText="quantity" SortExpression="quantity" />
                    <asp:BoundField DataField="current_status" HeaderText="current_status" SortExpression="current_status" />
                    <asp:BoundField DataField="date_time" HeaderText="date_time" SortExpression="date_time" />
                    <asp:BoundField DataField="unit_of_measure" HeaderText="unit_of_measure" SortExpression="unit_of_measure" />
                    <asp:BoundField DataField="item_description" HeaderText="item_description" SortExpression="item_description" />

                    

                </Columns>
            </asp:GridView>

          
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SSISConnectionString %>" SelectCommand="SELECT Requests.username, Requests.reason, Request_Event.quantity, Requests.current_status, Request_Event.date_time, Stock_Inventory.unit_of_measure, Stock_Inventory.item_description FROM Request_Details INNER JOIN Request_Event ON Request_Details.request_detail_id = Request_Event.request_detail_id INNER JOIN Requests ON Request_Details.request_id = Requests.request_id INNER JOIN Stock_Inventory ON Request_Details.item_code = Stock_Inventory.item_code"></asp:SqlDataSource>

          
        </div>

    </div>

</asp:content>
