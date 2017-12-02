<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="GenerateDisbursementForm.aspx.cs" Inherits="SSISTeam2.Views.StoreClerk.GenerateDisbursementForm" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">
    <div class="panel panel-default">
        <!-- Default panel contents -->
        <div class="panel-heading">Generate Disbursement Form</div>
       <div class="table-responsive" style="border:none" >  
     <table class="table" style="border:none">
         
          <tbody>  
              <tr>
                <td>Department
                <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true"/></td>
            </tr>
            <tr>
                <td>Collection Point
                    <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="true"></asp:DropDownList>
                </td>
            </tr> 
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Representative Name"></asp:Label>
                    <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                </td>

            </tr>
            <tr>
                <td>
                <asp:Label ID="Label3" runat="server" Text="Collection Point"></asp:Label>
                <asp:Label ID="Label4" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="GridView1" runat="server"></asp:GridView>
                </td>
            </tr>
          </tbody>  
    </table>  
</div>  
        
  
    
  
</div>      



</asp:Content>