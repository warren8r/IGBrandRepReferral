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
                                // $(this).dialog("close");
                                $("#confirmDialog").dialog("close");

                                // _DialogConfirmed(); // MUST implement on local page
                            }
                        }

                    });

            });
            //Dialog('Error', content);
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
                                        ID
                                    </asp:TableHeaderCell>
                                    <asp:TableHeaderCell>
                                        Rep's Name
                                    </asp:TableHeaderCell>
                                    <asp:TableHeaderCell>
                                        Parent's Name
                                    </asp:TableHeaderCell>
                                    <asp:TableHeaderCell>
                                        How Did You Hear?
                                    </asp:TableHeaderCell>
                                    <asp:TableHeaderCell></asp:TableHeaderCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <telerik:RadTextBox ID="txtID" runat="server"></telerik:RadTextBox>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <telerik:RadTextBox ID="txtRepsName" runat="server"></telerik:RadTextBox>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <telerik:RadTextBox ID="txtParentsName" runat="server"></telerik:RadTextBox>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <telerik:RadDropDownList ID="ddlHowHear" runat="server" AppendDataBoundItems="true">
                                            <Items>
                                                <telerik:DropDownListItem Value="0" Text="--Select--" />
                                            </Items>
                                        </telerik:RadDropDownList>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableHeaderCell>
                                        Instagram Username
                                    </asp:TableHeaderCell>
                                    <asp:TableHeaderCell>
                                        Rep's Age
                                    </asp:TableHeaderCell>
                                    <asp:TableHeaderCell>
                                        PayPal Email
                                    </asp:TableHeaderCell>
                                    <asp:TableHeaderCell>
                                        Do you have a small shop?
                                    </asp:TableHeaderCell>
                                    <asp:TableHeaderCell></asp:TableHeaderCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <telerik:RadTextBox ID="RadTextBox1" runat="server"></telerik:RadTextBox>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <telerik:RadTextBox ID="RadTextBox2" runat="server"></telerik:RadTextBox>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <telerik:RadTextBox ID="RadTextBox3" runat="server"></telerik:RadTextBox>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <telerik:RadDropDownList ID="RadDropDownList1" runat="server" AppendDataBoundItems="true">
                                            <Items>
                                                <telerik:DropDownListItem Value="0" Text="--Select--" />
                                            </Items>
                                        </telerik:RadDropDownList>
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

                <telerik:RadGrid ID="RadGrid1" runat="server" AutoGenerateColumns="false" OnNeedDataSource="RadGrid1_NeedDataSource" OnItemDataBound="RadGrid1_ItemDataBound">
                    <MasterTableView DataKeyNames="RequestID, AttachmentExists">
                        <Columns>
                            <telerik:GridBoundColumn HeaderText="First Name" DataField="FirstName" UniqueName="FirstName"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Last Name" DataField="LastName" UniqueName="LastName"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Phone Number" DataField="PhoneNumber" UniqueName="PhoneNumber"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="E-Mail" DataField="Email" UniqueName="Email"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Company Name" DataField="CompanyName" UniqueName="CompanyName"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Company URL" DataField="CompanyURL" UniqueName="CompanyURL"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Demos" DataField="DemoName" UniqueName="DemoName"></telerik:GridBoundColumn>
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
            <%--<Triggers>
                <asp:PostBackTrigger ControlID="btnDownloadAttachment" />
            </Triggers>--%>
        </asp:UpdatePanel>
    </fieldset>

</asp:Content>