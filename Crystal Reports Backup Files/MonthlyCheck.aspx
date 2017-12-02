<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="MonthlyCheck.aspx.cs" Inherits="SSISTeam2.MonthlyCheck" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p>
        Monthly Stocktake
    </p>
        Sort By:
        <asp:DropDownList ID="SortDDL" runat="server">
            <asp:ListItem>Category</asp:ListItem>
            <asp:ListItem Value="Name">Item Name</asp:ListItem>
            <asp:ListItem>Quantity</asp:ListItem>
            <asp:ListItem Value="Accounted">Accounted For</asp:ListItem>
        </asp:DropDownList>
        Date:
        <asp:TextBox ID="DateTB" runat="server"></asp:TextBox>
        <br />
    <asp:GridView ID="MonthlyCheckGV" runat="server" 
        AutoGenerateColumns="false" 
        AllowPaging="True" 
        PageSize="2" 
        PagerStyle-HorizontalAlign="Center" 
        PagerSettings-Position="TopAndBottom" 
        OnPageIndexChanging="MonthlyCheckGV_PageIndexChanging">
        
<%--        <PagerSettings Mode="NumericFirstLast" FirstPageText="First" LastPageText="Last" PreviousPageText="Previous" NextPageText="Next" />
        <PagerTemplate>
            <asp:Button Text="First" runat="server" CommandName="Page" CommandArgument="First" CssClass="btn btn-default btn-sm" />
            <asp:Button Text="Prev" runat="server" CommandName="Page" CommandArgument="Prev" CssClass="btn btn-default btn-sm" />
            
            <asp:DropDownList ID="DropDownList_JumpToPage" runat="server" OnSelectedIndexChanged="DropDownList_JumpToPage_SelectedIndexChanged" AutoPostBack="True" CssClass="btn btn-default btn-sm"></asp:DropDownList>

            <asp:Label ID="Paging_CurrentPage" Text="" runat="server"><%# " / " + GridView_EditBooks.PageCount %></asp:Label>
            <asp:Button Text="Next" runat="server" CommandName="Page" CommandArgument="Next" CssClass="btn btn-default btn-sm" />
            <asp:Button Text="Last" runat="server" CommandName="Page" CommandArgument="Last" CssClass="btn btn-default btn-sm" />
        </PagerTemplate>--%>

        <Columns>

            <asp:TemplateField HeaderText="No.">
                <ItemTemplate>
                    <%# Container.DataItemIndex + 1 %>
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
    
    <asp:Button ID="nextBtn" runat="server" Text="Next" OnClick="nextBtn_Click" /><asp:Label ID="testLabel" runat="server" Text="Label"></asp:Label>
</asp:Content>
