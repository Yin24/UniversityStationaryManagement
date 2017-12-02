<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DepartmentInfo.aspx.cs" Inherits="SSISTeam2.Views.DepartmentHead.DepartmentInfo" %>

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

    Collection Point: 
    <asp:Label ID="LabelCollectP" runat="server" Text="Label"></asp:Label>
    <br />
 
    Representative Name: 
    <asp:Label ID="LabelRepName" runat="server" Text="Label"></asp:Label>
    <br />  

        <asp:Button ID="BtnChangeCpRn" runat="server" Text="Change CollectionPoint and RepresentativeName" OnClick="BtnChangeCpRn_Click" />
    </div>
        

    </form>
</body>
</html>
