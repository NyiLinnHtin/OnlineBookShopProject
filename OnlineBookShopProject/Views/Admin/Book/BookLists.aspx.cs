﻿using OnlineBookShopProject.Assets.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineBookShopProject.Views.Admin.Book
{
    public partial class BookLists : System.Web.UI.Page
    {
        Models.Functions Con;
        int bookId;

        string mode, encryptedMode, encryptedBookId;
        protected void Page_Load(object sender, EventArgs e)
        {
            Con = new Models.Functions();
            if (!IsPostBack)
            {
                DisplayCategories();
                ShowAllBooks();
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

        protected void gvAuthors_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "SelectAuthor")
            {
                if (e.CommandName == "SelectAuthor")
                {
                    string commandArg = e.CommandArgument.ToString();
                    string[] args = commandArg.Split(';');

                    string authorId = args[0]; // Get AuthorId
                    string authorName = args[1]; // Get AuthorName

                    // Set the hidden field and text box values
                    hfAuthorId.Value = authorId;  // Set the hidden field value
                    txtAuthor.Text = authorName;   // Set the text box value
                }
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
            lblAuthorSearchErr.Visible = false;
            gvAuthors.DataSource = null;
            gvAuthors.DataBind();

            string authorID = txtAuthorID.Text.Trim();
            string authorName = txtAuthorName.Text.Trim();

            string query = "SELECT * FROM Author WHERE 1=1";

            if (!string.IsNullOrEmpty(authorID))
            {
                if (int.TryParse(authorID, out int authorId))
                {
                    query += $" AND AuthorId = {authorId}";
                }
                else
                {
                    lblAuthorSearchErr.Visible = true;
                    lblAuthorSearchErr.Text = "AuthorId must be a valid number.";
                    return;
                }
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
            }
        }

        protected void btnAuthorSearch_Click(object sender, EventArgs e)
        {

            txtAuthorID.Text = "";
            txtAuthorName.Text = "";
            DisplayAuthors();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "openModal", "showModal();", true);
        }

        private void ShowAllBooks()
        {
            gridBooks.DataSource = null;
            string query = @"
                    SELECT 
                        b.BookId,
                        b.BookName,
                        b.BookAuthor,
                        COALESCE(a.AuthorName, 'Unknown') AS AuthorName,
                        b.BookCategory,
                        COALESCE(c.Category, 'Unknown') AS Category, 
                        FORMAT(b.BookQty, 'N0') AS BookQty,    -- No decimal places, comma-separated
                        FORMAT(b.BookPrice, 'N2') AS BookPrice -- Two decimal places, comma-separated
                    FROM 
                        Book b
                    LEFT JOIN 
                        Author a ON b.BookAuthor = a.AuthorId
                    LEFT JOIN 
                        Category c ON b.BookCategory = c.CategoryId";

            // Initialize where clause
            string whereClause = " WHERE 1=1 "; // A trick to simplify adding multiple conditions

            // If Book Name is provided
            if (!string.IsNullOrEmpty(txtBookName.Value))
            {
                whereClause += $" AND b.BookName LIKE '%{txtBookName.Value}%'";
            }

            // If Author ID is provided
            if (!string.IsNullOrEmpty(hfAuthorId.Value))
            {
                whereClause += $" AND b.BookAuthor = {hfAuthorId.Value}";
            }

            // If Category is selected
            if (DDLCategory.SelectedIndex != 0)
            {
                whereClause += $" AND b.BookCategory = '{DDLCategory.SelectedValue}'";
            }

            // If Minimum Price is provided
            if (!string.IsNullOrEmpty(txtPriceStart.Text))
            {
                whereClause += $" AND b.BookPrice >= {txtPriceStart.Text}";
            }

            // If Maximum Price is provided
            if (!string.IsNullOrEmpty(txtPriceEnd.Text))
            {
                whereClause += $" AND b.BookPrice <= {txtPriceEnd.Text}";
            }

            // Append the where clause to the main query
            query += whereClause;

            // Execute the query
            DataTable dt = Con.GetData(query);

            // Check if the DataSource (DataTable) has any rows
            if (dt == null || dt.Rows.Count == 0)
            {
                // Show the no data label
                lblNoListDisplay.Visible = true;
                gridBooks.DataSource = null;
                gridBooks.DataBind();
            }
            else
            {
                // Hide the no data label
                lblNoListDisplay.Visible = false;
                gridBooks.DataSource = dt;
                gridBooks.DataBind();
            }
        }

        protected void gridBooks_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridBooks.PageIndex = e.NewPageIndex;
            ShowAllBooks();
        }

        protected void gridBooks_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditBook")
            {
                bookId = Convert.ToInt32(e.CommandArgument);
                mode = Common_Datas.Mode.Edit;

                // Encrypt the parameters
                encryptedMode = Common_Functions.Encrypt(mode);
                encryptedBookId = Common_Functions.Encrypt(bookId.ToString());

                // Redirect to the CategoryDetail.aspx page with encrypted parameters
                Response.Redirect($"BookDetail.aspx?Mode={encryptedMode}&BookId={encryptedBookId}");
            }
            else if (e.CommandName == "DeleteBook")
            {
                bookId = Convert.ToInt32(e.CommandArgument);
                mode = Common_Datas.Mode.Delete;

                // Encrypt the parameters
                encryptedMode = Common_Functions.Encrypt(mode);
                encryptedBookId = Common_Functions.Encrypt(bookId.ToString());

                // Redirect to the CategoryDetail.aspx page with encrypted parameters
                Response.Redirect($"BookDetail.aspx?Mode={encryptedMode}&BookId={encryptedBookId}");
            }
            else if (e.CommandName == "ViewBook")
            {
                bookId = Convert.ToInt32(e.CommandArgument);
                mode = Common_Datas.Mode.View;

                // Encrypt the parameters
                encryptedMode = Common_Functions.Encrypt(mode);
                encryptedBookId = Common_Functions.Encrypt(bookId.ToString());

                // Redirect to the CategoryDetail.aspx page with encrypted parameters
                Response.Redirect($"BookDetail.aspx?Mode={encryptedMode}&BookId={encryptedBookId}");
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtBookName.Value = "";
            hfAuthorId.Value = "";
            txtAuthor.Text = "";
            DDLCategory.SelectedIndex = 0;
            txtPriceStart.Text = "";
            txtPriceEnd.Text = "";

            lblDisplayError.Text = "";
            lblDisplayError.Visible = false;

            ShowAllBooks();
        }

        protected void btnGridSearch_Click(object sender, EventArgs e)
        {
            if (!checkSearchCondition())
            {
                ShowAllBooks();
            }
        }

        private bool checkSearchCondition()
        {
            bool isError = false;
            if(Int32.Parse(txtPriceStart.Text.Trim()) > Int32.Parse(txtPriceEnd.Text.Trim()))
            {
                lblDisplayError.Text = "Min Price must be lower than Max Price!!!";
                lblDisplayError.Visible = true;
                isError = true;
                return isError;
            }
            lblDisplayError.Visible = false;
            return isError;
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            mode = Common_Datas.Mode.Add;
            encryptedMode = Common_Functions.Encrypt(mode);
            Response.Redirect($"BookDetail.aspx?Mode={encryptedMode}");
        }
    }
}