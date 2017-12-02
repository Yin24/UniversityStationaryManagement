<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DEMO_ApproveRequest.aspx.cs" Inherits="SSISTeam2.DEMO_ViewRequestDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="lblRequestId" runat="server" Text="Id: "></asp:Label>
        <asp:Label ID="lblEmployeeName" runat="server" Text="Employee: "></asp:Label>
        <asp:Label ID="lblReason" runat="server" Text="Reason: "></asp:Label>

        <asp:Label ID="lblInfo" runat="server" Text=""></asp:Label>

        <asp:GridView ID="gvItems" runat="server">
            <Columns>
                <asp:TemplateField HeaderText="Item">
                    <ItemTemplate>
                        <asp:Label ID="lblItemDescription" runat="server" Text='<%# Eval("Key") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Quantity">
                    <ItemTemplate>
                        <asp:Label ID="lblItemQuantity" runat="server" Text='<%# Eval("Value") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <asp:Panel ID="panelApproval" runat="server">
            <asp:Button ID="btnApprove" runat="server" Text="Approve request" OnClick="btnApprove_Click" />

            <asp:Button ID="btnReject" runat="server" Text="Reject request" OnClick="btnReject_Click" />
        </asp:Panel>
    </div>
    </form>
</body>
</html>
