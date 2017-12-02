<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeBehind="register.aspx.cs" Inherits="SSISTeam2.register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead"
    runat="server">
    <title>Register Your SSIS Account</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">
    <div class="col-md-6 col-md-offset-4">
        <asp:CreateUserWizard ID="CreateUserWizard1" runat="server" OnCreatedUser="CreateUserWizard1_CreatedUser">
            <WizardSteps>
                <asp:CreateUserWizardStep runat="server">
                    <ContentTemplate>
                        <div class="panel panel-primary">
                            <div class="panel-heading" ">
                                <h2 class="panel-title" ">Sign Up for Your New Account</h2>
                            </div>
                            <div class="panel-body">

                                <div class="form-group">
                                    <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">User Name:</asp:Label>
                                    <asp:TextBox ID="UserName" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="CreateUserWizard1" Style="color: red">* User Name is required.</asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                                    <asp:TextBox ID="Password" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="CreateUserWizard1" Style="color: red">* Password is required.</asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="ConfirmPasswordLabel" runat="server" AssociatedControlID="ConfirmPassword">                      Confirm Password:</asp:Label>

                                    <asp:TextBox ID="ConfirmPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" ControlToValidate="ConfirmPassword"
                                        ErrorMessage="Confirm Password is required." ToolTip="Confirm Password is required."
                                        ValidationGroup="CreateUserWizard1" Style="color: red">* Confirm Password is required.</asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="Email">                       E-mail:</asp:Label>
                                    <asp:TextBox ID="Email" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="Email"
                                        ErrorMessage="E-mail is required." ToolTip="E-mail is required." ValidationGroup="CreateUserWizard1" Style="color: red">* E-mail is required.</asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="QuestionLabel" runat="server" AssociatedControlID="Question">
                        Security Question:</asp:Label>
                                    <asp:TextBox ID="Question" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="QuestionRequired" runat="server" ControlToValidate="Question"
                                        ErrorMessage="Security question is required." ToolTip="Security question is required."
                                        ValidationGroup="CreateUserWizard1" Style="color: red">* Security question is required.</asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="AnswerLabel" runat="server" AssociatedControlID="Answer">
                        Security Answer:</asp:Label>

                                    <asp:TextBox ID="Answer" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="AnswerRequired" runat="server" ControlToValidate="Answer"
                                        ErrorMessage="Security answer is required." ToolTip="Security answer is required."
                                        ValidationGroup="CreateUserWizard1" Style="color: red">* Security answer is required.</asp:RequiredFieldValidator>
                                </div>
                                <div style="color: red">
                                    <asp:CompareValidator ID="PasswordCompare" runat="server" ControlToCompare="Password"
                                        ControlToValidate="ConfirmPassword" Display="Dynamic" ErrorMessage="The Password and Confirmation Password must match."
                                        ValidationGroup="CreateUserWizard1"></asp:CompareValidator>
                                </div>
                                <div style="color: red">
                                    <asp:Literal ID="ErrorMessage" runat="server" EnableViewState="False"></asp:Literal>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="FullnameLabel" runat="server" AssociatedControlID="Fullname">                       Full Name:</asp:Label>
                                    <asp:TextBox ID="Fullname" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Fullname"
                                        ErrorMessage="Full Name is required." ToolTip="Full Name is required." ValidationGroup="CreateUserWizard1" Style="color: red">* Full Name is required.</asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="MobileNoLabel" runat="server" AssociatedControlID="MobileNo">                       Mobile Number:</asp:Label>
                                    <asp:TextBox ID="MobileNo" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="MobileNo"
                                        ErrorMessage="Mobile Number is required." ToolTip="Mobile Number is required." ValidationGroup="CreateUserWizard1" Style="color: red">* Mobile Number is required.</asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="DepartmentLabel" runat="server" AssociatedControlID="Department">                       Department:</asp:Label>
                                    <asp:DropDownList ID="Department" runat="server" CssClass="form-control" DataSourceID="SqlDataSource1" DataTextField="name" DataValueField="dept_code"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Department"
                                        ErrorMessage="Department is required." ToolTip="Department is required." ValidationGroup="CreateUserWizard1" Style="color: red">* Department is required.</asp:RequiredFieldValidator>
                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SSISConnectionString %>" SelectCommand="SELECT [dept_code], [name] FROM [Department]"></asp:SqlDataSource>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="RoleLabel" runat="server" AssociatedControlID="Role">                       Role:</asp:Label>
                                    <asp:DropDownList ID="Role" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="Employee">Employee</asp:ListItem>
                                        <asp:ListItem Value="Clerk">Store Clerk</asp:ListItem>
                                        <asp:ListItem Value="DeptHead">Department Head</asp:ListItem>
                                        <asp:ListItem Value="Manager">Store Manager</asp:ListItem>
                                        <asp:ListItem Value="Supervisor">Store Supervisor</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="Role"
                                        ErrorMessage="Role is required." ToolTip="Role is required." ValidationGroup="CreateUserWizard1" Style="color: red">* Role is required.</asp:RequiredFieldValidator>
                                </div>
                    </ContentTemplate>
                    <CustomNavigationTemplate>

                        <asp:Button ID="StepNextButton" runat="server" CommandName="MoveNext" Text="Create User" ValidationGroup="CreateUserWizard1" CssClass="btn btn-default" />

                    </CustomNavigationTemplate>
                </asp:CreateUserWizardStep>

<asp:CompleteWizardStep runat="server"></asp:CompleteWizardStep>

            </WizardSteps>
        </asp:CreateUserWizard>
        <asp:Label ID="Label1" runat="server"></asp:Label>
    </div>
</asp:Content>
