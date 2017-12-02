<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ViewAllAdjustment.aspx.cs" Inherits="SSISTeam2.ViewAllAdjustment" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderHead"
    runat="server">
    <title>Adjustment Listing</title>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="table-responsive ">
        <div class="panel-heading"><h3>View All Pending Adjustment</h3></div>
    </div>
    <asp:GridView ID="ViewAdjustmentGV" runat="server" 
        AutoGenerateColumns="False"
        GridLines="None"
        AllowPaging="true"
        PageSize="10"
        HeaderStyle-CssClass="text-center-impt"
        CssClass="table table-responsive table-striped"
        PagerStyle-HorizontalAlign="Center" 
        PagerSettings-Position="TopAndBottom" OnPageIndexChanging="ViewAdjustmentGV_PageIndexChanging" 
        >
        <Columns>
            <asp:TemplateField HeaderText="No.">
                <ItemTemplate>
                    <asp:Label ID="rowIndex" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>                    
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Clerk">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("clerk") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Status">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("status") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Date">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("date", "{0:MMMM d, yyyy}") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Highest Single Cost of Adjustment" ItemStyle-Width="25%">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("highestCost") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="View Details">
                <ItemTemplate>
                    <asp:Button ID="detailBtn" runat="server" Text="Details" OnClick="detailBtn_Click" CssClass="btn btn-primary"/>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:GridView ID="GridView1" runat="server"></asp:GridView>
</asp:Content>
