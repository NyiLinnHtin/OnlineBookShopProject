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

        protected void LoginBtn_Click(object sender, EventArgs e)
        {

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