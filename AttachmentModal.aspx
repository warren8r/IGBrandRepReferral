<%@ Page Title="Download Attachments" Language="C#" MasterPageFile="~/IGBrandRepReferral.Master" AutoEventWireup="true" CodeBehind="AttachmentModal.aspx.cs" Inherits="IGBrandRepReferral.AttachmentModal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="customCss" runat="server"></asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        function Close() {
            GetRadWindow().close(); // Close the window 
        }
        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow; //Will work in Moz in all cases, including clasic dialog 
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow; //IE (and Moz as well) 

            return oWindow;
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
                            <h2>Download Attachments</h2>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                <asp:Table runat="server" HorizontalAlign="Center" Width="50%">
                    <asp:TableRow>
                        <asp:TableCell>
                            <telerik:RadGrid ID="RadGrid1" runat="server" AutoGenerateColumns="false" OnNeedDataSource="RadGrid1_NeedDataSource" OnItemDataBound="RadGrid1_ItemDataBound">
                                <MasterTableView DataKeyNames="ID">
                                    <Columns>
                                        <telerik:GridBoundColumn HeaderText="File Name" DataField="FileName" UniqueName="FileName"></telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn HeaderText="Attachment">
                                            <ItemTemplate>
                                                <telerik:RadButton ID="btnDownload" runat="server" Text="Download" OnClick="btnDownload_Click"></telerik:RadButton>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Button ID="btnClose" runat="server" OnClientClick="Close();" Text="Close" > </asp:Button>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>

            </ContentTemplate>
        </asp:UpdatePanel>
    </fieldset>

</asp:Content>