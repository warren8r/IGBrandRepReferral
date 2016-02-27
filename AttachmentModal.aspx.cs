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
    public partial class AttachmentModal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ViewState["RequestID"] = Request.QueryString["RequestID"].ToString();
        }

        protected void RadGrid1_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            int requestID = Int32.Parse(ViewState["RequestID"].ToString());
            DataTable dt = App_Code.FortechDemosDO.RequestFileInfo(requestID);
            RadGrid1.DataSource = dt;
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            RadButton btnDownload = (RadButton)sender;
            GridDataItem item = (GridDataItem)btnDownload.NamingContainer;

            string ID = item.GetDataKeyValue("ID").ToString();

            DataTable dt = App_Code.FortechDemosDO.DownloadFileBinary(Int32.Parse(ID));

            string fileName = dt.Rows[0]["FileName"].ToString();
            byte[] bytes = (byte[])dt.Rows[0]["FileSource"];
            string fileType = dt.Rows[0]["FileType"].ToString();

            System.Web.HttpContext context = System.Web.HttpContext.Current;
            context.Response.Clear();
            context.Response.ClearHeaders();
            context.Response.ClearContent();
            context.Response.AppendHeader("content-length", bytes.Length.ToString());
            context.Response.ContentType = fileType;
            context.Response.AppendHeader("content-disposition", "attachment; filename=" + fileName);
            context.Response.BinaryWrite(bytes);
            context.ApplicationInstance.CompleteRequest();
        }

        protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        {
            foreach (GridDataItem item in RadGrid1.Items)
            {
                RadButton btnDownload = item.FindControl("btnDownload") as RadButton;
                ScriptManager.GetCurrent(this).RegisterPostBackControl(btnDownload);
            }
        }
    }
}