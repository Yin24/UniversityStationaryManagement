<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="MonthlyCheck.aspx.cs" Inherits="SSISTeam2.MonthlyCheck" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderHead"
    runat="server">
    <title>Stationery Stocktake</title>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="table-responsive ">
        <div class="panel-heading">
            <h3>Stationery Stocktake</h3>
        </div>
        <div class="panel-heading">
            <h4>
                <asp:Label ID="CheckLabel" runat="server" Text=""></asp:Label></h4>
        </div>

    </div>


    <asp:GridView ID="MonthlyCheckGV" runat="server"
        AutoGenerateColumns="false"
        AllowPaging="True"
        PageSize="5"
        HeaderStyle-CssClass="text-center-impt"
        CssClass="table table-responsive table-striped"
        GridLines="None"
        PagerStyle-HorizontalAlign="Center"
        PagerSettings-Position="TopAndBottom"
        OnDataBound="GridView_EditBooks_DataBound"
        OnPageIndexChanging="OnPageIndexChanging" ShowHeaderWhenEmpty="True" EmptyDataText="No records Found"
        OnDataBinding="MonthlyCheckGV_DataBinding">

        <PagerSettings Mode="NumericFirstLast" FirstPageText="First" LastPageText="Last" PreviousPageText="Previous" NextPageText="Next" />
        <PagerTemplate>
            <asp:Button Text="First" runat="server" CommandName="Page" CommandArgument="First" CssClass="btn btn-default btn-sm" />
            <asp:Button Text="Prev" runat="server" CommandName="Page" CommandArgument="Prev" CssClass="btn btn-default btn-sm" />

            <asp:DropDownList ID="DropDownList_JumpToPage" runat="server" OnSelectedIndexChanged="DropDownList_JumpToPage_SelectedIndexChanged" AutoPostBack="True" CssClass="btn btn-default btn-sm"></asp:DropDownList>

            <asp:Label ID="Paging_CurrentPage" Text="" runat="server"><%# " / " + MonthlyCheckGV.PageCount %></asp:Label>
            <asp:Button Text="Next" runat="server" CommandName="Page" CommandArgument="Next" CssClass="btn btn-default btn-sm" />
            <asp:Button Text="Last" runat="server" CommandName="Page" CommandArgument="Last" CssClass="btn btn-default btn-sm" />
        </PagerTemplate>

        <Columns>

            <asp:TemplateField HeaderText="No.">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>' ID="rowIndex"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Item Description">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("description") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Category">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("catName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Item Quantity">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("currentQuantity") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Actual Quantity">
                <ItemTemplate>
                    <asp:TextBox runat="server" Text='<%# Eval("actualQuantity") %>' OnTextChanged="MonthlyCheckGV_OnTextChange" AutoPostBack="True"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>

        <PagerSettings Position="TopAndBottom"></PagerSettings>

        <PagerStyle HorizontalAlign="Center"></PagerStyle>
    </asp:GridView>

    <asp:Button ID="nextBtn" runat="server" Text="Next" OnClick="nextBtn_Click" CssClass="btn btn-primary" />
</asp:Content>
