<%@ Page Title="Download Attachments" Language="C#" MasterPageFile="~/IGBrandRepReferral.Master" AutoEventWireup="true" CodeBehind="AttachmentModal.aspx.cs" Inherits="IGBrandRepReferral.AttachmentModal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="customCss" runat="server"></asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

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
                </asp:Table>

            </ContentTemplate>
        </asp:UpdatePanel>
    </fieldset>

</asp:Content>