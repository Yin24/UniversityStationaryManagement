<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeBehind="MakeNewRequests.aspx.cs" Inherits="SSISTeam2.Views.StoreClerk.DEMO_MakeNewRequests" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">

    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="row">

                    <asp:GridView ID="GridView1" runat="server"
                        AutoGenerateColumns="false" OnRowDataBound="GridView1_RowDataBound" CssClass="table table-responsive table-striped" GridLines="None" BorderColor="White">
                        <Columns>
                            <asp:TemplateField HeaderText="No.">
                                <ItemTemplate>
                                    <asp:Label ID="NumLabel" runat="server" Text='<%# Eval("Num") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item Category">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlCategories" runat="server" OnSelectedIndexChanged="ddlCategories_SelectedIndexChanged" AutoPostBack="True" CssClass="form-control">
                                    </asp:DropDownList>
                                    <%--<asp:Label runat="server" Text='<%# Eval("CurrentCatName") %>'></asp:Label>--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description">
                                <ItemTemplate>
                                    <asp:ListBox ID="lbDescriptions" runat="server" AutoPostBack="True" OnSelectedIndexChanged="lbDescriptions_SelectedIndexChanged" CssClass="form-control"></asp:ListBox>
                                    <%--<asp:DropDownList ID="ddlDescriptions" runat="server"></asp:DropDownList>--%>
                                    <%--<asp:Label runat="server" Text='<%# Eval("CurrentDescription") %>'></asp:Label>--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unit of Measure">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Eval("UnitOfMeasure") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Quantity">
                                <ItemTemplate>
                                    <asp:TextBox ID="tbQuantity" runat="server" Text='<%# Eval("Quantity") %>' AutoPostBack="True" OnTextChanged="tbQuantity_TextChanged" CssClass="form-control" />
                                    <%--<asp:Label runat="server" Text='<%# Eval("Quantity") %>'></asp:Label>--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Previously Approved">
                                <ItemTemplate>
                                    <asp:ListBox ID="lbPrevApproved" runat="server" Enabled="False" CssClass="form-control"></asp:ListBox>
                                    <%--<asp:Label runat="server" Text='<%# "hi" %>'></asp:Label>--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Actions">
                                <ItemTemplate>
                                    <asp:Button runat="server" Text="Remove" ID="btnRemoveRow" OnClick="btnRemoveRow_Click" CssClass="btn btn-danger" />
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>
                </div>
                <div class="row">
                    <asp:Panel runat="server" ID="panelCannotChange">
                        <div class="alert alert-warning">
                            <asp:Label Text="This request cannot be changed. " runat="server" />
                            <asp:Label ID="lblCannotChangeInfo" runat="server" />
                        </div>
                    </asp:Panel>
                </div>
                <div class="row">
                    <asp:Panel runat="server" ID="panelNormalBtns">
                        <div class="col-xs-6">
                            <div class="row">
                                <asp:Button ID="btnNewRow" Text="Add new entry" runat="server" OnClick="btnNewRow_Click" CssClass="btn btn-success" />
                            </div>
                            <br />
                            <div class="row">
                                <div class="panel panel-info">
                                    <div class="panel-heading">
                                        <asp:Label Text="Reason for request" runat="server" />
                                    </div>
                                    <div class="">
                                        <asp:TextBox ID="tbReason" runat="server" TextMode="MultiLine" MaxLength="4000" Height="100px" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <asp:Button ID="btnSubmit" Text="Submit request" runat="server" OnClick="btnSubmit_Click" CssClass="btn btn-primary" />
                                <br />
                                <asp:Button ID="btnCancelRequest" Text="Remove this request" runat="server" OnClick="btnCancelRequest_Click" CssClass="btn btn-danger" />
                            </div>
                        </div>
                    </asp:Panel>
                </div>                
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <%--</form>--%>
</asp:Content>

<%--</body>
</html>--%>
