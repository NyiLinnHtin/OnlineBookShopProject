using OnlineBookShopProject.Assets.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineBookShopProject.Views.Admin.Categories
{
    public partial class CategoryLists : System.Web.UI.Page
    {
        Models.Functions Con;
        int categoryId;

        string mode, encryptedMode,encryptedCategoryId;

        protected void Page_Load(object sender, EventArgs e)
        {
            Con = new Models.Functions();
            ShowAllCategories();
        }

        private void ShowAllCategories()
        {
            string Query = "SELECT * FROM Category";

            if (!string.IsNullOrEmpty(txtCategory.Value))
            {
                Query += " WHERE Category LIKE '%" + txtCategory.Value + "%';";
            }

            gridCategory.DataSource = Con.GetData(Query);
            gridCategory.DataBind();
        }

        protected void gridCategory_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridCategory.PageIndex = e.NewPageIndex;
            ShowAllCategories();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ShowAllCategories();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtCategory.Value = "";
            ShowAllCategories();
        }

        protected void gridCategory_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditCategory")
            {
                categoryId = Convert.ToInt32(e.CommandArgument);
                mode = Common_Datas.Mode.Edit;

                // Encrypt the parameters
                encryptedMode = Common_Functions.Encrypt(mode);
                encryptedCategoryId = Common_Functions.Encrypt(categoryId.ToString());

                // Redirect to the CategoryDetail.aspx page with encrypted parameters
                Response.Redirect($"CategoryDetail.aspx?Mode={encryptedMode}&CategoryId={encryptedCategoryId}");
            }
            else if (e.CommandName == "DeleteCategory")
            {
                categoryId = Convert.ToInt32(e.CommandArgument);
                mode = Common_Datas.Mode.Delete;

                // Encrypt the parameters
                encryptedMode = Common_Functions.Encrypt(mode);
                encryptedCategoryId = Common_Functions.Encrypt(categoryId.ToString());

                // Redirect to the CategoryDetail.aspx page with encrypted parameters
                Response.Redirect($"CategoryDetail.aspx?Mode={encryptedMode}&CategoryId={encryptedCategoryId}");
            }
            else if (e.CommandName == "ViewCategory")
            {
                categoryId = Convert.ToInt32(e.CommandArgument);
                mode = Common_Datas.Mode.View;

                // Encrypt the parameters
                encryptedMode = Common_Functions.Encrypt(mode);
                encryptedCategoryId = Common_Functions.Encrypt(categoryId.ToString());

                // Redirect to the CategoryDetail.aspx page with encrypted parameters
                Response.Redirect($"CategoryDetail.aspx?Mode={encryptedMode}&CategoryId={encryptedCategoryId}");
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            mode = Common_Datas.Mode.Add;
            encryptedMode = Common_Functions.Encrypt(mode);
            Response.Redirect($"CategoryDetail.aspx?Mode={encryptedMode}");
        }
    }
}