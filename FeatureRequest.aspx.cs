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
using System.Web.Services;

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
                DataTable dt = App_Code.IGBrandRepReferralDO.GetAllHowHear();
                cbHowHear.DataTextField = "HowHearDesc";
                cbHowHear.DataValueField = "ID";
                cbHowHear.DataSource = dt;
                cbHowHear.DataBind();
            }
            dpRepsBirthday.MaxDate = DateTime.Now;
        }
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");

            if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
            {
                //These headers are handling the "pre-flight" OPTIONS call sent by the browser
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE");
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Accept");
                HttpContext.Current.Response.AddHeader("Access-Control-Max-Age", "1728000");
                HttpContext.Current.Response.End();
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
                string script = "ErrorDialog(\" Please use a valid email \");";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "SaveProgram", script, true);
            }
            else if (Validation() && validEmail)
            {
                try
                {
                    int ID = 0;
                    string RepsName = txtRepsName.Text;
                    string ParentsName = txtParentsName.Text;
                    string Email = txtEmail.Text;
                    string InstagramUsername = txtInstagramUsername.Text;
                    string RepsBirthday = dpRepsBirthday.SelectedDate.ToString();
                    string PayPalEmail = txtPayPalEmail.Text;
                    string RepsBioResume = txtRepsBioResume.Text;
                    bool HaveSmallShop = chkHaveSmallShop.Checked;
                    string SmallShopUsername = txtHaveSmallShop.Text;
                    int HowHear = 1;
                    Int32.TryParse(cbHowHear.SelectedValue, out HowHear);
                    string WhatDoYouWant = txtWhatDoYouWant.Text;
                    bool IsAttachment = false;

                    int requestID = App_Code.IGBrandRepReferralDO.InsertUpdateFeatureRequest(ID, RepsName, ParentsName, Email, InstagramUsername, RepsBirthday, PayPalEmail, RepsBioResume, HaveSmallShop, SmallShopUsername, HowHear, WhatDoYouWant);

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
                    DateTime CurrentDateTime = DateTime.Now;
                    //Send email to Matt.Warren@fortechenergyinc.com
                    //string userName = "Angela Warren";
                    string userEmail = "igbrandrepreferral@gmail.com";
                    //string userEmail = "edsptech@gmail.com";
                    string userSubject = "[New IG Feature Request] Feature Request from " + InstagramUsername + " - Rep's Name: " + RepsName + " (Parent: " + ParentsName + ")";
                    string userEmailContent = BuildEmail(InstagramUsername, RepsName, RepsBirthday, ParentsName, Email, PayPalEmail, RepsBioResume, HaveSmallShop, SmallShopUsername, HowHear, WhatDoYouWant, IsAttachment, CurrentDateTime);
                    SendEmailViaNetMail(userEmail, userSubject, userEmailContent, true);

                    string userName1 = ParentsName;
                    string userEmail1 = Email;
                    string userSubject1 = "Thank You, From IG Brand Rep!";
                    string userEmailContent1 = BuildEmailToClient(RepsName, ParentsName, CurrentDateTime);
                    SendEmailViaNetMail(userEmail1, userSubject1, userEmailContent1, true);

                    string submitMessage = "We will be following-up with a PayPal invoice in the next 24-48 hours";
                    string script = "SuccessDialog(\" Request successfully sent! " + submitMessage + "\");";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "SaveProgram", script, true);

                    ClearFields();
                }
                catch (Exception ex)
                {
                    string script = "ErrorDialog(\" Unable to place Feature Request \");";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "SaveProgram", script, true);
                }
            }
            else
            {
                string script = "ErrorDialog(\" Please fill required fields \");";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "SaveProgram", script, true);
            }
        }
        [WebMethod]
        protected void chkHaveSmallShop_CheckedChanged(object sender, EventArgs e)
        {
            if (chkHaveSmallShop.Checked == true)
            {
                txtHaveSmallShop.Visible = true;
            }
            else
            {
                txtHaveSmallShop.Visible = false;
            }

        }
        #endregion

        #region Utility Methods
        protected void ClearFields()
        {
            txtRepsName.Text = "";
            txtParentsName.Text = "";
            txtEmail.Text = "";
            txtInstagramUsername.Text = "";
            dpRepsBirthday.SelectedDate  = null;
            txtPayPalEmail.Text = "";
            txtRepsBioResume.Text = "";
            chkHaveSmallShop.Checked = false;
            txtHaveSmallShop.Text = "";
            txtHaveSmallShop.Visible = false;
            cbHowHear.SelectedValue = "1";
            txtWhatDoYouWant.Text = "";
        }
        protected bool Validation()
        {
            if (txtRepsName.Text == "" || txtParentsName.Text == "" || txtInstagramUsername.Text == "" || txtEmail.Text == ""
                || dpRepsBirthday.SelectedDate == null || txtPayPalEmail.Text == "" || txtRepsBioResume.Text == "" || cbHowHear.SelectedIndex == 0)
            {
                if (txtRepsName.Text == "" || txtParentsName.Text == "" || txtInstagramUsername.Text == "" || txtEmail.Text == "" || dpRepsBirthday.SelectedDate == null || txtPayPalEmail.Text == "" || txtRepsBioResume.Text == "")
                    MainPage.Visible = false;
                else
                    MainPage.Visible = true;
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
        protected static string BuildEmail(string InstagramUsername, string RepsName, string RepsBirthday, string ParentsName, string Email, string PayPalEmail, string RepsBioResume, bool HaveSmallShop, string SmallShopUsername, int HowHear, string WhatDoYouWant, bool IsAttachment, DateTime DateRequestMade)
        {
            string HasUploadedAttachments = "Have Not Submitted Photos As Yet";
            string HowDidYouHearAboutUs = "Decline to Say";
            string DoesHaveSmallShop = "No";
            if (HaveSmallShop)
                DoesHaveSmallShop = "Yes";
            if (HowHear > 1)
                HowDidYouHearAboutUs = App_Code.IGBrandRepReferralDO.GetHowHear(HowHear);
            if (IsAttachment)
                HasUploadedAttachments = "I have Uploaded my Photos to Your Website";
            DateTime today = DateTime.Today;
            int Age = 0;
            if (!String.IsNullOrWhiteSpace(RepsBirthday))
                Age = today.Year - Convert.ToDateTime(RepsBirthday).Year;
            StringBuilder oStr = new StringBuilder("");
            try
            {
                oStr.Append("<p style='text-align:center; font-size: x-large; color: lightcoral;'><strong>IG Brand Rep Referral Request</strong></p><hr />");
                oStr.Append("<p style='color: #33cccc;font-size: medium;'><strong>Requestor's IG Handle:</strong></p>");
                oStr.Append(InstagramUsername);
                oStr.Append("<p style='color: #33cccc;font-size: medium;'><strong>Rep's Name:</strong></p>");
                oStr.Append(RepsName);
                oStr.Append("<p style='color: #33cccc;font-size: medium;'><strong>Rep's Age:</strong></p>");
                oStr.Append(Age);
                oStr.Append("<p style='color: #33cccc;font-size: medium;'><strong>Rep's Birthday:</strong></p>");
                oStr.Append(RepsBirthday);
                oStr.Append("<p style='color: #33cccc;font-size: medium;'><strong>Parent's Name:</strong></p>");
                oStr.Append(ParentsName);
                oStr.Append("<p style='color: #33cccc;font-size: medium;'><strong>Requestor's Email:</strong></p>");
                oStr.Append(Email);
                oStr.Append("<p style='color: #33cccc;font-size: medium;'><strong>PayPal Email:</strong></p>");
                oStr.Append(PayPalEmail);
                oStr.Append("<p style='color: #33cccc;font-size: medium;'><strong>Rep's Bio and Resume:</strong></p>");
                oStr.Append(RepsBioResume);
                oStr.Append("<p style='color: #33cccc;font-size: medium;'><strong>Does Requestor Have a Small Shop?:</strong></p>");
                oStr.Append(DoesHaveSmallShop);
                oStr.Append("<p style='color: #33cccc;font-size: medium;'><strong>Small Shop Username:</strong></p>");
                oStr.Append(SmallShopUsername);
                oStr.Append("<p style='color: #33cccc;font-size: medium;'><strong>How Did You Hear About Us:</strong></p>");
                oStr.Append(HowDidYouHearAboutUs);
                oStr.Append("<p style='color: #33cccc;font-size: medium;'><strong>Please describe what you need/want from your Instagram brander community and how we can best support you:</strong></p>");
                oStr.Append(WhatDoYouWant);
                oStr.Append("<p style='color: #33cccc;font-size: medium;'><strong>Did You Upload An Attachment?:</strong></p>");
                oStr.Append(HasUploadedAttachments);
                oStr.Append("<p style='color: #33cccc;font-size: medium;'><strong>Date and Time Request Submitted:</strong></p>");
                oStr.Append(DateRequestMade.ToString());
                oStr.Append("<p style='color: lightcoral;font-size: x-large;'><strong>I love you, Beautiful!</strong></p>");
            }

            catch (Exception ex)
            {

            }
            return oStr.ToString();
        }
        protected static string BuildEmailToClient(string RepsName, string ParentsName, DateTime DateRequestMade)
        {
            StringBuilder oStr = new StringBuilder("");
            try
            {
                oStr.Append("<p style='color: #33cccc;font-size: medium;'><strong>IG Brand Rep Referral</strong></p><p>Hello ");
                oStr.Append(ParentsName);
                oStr.Append(",</p><p>Thank you for your interest in featuring ");
                oStr.Append(RepsName);
                oStr.Append(" in the IG Brand Rep Referral community!</p>");
                oStr.Append("<p>Once we verify that your account meets IG's terms of use, we will send an invoice through PayPal.</p><p>When pay-ment is received, your spot is secured!</p><p>We will be in touch within 24 hours and are excited for the opportunity to work with you!</p><p></p>");
                oStr.Append("<p>Have an amazing day- we'll talk to you soon!</p><p></p><p>Angela</p><p>igbrandrepreferral@gmail.com</p>");
            }

            catch (Exception ex)
            {

            }
            return oStr.ToString();
        }
        protected Boolean SendEmailViaNetMail(string ToEmail, string Subject, string MsgBody, Boolean bHtml)
        {
            string SmtpServer = "smtp.gmail.com";
            int SmtpPortNo = 587;
            string SmtpUserName = "igbrandrepreferralmail@gmail.com";
            string SmtpPassword = "j3nk1ns1";
            string FromEmail = "igbrandrepreferral@gmail.com";

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