<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="DEMO_RolesTest.aspx.cs" Inherits="SSISTeam2.DEMO_RolesTest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
    <title>Roles Test</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
                <table>
                    <tr>
                        <td>Is DeptHead:</td>
                        <td><%= userModel.isDeptHead() %></td>
                    </tr>
                    <tr>
                        <td>Is DeptHead, but not store Mgr:</td>
                        <td><%= userModel.isDeptHeadButNotStoreManager() %></td>
                    </tr>
                    <tr>
                        <td>Is DeptRepresentative:</td>
                        <td><%= userModel.isDepartmentRep() %></td>
                    </tr>
                    <tr>
                        <td>Is Delegate:</td>
                        <td><%= userModel.isDelegateHead() %></td>
                    </tr>
                    <tr>
                        <td>Is StoreManager:</td>
                        <td><%= userModel.isStoreManager() %></td>
                    </tr>
                    <tr>
                        <td>Is Supervisor:</td>
                        <td><%= userModel.isStoreSupervisor() %></td>
                    </tr>
                    <tr>
                        <td>Is Clerk:</td>
                        <td><%= userModel.isStoreClerk() %></td>
                    </tr>
                    <tr>
                        <td>Is Employee:</td>
                        <td><%= userModel.isEmployee() %></td>
                    </tr>
                </table>
            </div>
</asp:Content>
