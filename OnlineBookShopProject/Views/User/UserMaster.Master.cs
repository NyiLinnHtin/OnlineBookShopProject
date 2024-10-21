using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineBookShopProject.Views.User
{
    public partial class UserMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if the user is logged in (i.e., Session has a value)
                if (Session["UserName"] != null)
                {
                    // Display the logged-in username in the label
                    lblUserName.Text = Session["UserName"].ToString();
                }
                else
                {
                    // Optional: Redirect to login page if the user is not logged in
                    //Response.Redirect("~/Views/Login.aspx");
                }
            }
        }
    }
}