using System;
using System.IO;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace IGBrandRepReferral
{
    public partial class FeatureRequest : Page
    {
        #region Global Variables
        bool invalid = false;
        #endregion

        #region Page Behavior
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable dt = App_Code.IGBrandRepReferralDO.GetAllDemos();
                cbDemos.DataTextField = "DemoName";
                cbDemos.DataValueField = "ID";
                cbDemos.DataSource = dt;
                cbDemos.DataBind();

                DataTable dtCountry = App_Code.IGBrandRepReferralDO.GetAllCountries();
                ddlCountry.DataTextField = "Name";
                ddlCountry.DataValueField = "ID";
                ddlCountry.DataSource = dtCountry;
                ddlCountry.DataBind();

                DataTable dtState = App_Code.IGBrandRepReferralDO.GetAllStates();
                ddlState.DataTextField = "Name";
                ddlState.DataValueField = "ID";
                ddlState.DataSource = dtState;
                ddlState.DataBind();
                ddlState.Enabled = false;

                cbDemos.SelectedValue = "1220";
            }
        }
        #endregion

        #region Buttons
        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            bool validEmail = IsValidEmail(txtEmail.Text);

            if (Validation() && !validEmail)
            {
                string script = "ErrorDialog(\" Please use a valid company email \");";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "SaveProgram", script, true);
                //lblMessage.Text = "Please enter valid company e-mail address";
                //lblMessage.Visible = true;
            }
            else if (Validation() && validEmail)
            {
                try
                {
                    string firstName = txtFirstName.Text;
                    string lastName = txtLastName.Text;
                    string phone = txtPhoneNumber.Text;
                    string email = txtEmail.Text;
                    string companyName = txtCompanyName.Text;
                    string companyURL = txtCompanyURL.Text;
                    string position = txtPositionTitle.Text;
                    string demosRequested = cbDemos.SelectedText;
                    string address1 = txtAddress1.Text;
                    string address2 = txtAddress2.Text;
                    int countryID = Int32.Parse(ddlCountry.SelectedValue);
                    string city = txtCity.Text;
                    int stateID = 0;
                    Int32.TryParse(ddlState.SelectedValue, out stateID);
                    string zipCode = txtZipCode.Text;
                    bool IsAttachment = false;
                    int requestID = App_Code.IGBrandRepReferralDO.InsertRequest(firstName, lastName, phone, email, companyName, companyURL, position, address1, address2, countryID, city, stateID, zipCode);

                    App_Code.IGBrandRepReferralDO.InsertRequestDemoLookup(requestID, Int32.Parse(cbDemos.SelectedValue));

                    if (AttachmentUpload.UploadedFiles.Count > 0)
                    {
                        IsAttachment = true;

                        foreach (UploadedFile file in AttachmentUpload.UploadedFiles)
                        {
                            string Filename1 = file.GetName();

                            byte[] FileBytes = new byte[file.InputStream.Length];
                            file.InputStream.Read(FileBytes, 0, FileBytes.Length);
                            string Extension1 = file.GetExtension();
                            string FileType = null;

                            switch (Extension1)
                            {
                                case ".doc":
                                    FileType = "application/vnd.ms-word";
                                    break;
                                case ".docx":
                                    FileType = "application/vnd.ms-word";
                                    break;
                                case ".xls":
                                    FileType = "application/vnd.ms-excel";
                                    break;
                                case ".xlsx":
                                    FileType = "application/vnd.ms-excel";
                                    break;
                                case ".jpg":
                                    FileType = "image/jpg";
                                    break;
                                case ".png":
                                    FileType = "image/png";
                                    break;
                                case ".gif":
                                    FileType = "image/gif";
                                    break;
                                case ".pdf":
                                    FileType = "application/pdf";
                                    break;
                            }
                            int fileID = App_Code.IGBrandRepReferralDO.UploadFile(Filename1, FileBytes, FileType, requestID);
                        }
                    }


                    //foreach (RadComboBoxItem item in cbDemos.CheckedItems)
                    //{
                    //    if (item.Checked)
                    //    {
                    //        App_Code.IGBrandRepReferralDO.InsertRequestDemoLookup(requestID, Int32.Parse(item.Value));
                    //        demosRequested += item.Text + ", ";
                    //    }
                    //}

                    //demosRequested = demosRequested.Substring(0, demosRequested.Length - 2);


                    //Send email to Matt.Warren@fortechenergyinc.com
                    string userName = "Matt Warren";
                    string userEmail = "matt.warren@fortechenergyinc.com";
                    string userSubject = "Demo Request from " + firstName + " " + lastName + " (" + companyName + ")";
                    //string userBody = "First Name: " + firstName + Environment.NewLine + 
                    //                  "Last Name: " + lastName + Environment.NewLine + 
                    //                  "Phone Number: " + phone + Environment.NewLine + 
                    //                  "Email: " + email + Environment.NewLine +
                    //                  "Company Name: " + companyName + Environment.NewLine + 
                    //                  "Company URL: " + companyURL + Environment.NewLine + 
                    //                  "Position/Title: " + position + Environment.NewLine + 
                    //                  "Demos Requested: " + demosRequested + Environment.NewLine;

                    string userEmailContent = BuildEmail(firstName, lastName, phone.ToString(), email, companyName, companyURL, position, demosRequested);
                    SendEmailViaNetMail(userEmail, userSubject, userEmailContent, true);

                    string submitMessage = "Password(s) will be sent to the email provided pending approval (24-48 hours)";
                    string script = "SuccessDialog(\" Successfully placed request for demo! " + submitMessage + "\");";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "SaveProgram", script, true);

                    //lblMessage.Text = "Successfully placed request for demo!" + Environment.NewLine +
                    //                  "Password(s) will be sent to the email provided pending approval (24-48 hours)";
                    //lblMessage.Visible = true;

                    ClearFields();
                }
                catch (Exception ex)
                {
                    string script = "ErrorDialog(\" Unable to place request for demo \");";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "SaveProgram", script, true);
                }
            }
            else
            {
                string script = "ErrorDialog(\" Please fill required fields \");";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "SaveProgram", script, true);

                //lblMessage.Text = "Please fill required fields";
                //lblMessage.Visible = true;
            }
        }
        #endregion

        #region Utility Methods
        protected void ClearFields()
        {
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtPhoneNumber.Text = "";
            txtEmail.Text = "";
            txtCompanyName.Text = "";
            txtCompanyURL.Text = "";
            txtPositionTitle.Text = "";
            cbDemos.SelectedIndex = -1;
            txtAddress1.Text = "";
            txtAddress2.Text = "";
            ddlCountry.SelectedIndex = -1;
            txtCity.Text = "";
            ddlState.SelectedIndex = -1;
            rfState.Enabled = false;
            txtZipCode.Text = "";
            lblStateError.Visible = false;
        }
        protected bool Validation()
        {
            if (txtFirstName.Text == "" || txtLastName.Text == "" || txtPhoneNumber.Text == "" || txtEmail.Text == ""
                || txtCompanyName.Text == "" || txtCompanyURL.Text == "" || txtPositionTitle.Text == "" || cbDemos.SelectedIndex == -1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool IsValidEmail(string strIn)
        {
            invalid = false;
            if (String.IsNullOrEmpty(strIn))
                return false;

            // Use IdnMapping class to convert Unicode domain names.
            try
            {
                strIn = Regex.Replace(strIn, @"(@)(.+)$", this.DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }

            if (invalid)
                return false;

            // Return true if strIn is in valid e-mail format.
            try
            {
                return Regex.IsMatch(strIn,
                      @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                      @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                      RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
        private string DomainMapper(Match match)
        {
            // IdnMapping class with default property values.
            IdnMapping idn = new IdnMapping();

            string domainName = match.Groups[2].Value;
            try
            {
                domainName = idn.GetAscii(domainName);
            }
            catch (ArgumentException)
            {
                invalid = true;
            }
            return match.Groups[1].Value + domainName;
        }
        protected static string BuildEmail(string firstName, string lastName, string phone, string email, string companyName, string companyURL, string position, string demos)
        {
            System.Text.RegularExpressions.Regex oReg = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z]+(([\'\,\.\- ][a-zA-Z])?[a-zA-Z]*)*\s+<(\w[-._\w]*\w@\w[-._\w]*\w\.\w{2,3})>$|^(\w[-._\w]*\w@\w[-._\w]*\w\.\w{2,4})$");
            StringBuilder oStr = new StringBuilder("");
            try
            {

                if (oReg.IsMatch(email) == true)
                {
                    //	Email address given by the user appears to be valid.

                    //ClassSmtp oSmtp = new ClassSmtp();
                    //string AdminEmail = Util.ReadString("AdminEmail", "");

                    oStr.Append("<table>");
                    oStr.Append("<tr>");
                    oStr.Append("<td>");
                    oStr.Append("First Name:");
                    oStr.Append("</td>");
                    oStr.Append("<td>");
                    oStr.Append(firstName);
                    oStr.Append("</td>");
                    oStr.Append("</tr>");
                    oStr.Append("<tr>");
                    oStr.Append("<td>");
                    oStr.Append("Last Name:");
                    oStr.Append("</td>");
                    oStr.Append("<td>");
                    oStr.Append(lastName);
                    oStr.Append("</td>");
                    oStr.Append("</tr>");
                    oStr.Append("<tr>");
                    oStr.Append("<td>");
                    oStr.Append("Phone:");
                    oStr.Append("</td>");
                    oStr.Append("<td>");
                    oStr.Append(phone);
                    oStr.Append("</td>");
                    oStr.Append("</tr>");
                    oStr.Append("<tr>");
                    oStr.Append("<td>");
                    oStr.Append("Email:");
                    oStr.Append("</td>");
                    oStr.Append("<td>");
                    oStr.Append(email);
                    oStr.Append("</td>");
                    oStr.Append("</tr>");
                    oStr.Append("<tr>");
                    oStr.Append("<td>");
                    oStr.Append("Company Name:");
                    oStr.Append("</td>");
                    oStr.Append("<td>");
                    oStr.Append(companyName);
                    oStr.Append("</td>");
                    oStr.Append("</tr>");
                    oStr.Append("<tr>");
                    oStr.Append("<td>");
                    oStr.Append("Company URL:");
                    oStr.Append("</td>");
                    oStr.Append("<td>");
                    oStr.Append(companyURL);
                    oStr.Append("</td>");
                    oStr.Append("</tr>");
                    oStr.Append("<tr>");
                    oStr.Append("<td>");
                    oStr.Append("Position:");
                    oStr.Append("</td>");
                    oStr.Append("<td>");
                    oStr.Append(position);
                    oStr.Append("</td>");
                    oStr.Append("</tr>");
                    oStr.Append("<tr>");
                    oStr.Append("<td>");
                    oStr.Append("Demos Requested:");
                    oStr.Append("</td>");
                    oStr.Append("<td>");
                    oStr.Append(demos);
                    oStr.Append("</td>");
                    oStr.Append("</tr>");
                    //oStr.Append("<tr><td colspan='2'>Sent from Fortech Timesheet Copyright(c)2015</td></tr>");
                    oStr.Append("</table>");
                }

            }

            catch (Exception ex)
            {

            }
            return oStr.ToString();
        }
        protected Boolean SendEmailViaNetMail(string ToEmail, string Subject, string MsgBody, Boolean bHtml)
        {
            string SendMethod = "Net.Mail";
            string SmtpServer = "smtp.gmail.com";
            int SmtpPortNo = 587;
            string SmtpUserName = "fortech2015@gmail.com";
            string SmtpPassword = "fortech$";
            string FromEmail = "goaltrackfortech@gmail.com";

            Boolean bRetVal = true;

            try
            {
                MailMessage oMail = new MailMessage();

                oMail.From = new MailAddress(FromEmail);
                oMail.To.Add(ToEmail);
                oMail.Subject = Subject;
                oMail.Body = MsgBody;
                oMail.IsBodyHtml = bHtml;

                SmtpClient oSmtp = new SmtpClient(SmtpServer);
                oSmtp.EnableSsl = true;
                //	Authenticate with mail server.
                oSmtp.Credentials = new NetworkCredential(SmtpUserName, SmtpPassword);
                oSmtp.Port = SmtpPortNo;
                oSmtp.Send(oMail);
            }

            catch (Exception ex)
            {
                bRetVal = false;
            }

            return bRetVal;
        }
        #endregion
    }
}