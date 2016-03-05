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
                DataTable dt = App_Code.IGBrandRepReferralDO.GetAllHowHear();
                cbHowHear.DataTextField = "HowHearDesc";
                cbHowHear.DataValueField = "ID";
                cbHowHear.DataSource = dt;
                cbHowHear.DataBind();

                dpRepsBirthday.MaxDate = DateTime.Now;
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
                    string userName = "Angela Warren";
                    string userEmail = "igbrandrepreferral@gmail.com";
                    string userSubject = "[AutoFeature] Feature Request from " + InstagramUsername + " Rep's Name: " + RepsName + " (" + ParentsName + ")";
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
                oStr.Append("<table>");
                oStr.Append("<tr>");
                oStr.Append("<td>");
                oStr.Append("Requestor's IG Handle:");
                oStr.Append("</td>");
                oStr.Append("<td>");
                oStr.Append(InstagramUsername);
                oStr.Append("</td>");
                oStr.Append("</tr>");
                oStr.Append("<tr>");
                oStr.Append("<td>");
                oStr.Append("Rep's Name:");
                oStr.Append("</td>");
                oStr.Append("<td>");
                oStr.Append(RepsName);
                oStr.Append("</td>");
                oStr.Append("</tr>");
                oStr.Append("<tr>");
                oStr.Append("<td>");
                oStr.Append("Rep's Age:");
                oStr.Append("</td>");
                oStr.Append("<td>");
                oStr.Append(Age);
                oStr.Append("</td>");
                oStr.Append("</tr>");
                oStr.Append("<tr>");
                oStr.Append("<td>");
                oStr.Append("Rep's Birthday:");
                oStr.Append("</td>");
                oStr.Append("<td>");
                oStr.Append(RepsBirthday);
                oStr.Append("</td>");
                oStr.Append("</tr>");
                oStr.Append("<tr>");
                oStr.Append("<td>");
                oStr.Append("Parent's Name:");
                oStr.Append("</td>");
                oStr.Append("<td>");
                oStr.Append(ParentsName);
                oStr.Append("</td>");
                oStr.Append("</tr>");
                oStr.Append("<tr>");
                oStr.Append("<td>");
                oStr.Append("Requestor's Email:");
                oStr.Append("</td>");
                oStr.Append("<td>");
                oStr.Append(Email);
                oStr.Append("</td>");
                oStr.Append("</tr>");
                oStr.Append("<tr>");
                oStr.Append("<td>");
                oStr.Append("PayPal Email:");
                oStr.Append("</td>");
                oStr.Append("<td>");
                oStr.Append(PayPalEmail);
                oStr.Append("</td>");
                oStr.Append("</tr>");
                oStr.Append("<tr>");
                oStr.Append("<td>");
                oStr.Append("Rep's Bio and Resume:");
                oStr.Append("</td>");
                oStr.Append("<td>");
                oStr.Append(RepsBioResume);
                oStr.Append("</td>");
                oStr.Append("</tr>");
                oStr.Append("<tr>");
                oStr.Append("<td>");
                oStr.Append("Does Requestor Have a Small Shop?:");
                oStr.Append("</td>");
                oStr.Append("<td>");
                oStr.Append(DoesHaveSmallShop);
                oStr.Append("</td>");
                oStr.Append("</tr>");
                oStr.Append("<tr>");
                oStr.Append("<td>");
                oStr.Append("Small Shop Username:");
                oStr.Append("</td>");
                oStr.Append("<td>");
                oStr.Append(SmallShopUsername);
                oStr.Append("</td>");
                oStr.Append("</tr>");
                oStr.Append("<tr>");
                oStr.Append("<td>");
                oStr.Append("How Did You Hear About Us:");
                oStr.Append("</td>");
                oStr.Append("<td>");
                oStr.Append(HowDidYouHearAboutUs);
                oStr.Append("</td>");
                oStr.Append("</tr>");
                oStr.Append("<tr>");
                oStr.Append("<td>");
                oStr.Append("Please describe what you need/want from your Instagram brander community and how we can best support you:");
                oStr.Append("</td>");
                oStr.Append("<td>");
                oStr.Append(WhatDoYouWant);
                oStr.Append("</td>");
                oStr.Append("</tr>");
                oStr.Append("<tr>");
                oStr.Append("<td>");
                oStr.Append("Did You Upload An Attachment?:");
                oStr.Append("</td>");
                oStr.Append("<td>");
                oStr.Append(HasUploadedAttachments);
                oStr.Append("</td>");
                oStr.Append("</tr>");
                oStr.Append("<tr>");
                oStr.Append("<td>");
                oStr.Append("Date and Time Request Submitted:");
                oStr.Append("</td>");
                oStr.Append("<td>");
                oStr.Append(DateRequestMade.ToString());
                oStr.Append("</td>");
                oStr.Append("</tr>");
                //oStr.Append("<tr><td colspan='2'>Sent from Fortech Timesheet Copyright(c)2015</td></tr>");
                oStr.Append("</table>");

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
                oStr.Append("<p><span style='color: #33cccc;'><strong><span style='font-size: medium;'>IG Brand Rep Referral</span></strong></span></p><p>Hello "); 
                oStr.Append(ParentsName);
                oStr.Append(",</p><p>Thank you for signing ");
                oStr.Append(RepsName);
                oStr.Append(" up with IG Brand Rep Referral on ");
                oStr.Append(DateRequestMade);
                oStr.Append(" .</p><p>We look forward to serving your needs!</p><p>Much Thanks,</p><p>Angela</p><p>igbrandrepreferral@gmail.com</p>");
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