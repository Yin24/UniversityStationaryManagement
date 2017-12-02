<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeBehind="notifysuccess.aspx.cs" Inherits="SSISTeam2.notifysuccess" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead"
    runat="server">
    <title>Your Account has been Created</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">
    <div class="col-md-6 col-md-offset-3">

        <div class="jumbotron alert-success">
            <h4>Your account was successfully created.</h4>

            <p>
                <asp:Button ID="Button1" runat="server" Text="Login Now" class="btn btn-primary" OnClick="Button1_Click" /></p>
        </div>

    </div>

</asp:Content>
