<%@ Page Title="View Requests" Language="C#" MasterPageFile="~/IGBrandRepReferral.Master" AutoEventWireup="true" CodeBehind="ViewRequests.aspx.cs" Inherits="IGBrandRepReferral.ViewRequests" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="customCss" runat="server"></asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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

    <asp:UpdateProgress ID="loader" runat="server" DisplayAfter="0">
        <ProgressTemplate>

            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; padding-top: 15%; z-index: 9999999;">

                <div class="loader2">Loading...</div>

            </div>

        </ProgressTemplate>
    </asp:UpdateProgress>

    <fieldset>
        <asp:UpdatePanel runat="server" UpdateMode="Conditional">
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
                                        First Name
                                    </asp:TableHeaderCell>
                                    <asp:TableHeaderCell>
                                        Last Name
                                    </asp:TableHeaderCell>
                                    <asp:TableHeaderCell>
                                        Company
                                    </asp:TableHeaderCell>
                                    <asp:TableHeaderCell>
                                        Demo Requested
                                    </asp:TableHeaderCell>
                                    <asp:TableHeaderCell></asp:TableHeaderCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <telerik:RadTextBox ID="txtFirstName" runat="server"></telerik:RadTextBox>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <telerik:RadTextBox ID="txtLastName" runat="server"></telerik:RadTextBox>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <telerik:RadTextBox ID="txtCompany" runat="server"></telerik:RadTextBox>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <telerik:RadDropDownList ID="ddlDemo" runat="server" AppendDataBoundItems="true">
                                            <Items>
                                                <telerik:DropDownListItem Value="0" Text="--Select--" />
                                            </Items>
                                        </telerik:RadDropDownList>
                                    </asp:TableCell>
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