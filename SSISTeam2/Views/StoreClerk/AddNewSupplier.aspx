<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeBehind="AddNewSupplier.aspx.cs" Inherits="SSISTeam2.Views.StoreClerk.AddNewSupplier" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead"
    runat="server">
    <title>Add a New Supplier</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">

<%--    <div class="panel panel-default">
        <!-- Default panel contents -->
        <div class="panel-heading">
            <h3>Add New Supplier</h3>
        </div>

    </div>--%>

<%--    <form class="form-horizontal">
        <div class="form-group">
            <label class="control-label col-sm-2" for="label_supplierId">Supplier ID:</label>
            <div class="col-sm-10">
                <input type="txt_supplierId" class="form-control" id="supplierId" >
            </div>
        </div>
        <div class="form-group">
            <label class="control-label col-sm-2" for="label_supplierName">Supplier Name:</label>
            <div class="col-sm-10">
                <input type="txt_supplierName" class="form-control" id="supplierName" >
            </div>
        </div>
        <div class="form-group">
            <label class="control-label col-sm-2" for="label_contactName">Contact Name:</label>
            <div class="col-sm-10">
                <input type="txt_contactName" class="form-control" id="contactName" >
            </div>
        </div>
        <div class="form-group">
            <label class="control-label col-sm-2" for="label_contactNumber">Contact Number:</label>
            <div class="col-sm-10">
                <input type="txt_contactNumber" class="form-control" id="contactNumber" >
            </div>
        </div>
        <div class="form-group">
            <label class="control-label col-sm-2" for="label_faxNum">Fax Number:</label>
            <div class="col-sm-10">
                <input type="txt_faxNum" class="form-control" id="faxNum" >
            </div>
        </div>
        <div class="form-group">
            <label class="control-label col-sm-2" for="label_address">Address:</label>
            <div class="col-sm-10">
                <input type="txt_address" class="form-control" id="address" >
            </div>
        </div>
        <div class="form-group">
            <label class="control-label col-sm-2" for="label_gst">GST Registration Number:</label>
            <div class="col-sm-10">
                <input type="txt_gst" class="form-control" id="gst" >
            </div>
        </div>

        <div class="form-group">
            <div class="col-sm-offset-2 col-sm-10">
               <asp:Button ID="delete" runat="server" Text="Submit"
                                CssClass="btn btn-primary" OnClick="save_Click"/>
            </div>
        </div>
    </form>--%>

    <div class="panel panel-default">
            <div class="panel-heading">
                <h3 class="panel-title">Add New Supplier</h3>
            </div>
            <div class="panel-body">
                <div class="form-group">
                    <label for="label_supplierId">Supplier ID: </label>
                    <asp:TextBox ID="tb_supplierId" class="form-control" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="vldTitle" runat="server" ErrorMessage="Supplier ID Is Required" ControlToValidate="tb_supplierId" ForeColor="Red"></asp:RequiredFieldValidator>

                </div>
                <div class="form-group">
                    <label for="label_supplierName">Supplier Name: </label><br />
                   <asp:TextBox ID="tb_supplierName" class="form-control" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Supplier name Is Required" ControlToValidate="tb_supplierName" ForeColor="Red"></asp:RequiredFieldValidator>
                </div>
                <div class="form-group">
                    <label for="label_contactName">Contact Name: </label>
                    <asp:TextBox ID="tb_contactName" class="form-control" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="vldISBN" runat="server" ErrorMessage="Contact Name is Required" ControlToValidate="tb_contactName" ForeColor="Red"></asp:RequiredFieldValidator>
                </div>
                <div class="form-group">
                    <label for="label_contactNum">Contact Number: </label>
                    <asp:TextBox ID="tb_contactNum" class="form-control" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="vldPrice" runat="server" ErrorMessage="Contact Number Is Required" ControlToValidate="tb_contactNum" ForeColor="Red"></asp:RequiredFieldValidator>

                </div>
               
                <div class="form-group">
                    <label for="label_faxNum">Fax Number: </label>
                    <asp:TextBox ID="tb_faxNum" class="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="label_address">Address: </label>
                    <asp:TextBox ID="tb_address" class="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="label_gst">GST Registration Number: </label>
                    <asp:TextBox ID="tb_gst" class="form-control" runat="server"></asp:TextBox>
                </div>
                
                <asp:Button runat="server" ID="SubmitButton" Text="Submit"  CssClass="btn btn-primary" OnClick="SubmitButton_Click"/>
               <asp:Button ID="CancelButton" CausesValidation="false" runat="server" Text="Cancel" CssClass="btn btn-primary" OnClick="CancelButton_Click" />
                 <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" />

            </div>
        </div>


</asp:Content>
