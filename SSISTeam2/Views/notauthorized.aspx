<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="notauthorized.aspx.cs" MasterPageFile="~/MasterPage.master" Inherits="SSISTeam2.notauthorized" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead"
    runat="server">
    <title>Not Authorized</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">
    <div class="col-md-6 col-md-offset-3">

        <div class="jumbotron alert-danger">
            <h3>Oops! You are not authorized to do this!</h3>       
        </div>

    </div>

</asp:Content>
