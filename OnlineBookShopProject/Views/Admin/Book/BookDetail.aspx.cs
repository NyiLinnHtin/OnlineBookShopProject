using OnlineBookShopProject.Assets.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineBookShopProject.Views.Admin
{
    public partial class Books : System.Web.UI.Page
    {
        Models.Functions Con;
        int bookId, errorCd;
        string mode;
        string encryptedMode, encryptedBookId;

        protected bool IsModalVisible { get; set; } = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            Con = new Models.Functions();
            if (!IsPostBack)
            {
                //bookImage.ImageUrl = "~/Assets/images/UnknownBook.png";  // Default image

                encryptedMode = Request.QueryString["Mode"];
                DisplayCategories();
                if (!string.IsNullOrEmpty(encryptedMode))
                {
                    mode = Common_Functions.Decrypt(encryptedMode);
                    ViewState["Mode"] = mode;

                    if (mode == Common_Datas.Mode.Edit)
                    {
                        btnRegister.Visible = false;
                        btnUpdate.Visible = true;
                        btnDelete.Visible = false;

                        DisplayValues();
                    }
                    else if (mode == Common_Datas.Mode.Add)
                    {
                        btnRegister.Visible = true;
                        btnUpdate.Visible = false;
                        btnDelete.Visible = false;
                    }
                    else if (mode == Common_Datas.Mode.Delete)
                    {
                        btnRegister.Visible = false;
                        btnUpdate.Visible = false;
                        btnDelete.Visible = true;

                        DisplayValues();
                        bookImage.Attributes.Remove("onclick");
                        bookImage.Style["cursor"] = "not-allowed";
                        txtBookName.Enabled = false;
                        txtBookDescription.Enabled = false;
                        DDLCategory.Enabled = false;
                        btnAuthorSearch.Visible = false;
                        txtBookPrice.Disabled = true;
                        txtBookQty.Disabled = true;
                    }
                    else
                    {
                        btnRegister.Visible = false;
                        btnUpdate.Visible = false;
                        btnDelete.Visible = false;

                        DisplayValues();
                        bookImage.Attributes.Remove("onclick");
                        bookImage.Style["cursor"] = "not-allowed";
                        txtBookName.Enabled = false;
                        txtBookDescription.Enabled = false;
                        DDLCategory.Enabled = false;
                        btnAuthorSearch.Visible = false;
                        txtBookPrice.Disabled = true;
                        txtBookQty.Disabled = true;
                    }
                }
                else
                {
                    lblError.Text = "Invalid or missing parameters.";
                    lblError.Visible = true;
                }
            }
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

        private void DisplayValues()
        {
            encryptedBookId = Request.QueryString["BookId"];
            bookId = Convert.ToInt32(Common_Functions.Decrypt(encryptedBookId));
            ViewState["BookId"] = bookId;
            string query = String.Format(@"
                                SELECT 
                                    b.BookId,
                                    b.BookName,
                                    b.BookDescription,
                                    b.BookAuthor,
                                    COALESCE(a.AuthorName, 'Unknown') AS AuthorName,
                                    b.BookCategory,
                                    COALESCE(c.Category, 'Unknown') AS Category, 
                                    b.BookPrice,
                                    b.BookQty
                                FROM 
                                    Book b
                                LEFT JOIN 
                                    Author a ON b.BookAuthor = a.AuthorId
                                LEFT JOIN 
                                    Category c ON b.BookCategory = c.CategoryId
                                WHERE 
                                    b.BookId = {0}", bookId);

            DataTable dt = Con.GetData(query);

            if (dt.Rows.Count > 0)
            {
                txtBookName.Text = dt.Rows[0]["BookName"].ToString();
                txtBookDescription.Text = dt.Rows[0]["BookDescription"].ToString();
                HDAuthorID.Value = dt.Rows[0]["BookAuthor"].ToString();
                txtAuthor.Text = dt.Rows[0]["AuthorName"].ToString();
                DDLCategory.SelectedValue = dt.Rows[0]["BookCategory"].ToString();
                txtBookPrice.Value = dt.Rows[0]["BookPrice"].ToString();
                txtBookQty.Value = dt.Rows[0]["BookQty"].ToString();
            }
            else
            {
                lblError.Text = "No data found for the given category.";
                lblError.Visible = true;
            }
        }

        protected void gvAuthors_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "SelectAuthor")
            {
                string commandArg = e.CommandArgument.ToString();
                string[] args = commandArg.Split(';');

                string authorId = args[0];
                string authorName = args[1];

                HDAuthorID.Value = authorId;
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
                query += $" AND AuthorName LIKE '%' + '{authorName}' + '%'";
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

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            CheckError();

            if (errorCd == 0)
            {
                ShowConfirmMessage("Are you sure you want to Update?");
            }
        }

        protected void ShowConfirmMessage(string message)
        {
            lblConfirmContents.Text = message;
            confirmationModal.Style["display"] = "block";
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            confirmationModal.Style["display"] = "none";
            UpdateData();
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            confirmationModal.Style["display"] = "none";
        }

        protected void ShowSuccessMessage(string message)
        {
            lblSuccessContents.Text = message;
            successModal.Style["display"] = "block";
        }

        protected void btnSuccessOK_Click(object sender, EventArgs e)
        {
            successModal.Style["display"] = "none";
            Response.Redirect("CategoryLists.aspx");
        }

        protected void btnAuthorSearch_Click(object sender, EventArgs e)
        {

            txtAuthorID.Text = "";
            txtAuthorName.Text = "";
            DisplayAuthors();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "openModal", "showModal();", true);
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("BookLists.aspx");
        }

        private void CheckError()
        {
            errorCd = 0;
            if (string.IsNullOrEmpty(txtBookName.Text))
            {
                txtBookName.CssClass = "form-control border-error";
                lblError.Text = "Please Fill all Required Fields";
                lblError.Visible = true;
                errorCd = 1;
            }
            else
            {
                txtBookName.CssClass = "form-control border-black";
                lblError.Visible = false;
            }

            if (string.IsNullOrEmpty(txtBookDescription.Text))
            {
                txtBookDescription.CssClass = "form-control border-error";
                lblError.Text = "Please Fill all Required Fields";
                lblError.Visible = true;
                errorCd = 1;
            }
            else
            {
                txtBookName.CssClass = "form-control border-black";
                lblError.Visible = false;
            }
        }

        private void UpdateData()
        {
            mode = ViewState["Mode"].ToString();
            //GetBookImage();
            //Update Function
            //if (mode == Common_Datas.Mode.Edit)
            //{
            //    try
            //    {
            //        bookId = Convert.ToInt32(ViewState["BookId"]);
            //        string Query = string.Format(@"UPDATE Book 
            //                SET BookImage = '{1}'
            //                BookName = '{2}', 
            //                BookDescription = '{3}',
            //                BookAuthor = '{4}',
            //                BookCategory = '{5}',
            //                BookQty = '{6}',
            //                BookPrice = '{7}',
            //                WHERE BookId = {0}",);
            //        Con.SetData(Query);

            //        ShowSuccessMessage("Category updated successfully!");
            //    }
            //    catch (Exception ex)
            //    {
            //        lblError.Text = ex.Message;
            //        lblError.Visible = true;
            //    }

            //}

            ////Register Method
            //else if (mode == Common_Datas.Mode.Add)
            //{
            //    try
            //    {
            //        string Query = $"insert into Category values('{txtCategory.Text}','{txtDescription.Value}')";
            //        Con = new Models.Functions();
            //        Con.SetData(Query);

            //        ShowSuccessMessage("Category registered successfully!");
            //    }
            //    catch (Exception ex)
            //    {
            //        lblError.Text = ex.Message;
            //        lblError.Visible = true;
            //    }
            //}

            ////Delete Function
            //else if (mode == Common_Datas.Mode.Delete)
            //{
            //    try
            //    {
            //        categoryId = Convert.ToInt32(ViewState["CategoryId"]);
            //        string Query = $"DELETE FROM Category WHERE CategoryId = {categoryId}";
            //        Con = new Models.Functions();
            //        Con.SetData(Query);

            //        ShowSuccessMessage("Category deleted successfully!");
            //    }
            //    catch (Exception ex)
            //    {
            //        lblError.Text = ex.Message;
            //        lblError.Visible = true;
            //    }
            //}
        }
    }
}