<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="RepViewDisbursements.aspx.cs" Inherits="SSISTeam2.Views.Employee.RepViewDisbursements" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
    <title>View Disbursements</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-xs-12">
            <asp:Label Text="View Recent Disbursements" runat="server" CssClass="h1" />
        </div>
    </div>

    <div style="margin: 10px"></div>

    <div class="row">
        <div class="col-xs-12">
            <asp:Label Text="Find out what to distribute to your department" runat="server" CssClass="h5 grey-text" />
        </div>
    </div>

    <div style="margin: 10px"></div>

    <asp:Label ID="lblDebug" Text="" runat="server" />

    <div class="row">
        <div class="col-sm-3">
            <div class="panel panel-info">
                <div class="panel-heading">
                    1. Select a disbursement
                </div>
                <div class="panel-body">
                    <asp:ListBox ID="lboxRecents" runat="server" CssClass="form-control" OnSelectedIndexChanged="lboxRecents_SelectedIndexChanged" AutoPostBack="true"></asp:ListBox>
                </div>
            </div>
        </div>

        <div class="col-sm-4">
            <div class="panel panel-info">
                <div class="panel-heading">
                    2. Select a name
                </div>
                <div class="panel-body">
                    <asp:ListBox ID="lboxRequests" runat="server" CssClass="form-control" OnSelectedIndexChanged="lboxRequests_SelectedIndexChanged" AutoPostBack="true"></asp:ListBox>
                </div>
            </div>
        </div>

        <div class="col-sm-4">
            <div class="panel panel-info">
                <div class="panel-heading">
                    3. View items
                </div>
                <div class="panel-body">
                    <asp:ListBox ID="lboxItems" runat="server" CssClass="form-control"></asp:ListBox>
                </div>
            </div>
        </div>

    </div>
    <asp:Button runat="server" Text="Back" CssClass="btn btn-primary" OnClick="btnBack_Click" />
</asp:Content>
