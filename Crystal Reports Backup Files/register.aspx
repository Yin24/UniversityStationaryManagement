<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeBehind="register.aspx.cs" Inherits="SSISTeam2.register" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">
    <div class="col-md-6 col-md-offset-4">
        <asp:CreateUserWizard ID="CreateUserWizard1" runat="server" OnCreatedUser="CreateUserWizard1_CreatedUser">
            <WizardSteps>
                <asp:CreateUserWizardStep ID="CreateUserWizardStep1" runat="server">
                    <ContentTemplate>
                        <div class="panel panel-primary">
                            <div class="panel-heading" ">
                                <h2 class="panel-title" ">Sign Up for Your New Account</h2>
                            </div>
                            <div class="panel-body">

                                <div class="form-group">
                                    <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">User Name:</asp:Label>
                                    <asp:TextBox ID="UserName" runat="server" class="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="CreateUserWizard1" Style="color: red">* User Name is required.</asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                                    <asp:TextBox ID="Password" runat="server" TextMode="Password" class="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="CreateUserWizard1" Style="color: red">* Password is required.</asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="ConfirmPasswordLabel" runat="server" AssociatedControlID="ConfirmPassword">                      Confirm Password:</asp:Label>

                                    <asp:TextBox ID="ConfirmPassword" runat="server" TextMode="Password" class="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" ControlToValidate="ConfirmPassword"
                                        ErrorMessage="Confirm Password is required." ToolTip="Confirm Password is required."
                                        ValidationGroup="CreateUserWizard1" Style="color: red">* Confirm Password is required.</asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="Email">                       E-mail:</asp:Label>
                                    <asp:TextBox ID="Email" runat="server" class="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="Email"
                                        ErrorMessage="E-mail is required." ToolTip="E-mail is required." ValidationGroup="CreateUserWizard1" Style="color: red">* E-mail is required.</asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="QuestionLabel" runat="server" AssociatedControlID="Question">
                        Security Question:</asp:Label>
                                    <asp:TextBox ID="Question" runat="server" class="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="QuestionRequired" runat="server" ControlToValidate="Question"
                                        ErrorMessage="Security question is required." ToolTip="Security question is required."
                                        ValidationGroup="CreateUserWizard1" Style="color: red">* Security question is required.</asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="AnswerLabel" runat="server" AssociatedControlID="Answer">
                        Security Answer:</asp:Label>

                                    <asp:TextBox ID="Answer" runat="server" class="form-control"></asp:TextBox>
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
                    </ContentTemplate>
                    <CustomNavigationTemplate>

                        <asp:Button ID="StepNextButton" runat="server" CommandName="MoveNext" Text="Create User" ValidationGroup="CreateUserWizard1" class="btn btn-default" />

                    </CustomNavigationTemplate>
                </asp:CreateUserWizardStep>

            </WizardSteps>
        </asp:CreateUserWizard>
    </div>
</asp:Content>
