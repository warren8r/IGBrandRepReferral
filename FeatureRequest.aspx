<%@ Page Title="Feature Request" Language="C#" MasterPageFile="~/IGBrandRepReferral.Master" AutoEventWireup="true" CodeBehind="FeatureRequest.aspx.cs" Inherits="IGBrandRepReferral.FeatureRequest" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="customCss" runat="server"></asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
    <script type="text/javascript">

        function CheckEmail() {
            var email = document.getElementById('<%=txtEmail.ClientID%>');
            var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;

            if (!filter.test(email.value)) {
                email.style.border = '1px solid red';
                email.focus;
                return false;
            }
            else {
                email.style.border = '';
                return true;
            }
        }

        function CheckPayPalEmail() {
            var email = document.getElementById('<%=txtPayPalEmail.ClientID%>');
                    var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;

                    if (!filter.test(email.value)) {
                        email.style.border = '1px solid red';
                        email.focus;
                        return false;
                    }
                    else {
                        email.style.border = '';
                        return true;
                    }
        }

        function IsValidEmail(sender, eventArgs) {
            var email = eventArgs.get_newValue();
            var id = '<%=lblMessage.ClientID%>';
            var lblMessage2 = document.getElementById(id);
            lblMessage2.style.display = 'none';
            
        }

        function OnClickValidate() {
            if (Page_ClientValidate()) {
                var updateProgress = $get("<%= loader.ClientID %>");
                updateProgress.style.display = "block";
                return true;
            }
            else if (document.getElementById('<%=cbHowHear.ClientID%>').value == '--Select--') {
                document.getElementById('<%=cbHowHear.ClientID%>').style.border = '1px solid red';
            }
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

        function ClearCBValidation() {
            var myVal = document.getElementById('<%=HowHearValidator.ClientID%>');
            ValidatorEnable(myVal, false);
        }

        function Clear() {
            var myVal = document.getElementById('<%=RepsNameValidator.ClientID%>');
            ValidatorEnable(myVal, false);
            document.getElementById('<%=txtRepsName.ClientID%>').value = "";

            myVal = document.getElementById('<%=ParentsNameValidator.ClientID%>');
            ValidatorEnable(myVal, false);
            document.getElementById('<%=txtParentsName.ClientID%>').value = "";

            myVal = document.getElementById('<%=EmailValidator.ClientID%>');
            ValidatorEnable(myVal, false);
            document.getElementById('<%=txtEmail.ClientID%>').value = "";

            myVal = document.getElementById('<%=InstagramUsernameValidator.ClientID%>');
            ValidatorEnable(myVal, false);
            document.getElementById('<%=txtInstagramUsername.ClientID%>').value = "";

            myVal = document.getElementById('<%=RepsBirthdayValidator.ClientID%>');
            ValidatorEnable(myVal, false);

            myVal = document.getElementById('<%=PayPalEmailValidator.ClientID%>');
            ValidatorEnable(myVal, false);
            document.getElementById('<%=txtPayPalEmail.ClientID%>').value = "";

            myVal = document.getElementById('<%=RepsBioResumeValidator.ClientID%>');
            ValidatorEnable(myVal, false);
            document.getElementById('<%=txtRepsBioResume.ClientID%>').value = "";

            myVal = document.getElementById('<%=HowHearValidator.ClientID%>');
            ValidatorEnable(myVal, false);

            document.getElementById('<%=chkHaveSmallShop.ClientID%>').checked = false;
            document.getElementById('<%=txtHaveSmallShop.ClientID%>').value = "";

            return true;
        }

    </script>
    <link href="/Styles/jquery-ui.min.css" rel="stylesheet" />

    <link href="/Styles/jquery-ui.theme.min.css" rel="stylesheet" />

    <script src="/js/jquery-1.12.1.js"></script>

    <script src="/js/jquery-ui.js"></script>

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
                                <asp:Table ID="Table2" runat="server" HorizontalAlign="Center" Width="15%">
                                    <asp:TableRow>
                                        <asp:TableHeaderCell>
                                            Rep's Name<font color="red">*</font>
                                            <asp:RequiredFieldValidator ID="RepsNameValidator" runat="server" ControlToValidate="txtRepsName" ForeColor="Red" ValidationGroup="MainValidation"></asp:RequiredFieldValidator>
                                        </asp:TableHeaderCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell>
                                            <telerik:RadTextBox ID="txtRepsName" runat="server" Width="100%"></telerik:RadTextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow Height="25px">
                                        <asp:TableCell>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableHeaderCell>
                                            Parent's Name<font color="red">*</font>
                                            <asp:RequiredFieldValidator ID="ParentsNameValidator" runat="server" ControlToValidate="txtParentsName" ForeColor="Red" ValidationGroup="MainValidation"></asp:RequiredFieldValidator>
                                        </asp:TableHeaderCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell>
                                            <telerik:RadTextBox ID="txtParentsName" runat="server" Width="100%"></telerik:RadTextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow Height="25px">
                                        <asp:TableCell>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableHeaderCell>
                                            E-Mail<font color="red">*</font>
                                            <asp:RequiredFieldValidator ID="EmailValidator" runat="server" ControlToValidate="txtEmail" ForeColor="Red" ValidationGroup="MainValidation"></asp:RequiredFieldValidator>
                                            <asp:ImageButton ID="ImageButton1" runat="server" OnClientClick="return false;" Width="18px" ImageUrl="~/images/info_small.png" />
                                            <telerik:RadToolTip ID="RadToolTip1" runat="server" Position="MiddleRight" RelativeTo="Element"
                                                TargetControlID="ImageButton1" Width="200px" HideEvent="ManualClose"
                                                OnClientShow="OnClientShow">
                                                Must be a valid e-mail address.
                                            </telerik:RadToolTip>
                                        </asp:TableHeaderCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell>
                                            <asp:TextBox ID="txtEmail" runat="server" onchange="javascript: CheckEmail();" Width="100%"></asp:TextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow Height="25px">
                                        <asp:TableCell>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableHeaderCell>
                                            Instagram Username<font color="red">*</font>
                                            <asp:RequiredFieldValidator ID="InstagramUsernameValidator" runat="server" ControlToValidate="txtInstagramUsername" ForeColor="Red" ValidationGroup="MainValidation"></asp:RequiredFieldValidator>
                                        </asp:TableHeaderCell>
                                   </asp:TableRow>
                                   <asp:TableRow>
                                        <asp:TableCell>
                                            <telerik:RadTextBox ID="txtInstagramUsername" runat="server" Width="100%"></telerik:RadTextBox>
                                        </asp:TableCell>
                                   </asp:TableRow>
                                    <asp:TableRow Height="25px">
                                        <asp:TableCell>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                   <asp:TableRow>
                                        <asp:TableHeaderCell>
                                            Rep's Birthday<font color="red">*</font>
                                            <asp:RequiredFieldValidator ID="RepsBirthdayValidator" ErrorMessage=" Happy __ :)" runat="server" ControlToValidate="dpRepsBirthday" ForeColor="Red" ValidationGroup="MainValidation"></asp:RequiredFieldValidator>
                                        </asp:TableHeaderCell>
                                   </asp:TableRow>
                                   <asp:TableRow>
                                        <asp:TableCell>
                                            <telerik:RadDatePicker ID="dpRepsBirthday" runat="server" Width="100%"></telerik:RadDatePicker>
                                        </asp:TableCell>
                                   </asp:TableRow>
                                    <asp:TableRow Height="25px">
                                        <asp:TableCell>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                   <asp:TableRow>
                                        <asp:TableHeaderCell>
                                            Pay Pal E-Mail<font color="red">*</font>
                                            <asp:RequiredFieldValidator ID="PayPalEmailValidator" runat="server" ControlToValidate="txtPayPalEmail" ForeColor="Red" ValidationGroup="MainValidation"></asp:RequiredFieldValidator>
                                            <asp:ImageButton ID="PeriodToolTip" runat="server" OnClientClick="return false;" Width="18px" ImageUrl="~/images/info_small.png" />
                                            <telerik:RadToolTip ID="RadToolTip4" runat="server" Position="MiddleRight" RelativeTo="Element"
                                                TargetControlID="PeriodToolTip" Width="200px" HideEvent="ManualClose"
                                                OnClientShow="OnClientShow">
                                                Must be a valid e-mail address.
                                            </telerik:RadToolTip>
                                        </asp:TableHeaderCell>
                                   </asp:TableRow>
                                   <asp:TableRow>
                                        <asp:TableCell>
                                            <asp:TextBox ID="txtPayPalEmail" runat="server" onchange="javascript: CheckPayPalEmail();" Width="100%"></asp:TextBox>
                                        </asp:TableCell>
                                   </asp:TableRow>
                                    <asp:TableRow Height="25px">
                                        <asp:TableCell>
                                        </asp:TableCell>
                                   </asp:TableRow>
                                </asp:Table>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                    <asp:Table ID="Table5" runat="server" HorizontalAlign="Center">
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Table ID="Table6" runat="server" HorizontalAlign="Center" Width="15%">
                                   <asp:TableRow>
                                        <asp:TableHeaderCell>
                                            Rep's Bio/Resume<font color="red">*</font>
                                            <asp:RequiredFieldValidator ID="RepsBioResumeValidator" runat="server" ControlToValidate="txtRepsBioResume" ForeColor="Red" ValidationGroup="MainValidation"></asp:RequiredFieldValidator>
                                        </asp:TableHeaderCell>
                                   </asp:TableRow>
                                   <asp:TableRow>
                                        <asp:TableCell>
                                                 <telerik:RadTextBox ID="txtRepsBioResume" runat="server" Width="672px" TextMode="MultiLine" Height="400px"></telerik:RadTextBox> 
                                        </asp:TableCell>
                                   </asp:TableRow>
                                    <asp:TableRow Height="25px">
                                        <asp:TableCell>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                    <asp:Table ID="Table7" runat="server" HorizontalAlign="Center">
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Table ID="Table8" runat="server" HorizontalAlign="Center" Width="35%">
                                   <asp:TableRow>
                                        <asp:TableHeaderCell>
                                            Do You Have a Small Shop?
                                        </asp:TableHeaderCell>
                                   </asp:TableRow>
                                   <asp:TableRow>
                                        <asp:TableCell>
                                            <asp:CheckBox ID="chkHaveSmallShop" runat="server" Text="Yes?" OnCheckedChanged="chkHaveSmallShop_CheckedChanged" AutoPostBack="true"/>
                                            <telerik:RadTextBox ID="txtHaveSmallShop" DisplayText="Instagram Username?" Visible="false" runat="server"></telerik:RadTextBox>
                                        </asp:TableCell>
                                   </asp:TableRow>
                                    <asp:TableRow Height="25px">
                                        <asp:TableCell>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                   <asp:TableRow>
                                        <asp:TableHeaderCell>
                                            How Did You Hear About Us?<font color="red">*</font>
                                            <asp:RequiredFieldValidator ID="HowHearValidator" runat="server" ControlToValidate="cbHowHear" ForeColor="Red" ValidationGroup="MainValidation"></asp:RequiredFieldValidator>
                                        </asp:TableHeaderCell>
                                   </asp:TableRow>
                                   <asp:TableRow>
                                        <asp:TableCell>
                                            <telerik:RadDropDownList ID="cbHowHear" runat="server" Enabled="true" CausesValidation="false" Width="100%" OnClientSelectedIndexChanged="ClearCBValidation"></telerik:RadDropDownList>
                                        </asp:TableCell>
                                   </asp:TableRow>
                                    <asp:TableRow Height="25px">
                                        <asp:TableCell>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                   <asp:TableRow>
                                        <asp:TableHeaderCell>
                                            Please Describe What You Need or Want from your Instagram Brand Rep Community and How We Can Best Support You?
                                        </asp:TableHeaderCell>
                                   </asp:TableRow>
                                   <asp:TableRow>
                                        <asp:TableCell>
                                            <telerik:RadTextBox ID="txtWhatDoYouWant" runat="server" Width="100%" TextMode="MultiLine" Height="100px"></telerik:RadTextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow Height="25px">
                                        <asp:TableCell>
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
                                        </asp:TableCell>
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
                                    <br /><asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Please enter valid PayPal e-mail address" ValidationExpression="^[\w\.\-]+@[a-zA-Z0-9\-]+(\.[a-zA-Z0-9\-]{1,})*(\.[a-zA-Z]{2,3}){1,2}$" ControlToValidate="txtPayPalEmail" Display="Dynamic" ForeColor="Red"></asp:RegularExpressionValidator>
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