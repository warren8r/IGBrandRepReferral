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
            }
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