<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewPending.aspx.cs" Inherits="SSISTeam2.Views.DepartmentHead.ViewPending"
    MasterPageFile="~/MasterPage.Master" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderHead"
    runat="server">
    <title>View Pending Requests</title>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <div class="panel panel-default">
    <div class="panel-heading"><h3> View All Pending </h3></div>

    <asp:Label ID="Label1" runat="server" Font-Size="Large"></asp:Label>

      <div class="panel-body">

    <div class="table-responsive">
        <asp:GridView ID="GridView1" runat="server"
            HeaderStyle-CssClass="text-center-impt" 
                   CssClass="table table-responsive table-striped"
                   GridLines="None"
                   AutoGenerateColumns="False">
                 <Columns>

                 <asp:TemplateField HeaderText="No.">
                    <ItemTemplate>
                        <%# Container.DataItemIndex +1 %>.
                    </ItemTemplate>
                </asp:TemplateField>
                 
              <asp:TemplateField HeaderText="Requested Id">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Eval("request_id") %>' ID="lbReqId"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Requested User">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Eval("username") %>' ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                  
                <asp:TemplateField HeaderText="Status">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Eval("current_status") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Date">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Eval("date_time") %>' ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
               
                <asp:TemplateField >
                    <ItemTemplate>
                        <asp:Button  runat="server" Text="View" OnClick="lbReqId_Click" CssClass="btn btn-default" />
                    </ItemTemplate>
                </asp:TemplateField>
            
                 </Columns>

        </asp:GridView>    
    </div>
          </div>
       </div>
     <asp:Button runat="server" Text="Back" CssClass="btn btn-primary" OnClick="btnBack_Click" />   
</asp:Content>
