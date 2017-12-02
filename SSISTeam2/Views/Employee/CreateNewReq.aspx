<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateNewReq.aspx.cs" Inherits="SSISTeam2.Views.Employee.CreateNewReq" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="cat_id" DataSourceID="SqlDataSource1" >
            <Columns>
                <asp:TemplateField>
          <ItemTemplate><%#Container.DataItemIndex+1 %></ItemTemplate>
        </asp:TemplateField>
                <asp:BoundField DataField="cat_id" HeaderText="cat_id" InsertVisible="False" ReadOnly="True" SortExpression="cat_id" />
                <asp:BoundField DataField="cat_name" HeaderText="cat_name" SortExpression="cat_name" />
                <asp:BoundField DataField="deleted" HeaderText="deleted" SortExpression="deleted" />
            </Columns>
 
        </asp:GridView>
        
<asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false" OnRowDataBound="OnRowDataBound">
    <Columns>
        
        
        <asp:TemplateField HeaderText = "Category">
            <ItemTemplate>
                <asp:Label ID="lblCategory" runat="server" Text='<%# Eval("cat_name") %>' Visible = "false" />
                <asp:DropDownList ID="ddlCategory" runat="server">
                </asp:DropDownList>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SSISConnectionString2 %>" SelectCommand="SELECT * FROM [Category]"></asp:SqlDataSource>
    </div>
    </form>
</body>
</html>
