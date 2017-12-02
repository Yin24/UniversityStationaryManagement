<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeBehind="ViewCatalogue.aspx.cs" Inherits="SSISTeam2.Views.Employee.ViewCat" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead"
    runat="server">
    <title>Stationery Catalogue</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">
    <asp:Panel ID="Panel1" runat="server" DefaultButton="Button1">
        <div class="table-responsive">
            <div class="panel-heading">
                <h3>View Stationery Catalogue</h3>
            </div>
        </div>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="Search_Click" />
        <br />
        <br />
        <asp:GridView ID="GridView1" runat="server"
            AutoGenerateColumns="False"
             OnDataBound="GridView_EditBooks_DataBound"
            AllowPaging="True"
            PageSize="5"
            HeaderStyle-CssClass="text-center-impt"
            CssClass="table table-responsive table-striped"
            GridLines="None"
            PagerStyle-HorizontalAlign="Center"
            PagerSettings-Position="TopAndBottom"
            EmptyDataText="No records Found"
            OnPageIndexChanging="GridView1_PageIndexChanging">

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
                <asp:BoundField DataField="categoryName" HeaderText="Category Name" SortExpression="categoryName" />
                <asp:BoundField DataField="Description" HeaderText="Item Description" SortExpression="Description" />
            </Columns>
        </asp:GridView>
        <asp:Button runat="server" Text="Back" CssClass="btn btn-primary" OnClick="btnBack_Click" />

    </asp:Panel>
</asp:Content>
