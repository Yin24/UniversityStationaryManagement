<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApproveReject.aspx.cs" Inherits="SSISTeam2.Views.DepartmentHead.ApproveReject" 
     MasterPageFile="~/MasterPage.Master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderHead"
    runat="server">
    <title>Approve/Reject Request</title>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <div class="panel panel-default">
    <div class="panel-heading"><h3>Approve Reject </h3></div>

    <div class="panel-body">     
        <asp:Label ID="lblInfo" runat="server" Font-Size="Medium" />
        <br />
        Request ID: <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        <br />
        Request Date: <asp:Label ID="lbRqDate" runat="server" Text="-"></asp:Label>
         <br />
         Request Employee: <asp:Label ID="lbRqEmp" runat="server" Text="-"></asp:Label>
         <br />
        Request Reason: <asp:Label ID="lblRequestReason" runat="server" Text="-"></asp:Label>
         <br />
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
                 
              <asp:TemplateField HeaderText="Item Description">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Eval("item_description") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Requested Quantity">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Eval("orig_quantity") %>' ID="lbQty1"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                  
                <asp:TemplateField HeaderText="Unit">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Eval("unit_of_measure") %>' ID="lbUni1"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                 </Columns>
                </asp:GridView>
         </div>
         </div> 
         
    <div>

      <div class="panel-heading" style="width:700px" >
           <asp:Label Text="Reason for Approve/Reject" runat="server" Width="700px"/>
     </div>
    <div class="">
        <asp:TextBox ID="tbReason" runat="server" TextMode="MultiLine" MaxLength="1000" Height="70px" CssClass="form-control" Width="705px"></asp:TextBox>
     </div>
        <br />
     <asp:Table ID="Table1" runat="server">
             <asp:TableRow>
                <asp:TableCell HorizontalAlign="Left"><asp:Button ID="btnApprove" runat="server" Text="Approve" OnClick="btnApprove_Click" CssClass="btn btn-success" /></asp:TableCell>
                <asp:TableCell HorizontalAlign="Center"> <asp:Button ID="btnReject" runat="server" Text="Reject" OnClick="btnReject_Click" CssClass="btn btn-danger"/></asp:TableCell>
 		        <asp:TableCell HorizontalAlign="Right"><asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" CssClass="btn btn-default" /></asp:TableCell>
             </asp:TableRow>
         </asp:Table>

        <asp:Label ID="lbAppRej" runat="server" ForeColor="#FF3300"></asp:Label>
    </div> 
       </div> 
  
    <hr />

     <div class="panel panel-default">
           <div class="panel-heading"><h3>Most recently approved request, pending disbursement from store</h3></div> 
         <asp:Label ID="lbLastApp" runat="server" Font-Size="Medium" ></asp:Label>
             
       <br />
         <asp:Table ID="Table2" runat="server">
             <asp:TableRow>
                 <asp:TableCell>Requested ID</asp:TableCell>
                <asp:TableCell>   :<asp:Label ID="lbLastReqID" runat="server" Text="-"></asp:Label></asp:TableCell>
             </asp:TableRow>
               <asp:TableRow>
                <asp:TableCell>Requested Date</asp:TableCell>
                <asp:TableCell>   :<asp:Label ID="lbLastReqDate" runat="server" Text="-"></asp:Label></asp:TableCell>
             </asp:TableRow>
                <asp:TableRow>
                <asp:TableCell>Requested Employee</asp:TableCell>
                 <asp:TableCell>   :<asp:Label ID="lbLastReqEmp" runat="server" Text="-"></asp:Label></asp:TableCell>   
             </asp:TableRow>
         </asp:Table>
                 
                <br />

     <div class="panel-body"> 
          <div class="table-responsive">
            <asp:GridView ID="GridView2" runat="server"
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

                                        <asp:TemplateField HeaderText="Item Description">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Eval("item_description") %>' ID="lbDesp1"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Requested Quantity">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Eval("orig_quantity") %>' ID="lbQty1"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                  
                <asp:TemplateField HeaderText="Unit">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Eval("unit_of_measure") %>' ID="lbUni1"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                 </Columns>
                </asp:GridView>
              </div>
             </div>
    </div>

        
</asp:Content>
