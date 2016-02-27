using System;
using System.Web;
using System.Web.UI;
using System.Data;
using Telerik.Web.UI;

public partial class Masters_RigTrack : System.Web.UI.MasterPage
{
    const bool DisableAjax = false;
    const bool DisableGlobal = false;

    protected void PageInit(object sender, EventArgs e)
    {
        HttpContext.Current.ClearError();

        if (DisableAjax == true)
        {
           // RadScriptManager1.EnablePartialRendering = false;
        }
    }
    protected void lnk_myaccount_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/ClientAdmin/MyAccount.aspx");
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (DisableAjax == true)
        {
            GetUserControls(Page.Controls);
        }

        if (DisableGlobal == false && IsPostBack == false)
            globalSetting(Page.Controls);
    }

        //IF HOMEPAGE HIDE THE BREADCRUMBS - But Catch Errors
    protected void lnk_logout_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Session.Clear();
        Response.Redirect("~/ClientLogin.aspx");
    }
    protected void RadMenu1_PreRender(object sender, EventArgs e)
    {

        //foreach (RadMenuItem rootItem in RadMenu1.Items)
        //{

        //    foreach (RadMenuItem childItem in rootItem.Items)
        //    {
        //        if ( !String.IsNullOrWhiteSpace( childItem.NavigateUrl ))
        //            childItem.NavigateUrl = childItem.NavigateUrl + ( String.IsNullOrWhiteSpace(  rootItem.Target )? "": "?&a=" + rootItem.Target + "");// "employee_login/doctors_area/forms.aspx";
        //        else
        //            childItem.NavigateUrl = "javascript:void('Menu')";
        //    }
        //}
    }

    public void GetUserControls(ControlCollection controls)
    {
        foreach (Control ctl in controls)
        {
            if (ctl is RadAjaxPanel)
                ((RadAjaxPanel)ctl).EnableAJAX = false;

            if (ctl.Controls.Count > 0)
                GetUserControls(ctl.Controls);
        }
    }

    /**FIND SPECIFIC CONTROLS AND APPLY CUSTOM SETTINGS GLOBALLY THROUGHOUT THE SOFTWARE
     * SOME OF THESE SETTINGS COULD COME FROM THE DATABASE THERE IS A DATA SOURCE ON THE FRONT END FOR THIS. gridSettings
     **/
    public void globalSetting(ControlCollection controls)
    {
        //GET SETTINGS
        int defaultPageSize = 10;
        string[] pageSize = new string[] { };

    }

    protected void btnLogo_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/ClientAdmin/ClientHome.aspx");
    }
}
