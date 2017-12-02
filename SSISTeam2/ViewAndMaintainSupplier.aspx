<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewAndMaintainSupplier.aspx.cs" Inherits="SSISTeam2.ViewAndMaintainSupplier" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" Text="Search" OnClick="Button1_Click" />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"  AllowPaging="True" PageSize="3"
                HeaderStyle-CssClass="text-center-impt"
                CssClass="table table-responsive table-striped"
                GridLines="None"
                PagerStyle-HorizontalAlign="Center" PagerSettings-Position="TopAndBottom" DataKeyNames="supplier_id"  OnRowEditing="OnRowEditing" OnRowUpdating="OnRowUpdating" OnRowCancelingEdit="OnRowCancelingEdit" OnRowDeleting="OnRowDeleting">
                           <emptydatarowstyle forecolor="Red" />
<HeaderStyle CssClass="text-center-impt"></HeaderStyle>
           

                <PagerSettings Mode="NumericFirstLast" FirstPageText="First" LastPageText="Last" PreviousPageText="Previous" NextPageText="Next"/>

<PagerStyle HorizontalAlign="Center"></PagerStyle>
                <PagerTemplate>
                    <asp:Button Text="First" runat="server" CommandName="Page" CommandArgument="First" CssClass="btn btn-default btn-sm" />
                    <asp:Button Text="Prev" runat="server" CommandName="Page" CommandArgument="Prev" CssClass="btn btn-default btn-sm" />
                    <asp:Button Text="Next" runat="server" CommandName="Page" CommandArgument="Next" CssClass="btn btn-default btn-sm" />
                    <asp:Button Text="Last" runat="server" CommandName="Page" CommandArgument="Last" CssClass="btn btn-default btn-sm" />
                </PagerTemplate>
      

            <Columns>
          
                <asp:TemplateField HeaderText="Supplier ID" SortExpression="Supplier ID" >
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server"
                        Text='<%# Eval("supplier_id") %>' BackColor="Yellow" ReadOnly="True" ></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                         
                        <asp:Label ID="Label1" runat="server"
                        Text='<%# Eval("supplier_id") %>'  ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="GST Registration No" SortExpression="GST Registration No">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox2" runat="server"
                        Text='<%# Eval("gst_reg_num") %>' BackColor="Yellow"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                         <asp:Label ID="Label2" runat="server"  Text='<%# Bind("gst_reg_num") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="SupplierName" SortExpression="SupplierName">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox3" runat="server"
                        Text='<%# Eval("name") %>' BackColor="Yellow"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server"
                        Text='<%# Eval("name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Address" SortExpression="Address">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox4" runat="server" Text='<%# Eval("address") %>' BackColor="Yellow"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server"
                        Text='<%# Eval("address") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Contact Name" SortExpression="Contact Name">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox5" runat="server"
                        Text='<%# Eval("contact_name") %>' BackColor="Yellow"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label5" runat="server"
                        Text='<%# Eval("contact_name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Phone No" SortExpression="Phone No">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox6" runat="server"
                        Text='<%# Eval("contact_num") %>' BackColor="Yellow"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label6" runat="server"
                        Text='<%# Eval("contact_num") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Fax No" SortExpression="Fax No">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox7" runat="server"
                        Text='<%# Eval("fax_num") %>' BackColor="Yellow"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label7" runat="server"
                        Text='<%# Eval("fax_num") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" ValidationGroup="vgEdit"/>
            </Columns>
            <PagerStyle HorizontalAlign="Center" BackColor="#284775" ForeColor="White" />
		<SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
		<HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
		<EditRowStyle BackColor="#999999" />
		<AlternatingRowStyle BackColor="White" ForeColor="#284775" />


<HeaderStyle CssClass="text-center-impt"></HeaderStyle>

        </asp:GridView>
    </form>
</body>
</html>
