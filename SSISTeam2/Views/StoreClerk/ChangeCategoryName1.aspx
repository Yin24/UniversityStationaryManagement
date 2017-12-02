<%@ Page Language="C#"  AutoEventWireup="true" CodeBehind="ChangeCategoryName1.aspx.cs" Inherits="SSISTeam2.ChangeCategoryName1" 
    MasterPageFile="~/MasterPage.master"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead"
    runat="server">
    <title>Maintain Category Name</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">

    <asp:Panel ID="Panel1" runat="server" DefaultButton ="Button1">
        <!-- Default panel contents -->
        <div class="panel-heading"><h3>Maintain Category Listing</h3></div>
        <asp:Button ID="Button2" runat="server" Text="Add New Category" CssClass="btn btn-primary" OnClick="Button2_Click" />
        <br />
    <br />
        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" Text="Search" OnClick="Button1_Click" />
    <br />
    <br />
        <asp:label id="Message" forecolor="Red"  runat="server"/>
    <br />
    <br />
        <%--<asp:Button ID="Button2" runat="server" Text="Add New Category" OnClick="Button2_Click" />--%>
        <asp:GridView ID="GridView1" runat="server" 
            AutoGenerateColumns="False" 
           DataKeyNames="cat_id" 
            OnRowDeleting="OnRowDeleting"
            OnRowEditing="OnRowEditing" 
            OnRowUpdating="OnRowUpdating" 
            OnRowCancelingEdit="OnRowCancelingEdit"  
            AllowPaging="true"
                PageSize="10"
                HeaderStyle-CssClass="text-center-impt"
                CssClass="table table-responsive table-striped" 
                GridLines="None"
               PagerStyle-HorizontalAlign="Center" 
            PagerSettings-Position="TopAndBottom"  
            OnPageIndexChanging="GridView1_PageIndexChanging">

                          <PagerSettings Mode="NumericFirstLast" FirstPageText="First" LastPageText="Last" PreviousPageText="Previous" NextPageText="Next"/>

<PagerStyle HorizontalAlign="Center"></PagerStyle>
                <PagerTemplate>
                    <asp:Button Text="First" runat="server" CommandName="Page" CommandArgument="First" CssClass="btn btn-default btn-sm" />
                    <asp:Button Text="Prev" runat="server" CommandName="Page" CommandArgument="Prev" CssClass="btn btn-default btn-sm" />
                    <asp:Button Text="Next" runat="server" CommandName="Page" CommandArgument="Next" CssClass="btn btn-default btn-sm" />
                    <asp:Button Text="Last" runat="server" CommandName="Page" CommandArgument="Last" CssClass="btn btn-default btn-sm" />
                </PagerTemplate>
             
            <Columns>
                 
                <asp:TemplateField ItemStyle-Width="10%" HeaderText="Category ID" SortExpression="cat_id">
 <%--                   <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Eval("cat_id") %>' BackColor="Yellow"></asp:TextBox>
                    </EditItemTemplate>--%>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" CssClass="text-bold"
                        Text='<%# Eval("cat_id")  %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="20%" HeaderText="Category Name" SortExpression="cat_name">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox2" runat="server"
                        Text='<%# Eval("cat_name")  %>' BackColor="Yellow"></asp:TextBox>
                    </EditItemTemplate> 
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" CssClass="text-bold"
                        Text='<%# Bind("cat_name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
               
                <%--<asp:CommandField ShowDeleteButton="True" ShowEditButton="True"  />--%>
                <asp:TemplateField ItemStyle-Width="30%">  
                    <ItemTemplate >  
                        <asp:Button ID="btn_Edit" runat="server" Text="Edit" CommandName="Edit" CssClass="btn btn-primary"/>
                        <asp:Button ID="btn_Delete" runat="server" Text="Delete" CommandName="Delete" CssClass="btn btn-danger"
                            OnClientClick="return confirm('Are you sure you want to cancel this request?');" />    
                    </ItemTemplate>  

                    <EditItemTemplate>  
                        <asp:Button ID="btn_Update" runat="server" Text="Update" CommandName="Update" CssClass="btn btn-primary"/>  
                        <asp:Button ID="btn_Cancel" runat="server" Text="Cancel" CommandName="Cancel" CssClass="btn btn-primary"/>  
                    </EditItemTemplate>  
                </asp:TemplateField>      
            </Columns>
            <%-- <FooterStyle ForeColor="#003399" 
               BackColor="#99CCCC"></FooterStyle>
            <PagerStyle ForeColor="#003399" HorizontalAlign="Left" 
               BackColor="#99CCCC"></PagerStyle>


            <HeaderStyle ForeColor="#CCCCFF" Font-Bold="True" 
               BackColor="#003399"></HeaderStyle>
            <SelectedRowStyle ForeColor="#CCFF99" Font-Bold="True" 
                BackColor="#009999"></SelectedRowStyle>
            <RowStyle ForeColor="#003399" BackColor="White"></RowStyle>--%>
        </asp:GridView>
        
        <br/>
    </asp:Panel>
</asp:Content>