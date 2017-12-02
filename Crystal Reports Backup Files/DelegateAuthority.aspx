<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DelegateAuthority.aspx.cs" Inherits="SSISTeam2.Views.DepartmentHead.DelegateAuthority" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
       
        Date: <asp:Label ID="lbCurrentDate" runat="server" Text="Label"></asp:Label>
        <br />

        Delegate Employee:<asp:DropDownList ID="ddlEmployee" runat="server"></asp:DropDownList>
        <br />

        Reason:<asp:TextBox ID="tbReason" runat="server" TextMode="MultiLine"></asp:TextBox>
        <br />

        Start Date:<asp:TextBox ID="tbStartDate" runat="server" TextMode="Date"></asp:TextBox>
        <br />
        End Date:<asp:TextBox ID="tbEndDate" runat="server" TextMode="Date"></asp:TextBox>
        <br />

        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" />

         <br />
        <asp:Label ID="lbDateError" runat="server" ForeColor="#FF3300"></asp:Label>

    </div>
    </form>

</body>
</html>
