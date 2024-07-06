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
    public class TaskDAL
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        public List<Task> GetTasks(int ProfileId)
        {
            List<Task> TaskList = new List<Task>();
            using (SqlConnection con = new SqlConnection(strcon))
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("sp_GetTasks", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_GetTasks";
                cmd.Parameters.AddWithValue("@ProfileId", ProfileId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                foreach (DataRow dr in dt.Rows)
                {
                    TaskList.Add(new Task
                    {
                        Id = (int)dr["Id"],
                        ProfileId = (int)dr["ProfileId"],
                        TaskName = dr["TaskName"].ToString(),
                        TaskDescription =dr["TaskDescription"].ToString(),
                        StartTime = (DateTime)dr["StartTime"],
                        Status = (int)dr["Status"]
                    });
                }
            }
            return TaskList;
        }
        public bool InsertTask(Task model)
        {
            int id = 0;
            using (SqlConnection con = new SqlConnection(strcon))
            {

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("sp_AddTask", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_AddTask";
                cmd.Parameters.AddWithValue("@ProfileId", model.ProfileId);
                cmd.Parameters.AddWithValue("@TaskName", model.TaskName);
                cmd.Parameters.AddWithValue("@TaskDescription", model.TaskDescription);
                cmd.Parameters.AddWithValue("@StartTime", model.StartTime);
                cmd.Parameters.AddWithValue("@Status", model.Status);
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
        public Task getTaskByID(int Id)
        {
            Task task = null;
            using (SqlConnection con = new SqlConnection(strcon))
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("sp_GetTaskByID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_GetTaskByID";
                cmd.Parameters.AddWithValue("@Id",Id);
                
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    task = new Task
                    {
                        Id = (int)dr["Id"],
                        ProfileId = (int)dr["ProfileId"],
                        TaskName = dr["TaskName"].ToString(),
                        TaskDescription = dr["TaskDescription"].ToString(),
                        StartTime = (DateTime)dr["StartTime"],
                        Status = (int)dr["Status"]
                    };
                }
            }
            return task;
        }
        public bool UpdateTask(Task model)
        {
            int id = 0;
            using (SqlConnection con = new SqlConnection(strcon))
            {

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("sp_UpdateTask", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_UpdateTask";
                cmd.Parameters.AddWithValue("@Id", model.Id);
                cmd.Parameters.AddWithValue("@ProfileId", model.ProfileId);
                cmd.Parameters.AddWithValue("@TaskName", model.TaskName);
                cmd.Parameters.AddWithValue("@TaskDescription", model.TaskDescription);
                cmd.Parameters.AddWithValue("@StartTime", model.StartTime);
                cmd.Parameters.AddWithValue("@Status", model.Status);
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
        public void DeleteTask(int Id)
        {

            using (SqlConnection con = new SqlConnection(strcon))
            {

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("sp_DeleteTask", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_DeleteTask";
                cmd.Parameters.AddWithValue("@Id",Id);
                cmd.ExecuteNonQuery();

            }
        }
    }
}