<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeBehind="MakeNewRequest.aspx.cs" Inherits="SSISTeam2.Views.StoreClerk.MakeNewRequest" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead"
    runat="server">
    <title>Make New Request</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">
    <div class="table-responsive ">
        <div class="panel-heading">
            <h3>

                <asp:Label ID="lblPageTitle" Text="Create New Request" runat="server" />

            </h3>
        </div>
    </div>
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="table-responsive">
                        <asp:GridView ID="GridViewRequest" runat="server"
                            AutoGenerateColumns="false"
                            OnRowDataBound="GridViewRequest_RowDataBound"
                            CssClass="table table-responsive table-striped"
                            GridLines="None"
                            BorderColor="White">
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
                                        <asp:Label runat="server" Text='<%# Eval("UnitOfMeasure") %>' EnableViewState="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Quantity" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:TextBox ID="tbQuantity" runat="server" Text='<%# Eval("Quantity") %>'
                                            max='<%# Eval("Quantity") %>'
                                            
                                            onchange="checkQty(event)"
                                            CssClass="form-control" />
                                        <%----%>
                                        <%--AutoPostBack="True"--%>
                                        <%--OnTextChanged="tbQuantity_TextChanged"--%>
                                        <%--<asp:Label runat="server" Text='<%# Eval("Quantity") %>'></asp:Label>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Previously Approved">
                                    <ItemTemplate>
                                        <asp:ListBox ID="lbPrevApproved" runat="server" Enabled="False" CssClass="form-control" EnableViewState="false"></asp:ListBox>
                                        <%--<asp:Label runat="server" Text='<%# "hi" %>'></asp:Label>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Actions">
                                    <ItemTemplate>
                                        <asp:Button runat="server" AutoPostBack="True" Text="Remove" ID="btnRemoveRow" OnClick="btnRemoveRow_Click" OnClientClick="pauseClick(event)" CssClass="btn btn-danger" UseSubmitBehavior="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                    </div>
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
                                <asp:Button ID="btnNewRow" Text="Add new entry" runat="server" OnClick="btnNewRow_Click" CssClass="btn btn-success" OnClientClick="pauseClick(event)" UseSubmitBehavior="false" />
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
                                <span id="submitWarning" style="color:red"></span>
                                <br />
                                <asp:Button ID="btnSubmit" Text="Submit request" runat="server" OnClick="btnSubmit_Click" CssClass="btn btn-primary" OnClientClick="pauseClick(event)" UseSubmitBehavior="false" />
                                <br />
                                <br />
                                <asp:LinkButton ID="btnBack" runat="server" Text="Back"  CssClass="btn btn-primary" OnClientClick="JavaScript:window.history.back(1);return false;"/>
                                <asp:Button ID="btnCancelRequest" Text="Cancel this request" runat="server" OnClick="btnCancelRequest_Click" CssClass="btn btn-danger" />
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <script>
            window.addEventListener('input', function (e) {
                //console.log("keyup event detected! coming from this element:", e.target);

                if (e.target.name.indexOf("tbQuantity") !== -1) {
                    // tbQuantity textbox
                    //console.log("Textbox with this value: " + e.target.value);

                    var tb = e.target;
                    var isNum = /^\d+$/.test(tb.value);
                    if (!isNum) {
                        //tb.value = tb.max;
                        tb.style = "color:red";

                        var inputs = document.querySelectorAll('input');
                        for (var tb of inputs.values()) {
                            if (tb.name.indexOf("btnSubmit") !== -1) {
                                tb.disabled = true;
                                document.querySelector('#submitWarning').innerHTML = "Cannot submit, one or more of the quantities are not whole numbers.";
                                break;
                            }
                        }
                    } else {
                        tb.style = "color:black";
                    }
                }

            }, false);

            function pauseClick(event) {
                var inputs = document.querySelectorAll('input');

                for (var input of inputs.values()) {
                    if (input.name.indexOf("btn") !== -1) {
                        input.disabled = true;
                    }
                }
                return true;
            }
            


            function checkQty(event) {
                // Check all inputs

                var inputs = document.querySelectorAll('input');
                //console.log(typeof (inputs));
                //console.log(inputs);

                var allOk = true;

                var btnSubmit = null;

                for (var tb of inputs.values()) {
                    //console.log(tb);
                    if (tb.name.indexOf("btnSubmit") !== -1) {
                        btnSubmit = tb;
                    }

                    if (tb.name.indexOf("tbQuantity") !== -1) {
                        console.log(tb);

                        var isNum = /^\d+$/.test(tb.value);
                        if (!isNum) {
                            //tb.value = tb.max;
                            allOk = false;
                        }
                    }
                }

                // find submit button - btnSubmit
                

                if (allOk) {
                    btnSubmit.disabled = false;
                    document.querySelector('#submitWarning').innerHTML = "";
                } else {
                    btnSubmit.disabled = true;
                    document.querySelector('#submitWarning').innerHTML = "Cannot submit, one or more of the quantities are not whole numbers.";
                }
            }
        </script>
    </div>
    <%--</form>--%>
</asp:Content>

<%--</body>
</html>--%>
