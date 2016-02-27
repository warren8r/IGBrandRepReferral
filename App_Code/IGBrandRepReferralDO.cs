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
        public static DataTable GetAllDemos()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Connection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[GetAllDemos]", conn);
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

        public static DataTable GetAllCountries()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Connection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[GetAllCountries]", conn);
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

        public static DataTable GetAllStates()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Connection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[GetAllStates]", conn);
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

        public static int InsertRequest(string firstName, string lastName, string phone, string email, string companyName, string companyURL, string position,
                                        string address1, string address2, int countryID, string city, int stateID, string zipCode)
        {
            int id = 0;
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Connection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[InsertRequest]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FirstName", firstName);
                cmd.Parameters.AddWithValue("@LastName", lastName);
                cmd.Parameters.AddWithValue("@Phone", phone);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@CompanyName", companyName);
                cmd.Parameters.AddWithValue("@CompanyURL", companyURL);
                cmd.Parameters.AddWithValue("@Position", position);
                cmd.Parameters.AddWithValue("@Address1", address1);
                cmd.Parameters.AddWithValue("@Address2", address2);
                cmd.Parameters.AddWithValue("@CountryID", countryID);
                cmd.Parameters.AddWithValue("@City", city);
                cmd.Parameters.AddWithValue("@StateID", stateID);
                cmd.Parameters.AddWithValue("@ZipCode", zipCode);

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

        public static void InsertRequestDemoLookup(int requestID, int demoID)
        {
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Connection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[InsertRequestDemoLookup]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RequestID", requestID);
                cmd.Parameters.AddWithValue("@DemoID", demoID);

                conn.Open();

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                }
            }
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