using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineBookShopProject.Views
{
    public partial class Login : System.Web.UI.Page
    {
        Models.Functions Con;
        protected void Page_Load(object sender, EventArgs e)
        {
            // Hide error message on page load
            lblErrorMessage.Visible = false;
        }

        protected void LoginBtn_Click(object sender, EventArgs e)
        {
            Con = new Models.Functions();
            CheckLogin();
        }

        private void CheckLogin()
        {
            string query = $"Select * from UserAccount where UserName = '{txtUserName.Text.Trim()}' and Password = {txtPassword.Text.Trim()}";
            int userRole = 0;
            DataTable dt = Con.GetData(query);
            if (dt.Rows.Count > 0)
            {
                Session["UserName"] = dt.Rows[0]["UserName"].ToString();
                Session["UserId"] = dt.Rows[0]["UserId"].ToString();

                userRole = Int32.Parse(dt.Rows[0]["UserRole"].ToString());
                lblErrorMessage.Visible = false;

                // Redirect based on role
                if (userRole == 1) // Admin role
                {
                    Response.Redirect("Admin/Book/BookLists.aspx");
                }
                else if (userRole == 2) // User role
                {
                    Response.Redirect("User/Book/BookList_User.aspx");
                }
            }
            else
            {
                lblErrorMessage.Text = "Invalid username or password.";
                lblErrorMessage.Visible = true;
            }
        }
    }
}