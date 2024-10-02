using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineBookShopProject.Views.Admin
{
    public partial class Books : System.Web.UI.Page
    {
        Models.Functions Con;
        protected bool IsModalVisible { get; set; } = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            Con = new Models.Functions();
            DisplayCategories();
        }

        private void DisplayCategories()
        {
            string Query = "SELECT CategoryId, Category FROM Category order by Category";
            DataTable dt = Con.GetData(Query);

            DDLCategory.Items.Clear();

            if (dt.Rows.Count > 0)
            {
                DDLCategory.DataSource = dt;
                DDLCategory.DataTextField = "Category";
                DDLCategory.DataValueField = "CategoryId";
                DDLCategory.DataBind();

                DDLCategory.Items.Insert(0, new ListItem("-- Select a category to proceed --", "0"));
            }
            else
            {
                DDLCategory.Items.Insert(0, new ListItem("No Categories Available", "0"));
            }
        }

        protected void gvAuthors_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "SelectAuthor")
            {
                // Get the author's name from CommandArgument
                string authorName = e.CommandArgument.ToString();

                // Set the author's name to the txtAuthor textbox
                txtAuthor.Text = authorName;
            }
        }

        protected void gvAuthors_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAuthors.PageIndex = e.NewPageIndex;
            DisplayAuthors();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DisplayAuthors();
            hfModalOpen.Value = "true";
        }

        private void DisplayAuthors()
        {
            string authorID = txtAuthorID.Text.Trim();
            string authorName = txtAuthorName.Text.Trim();

            string query = "SELECT * FROM Author WHERE 1=1";

            if (!string.IsNullOrEmpty(authorID))
            {
                query += $" AND AuthorId = {authorID}";
                // Add parameter for AuthorID
            }
            if (!string.IsNullOrEmpty(authorName))
            {
                query += $" AND AuthorName LIKE '%' + {authorName} + '%'";
                // Add parameter for AuthorName
            }

            DataTable dt = Con.GetData(query);

            if (dt.Rows.Count > 0)
            {
                lblNoAuthor.Visible = false;
                gvAuthors.DataSource = dt;
                gvAuthors.DataBind();
            }
            else
            {
                lblNoAuthor.Visible = true;
                gvAuthors.DataSource = null;
                gvAuthors.DataBind();
            }
        }

        protected void btnAuthorSearch_Click(object sender, EventArgs e)
        {

            txtAuthorID.Text = "";
            txtAuthorName.Text = "";
            DisplayAuthors();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "openModal", "showModal();", true);
        }
    }
}