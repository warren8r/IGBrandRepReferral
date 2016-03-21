using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IGBrandRepReferral.App_Code
{
    public class IGBrandRepReferralDO
    {
        public static DataTable GetAllHowHear()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Connection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[GetAllHowHear]", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                try
                {
                    da.Fill(dt);
                }
                catch (Exception ex)
                {
                }
            }
            return dt;
        }

        public static DataTable GetAllBooleanList()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Connection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[GetAllBooleanList]", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                try
                {
                    da.Fill(dt);
                }
                catch (Exception ex)
                {
                }
            }
            return dt;
        }

        public static int InsertUpdateFeatureRequest(int ID, string RepsName, string ParentsName, string Email, string InstagramUsername, string RepsBirthday, string PayPalEmail, string RepsBioResume, bool HaveSmallShop, string SmallShopUsername, int HowHear, string WhatDoYouWant)
        {
            int id = 0;
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Connection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[InsertUpdateFeatureRequest]", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@RepsName", RepsName);
                cmd.Parameters.AddWithValue("@ParentsName", ParentsName);
                cmd.Parameters.AddWithValue("@Email", Email);
                cmd.Parameters.AddWithValue("@InstagramUsername", InstagramUsername);
                cmd.Parameters.AddWithValue("@RepsBirthday", RepsBirthday);
                cmd.Parameters.AddWithValue("@PayPalEmail", PayPalEmail);
                cmd.Parameters.AddWithValue("@RepsBioResume", RepsBioResume);
                cmd.Parameters.AddWithValue("@HaveSmallShop", HaveSmallShop);
                cmd.Parameters.AddWithValue("@SmallShopUsername", SmallShopUsername);
                cmd.Parameters.AddWithValue("@HowHear", HowHear);
                cmd.Parameters.AddWithValue("@WhatDoYouWant", WhatDoYouWant);

                conn.Open();

                try
                {
                    object rtnObj = cmd.ExecuteScalar();
                    id = Convert.ToInt32(rtnObj.ToString());
                }
                catch (Exception ex)
                {
                }
            }
            return id;
        }

        public static int UpdateFeatureRequest(int ID_Val, string RepsName_Val, string ParentsName_Val, string Email_Val, string InstagramUsername_Val, string RepsBirthday_Val, string PayPalEmail_Val, string RepsBioResume_Val, bool HaveSmallShop_Val, string SmallShopUsername_Val, int HowHear_Val, string WhatDoYouWant_Val, bool HasPaid_Val, string PayPalInvoiceNumber_Val, string Notes_Val)
        {
            int id = 0;
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Connection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[InsertUpdateFeatureRequest]", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID", ID_Val);
                cmd.Parameters.AddWithValue("@RepsName", RepsName_Val);
                cmd.Parameters.AddWithValue("@ParentsName", ParentsName_Val);
                cmd.Parameters.AddWithValue("@Email", Email_Val);
                cmd.Parameters.AddWithValue("@InstagramUsername", InstagramUsername_Val);
                cmd.Parameters.AddWithValue("@RepsBirthday", RepsBirthday_Val);
                cmd.Parameters.AddWithValue("@PayPalEmail", PayPalEmail_Val);
                cmd.Parameters.AddWithValue("@RepsBioResume", RepsBioResume_Val);
                cmd.Parameters.AddWithValue("@HaveSmallShop", HaveSmallShop_Val);
                cmd.Parameters.AddWithValue("@SmallShopUsername", SmallShopUsername_Val);
                cmd.Parameters.AddWithValue("@HowHear", HowHear_Val);
                cmd.Parameters.AddWithValue("@WhatDoYouWant", WhatDoYouWant_Val);
                cmd.Parameters.AddWithValue("@HasPaid", HasPaid_Val);
                cmd.Parameters.AddWithValue("@PayPalInvoiceNumber", PayPalInvoiceNumber_Val);
                cmd.Parameters.AddWithValue("@Notes", Notes_Val);

                conn.Open();

                try
                {
                    object rtnObj = cmd.ExecuteScalar();
                    id = Convert.ToInt32(rtnObj.ToString());
                }
                catch (Exception ex)
                {
                }
            }
            return id;
        }

        public static string GetHowHear(int HowHear)
        {
            string HowDidYouHear = "";

            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Connection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[GetHowHear]", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@HowHearID", HowHear);

                conn.Open();

                try
                {
                    object rtnObj = cmd.ExecuteScalar();
                    HowDidYouHear = rtnObj.ToString();
                }
                catch (Exception ex)
                {
                }
            }
            return HowDidYouHear;
        }
        public static int GetHowHearID(string HowHearDesc)
        {
            int HowDidYouHear = 0;

            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Connection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[GetHowHearID]", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@HowHearDesc", HowHearDesc);

                conn.Open();

                try
                {
                    object rtnObj = cmd.ExecuteScalar();
                    HowDidYouHear = Convert.ToInt32(rtnObj);
                }
                catch (Exception ex)
                {
                }
            }
            return HowDidYouHear;
        }
        public static int GetRemainingRequests()
        {
            int Remaining = 0;

            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Connection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[GetRemainingFeatures]", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();

                try
                {
                    object rtnObj = cmd.ExecuteScalar();
                    Remaining = Convert.ToInt32(rtnObj);
                }
                catch (Exception ex)
                {
                }
            }
            return Remaining;
        }
        public static int UploadFile(string fileName, byte[] fileBytes, string fileType, int requestID)
        {
            int id = 0;
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Connection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[UploadFile]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FileName", fileName);
                cmd.Parameters.AddWithValue("@FileSource", fileBytes);
                cmd.Parameters.AddWithValue("@FileType", fileType);
                cmd.Parameters.AddWithValue("@RequestID", requestID);

                conn.Open();

                try
                {
                    object rtnObj = cmd.ExecuteScalar();
                    id = Int32.Parse(rtnObj.ToString());
                }
                catch (Exception ex)
                {
                }
            }
            return id;
        }
        public static DataTable SearchRequests(string RepsName, string ParentsName, int HowHear, string InstagramUsername, string RepsBirthdayBegin, string RepsBirthdayEnd, string Email, string HasSmallShop, string SmallShopName, string PayPalInvoiceNumber, string PayPalEmail, string RequestDateRangeBegin, string RequestDateRangeEnd, int HasPaid)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Connection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[SearchRequests]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RepsName", RepsName);
                cmd.Parameters.AddWithValue("@ParentsName", ParentsName);
                cmd.Parameters.AddWithValue("@HowHear", HowHear);
                cmd.Parameters.AddWithValue("@InstagramUsername", InstagramUsername);
                cmd.Parameters.AddWithValue("@RepsBirthdayBegin", RepsBirthdayBegin);
                cmd.Parameters.AddWithValue("@RepsBirthdayEnd", RepsBirthdayEnd);
                cmd.Parameters.AddWithValue("@Email", Email);
                cmd.Parameters.AddWithValue("@HasSmallShop", HasSmallShop);
                cmd.Parameters.AddWithValue("@SmallShopName", SmallShopName);
                cmd.Parameters.AddWithValue("@PayPalInvoiceNumber", PayPalInvoiceNumber);
                cmd.Parameters.AddWithValue("@PayPalEmail", PayPalEmail);
                cmd.Parameters.AddWithValue("@RequestDateRangeBegin", RequestDateRangeBegin);
                cmd.Parameters.AddWithValue("@RequestDateRangeEnd", RequestDateRangeEnd);
                cmd.Parameters.AddWithValue("@HasPaid", HasPaid);

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                try
                {
                    da.Fill(dt);
                }
                catch (Exception ex)
                {
                }
            }
            return dt;
        }
        public static DataTable GetTodaysBirthdays()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Connection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[GetTodaysBirthdays]", conn);

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                try
                {
                    da.Fill(dt);
                }
                catch (Exception ex)
                {
                }
            }
            return dt;
        }
        public static DataTable GetTomorrowsBirthdays()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Connection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[GetTomorrowsBirthdays]", conn);

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                try
                {
                    da.Fill(dt);
                }
                catch (Exception ex)
                {
                }
            }
            return dt;
        }
        public static DataTable GetDay3sBirthdays()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Connection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[GetDay3sBirthdays]", conn);

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                try
                {
                    da.Fill(dt);
                }
                catch (Exception ex)
                {
                }
            }
            return dt;
        }
        public static DataTable GetDay4sBirthdays()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Connection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[GetDay4sBirthdays]", conn);

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                try
                {
                    da.Fill(dt);
                }
                catch (Exception ex)
                {
                }
            }
            return dt;
        }
        public static DataTable GetDay5sBirthdays()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Connection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[GetDay5sBirthdays]", conn);

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                try
                {
                    da.Fill(dt);
                }
                catch (Exception ex)
                {
                }
            }
            return dt;
        }
        public static DataTable GetDay6sBirthdays()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Connection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[GetDay6sBirthdays]", conn);

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                try
                {
                    da.Fill(dt);
                }
                catch (Exception ex)
                {
                }
            }
            return dt;
        }
        public static DataTable GetDay7sBirthdays()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Connection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[GetDay7sBirthdays]", conn);

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                try
                {
                    da.Fill(dt);
                }
                catch (Exception ex)
                {
                }
            }
            return dt;
        }
        public static DataTable GetEndOfWeeksBirthdays()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Connection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[GetEndOfWeeksBirthdays]", conn);

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                try
                {
                    da.Fill(dt);
                }
                catch (Exception ex)
                {
                }
            }
            return dt;
        }
        public static DataTable DownloadFileBinary(int ID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Connection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[DownloadFileBinary]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", ID);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                conn.Open();
                try
                {
                    da.Fill(dt);
                }
                catch (Exception ex)
                {
                }
            }
            return dt;
        }
        public static DataTable RequestFileInfo(int requestID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Connection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[RequestFileInfo]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RequestID", requestID);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                conn.Open();
                try
                {
                    da.Fill(dt);
                }
                catch (Exception ex)
                {
                }
            }
            return dt;
        }
    }
}