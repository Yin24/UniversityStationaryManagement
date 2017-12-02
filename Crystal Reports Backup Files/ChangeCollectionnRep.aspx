<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangeCollectionnRep.aspx.cs" Inherits="SSISTeam2.Views.DepartmentHead.ChangeCollection_Rep" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
 
    Department Name: 
    <asp:Label ID="LabelDeptName" runat="server" Text="Label"></asp:Label>
    <br />

    Contact Name: 
    <asp:Label ID="LabelContactName" runat="server" Text="Label"></asp:Label>
    <br />

    Telephone No:
    <asp:Label ID="LabelPhNo" runat="server" Text="Label"></asp:Label>
    <br />

     Fax Number: 
    <asp:Label ID="LabelFaxNo" runat="server" Text="Label"></asp:Label>
    <br />

    Department Head Name: 
    <asp:Label ID="LabelHeadName" runat="server" Text="Label"></asp:Label>
    <br />

    Current Collection Point:
    <asp:Label ID="lbCurrentCollectP" runat="server" Text="Label"></asp:Label>
    <br />

    New Collection Point: 
    <asp:DropDownList ID="ddlCollectPoint" runat="server"></asp:DropDownList>
    <br />
 
    Current Representative Name:
    <asp:Label ID="lbRepName" runat="server" Text="Label"></asp:Label>
    <br />

    New Representative Name: 
    <asp:DropDownList ID="ddlRepName" runat="server"></asp:DropDownList>
    <br />  

        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />       
        &nbsp;
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />

    </div>
    </form>
</body>
</html>
