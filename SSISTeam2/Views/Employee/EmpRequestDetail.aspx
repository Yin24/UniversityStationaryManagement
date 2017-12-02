<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeBehind="EmpRequestDetail.aspx.cs" Inherits="SSISTeam2.Views.Employee.EmpRequestDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead"
    runat="server">
    <title>Your Request Details</title>
</asp:Content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1"
    runat="server">
    <div class="table-responsive ">
        <div class="panel-heading"><h3>View Request Details</h3></div>
    </div>
    <br />
    Request ID: <asp:Label ID="lblreqid" runat="server" Text='<%# Eval("request_id") %>'></asp:Label>
    <br />
    Date: <asp:Label ID="lblDate" runat="server" Text='<%# Eval("date_time") %>'></asp:Label>
   <br />
    Status:  <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("current_status") %>'></asp:Label>
   <br />
    Employee Name: <asp:Label ID="lblemployeename" runat="server" Text='<%# Eval("fullname") %>'></asp:Label>
   <br />
    Comments:  <asp:Label ID="lblcomment" runat="server" Text='<%# Eval("reason") %>'></asp:Label>
    <br />
    <br />
    <asp:GridView ID="GridView2" runat="server"
        GridLines="None"
        AutoGenerateColumns="false"
        AllowPaging="true"
        PageSize="10"
        HeaderStyle-CssClass="text-center-impt"
        CssClass="table table-responsive table-striped"
        PagerStyle-HorizontalAlign="Center" 
        PagerSettings-Position="TopAndBottom" 
         
        >

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

             <asp:TemplateField HeaderText="No.">
               <ItemTemplate>
                   <%#Container.DataItemIndex+1 %>
               </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Category" SortExpression="Category">
                        <ItemTemplate>
                            <asp:Label ID="lblcategory" runat="server" Text='<%# Eval("cat_name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
            <asp:TemplateField HeaderText="Description" SortExpression="Description">
                        <ItemTemplate>
                            <asp:Label ID="lbldescrip" runat="server" Text='<%# Eval("item_description") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
            <asp:TemplateField HeaderText="Unit of Measure" SortExpression="Unit of Measure">
                        <ItemTemplate>
                            <asp:Label ID="lblmeasure" runat="server" Text='<%# Eval("unit_of_measure") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
            <asp:TemplateField HeaderText="Quantity" SortExpression="Quantity">
                        <ItemTemplate>
                            <asp:Label ID="lblquantity" runat="server" Text='<%# Eval("orig_quantity") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
        </Columns>
       
    </asp:GridView>
     <br /> 
    <br />
     <asp:LinkButton ID="btnBack" runat="server" Text="Back"  CssClass="btn btn-primary" OnClick="btnBack_Click"/>
    <br />










</asp:content>