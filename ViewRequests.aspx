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
            body {
                margin: 0;
                padding: 0;
                font-family: Arial;
            }

            .modal {
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

            .center {
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

                .center img {
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
                    <asp:Timer ID="Timer1" runat="server" Interval="600000">
                    </asp:Timer>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <fieldset>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="dashboard">
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 100%">
                                <h2 align="center">IG Brand Rep Referral Dashboard for today,
                                    <asp:Label ID="TodaysDate" runat="server" /></h2>
                            </td>
                        </tr>
                    </table>
                    <hr />
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 35%" alignt="center">
                                <h4 align="center">This Week's Birthdays!</h4>
                            </td>
                            <td style="width: 15%"></td>
                            <td style="width: 35%" align="center">
                                <h4 align="center">Number of Features Requested Per Day (for the last 7 days)</h4>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 35%">
                                <table style="width: 100%">
                                    <tr id="BirthdayMessage" style="display:none;" runat="server">
                                        <td><font size="ExtraLarge" color="turquoise">No Birthdays This Week!</font></td>
                                    </tr>
                                    <tr id="TodayRow" style="display:none;" runat="server">
                                        <td><font size="2" color="turquoise">Birthdays for <asp:label id="Today" runat="server" /></font></td>
                                    </tr>
                                    <tr id="TodayGrid" style="display:none;" runat="server">
                                        <td>
                                            <telerik:RadGrid ID="TodaysBirthdayGrid" AllowFilteringByColumn="false" AllowSorting="true" runat="server" AutoGenerateColumns="false" OnNeedDataSource="TodaysBirthdayGrid_NeedDataSource">
                                                <MasterTableView DataKeyNames="ID">
                                                    <Columns>
                                                        <telerik:GridBoundColumn HeaderText="Rep's Name" DataField="RepsName" UniqueName="RepsName"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn HeaderText="Parent's Name" DataField="ParentsName" UniqueName="ParentsName"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn HeaderText="Email" DataField="Email" UniqueName="Email"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn HeaderText="Rep's Birthday" DataField="RepsBirthday" UniqueName="RepsBirthday" DataFormatString="{0:MM/dd/yyyy}"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn HeaderText="Age" DataField="Age" UniqueName="Age"></telerik:GridBoundColumn>
                                                    </Columns>
                                                </MasterTableView>
                                            </telerik:RadGrid>
                                        </td>
                                    </tr>
                                    <tr id="TomorrowRow" style="display:none;" runat="server">
                                        <td><font size="2" color="turquoise">Birthdays for <asp:label id="Tomorrow" runat="server" /></font></td>
                                    </tr>
                                    <tr id="TomorrowGrid" style="display:none;" runat="server">
                                        <td>
                                            <telerik:RadGrid ID="TomorrowsBirthdayGrid" AllowFilteringByColumn="false" AllowSorting="true" runat="server" AutoGenerateColumns="false" OnNeedDataSource="TomorrowsBirthdayGrid_NeedDataSource">
                                                <MasterTableView DataKeyNames="ID">
                                                    <Columns>
                                                        <telerik:GridBoundColumn HeaderText="Rep's Name" DataField="RepsName" UniqueName="RepsName"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn HeaderText="Parent's Name" DataField="ParentsName" UniqueName="ParentsName"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn HeaderText="Email" DataField="Email" UniqueName="Email"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn HeaderText="Rep's Birthday" DataField="RepsBirthday" UniqueName="RepsBirthday" DataFormatString="{0:MM/dd/yyyy}"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn HeaderText="Future Age" DataField="Age" UniqueName="Age"></telerik:GridBoundColumn>
                                                    </Columns>
                                                </MasterTableView>
                                            </telerik:RadGrid>
                                        </td>
                                    </tr>
                                    <tr id="Day3Row" style="display:none;" runat="server">
                                        <td><font size="2" color="turquoise">Birthdays for <asp:label id="Day3" runat="server" /></font></td>
                                    </tr>
                                    <tr id="Day3Grid" style="display:none;" runat="server">
                                        <td>
                                            <telerik:RadGrid ID="Day3sBirthdayGrid" AllowFilteringByColumn="false" AllowSorting="true" runat="server" AutoGenerateColumns="false" OnNeedDataSource="Day3sBirthdayGrid_NeedDataSource">
                                                <MasterTableView DataKeyNames="ID">
                                                    <Columns>
                                                        <telerik:GridBoundColumn HeaderText="Rep's Name" DataField="RepsName" UniqueName="RepsName"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn HeaderText="Parent's Name" DataField="ParentsName" UniqueName="ParentsName"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn HeaderText="Email" DataField="Email" UniqueName="Email"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn HeaderText="Rep's Birthday" DataField="RepsBirthday" UniqueName="RepsBirthday" DataFormatString="{0:MM/dd/yyyy}"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn HeaderText="Future Age" DataField="Age" UniqueName="Age"></telerik:GridBoundColumn>
                                                    </Columns>
                                                </MasterTableView>
                                            </telerik:RadGrid>
                                        </td>
                                    </tr>
                                    <tr id="Day4Row" style="display:none;" runat="server">
                                        <td><font size="2" color="turquoise">Birthdays for <asp:label id="Day4" runat="server" /></font></td>
                                    </tr>
                                    <tr id="Day4Grid" style="display:none;" runat="server">
                                        <td>
                                            <telerik:RadGrid ID="Day4sBirthdayGrid" AllowFilteringByColumn="false" AllowSorting="true" runat="server" AutoGenerateColumns="false" OnNeedDataSource="Day4sBirthdayGrid_NeedDataSource">
                                                <MasterTableView DataKeyNames="ID">
                                                    <Columns>
                                                        <telerik:GridBoundColumn HeaderText="Rep's Name" DataField="RepsName" UniqueName="RepsName"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn HeaderText="Parent's Name" DataField="ParentsName" UniqueName="ParentsName"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn HeaderText="Email" DataField="Email" UniqueName="Email"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn HeaderText="Rep's Birthday" DataField="RepsBirthday" UniqueName="RepsBirthday" DataFormatString="{0:MM/dd/yyyy}"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn HeaderText="Future Age" DataField="Age" UniqueName="Age"></telerik:GridBoundColumn>
                                                    </Columns>
                                                </MasterTableView>
                                            </telerik:RadGrid>
                                        </td>
                                    </tr>
                                    <tr id="Day5Row" style="display:none;" runat="server">
                                        <td><font size="2" color="turquoise">Birthdays for <asp:label id="Day5" runat="server" /></font></td>
                                    </tr>
                                    <tr id="Day5Grid" style="display:none;" runat="server">
                                        <td>
                                            <telerik:RadGrid ID="Day5sBirthdayGrid" AllowFilteringByColumn="false" AllowSorting="true" runat="server" AutoGenerateColumns="false" OnNeedDataSource="Day5sBirthdayGrid_NeedDataSource">
                                                <MasterTableView DataKeyNames="ID">
                                                    <Columns>
                                                        <telerik:GridBoundColumn HeaderText="Rep's Name" DataField="RepsName" UniqueName="RepsName"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn HeaderText="Parent's Name" DataField="ParentsName" UniqueName="ParentsName"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn HeaderText="Email" DataField="Email" UniqueName="Email"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn HeaderText="Rep's Birthday" DataField="RepsBirthday" UniqueName="RepsBirthday" DataFormatString="{0:MM/dd/yyyy}"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn HeaderText="Future Age" DataField="Age" UniqueName="Age"></telerik:GridBoundColumn>
                                                    </Columns>
                                                </MasterTableView>
                                            </telerik:RadGrid>
                                        </td>
                                    </tr>
                                    <tr id="Day6Row" style="display:none;" runat="server">
                                        <td><font size="2" color="turquoise">Birthdays for <asp:label id="Day6" runat="server" /></font></td>
                                    </tr>
                                    <tr id="Day6Grid" style="display:none;" runat="server">
                                        <td>
                                            <telerik:RadGrid ID="Day6sBirthdayGrid" AllowFilteringByColumn="false" AllowSorting="true" runat="server" AutoGenerateColumns="false" OnNeedDataSource="Day6sBirthdayGrid_NeedDataSource">
                                                <MasterTableView DataKeyNames="ID">
                                                    <Columns>
                                                        <telerik:GridBoundColumn HeaderText="Rep's Name" DataField="RepsName" UniqueName="RepsName"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn HeaderText="Parent's Name" DataField="ParentsName" UniqueName="ParentsName"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn HeaderText="Email" DataField="Email" UniqueName="Email"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn HeaderText="Rep's Birthday" DataField="RepsBirthday" UniqueName="RepsBirthday" DataFormatString="{0:MM/dd/yyyy}"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn HeaderText="Future Age" DataField="Age" UniqueName="Age"></telerik:GridBoundColumn>
                                                    </Columns>
                                                </MasterTableView>
                                            </telerik:RadGrid>
                                        </td>
                                    </tr>
                                    <tr id="Day7Row" style="display:none;" runat="server">
                                        <td><font size="2" color="turquoise">Birthdays for <asp:label id="Day7" runat="server" /></font></td>
                                    </tr>
                                    <tr id="Day7Grid" style="display:none;" runat="server">
                                        <td>
                                            <telerik:RadGrid ID="Day7sBirthdayGrid" AllowFilteringByColumn="false" AllowSorting="true" runat="server" AutoGenerateColumns="false" OnNeedDataSource="Day7sBirthdayGrid_NeedDataSource">
                                                <MasterTableView DataKeyNames="ID">
                                                    <Columns>
                                                        <telerik:GridBoundColumn HeaderText="Rep's Name" DataField="RepsName" UniqueName="RepsName"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn HeaderText="Parent's Name" DataField="ParentsName" UniqueName="ParentsName"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn HeaderText="Email" DataField="Email" UniqueName="Email"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn HeaderText="Rep's Birthday" DataField="RepsBirthday" UniqueName="RepsBirthday" DataFormatString="{0:MM/dd/yyyy}"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn HeaderText="Future Age" DataField="Age" UniqueName="Age"></telerik:GridBoundColumn>
                                                    </Columns>
                                                </MasterTableView>
                                            </telerik:RadGrid>
                                        </td>
                                    </tr>
                                    <tr id="EndOfWeekRow" style="display:none;" runat="server">
                                        <td><font size="2" color="turquoise">Birthdays for <asp:label id="EndOfWeek" runat="server" /></font></td>
                                    </tr>
                                    <tr id="EndOfWeekGrid" style="display:none;" runat="server">
                                        <td>
                                            <telerik:RadGrid ID="EndOfWeeksBirthdayGrid" AllowFilteringByColumn="false" AllowSorting="true" runat="server" AutoGenerateColumns="false" OnNeedDataSource="EndOfWeeksBirthdayGrid_NeedDataSource">
                                                <MasterTableView DataKeyNames="ID">
                                                    <Columns>
                                                        <telerik:GridBoundColumn HeaderText="Rep's Name" DataField="RepsName" UniqueName="RepsName"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn HeaderText="Parent's Name" DataField="ParentsName" UniqueName="ParentsName"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn HeaderText="Email" DataField="Email" UniqueName="Email"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn HeaderText="Rep's Birthday" DataField="RepsBirthday" UniqueName="RepsBirthday" DataFormatString="{0:MM/dd/yyyy}"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn HeaderText="Future Age" DataField="Age" UniqueName="Age"></telerik:GridBoundColumn>
                                                    </Columns>
                                                </MasterTableView>
                                            </telerik:RadGrid>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 15%"></td>
                            <td style="width: 35%" align="center">
                                <telerik:RadHtmlChart runat="server" ID="RadHtmlChart1" DataSourceID="SqlDataSource2">
                                    <PlotArea>
                                        <Series>
                                            <telerik:BarSeries Name="NumberOfFeatures" DataFieldY="TotalFeatures">
                                                <TooltipsAppearance Color="White"></TooltipsAppearance>
                                                <LabelsAppearance Visible="true">
                                                </LabelsAppearance>
                                            </telerik:BarSeries>
                                        </Series>
                                        <XAxis DataLabelsField="DayName">
                                            <MinorGridLines Visible="false"></MinorGridLines>
                                            <MajorGridLines Visible="false"></MajorGridLines>
                                        </XAxis>
                                        <YAxis Step="1">
                                            <LabelsAppearance Visible="true"></LabelsAppearance>
                                            <MinorGridLines Visible="false"></MinorGridLines>
                                        </YAxis>
                                    </PlotArea>
                                    <Legend>
                                        <Appearance Visible="false"></Appearance>
                                    </Legend>
                                </telerik:RadHtmlChart>
                                <br />
                                <font size="4" color="turquoise">Remaining Requests for the Week: <asp:label id="RemainingRequests" runat="server" /></font>
                            </td>
                            <td style="width: 30%"></td>
                        </tr>
                    </table>
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:Connection %>"
                        SelectCommand="SELECT COUNT([ID]) AS [TotalFeatures], CAST(DATENAME(dw, [CreatedDate]) AS NVARCHAR(150)) + ', ' + CONVERT(NVARCHAR(150), CAST([CreatedDate] as DATE), 101) AS DayName FROM [dbo].[FeatureRequest] WHERE [CreatedDate] >=dateadd(day,datediff(day,0,GetDate())- 7,0) GROUP BY DATENAME(dw, [CreatedDate]), CONVERT(DATE, [CreatedDate]) ORDER BY CONVERT(DATE, [CreatedDate]) DESC"></asp:SqlDataSource>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="viewrequests" runat="server" style="display: none">
                    <hr />
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

                    <telerik:RadGrid ID="RadGrid1" OnUpdateCommand="RadGrid1_UpdateCommand" OnPageSizeChanged="RadGrid1_PageSizeChanged" OnPageIndexChanged="RadGrid1_PageIndexChanged" AllowPaging="True" AllowFilteringByColumn="false" AllowSorting="true" runat="server" AutoGenerateColumns="false" OnNeedDataSource="RadGrid1_NeedDataSource" OnItemDataBound="RadGrid1_ItemDataBound">
                        <PagerStyle Mode="NextPrevAndNumeric" AlwaysVisible="true" Position="Bottom" PageSizeControlType="RadComboBox"></PagerStyle>
                        <MasterTableView DataKeyNames="ID, HasPaid, AttachmentExists" PagerStyle-AlwaysVisible="true">
                            <EditFormSettings>
                                <EditColumn UniqueName="EditCommandColumn" ButtonType="ImageButton" CancelImageUrl="/Images/Cancel2.gif"
                                    UpdateImageUrl="/Images/Update2.gif" >
                                </EditColumn>
                            </EditFormSettings>
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
                                <telerik:GridTemplateColumn HeaderText="Rep's Bio/Resume" DataField="RepsBioResume" UniqueName="RepsBioResume" ItemStyle-Font-Size="0.1px">
                                    <ItemTemplate>
                                        <%# Eval("RepsBioResume") %>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <telerik:RadEditor runat="server" ID="txtRadGridRepsBioResume" Content='<%#Bind("RepsBioResume") %>'></telerik:RadEditor>
                                    </EditItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn HeaderText="Have Small Shop?" DataField="HaveSmallShop" UniqueName="HaveSmallShop"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Small Shop Name" DataField="SmallShopUsername" UniqueName="SmallShopUsername"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="How Did You Hear?" DataField="HowHearDesc" UniqueName="HowHearDesc"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="What Do You Want?" DataField="WhatDoYouWant" UniqueName="WhatDoYouWant" ItemStyle-Font-Size="0.1px"></telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn HeaderText="Has Paid?" DataField="HasPaid" UniqueName="HasPaid">
                                    <ItemTemplate>
                                        <%# Eval("HasPaid").ToString()=="False"?"No":"Yes" %>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <telerik:RadDropDownList runat="server" ID="ddlRadGridHasPaid"></telerik:RadDropDownList>
                                    </EditItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn HeaderText="PayPal Invoice Number" DataField="PayPalInvoiceNumber" UniqueName="PayPalInvoiceNumber"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Request Date" DataField="RequestDate" UniqueName="RequestDate"></telerik:GridBoundColumn>
                                <%--                            <telerik:GridHTMLEditorColumn HeaderText="Notes" DataField="Notes" UniqueName="Notes"></telerik:GridHTMLEditorColumn>--%>
                                <telerik:GridTemplateColumn HeaderText="Notes" DataField="Notes" UniqueName="Notes">
                                    <ItemTemplate>
                                        <%# Eval("Notes") %>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <telerik:RadEditor runat="server" ID="txtRadGridNotes" Content='<%#Bind("Notes") %>'></telerik:RadEditor>
                                    </EditItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Attachments">
                                    <ItemTemplate>
                                        <telerik:RadButton ID="btnDownloadAttachment" runat="server" Text="View Attachments" OnClick="btnDownloadAttachment_Click"></telerik:RadButton>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                    <telerik:RadWindow runat="server" ID="RadWindow1" Width="1080" Height="800" Modal="true"></telerik:RadWindow>
                </div>
                <asp:Table ID="Table1" runat="server" HorizontalAlign="Center">
                    <asp:TableRow>
                        <asp:TableCell>
                            <telerik:RadButton ID="ShowViewRequests" runat="server" Text="Show Requests" OnClick="ShowViewRequests_Click"></telerik:RadButton>
                            <telerik:RadButton ID="HideViewRequests" runat="server" Text="Hide Requests" OnClick="HideViewRequests_Click"></telerik:RadButton>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </fieldset>

</asp:Content>
