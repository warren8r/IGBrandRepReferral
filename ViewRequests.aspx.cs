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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable dt = App_Code.IGBrandRepReferralDO.GetAllHowHear();
                ddlHowHear.DataTextField = "HowHearDesc";
                ddlHowHear.DataValueField = "ID";
                ddlHowHear.DataSource = dt;
                ddlHowHear.DataBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            RadGrid1.DataSource = Search();
            RadGrid1.DataBind();
        }

        protected void RadGrid1_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            RadGrid1.DataSource = Search();
        }

        protected DataTable Search()
        {
            DataTable dt = new DataTable();
            //string firstName = txtFirstName.Text;
            //string lastName = txtLastName.Text;
            //string companyName = txtCompany.Text;
            //int demoID = Int32.Parse(ddlDemo.SelectedValue);
            //dt = App_Code.IGBrandRepReferralDO.SearchRequests(firstName, lastName, companyName, demoID);
            return dt;
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        protected void ClearFields()
        {
            //txtFirstName.Text = "";
            //txtLastName.Text = "";
            //txtCompany.Text = "";
            //ddlDemo.SelectedIndex = -1;
            RadGrid1.DataSource = Search();
            RadGrid1.DataBind();
        }

        protected void btnDownloadAttachment_Click(object sender, EventArgs e)
        {
            RadButton btnDownload = (RadButton)sender;
            GridDataItem item = (GridDataItem)btnDownload.NamingContainer;

            string requestID = item.GetDataKeyValue("RequestID").ToString();

            RadWindow1.NavigateUrl = "AttachmentModal.aspx?RequestID=" + requestID;
            string script = "function f(){$find(\"" + RadWindow1.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);

            //DataTable dt = App_Code.IGBrandRepReferralDO.DownloadFileBinary(Int32.Parse(requestID));

            //string fileName = dt.Rows[0]["FileName"].ToString();
            //byte[] bytes = (byte[])dt.Rows[0]["FileSource"];
            //string fileType = dt.Rows[0]["FileType"].ToString();

            //System.Web.HttpContext context = System.Web.HttpContext.Current;
            //context.Response.Clear();
            //context.Response.ClearHeaders();
            //context.Response.ClearContent();
            //context.Response.AppendHeader("content-length", bytes.Length.ToString());
            //context.Response.ContentType = fileType;
            //context.Response.AppendHeader("content-disposition", "attachment; filename=" + fileName);
            //context.Response.BinaryWrite(bytes);
            //context.ApplicationInstance.CompleteRequest();
        }

        protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        {
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
        }
    }
}