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
            sda.Fill(dt);
            return dt;
        }

        public int SetData(string Query)
        {
            int cnt = 0;
            if(Con.State == ConnectionState.Closed)
            {
                Con.Open();
            }
            cmd.CommandText = Query;
            cnt = cmd.ExecuteNonQuery();
            Con.Close();
            return cnt;
        }

    }
}