<%@ Page Title="Feature Request" Language="C#" MasterPageFile="~/IGBrandRepReferral.Master" AutoEventWireup="true" CodeBehind="FeatureRequest.aspx.cs" Inherits="IGBrandRepReferral.FeatureRequest" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="customCss" runat="server"></asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
    <script type="text/javascript">
        function IsValidEmail(sender, eventArgs) {
            var email = eventArgs.get_newValue();
            var id = '<%=lblMessage.ClientID%>';
            var lblMessage2 = document.getElementById(id);
            lblMessage2.style.display = 'none';
        }

        function OnClickValidate() {
            var phone = ValidatePhone();
            var email = ValidateEmail();
            if (phone && email) {
                var updateProgress = $get("<%= loader.ClientID %>");
                updateProgress.style.display = "block";
                return true;
            }
            else {
                return false;
            }
        }

        function ValidatePhone() {
            var textbox = $find('<%= txtPhoneNumber.ClientID %>');
            var length = textbox.get_textBoxValue().length;
            if (Page_ClientValidate()) {
                if (length < 10) {
                    ErrorDialog("Phone number must be at least 10 digits");
                    return false;
                }
                else {
                    return true;
                }
            }
            else {
                return false;
            }
        }

        function ValidateEmail() {
            var textbox = $find('<%= txtEmail.ClientID %>');
            var email = textbox.get_textBoxValue();
            if (Page_ClientValidate()) {
                return true;
            }
            else {
                return false;
            }
        }

        function EnableDisableState(sender, args) {
            var value = $find("<%=ddlCountry.ClientID%>").get_selectedItem().get_value();
            if (value == 1220) {
                var ddl = $find('<%= ddlState.ClientID %>');
                ddl.set_enabled(true);
                var id = '<%=lblStateError.ClientID%>';
                var lblMessage2 = document.getElementById(id);
                lblMessage2.style.visibility = 'visible';
                ValidatorEnable(document.getElementById('<%= rfState.ClientID %>'), true);
            }
            else {
                var ddl = $find('<%= ddlState.ClientID %>');
                ddl.set_enabled(false);
                var id = '<%=lblStateError.ClientID%>';
                var lblMessage2 = document.getElementById(id);
                lblMessage2.style.visibility = 'hidden';
                ValidatorEnable(document.getElementById('<%= rfState.ClientID %>'), false);
            }
        }
        function ManuallyDisableStateValidation() {
            alert("Before");
            document.getElementById('ddlState').style.border = '1px solid #333';
            alert("AFter");
        }
        function ValidatorUpdateDisplay(val) {
            if (typeof (val.display) == "string") {
                if (val.display == "None") {
                    return;
                }
                if (val.display == "Dynamic") {
                    val.style.display = val.isvalid ? "none" : "inline";
                    return;
                }

            }
            val.style.visibility = val.isvalid ? "hidden" : "visible";
            if (val.isvalid) {
                document.getElementById(val.controltovalidate).style.border = '1px solid #333';
            }
            else {
                document.getElementById(val.controltovalidate).style.border = '1px solid red';
            }
        }
        function SuccessDialog(content) {
            $(function () {
                $("#confirmDialog").color = "red"
                $("#confirmDialog").text(content)
                $("#confirmDialog").dialog(
                    {
                        title: "Success",
                        resizable: false,
                        modal: true,
                        color: "red",
                        buttons:
                        {
                            'OK': function () {
                                $("#confirmDialog").dialog("close");
                                //_DialogConfirmed(); // MUST implement on local page
                            }
                        }

                    });

            });
        }
        function ErrorDialog(content) {
            $(function () {
                $("#confirmDialog").color = "red"
                $("#confirmDialog").text(content)
                $("#confirmDialog").dialog(
                    {
                        title: "Error",
                        resizable: false,
                        modal: true,
                        color: "red",
                        buttons:
                        {
                            'OK': function () {
                                $("#confirmDialog").dialog("close");
                            }
                        }
                    });
            });
        }
        function OnClientShow(sender, args) {
            var btn = sender.getManualCloseButton();
            btn.style.left = "0px";
        }

        function Clear() {
            var myVal = document.getElementById('<%=RequiredFieldValidator1.ClientID%>');
            ValidatorEnable(myVal, false);
            document.getElementById('<%=txtFirstName.ClientID%>').value = "";

            myVal = document.getElementById('<%=RequiredFieldValidator2.ClientID%>');
            ValidatorEnable(myVal, false);
            document.getElementById('<%=txtLastName.ClientID%>').value = "";

            myVal = document.getElementById('<%=RequiredFieldValidator3.ClientID%>');
            ValidatorEnable(myVal, false);
            document.getElementById('<%=txtPhoneNumber.ClientID%>').value = "";

            myVal = document.getElementById('<%=RequiredFieldValidator4.ClientID%>');
            ValidatorEnable(myVal, false);
            document.getElementById('<%=txtEmail.ClientID%>').value = "";

            myVal = document.getElementById('<%=RequiredFieldValidator5.ClientID%>');
            ValidatorEnable(myVal, false);
            document.getElementById('<%=txtCompanyName.ClientID%>').value = "";

            myVal = document.getElementById('<%=RequiredFieldValidator6.ClientID%>');
            ValidatorEnable(myVal, false);
            document.getElementById('<%=txtCompanyURL.ClientID%>').value = "";

            myVal = document.getElementById('<%=RequiredFieldValidator7.ClientID%>');
            ValidatorEnable(myVal, false);
            document.getElementById('<%=txtPositionTitle.ClientID%>').value = "";

            myVal = document.getElementById('<%=RequiredFieldValidator8.ClientID%>');
            ValidatorEnable(myVal, false);

            myVal = document.getElementById('<%=RequiredFieldValidator9.ClientID%>');
            ValidatorEnable(myVal, false);
            document.getElementById('<%=txtAddress1.ClientID%>').value = "";

            myVal = document.getElementById('<%=rfCountry.ClientID%>');
            ValidatorEnable(myVal, false);

            myVal = document.getElementById('<%=RequiredFieldValidator10.ClientID%>');
            ValidatorEnable(myVal, false);
            document.getElementById('<%=txtCity.ClientID%>').value = "";

            myVal = document.getElementById('<%=rfState.ClientID%>');
            ValidatorEnable(myVal, false);

            myVal = document.getElementById('<%=RequiredFieldValidator11.ClientID%>');
            ValidatorEnable(myVal, false);
            document.getElementById('<%=txtZipCode.ClientID%>').value = "";

            return true;
        }

        function ClearState() {
            var myVal = document.getElementById('<%=rfState.ClientID%>');
            ValidatorEnable(myVal, false);

            return true;
        }

    </script>
    <link href="/Styles/jquery-ui.min.css" rel="stylesheet" />

    <link href="/Styles/jquery-ui.theme.min.css" rel="stylesheet" />

    <script src="//code.jquery.com/jquery-1.10.2.js"></script>

    <script src="//code.jquery.com/ui/1.10.4/jquery-ui.js"></script>

    <script src="../../JS/DialogBox.js"> </script>
    <style type="text/css">
        body
        {
            margin: 0;
            padding: 0;
            font-family: Arial;
        }
        .modal
        {
            position: fixed;
            z-index: 999;
            height: 100%;
            width: 100%;
            top: 0;
            background-color: Black;
            filter: alpha(opacity=60);
            opacity: 0.6;
            -moz-opacity: 0.8;
        }
        .center
        {
            z-index: 1000;
            margin: 300px auto;
            padding: 10px;
            width: 130px;
            background-color: White;
            border-radius: 10px;
            filter: alpha(opacity=100);
            opacity: 1;
            -moz-opacity: 1;
        }
        .center img
        {
            height: 128px;
            width: 128px;
        }
    </style>
    </telerik:RadCodeBlock>

        <asp:UpdateProgress ID="loader" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div id="div1" class="modal" visible="false">
                <div class="center">
                    <img alt="" src="/Images/ripple.gif" />
                </div>
            </div>
        </ProgressTemplate>
        </asp:UpdateProgress>

        <telerik:RadNotification ID="radnotMessage" runat="server" Text="Initial text" Position="BottomRight"
            AutoCloseDelay="3000" ShowCloseButton="false" Animation="Fade" Width="350" Title="Current time"
            EnableRoundedCorners="true">
        </telerik:RadNotification>

        <fieldset>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <asp:Table ID="MainPage" runat="server" HorizontalAlign="Center" Width="100%">
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Center">
                                <h2>Feature Request</h2>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                    <asp:Table ID="Table1" runat="server" HorizontalAlign="Center">
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Table ID="Table2" runat="server" HorizontalAlign="Center">
                                    <asp:TableRow>
                                        <asp:TableHeaderCell>
                                            First Name<font color="red">*</font>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFirstName" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </asp:TableHeaderCell>
                                        <asp:TableHeaderCell>
                                            Last Name<font color="red">*</font>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtLastName" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </asp:TableHeaderCell>
                                        <asp:TableHeaderCell>
                                            Phone Number<font color="red">*</font>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPhoneNumber" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </asp:TableHeaderCell>
                                        <asp:TableHeaderCell>
                                            E-Mail<font color="red">*</font>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtEmail" ForeColor="Red" Display="Static"></asp:RequiredFieldValidator>
                                            <asp:ImageButton ID="PeriodToolTip" runat="server" OnClientClick="return false;" Width="18px" ImageUrl="~/images/info_small.png" />
                                            <telerik:RadToolTip ID="RadToolTip4" runat="server" Position="MiddleRight" RelativeTo="Element"
                                                TargetControlID="PeriodToolTip" Width="200px" HideEvent="ManualClose"
                                                OnClientShow="OnClientShow">
                                                Must be a valid e-mail address.
                                            </telerik:RadToolTip>
                                        </asp:TableHeaderCell>
                                        <asp:TableHeaderCell>
                                            Company Name<font color="red">*</font>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtCompanyName" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </asp:TableHeaderCell>
                                        <asp:TableHeaderCell>
                                            Company URL<font color="red">*</font>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtCompanyURL" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </asp:TableHeaderCell>
                                        <asp:TableHeaderCell>
                                            Position/Title<font color="red">*</font>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtPositionTitle" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </asp:TableHeaderCell>
                                        <asp:TableHeaderCell>
                                            Select Demos<font color="red">*</font>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="cbDemos" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </asp:TableHeaderCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell>
                                            <telerik:RadTextBox ID="txtFirstName" runat="server" Width="100%"></telerik:RadTextBox>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <telerik:RadTextBox ID="txtLastName" runat="server" Width="100%"></telerik:RadTextBox>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <telerik:RadTextBox ID="txtPhoneNumber" runat="server" Width="100%"></telerik:RadTextBox>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <telerik:RadTextBox ID="txtEmail" runat="server" ClientEvents-OnValueChanged="IsValidEmail"></telerik:RadTextBox>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <telerik:RadTextBox ID="txtCompanyName" runat="server" Width="100%"></telerik:RadTextBox>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <telerik:RadTextBox ID="txtCompanyURL" runat="server" Width="100%"></telerik:RadTextBox>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <telerik:RadTextBox ID="txtPositionTitle" runat="server"></telerik:RadTextBox>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <telerik:RadDropDownList ID="cbDemos" runat="server" DefaultMessage="--Select--" AppendDataBoundItems="true" Enabled="true" CausesValidation="false"></telerik:RadDropDownList>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableHeaderCell ColumnSpan="2">
                                            Address Line 1<font color="red">*</font>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtAddress1" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </asp:TableHeaderCell>
                                        <asp:TableHeaderCell ColumnSpan="2">
                                            Address Line 2 (Optional)
                                        </asp:TableHeaderCell>
                                        <asp:TableHeaderCell>
                                            Country<font color="red">*</font>
                                            <asp:RequiredFieldValidator ID="rfCountry" runat="server" ControlToValidate="ddlCountry" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </asp:TableHeaderCell>
                                        <asp:TableHeaderCell>
                                            City<font color="red">*</font>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtCity" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </asp:TableHeaderCell>
                                        <asp:TableHeaderCell>
                                            State<asp:Label ID="lblStateError" runat="server" Text="*" ForeColor="Red" style="visibility:hidden;"></asp:Label>
                                            <asp:RequiredFieldValidator ID="rfState" runat="server" ControlToValidate="ddlState" ForeColor="Red" Enabled="false"></asp:RequiredFieldValidator>
                                        </asp:TableHeaderCell>
                                        <asp:TableHeaderCell>
                                            Zip Code<font color="red">*</font>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtZipCode" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </asp:TableHeaderCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell ColumnSpan="2">
                                            <telerik:RadTextBox ID="txtAddress1" runat="server" Width="100%"></telerik:RadTextBox>
                                        </asp:TableCell>
                                        <asp:TableCell ColumnSpan="2">
                                            <telerik:RadTextBox ID="txtAddress2" runat="server" Width="100%"></telerik:RadTextBox>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <telerik:RadDropDownList ID="ddlCountry" runat="server" AppendDataBoundItems="true" DefaultMessage="--Select--" Width="100%" DropDownHeight="240px" OnClientSelectedIndexChanged="EnableDisableState" CausesValidation="false"></telerik:RadDropDownList>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <telerik:RadTextBox ID="txtCity" runat="server" Width="100%"></telerik:RadTextBox>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <telerik:RadDropDownList ID="ddlState" runat="server" AppendDataBoundItems="true" DefaultMessage="--Select--" DropDownHeight="240px" CausesValidation="false" OnClientSelectedIndexChanged="EnableDisableState"></telerik:RadDropDownList>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <telerik:RadTextBox ID="txtZipCode" runat="server"></telerik:RadTextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableHeaderCell ColumnSpan="2">
                                            Upload File
                                        </asp:TableHeaderCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell ColumnSpan="2">
                                            <telerik:RadAsyncUpload RenderMode="Lightweight" TemporaryFolder="~/App_Data/RadUploadTemp" runat="server" ID="AttachmentUpload" MultipleFileSelection="Automatic" Width="100%" />
                                            <telerik:RadProgressArea RenderMode="Lightweight" runat="server" ID="RadProgressArea1"  />
                                        </asp:TableCell>

 <%--                                       Reps Name
