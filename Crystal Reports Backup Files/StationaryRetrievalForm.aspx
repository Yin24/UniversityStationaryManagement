<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StationaryRetrievalForm.aspx.cs" Inherits="SSISTeam2.Views.StoreClerk.StationaryRetrievalForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" onrowdeleting="gridView_RowDeleting">
        <Columns>
        <asp:TemplateField HeaderText="No.">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
        </asp:TemplateField>
            <asp:BoundField DataField="item_description" HeaderText="Item Description" />
            <asp:BoundField DataField="quantity" HeaderText="Total Quantity" />
            <asp:BoundField DataField="dept_code" HeaderText="Department Code" />
            <asp:BoundField DataField="quantity" HeaderText="Quantity" />
       <asp:CommandField ShowDeleteButton="True"/>
           
        </Columns>
        </asp:GridView>
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label><asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
    
        <asp:GridView ID="GridView2" runat="server"></asp:GridView>
    </div>
    </form>
</body>
</html>
