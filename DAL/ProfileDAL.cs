using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using InterviewWork.Models;


namespace InterviewWork.DAL
{
    public class ProfileDAL
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        public List<Profile> GetAll()
        {
            List<Profile> profileList = new List<Profile>();
            using (SqlConnection con = new SqlConnection(strcon))
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("sp_GetProfiles", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_GetProfiles";
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                foreach (DataRow dr in dt.Rows)
                {
                    profileList.Add(new Profile
                    {
                        ProfileId = (int)dr["ProfileId"],
                        FirstName = dr["FirstName"].ToString(),
                        LastName = dr["LastName"].ToString(),
                        DateOfBirth = (DateTime)dr["DateOfBirth"],
                        PhoneNumber = dr["PhoneNumber"].ToString(),
                        EmailId = dr["EmailId"].ToString()
                    });
                }
            }
            return profileList;
        }
        public bool InsertProfile(Profile model)
        {
            int id = 0;
            using (SqlConnection con = new SqlConnection(strcon))
            {

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("sp_InsertProfile", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_InsertProfile";
                cmd.Parameters.AddWithValue("@FirstName", model.FirstName);
                cmd.Parameters.AddWithValue("@LastName", model.LastName);
                cmd.Parameters.AddWithValue("@DateOfBirth", model.DateOfBirth);
                cmd.Parameters.AddWithValue("@PhoneNumber", model.PhoneNumber);
                cmd.Parameters.AddWithValue("@EmailId", model.EmailId);
                id = cmd.ExecuteNonQuery();
                con.Close();
            }
            if (id > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public Profile getProfileByID(int ProfileId)
        {
            List<Profile> profileList = new List<Profile>();
            Profile profile = null;
            using (SqlConnection con = new SqlConnection(strcon))
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("sp_GetProfilesByID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_GetProfilesByID";
                cmd.Parameters.AddWithValue("@ProfileId", ProfileId);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    profile = new Profile
                    {
                        ProfileId = (int)dr["ProfileId"],
                        FirstName = dr["FirstName"].ToString(),
                        LastName = dr["LastName"].ToString(),
                        DateOfBirth = (DateTime)dr["DateOfBirth"],
                        PhoneNumber = dr["PhoneNumber"].ToString(),
                        EmailId = dr["EmailId"].ToString()
                    };
                }
            }
            return profile;
        }
            
        

        public bool UpdateProfile(Profile model)
        {
            int id = 0;
            using (SqlConnection con = new SqlConnection(strcon))
            {

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("sp_UpdateProfile", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_UpdateProfile";
                cmd.Parameters.AddWithValue("@ProfileId", model.ProfileId);
                cmd.Parameters.AddWithValue("@FirstName", model.FirstName);
                cmd.Parameters.AddWithValue("@LastName", model.LastName);
                cmd.Parameters.AddWithValue("@DateOfBirth", model.DateOfBirth);
                cmd.Parameters.AddWithValue("@PhoneNumber", model.PhoneNumber);
                cmd.Parameters.AddWithValue("@EmailId", model.EmailId);
                id = cmd.ExecuteNonQuery();
                con.Close();
            }
            if (id > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public void DeleteProfile(int ProfileId)
        {
            
            using (SqlConnection con = new SqlConnection(strcon))
            {

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("sp_DeleteProfile", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_DeleteProfile";
                cmd.Parameters.AddWithValue("@ProfileId", ProfileId);
                cmd.ExecuteNonQuery();
                
            }
        }
    }
}