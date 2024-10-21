using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace OnlineBookShopProject.Models
{
    public class Functions
    {
        private SqlConnection Con;
        private SqlCommand cmd;
        private DataTable dt;
        private SqlDataAdapter sda;
        private string Constr;

        public Functions()
        {
            Constr = @"Data Source=ZD8B4608;Initial Catalog=BookShopDB;Persist Security Info=True;User ID=sa;Password=gx1433;Connect Timeout=30";
            Con = new SqlConnection(Constr);
            cmd = new SqlCommand();
            cmd.Connection = Con;
        }

        public  DataTable GetData(string Query)
        {
            dt = new DataTable();
            sda = new SqlDataAdapter(Query, Constr);
            try
            {
                sda.Fill(dt);
            }
            catch (SqlException ex)
            {
                throw new Exception("Error executing query: " + ex.Message);
            }

            return dt;
        }

        public int SetData(string Query)
        {
            int affectedRows = 0;
            if(Con.State == ConnectionState.Closed)
            {
                Con.Open();
            }

            cmd.CommandText = Query;
            affectedRows = cmd.ExecuteNonQuery();
            Console.WriteLine("Executing Query: " + Query);
            try
            {
                affectedRows = cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                affectedRows = -1;
                throw new Exception("Database operation failed: " + ex.Message);
            }
            finally
            {
                Con.Close();
            }
            return affectedRows;
        }

        public object GetScalar(string query)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Constr))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        return cmd.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error executing scalar query: " + ex.Message);
            }
            finally
            {
                Con.Close();
            }
        }
    }
}