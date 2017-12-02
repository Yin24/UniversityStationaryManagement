<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeBehind="AddNewTender.aspx.cs" Inherits="SSISTeam2.Views.StoreClerk.AddNewTender" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead"
    runat="server">
    <title>Add New Item</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">

    <div class="panel panel-default">
        <!-- Default panel contents -->
        <div class="panel-heading"><h3>Item Details</h3></div>

        <div class="panel-body">
                <div class="form-group">
                    <label for="label_supplierName">Supplier Name: </label>
                   <%-- <asp:DropDownList ID="DropDownList1" class="form-control" runat="server" AutoPostBack="true"></asp:DropDownList>--%>                 
                    <asp:DropDownList ID="DropDownList1" class="form-control" runat="server" AppendDataBoundItems="true" CssClass="form-control" AutoPostBack="true">
                    <asp:ListItem Text="Select---" Value="0"></asp:ListItem></asp:DropDownList>
                    <asp:Label ID="lblerror2" runat="server" Text="" ForeColor="#FF3300"></asp:Label>
                </div>
                <div class="form-group">
                    <label for="label_itemDescription">Item Description: </label><br />
                  <%-- <asp:DropDownList ID="DropDownList2" class="form-control" runat="server" AutoPostBack="true"></asp:DropDownList>--%>
                    <asp:DropDownList ID="DropDownList2" class="form-control" runat="server" AppendDataBoundItems="true" CssClass="form-control" AutoPostBack="true">
                    <asp:ListItem Text="Select---" Value="0"></asp:ListItem></asp:DropDownList>
                    <asp:Label ID="lblerror3" runat="server" Text="" ForeColor="#FF3300"></asp:Label>
                     </div>
            
                <div class="form-group">
                    <label for="label_tenderDate">Tender Date: </label>
                    <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" TextMode="Date" AutoPostBack="true"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Tender Date is Required" ControlToValidate="TextBox1" ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:Label ID="lblerror1" runat="server" Text="" ForeColor="#FF3300"></asp:Label>
                </div>
                </
                <div class="form-group">
                    <label for="label_price">Price: </label>
                    <asp:TextBox ID="TextBox2" class="form-control" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Price is Required" ControlToValidate="TextBox2" ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Please Enter digit only" ControlToValidate="TextBox2" ValidationExpression="[0-9]*\.?[0-9]*" ForeColor="Red" ValidationGroup="valGroup1"></asp:RegularExpressionValidator>
                </div>
               
                <%--<div class="form-group">
                    <label for="label_rank">Rank: </label>
                    <asp:TextBox ID="TextBox3" class="form-control" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Rank is Required" ControlToValidate="TextBox3" ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Please Enter Digit Only." ControlToValidate="TextBox3" ValidationExpression="\d*" ForeColor="Red" ValidationGroup="valGroup1"></asp:RegularExpressionValidator>
                </div>--%>
                <%--<div class="form-group">
                     <asp:Label ID="Label1" runat="server" Text="Label" ForeColor="Red"></asp:Label>
                </div>--%>
               
                     <asp:Label ID="Label1" runat="server" Text="" ForeColor="Red"></asp:Label>
              
                <div class="form-group">
                <asp:Button runat="server" ID="SubmitButton" Text="Submit"  CssClass="btn btn-primary" OnClick="InsertNewTender_Click"/>
                <asp:Button runat="server" ID="CancelButton" Text="Cancel"  CssClass="btn btn-primary" OnClick="Cancel_Click" CausesValidation="false"/>
               </div>
            
            <%--<asp:GridView ID="GridView1" runat="server"></asp:GridView>--%>
            </div>
        <!-- Table -->
    </div>
</asp:Content>

