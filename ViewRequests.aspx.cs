using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace IGBrandRepReferral
{
    public partial class ViewRequests : System.Web.UI.Page
    {
        #region Page Behavior
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrids();
                BindDays();
                RemainingRequests.Text = Convert.ToString(App_Code.IGBrandRepReferralDO.GetRemainingRequests());
            }
        }
        protected void RadGrid1_PageIndexChanged(object source, Telerik.Web.UI.GridPageChangedEventArgs e)
        {
            this.RadGrid1.CurrentPageIndex = e.NewPageIndex;
            RadGrid1.DataSource = Search();
        }
        protected void RadGrid1_PageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            RadGrid1.DataSource = Search();
        }
        protected void RadGrid1_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            RadGrid1.DataSource = Search();
        }
        protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                string HasSmallShopName = item["HaveSmallShop"].Text;
                if(HasSmallShopName == "False")
                    item["HaveSmallShop"].Text = "No";
                else
                    item["HaveSmallShop"].Text = "Yes";
            }
            foreach (GridDataItem item in RadGrid1.Items)
            {
                if (item.GetDataKeyValue("AttachmentExists").ToString() == "FALSE")
                {
                    RadButton btnDownload = item.FindControl("btnDownloadAttachment") as RadButton;
                    btnDownload.Enabled = false;
                }
                else
                {
                    RadButton btnDownload = item.FindControl("btnDownloadAttachment") as RadButton;
                    ScriptManager.GetCurrent(this).RegisterPostBackControl(btnDownload);
                }
            }
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                GridEditableItem item = e.Item as GridEditableItem;
                RadDropDownList list = (RadDropDownList)item.FindControl("ddlRadGridHasPaid") as RadDropDownList;
                DataTable dt = App_Code.IGBrandRepReferralDO.GetAllBooleanList();
                list.DataTextField = "Text";
                list.DataValueField = "Value";
                list.DataSource = dt;
                list.DataBind();
                string HasSmallShopName = item["HaveSmallShop"].Text;
                if (item.KeyValues.Contains("False"))
                    list.SelectedValue = "0";
                else
                    list.SelectedValue = "1";
                (item["RepsName"].Controls[0] as TextBox).ReadOnly = true;
                (item["ParentsName"].Controls[0] as TextBox).ReadOnly = true;
                (item["Email"].Controls[0] as TextBox).ReadOnly = true;
                (item["InstagramUsername"].Controls[0] as TextBox).ReadOnly = true;
                (item["RepsBirthday"].Controls[0] as TextBox).ReadOnly = true;
                (item["PayPalEmail"].Controls[0] as TextBox).ReadOnly = true;
                if((item["HaveSmallShop"].Controls[0] as TextBox).Text == "False")
                    (item["HaveSmallShop"].Controls[0] as TextBox).Text = "No";
                else
                    (item["HaveSmallShop"].Controls[0] as TextBox).Text = "Yes";
                (item["HaveSmallShop"].Controls[0] as TextBox).ReadOnly = true;
                (item["SmallShopUsername"].Controls[0] as TextBox).ReadOnly = true;
                (item["WhatDoYouWant"].Controls[0] as TextBox).TextMode = TextBoxMode.MultiLine;
                (item["WhatDoYouWant"].Controls[0] as TextBox).ReadOnly = true;
                (item["HowHearDesc"].Controls[0] as TextBox).ReadOnly = true;
                (item["RequestDate"].Controls[0] as TextBox).ReadOnly = true;
            }
        }
        protected void RadGrid1_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            int ID_Val = Convert.ToInt32(editedItem.GetDataKeyValue("ID"));
            string RepsName_Val = (editedItem["RepsName"].Controls[0] as TextBox).Text;
            string ParentsName_Val = (editedItem["ParentsName"].Controls[0] as TextBox).Text;
            string Email_Val = (editedItem["Email"].Controls[0] as TextBox).Text;
            string InstagramUsername_Val = (editedItem["InstagramUsername"].Controls[0] as TextBox).Text;
            string RepsBirthday_Val = (editedItem["RepsBirthday"].Controls[0] as TextBox).Text;
            string PayPalEmail_Val = (editedItem["PayPalEmail"].Controls[0] as TextBox).Text;
            RadEditor editor = (RadEditor)editedItem.FindControl("txtRadGridRepsBioResume") as RadEditor;
            string RepsBioResume_Val = editor.Text;
            string HaveSmallShop_inter = "False";
            if (!String.IsNullOrEmpty((editedItem["HaveSmallShop"].Controls[0] as TextBox).Text) && (editedItem["HaveSmallShop"].Controls[0] as TextBox).Text == "Yes")
                HaveSmallShop_inter = "True";
            else
                HaveSmallShop_inter = "False";
            bool HaveSmallShop_Val = Convert.ToBoolean(HaveSmallShop_inter);
            string SmallShopUsername_Val = (editedItem["SmallShopUsername"].Controls[0] as TextBox).Text;
            int HowHear_Val = App_Code.IGBrandRepReferralDO.GetHowHearID((editedItem["HowHearDesc"].Controls[0] as TextBox).Text);
            string WhatDoYouWant_Val = (editedItem["WhatDoYouWant"].Controls[0] as TextBox).Text;
            string HasPaid_inter = "False";
            RadDropDownList HasPaidList = (RadDropDownList)editedItem.FindControl("ddlRadGridHasPaid") as RadDropDownList;
            if (!String.IsNullOrEmpty(HasPaidList.SelectedText) && (HasPaidList.SelectedText == "Yes"))
                HasPaid_inter = "True";
            else
                HasPaid_inter = "False";
            bool HasPaid_Val = Convert.ToBoolean(HasPaid_inter);
            string PayPalInvoiceNumber_Val = (editedItem["PayPalInvoiceNumber"].Controls[0] as TextBox).Text;
            RadEditor editor2 = (RadEditor)editedItem.FindControl("txtRadGridNotes") as RadEditor;
            string Notes_Val = editor2.Text;

            int RequestID = App_Code.IGBrandRepReferralDO.UpdateFeatureRequest(ID_Val, RepsName_Val, ParentsName_Val, Email_Val, InstagramUsername_Val, RepsBirthday_Val, PayPalEmail_Val, RepsBioResume_Val, HaveSmallShop_Val, SmallShopUsername_Val, HowHear_Val, WhatDoYouWant_Val, HasPaid_Val, PayPalInvoiceNumber_Val, Notes_Val);

            string submitMessage = "You have succussfully updated the record, you BEAUTIFUL WOMAN!! <3";
            string script = "SuccessDialog(\" Update Successful! " + submitMessage + "\");";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "SaveProgram", script, true);
        }
        protected void TodaysBirthdayGrid_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            TodaysBirthdayGrid.DataSource = TodaysBirthdays();
        }
        protected void TomorrowsBirthdayGrid_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            TomorrowsBirthdayGrid.DataSource = TomorrowsBirthdays();
        }
        protected void Day3sBirthdayGrid_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            Day3sBirthdayGrid.DataSource = Day3sBirthdays();
        }
        protected void Day4sBirthdayGrid_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            Day4sBirthdayGrid.DataSource = Day4sBirthdays();
        }
        protected void Day5sBirthdayGrid_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            Day5sBirthdayGrid.DataSource = Day5sBirthdays();
        }
        protected void Day6sBirthdayGrid_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            Day6sBirthdayGrid.DataSource = Day6sBirthdays();
        }
        protected void Day7sBirthdayGrid_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            Day7sBirthdayGrid.DataSource = Day7sBirthdays();
        }
        protected void EndOfWeeksBirthdayGrid_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            EndOfWeeksBirthdayGrid.DataSource = EndOfWeeksBirthdays();
        }
        #endregion
        #region Buttons
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            RadGrid1.DataSource = Search();
            RadGrid1.DataBind();
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }
        protected void btnDownloadAttachment_Click(object sender, EventArgs e)
        {
            RadButton btnDownload = (RadButton)sender;
            GridDataItem item = (GridDataItem)btnDownload.NamingContainer;

            string requestID = item.GetDataKeyValue("ID").ToString();

            RadWindow1.NavigateUrl = "AttachmentModal.aspx?RequestID=" + requestID;
            string script = "function f(){$find(\"" + RadWindow1.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
        #endregion
        #region Utility Methods
        protected DataTable Search()
        {
            DataTable dt = new DataTable();

            string RepsName = txtRepsName.Text;
            string ParentsName = txtParentsName.Text;
            int HowHear = Int32.Parse(ddlHowHear.SelectedValue);
            string InstagramUsername = txtInstagramUsername.Text;

            string RepsBirthdayBegin = Convert.ToString(dpBirthdayBegin.SelectedDate);
            string RepsBirthdayEnd = Convert.ToString(dpBirthdayEnd.SelectedDate);
            string Email = txtEmail.Text;
            string HasSmallShop = ddHasSmallShop.SelectedValue;
            string SmallShopName = txtShopName.Text;

            string PayPalInvoiceNumber = txtPayPalInvoiceNumber.Text;
            string PayPalEmail = txtPayPalEmail.Text;
            string RequestDateRangeBegin = Convert.ToString(dpRequestBegin.SelectedDate);
            string RequestDateRangeEnd = Convert.ToString(dpRequestEnd.SelectedDate);
            int HasPaid = Int32.Parse(ddHasPaid.SelectedValue);

            dt = App_Code.IGBrandRepReferralDO.SearchRequests(RepsName, ParentsName, HowHear, InstagramUsername, RepsBirthdayBegin, RepsBirthdayEnd, Email, HasSmallShop, SmallShopName, PayPalInvoiceNumber, PayPalEmail, RequestDateRangeBegin, RequestDateRangeEnd, HasPaid);
            return dt;
        }
        protected DataTable TodaysBirthdays()
        {
            DataTable dt = new DataTable();
            dt = App_Code.IGBrandRepReferralDO.GetTodaysBirthdays();
            return dt;
        }
        protected DataTable TomorrowsBirthdays()
        {
            DataTable dt = new DataTable();
            dt = App_Code.IGBrandRepReferralDO.GetTomorrowsBirthdays();
            return dt;
        }
        protected DataTable Day3sBirthdays()
        {
            DataTable dt = new DataTable();
            dt = App_Code.IGBrandRepReferralDO.GetDay3sBirthdays();
            return dt;
        }
        protected DataTable Day4sBirthdays()
        {
            DataTable dt = new DataTable();
            dt = App_Code.IGBrandRepReferralDO.GetDay4sBirthdays();
            return dt;
        }
        protected DataTable Day5sBirthdays()
        {
            DataTable dt = new DataTable();
            dt = App_Code.IGBrandRepReferralDO.GetDay5sBirthdays();
            return dt;
        }
        protected DataTable Day6sBirthdays()
        {
            DataTable dt = new DataTable();
            dt = App_Code.IGBrandRepReferralDO.GetDay6sBirthdays();
            return dt;
        }
        protected DataTable Day7sBirthdays()
        {
            DataTable dt = new DataTable();
            dt = App_Code.IGBrandRepReferralDO.GetDay7sBirthdays();
            return dt;
        }
        protected DataTable EndOfWeeksBirthdays()
        {
            DataTable dt = new DataTable();
            dt = App_Code.IGBrandRepReferralDO.GetEndOfWeeksBirthdays();
            return dt;
        }
        protected void BindGrids()
        {
            DataTable dt = App_Code.IGBrandRepReferralDO.GetAllBooleanList();
            ddHasSmallShop.DataTextField = "Text";
            ddHasSmallShop.DataValueField = "Value";
            ddHasSmallShop.DataSource = dt;
            ddHasSmallShop.DataBind();

            DataTable dt1 = App_Code.IGBrandRepReferralDO.GetAllHowHear();
            ddlHowHear.DataTextField = "HowHearDesc";
            ddlHowHear.DataValueField = "ID";
            ddlHowHear.DataSource = dt1;
            ddlHowHear.DataBind();

            DataTable dt3 = App_Code.IGBrandRepReferralDO.GetAllBooleanList();
            ddHasPaid.DataTextField = "Text";
            ddHasPaid.DataValueField = "Value";
            ddHasPaid.DataSource = dt3;
            ddHasPaid.DataBind();
        }
        protected void BindDays()
        {
            TodaysDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
            Today.Text = DateTime.Now.ToString("MM/dd/yyyy");
            Tomorrow.Text = DateTime.Now.AddDays(1).ToString("MM/dd/yyyy");
            Day3.Text = DateTime.Now.AddDays(2).ToString("MM/dd/yyyy");
            Day4.Text = DateTime.Now.AddDays(3).ToString("MM/dd/yyyy");
            Day5.Text = DateTime.Now.AddDays(4).ToString("MM/dd/yyyy");
            Day6.Text = DateTime.Now.AddDays(5).ToString("MM/dd/yyyy");
            Day7.Text = DateTime.Now.AddDays(6).ToString("MM/dd/yyyy");
            EndOfWeek.Text = DateTime.Now.AddDays(7).ToString("MM/dd/yyyy");
        }
        protected void ClearFields()
        {
            txtRepsName.Text = "";
            txtParentsName.Text = "";
            ddlHowHear.SelectedIndex = 0;
            txtInstagramUsername.Text = "";

            dpBirthdayBegin.SelectedDate = null;
            dpBirthdayEnd.SelectedDate = null;
            txtEmail.Text = "";
            ddHasSmallShop.SelectedIndex = -1;
            txtShopName.Text = "";

            txtPayPalInvoiceNumber.Text = "";
            txtPayPalEmail.Text = "";
            dpRequestBegin.SelectedDate = null;
            dpRequestEnd.SelectedDate = null;
            ddHasPaid.SelectedIndex = -1;

            RadGrid1.DataSource = Search();
            RadGrid1.DataBind();
        }
        #endregion
    }
}