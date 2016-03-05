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

        public static DataTable SearchRequests(string firstName, string lastName, string companyName, int demoID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Connection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[SearchRequests]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FirstName", firstName);
                cmd.Parameters.AddWithValue("@LastName", lastName);
                cmd.Parameters.AddWithValue("@CompanyName", companyName);
                cmd.Parameters.AddWithValue("@DemoID", demoID);

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