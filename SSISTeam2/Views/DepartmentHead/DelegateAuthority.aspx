<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DelegateAuthority.aspx.cs" Inherits="SSISTeam2.Views.DepartmentHead.DelegateAuthority" 
         MasterPageFile="~/MasterPage.Master" Debug="true" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderHead"
    runat="server">
    <title>Delegation of Authority</title>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-default">

        <div class="panel-heading"><h2> Delegate Authority </h2></div>
                 
    <div class="panel-body">
         <asp:Label ID="lbCheckDelegate" runat="server" Font-Size="Medium" CssClass="alert-info" Font-Italic="True" ></asp:Label>

        <asp:Table ID="CurrentTable" runat="server" class="active" style="table-layout: auto; font-size: large;" CssClass="table table-responsive table-striped">
               
            <asp:TableRow>
                <asp:TableCell>Created Date </asp:TableCell>
                <asp:TableCell>: <asp:Label ID="lbCurDate" runat="server" Text="Label" ></asp:Label></asp:TableCell>
            </asp:TableRow>

             <asp:TableRow>
                <asp:TableCell>Current Delegate </asp:TableCell>
                <asp:TableCell>: <asp:Label ID="lbCurDelegate" runat="server" Text="-" ></asp:Label></asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>Reason</asp:TableCell>
                <asp:TableCell>: <asp:Label ID="lbCurReason" runat="server" Text="-"></asp:Label></asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>Start Date</asp:TableCell>
                <asp:TableCell>: <asp:Label ID="lbCurStart" runat="server" Text="-"></asp:Label></asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>End Date</asp:TableCell>
                <asp:TableCell>: <asp:Label ID="lbCurEnd" runat="server" Text="-" ></asp:Label></asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>Delegation</asp:TableCell>
                <asp:TableCell>: <asp:Label ID="lbDelgActive" runat="server" Text="-" ></asp:Label></asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell HorizontalAlign="Left"><asp:Button runat="server" Text="Delete" ID="btnDelete" OnClick="btnDelete_Click" CssClass="btn btn-default"/></asp:TableCell>
            </asp:TableRow>

        </asp:Table>
        
    </div>

    <div>
  
             
        <asp:Table ID="ChooseNewTable" runat="server" class="active" style="table-layout: auto; font-size: large;" CssClass="table table-responsive table-striped">
               
            <asp:TableRow>
                <asp:TableCell>Date </asp:TableCell>
                <asp:TableCell>: <asp:Label ID="lbCurrentDate" runat="server" Text="Label" ></asp:Label></asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>Choose Delegate Employee</asp:TableCell>
                <asp:TableCell> <asp:DropDownList ID="ddlEmployee" runat="server" AppendDataBoundItems="true" CssClass="form-control">
                    <asp:ListItem Text="Select---" Value="0"></asp:ListItem>
                                 </asp:DropDownList></asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>Reason</asp:TableCell>
                <asp:TableCell>: <asp:TextBox ID="tbReason" runat="server" TextMode="MultiLine"></asp:TextBox></asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>Start Date</asp:TableCell>
                <asp:TableCell>: <asp:TextBox ID="tbStartDate" runat="server" TextMode="Date"></asp:TextBox>
                </asp:TableCell>
          
           </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>End Date</asp:TableCell>
                <asp:TableCell>: <asp:TextBox ID="tbEndDate" runat="server" TextMode="Date"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Right"> <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" Width="150px" OnClick="btnCancel_Click"/></asp:TableCell>
                <asp:TableCell> <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" Width="150px" CssClass="btn btn-success"/></asp:TableCell>
                
            </asp:TableRow>
        </asp:Table>

        <asp:Label ID="lbDateError" runat="server" ForeColor="#FF3300"></asp:Label>
        <br />
        <asp:Label ID="Label1" runat="server" ForeColor="#FF3300"></asp:Label>
    </div> 
         </div> 
    
   
     <asp:Button runat="server" Text="Back" CssClass="btn btn-primary" OnClick="btnBack_Click" />
  
 </asp:Content>
