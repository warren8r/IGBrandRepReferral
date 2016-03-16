<%@ Page Title="View Requests" Language="C#" MasterPageFile="~/IGBrandRepReferral.Master" AutoEventWireup="true" CodeBehind="ViewRequests.aspx.cs" Inherits="IGBrandRepReferral.ViewRequests" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="customCss" runat="server"></asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
    <script type="text/javascript">
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
    </script>
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

    <asp:UpdateProgress ID="loader" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div id="div1" class="modal" visible="false">
                <div class="center">
                    <img alt="" src="/Images/ripple.gif" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <fieldset>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Table ID="MainPage" runat="server" HorizontalAlign="Center" Width="100%">
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="Center">
                            <h2>View Requests</h2>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                <asp:Table runat="server" HorizontalAlign="Center">
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Table runat="server" HorizontalAlign="Center">
                                <asp:TableRow>
                                    <asp:TableHeaderCell>
                                        Rep's Name
                                    </asp:TableHeaderCell>
                                    <asp:TableHeaderCell>
                                        Parent's Name
                                    </asp:TableHeaderCell>
                                    <asp:TableHeaderCell>
                                        How Did You Hear?
                                    </asp:TableHeaderCell>
                                    <asp:TableHeaderCell>
                                        Instagram Username
                                    </asp:TableHeaderCell>
                                    <asp:TableHeaderCell></asp:TableHeaderCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <telerik:RadTextBox ID="txtRepsName" runat="server"></telerik:RadTextBox>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <telerik:RadTextBox ID="txtParentsName" runat="server"></telerik:RadTextBox>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <telerik:RadDropDownList ID="ddlHowHear" runat="server"></telerik:RadDropDownList>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <telerik:RadTextBox ID="txtInstagramUsername" runat="server"></telerik:RadTextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableHeaderCell>
                                        Rep's Birthday Range (Begin)
                                    </asp:TableHeaderCell>
                                    <asp:TableHeaderCell>
                                        Rep's Birthday Range (End)
                                    </asp:TableHeaderCell>
                                    <asp:TableHeaderCell>
                                        Email
                                    </asp:TableHeaderCell>
                                    <asp:TableHeaderCell>
                                        Has Small Shop?
                                    </asp:TableHeaderCell>
                                    <asp:TableHeaderCell>
                                        Small Shop Name
                                    </asp:TableHeaderCell>
                                    <asp:TableHeaderCell></asp:TableHeaderCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <telerik:RadDatePicker ID="dpBirthdayBegin" runat="server"></telerik:RadDatePicker>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <telerik:RadDatePicker ID="dpBirthdayEnd" runat="server"></telerik:RadDatePicker>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <telerik:RadTextBox ID="txtEmail" runat="server"></telerik:RadTextBox>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <telerik:RadDropDownList ID="ddHasSmallShop" runat="server"></telerik:RadDropDownList>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <telerik:RadTextBox ID="txtShopName" runat="server"></telerik:RadTextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableHeaderCell>
                                        PayPal Invoice Number
                                    </asp:TableHeaderCell>
                                    <asp:TableHeaderCell>
                                        PayPal Email
                                    </asp:TableHeaderCell>
                                    <asp:TableHeaderCell>
                                        Request Date Range (Begin)
                                    </asp:TableHeaderCell>
                                    <asp:TableHeaderCell>
                                        Request Date Range (End)
                                    </asp:TableHeaderCell>
                                    <asp:TableHeaderCell>
                                        Has Paid?
                                    </asp:TableHeaderCell>
                                    <asp:TableHeaderCell></asp:TableHeaderCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <telerik:RadTextBox ID="txtPayPalInvoiceNumber" runat="server"></telerik:RadTextBox>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <telerik:RadTextBox ID="txtPayPalEmail" runat="server"></telerik:RadTextBox>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <telerik:RadDatePicker ID="dpRequestBegin" runat="server"></telerik:RadDatePicker>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <telerik:RadDatePicker ID="dpRequestEnd" runat="server"></telerik:RadDatePicker>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <telerik:RadDropDownList ID="ddHasPaid" runat="server"></telerik:RadDropDownList>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <telerik:RadButton ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"></telerik:RadButton>
                                        <telerik:RadButton ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click"></telerik:RadButton>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>

                <telerik:RadGrid ID="RadGrid1" OnPageSizeChanged="RadGrid1_PageSizeChanged" OnPageIndexChanged="RadGrid1_PageIndexChanged" AllowPaging="True" AllowFilteringByColumn="false" AllowSorting="true" runat="server" AutoGenerateColumns="false" OnNeedDataSource="RadGrid1_NeedDataSource" OnItemDataBound="RadGrid1_ItemDataBound">
                        <PagerStyle Mode="NextPrevAndNumeric" AlwaysVisible="true" Position="Bottom" PageSizeControlType="RadComboBox"></PagerStyle>
                    <MasterTableView DataKeyNames="ID, HasPaid, AttachmentExists" PagerStyle-AlwaysVisible="true">
                        <Columns>
                            <telerik:GridEditCommandColumn UniqueName="EditCommandColumn" HeaderText="Click to Edit">
                                <ItemStyle Width="50px"></ItemStyle>
                            </telerik:GridEditCommandColumn>                            
                            <telerik:GridBoundColumn HeaderText="Rep's Name" DataField="RepsName" UniqueName="RepsName"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Parent's Name" DataField="ParentsName" UniqueName="ParentsName"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Email" DataField="Email" UniqueName="Email"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Instagram Username" DataField="InstagramUsername" UniqueName="InstagramUsername"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Rep's Birthday" DataField="RepsBirthday" UniqueName="RepsBirthday" DataFormatString="{0:MM/dd/yyyy}"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="PayPal Email" DataField="PayPalEmail" UniqueName="PayPalEmail"></telerik:GridBoundColumn>
                            <telerik:GridHTMLEditorColumn HeaderText="Rep's Bio/Resume" DataField="RepsBioResume" UniqueName="RepsBioResume"></telerik:GridHTMLEditorColumn>
                            <telerik:GridBoundColumn HeaderText="Have Small Shop?" DataField="HaveSmallShop" UniqueName="HaveSmallShop"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Small Shop Name" DataField="SmallShopUsername" UniqueName="SmallShopUsername"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="How Did You Hear?" DataField="HowHearDesc" UniqueName="HowHearDesc"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="What Do You Want?" DataField="WhatDoYouWant" UniqueName="WhatDoYouWant"></telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn HeaderText="Has Paid?" ItemStyle-Width="240px" DataField="HasPaid" UniqueName="HasPaid">
                                <ItemTemplate> 
                                    <%# Eval("HasPaid").ToString()=="False"?"No":"Yes" %>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <telerik:RadDropDownList runat="server" ID="ddlRadGridHasPaid"></telerik:RadDropDownList>
                                </EditItemTemplate>
                            </telerik:GridTemplateColumn>                            
                            <telerik:GridBoundColumn HeaderText="PayPal Invoice Number" DataField="PayPalInvoiceNumber" UniqueName="PayPalInvoiceNumber"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Request Date" DataField="RequestDate" UniqueName="RequestDate"></telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn HeaderText="Attachments">
                                <ItemTemplate>
                                    <telerik:RadButton ID="btnDownloadAttachment" runat="server" Text="View Attachments" OnClick="btnDownloadAttachment_Click"></telerik:RadButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
                <telerik:RadWindow runat="server" ID="RadWindow1" Width="1080" Height="800" Modal="true"></telerik:RadWindow>
            </ContentTemplate>
        </asp:UpdatePanel>
    </fieldset>

</asp:Content>