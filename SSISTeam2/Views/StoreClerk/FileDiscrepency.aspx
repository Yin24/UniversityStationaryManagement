<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="FileDiscrepency.aspx.cs" Inherits="SSISTeam2.FileDiscrepency" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderHead"
    runat="server">
    <title>File Discrepancy</title>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="table-responsive ">
        <div class="panel-heading"><h3>File Discrepancy</h3></div>
    </div>
    <asp:GridView ID="FileDiscrepencyGV" runat="server" 
        AutoGenerateColumns="False"
        GridLines="None"
        AllowPaging="true"
        PageSize="10"
        HeaderStyle-CssClass="text-center-impt"
        CssClass="table table-responsive table-striped"
        PagerStyle-HorizontalAlign="Center" 
        PagerSettings-Position="TopAndBottom"
        >
        <Columns>
            <asp:TemplateField HeaderText="No.">
                <ItemTemplate>
                    <asp:Label ID="rowIndex" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Item Name">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("description") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Item Category">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("catName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Previous Quantity">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("currentQuantity") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Actual Quantity">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("actualQuantity") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Average Price">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("averagePrice", "{0:c}") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Reason">
                <ItemTemplate>
                    <asp:TextBox ID="reasonTB" runat="server" Text='<%# Eval("reason") %>' OnTextChanged="reasonTB_TextChanged" AutoPostBack="true"></asp:TextBox>
                    </br>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red" ErrorMessage="Please specify reason for discrepancy"
                            ControlToValidate="reasonTB" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:Label ID="lblWarning" Text="" runat="server" />
    <asp:Button ID="ConfirmBtn" runat="server" Text="Confirm" OnClick="ConfirmBtn_Click" CssClass="btn btn-primary"/>
</asp:Content>