Parent Name
email
instagram handle
reps age
reps birthday
pay pal email
How did you hear about us?
Reps bio and rep resume -Please insure shop names are spelled correctly and include @--%>


                                    </asp:TableRow>
                                </asp:Table>
                                <asp:Table ID="Table3" runat="server" Width="100%" HorizontalAlign="Center">
                                    <asp:TableRow>
                                        <asp:TableCell Width="49%"></asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Button ID="btnSave" runat="server" Text="Submit Request" OnClientClick="return OnClickValidate()" OnClick="btnSave_Click"/>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Button ID="btnClear" runat="server" Text="Clear" OnClientClick="return Clear()" OnClick="btnClear_Click" CausesValidation="false"></asp:Button>
                                        </asp:TableCell>
                                        <asp:TableCell Width="50%"></asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                                <div align="center">
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Please enter valid e-mail address" ValidationExpression="^[\w\.\-]+@[a-zA-Z0-9\-]+(\.[a-zA-Z0-9\-]{1,})*(\.[a-zA-Z]{2,3}){1,2}$" ControlToValidate="txtEmail" Display="Dynamic" ForeColor="Red"></asp:RegularExpressionValidator>
                                    <asp:label ID="lblMessage" runat="server" Text="Please enter valid company e-mail address" ForeColor="Red" Style="display: none;"></asp:label>
                                </div>
                                <asp:Table ID="Table4" runat="server" Width="100%" HorizontalAlign="Center">
                                    <asp:TableRow>
                                        <asp:TableCell Width="25%" HorizontalAlign="Center">
                                            <asp:Label ID="lblRequirements" runat="server" Text="Kindly enter valid data for above fields including valid PayPal email" ForeColor="Turquoise"></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </ContentTemplate>
               <Triggers>
                    <asp:PostBackTrigger ControlID="btnSave" />
               </Triggers>
            </asp:UpdatePanel>
        </fieldset>
</asp:Content>