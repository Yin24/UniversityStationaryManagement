<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DEMO_ViewAllPending.aspx.cs" Inherits="SSISTeam2.Views.DepartmentHead.DEMO_ViewAllPending" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="lblInfo" runat="server" Text=""></asp:Label>

        <asp:GridView ID="gvPendings" runat="server" AutoGenerateColumns="False" OnRowCommand="gvPendings_RowCommand">
            <Columns>
                <asp:TemplateField HeaderText="No.">
                    <ItemTemplate>
                        <asp:Label ID="lblNum" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Employee">
                    <ItemTemplate>
                        <asp:Label ID="lblEmployee" runat="server" Text='<%# Eval("Username") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Date">
                    <ItemTemplate>
                        <asp:Label ID="lblDate" runat="server" Text='<%# Eval("Date") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Reason">
                    <ItemTemplate>
                        <asp:Label ID="lblReason" runat="server" Text='<%# Eval("Reason") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:ButtonField ButtonType="Button" CommandName="Select" HeaderText="View" ShowHeader="True" Text="Details" />
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
