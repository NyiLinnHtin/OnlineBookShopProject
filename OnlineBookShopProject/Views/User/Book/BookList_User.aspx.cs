using OnlineBookShopProject.Assets.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineBookShopProject.Views.User.Book
{
    public partial class BookList_User : System.Web.UI.Page
    {
        Models.Functions Con;
        int userId;

        string mode, encryptedMode;
        protected void Page_Load(object sender, EventArgs e)
        {
            Con = new Models.Functions();
            userId = Convert.ToInt32(Session["UserId"].ToString());
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
            // Clear previous data source
            listBooks.DataSource = null;
            lblNoListDisplay.Visible = false;

            // Base query
            string query = @"
            SELECT 
                b.BookId,
                b.BookName,
                b.BookAuthor,
                COALESCE(a.AuthorName, 'Unknown') AS AuthorName,
                b.BookCategory,
                COALESCE(c.Category, 'Unknown') AS Category, 
                FORMAT(b.BookQty, 'N0') AS BookQty,
                FORMAT(b.BookPrice, 'N2') AS BookPrice,
                COALESCE(b.BookImage, '/UploadedImages/default-book-image.png') AS BookImage
            FROM 
                Book b
            LEFT JOIN 
                Author a ON b.BookAuthor = a.AuthorId
            LEFT JOIN 
                Category c ON b.BookCategory = c.CategoryId";

            // Initialize where clause
            string whereClause = " WHERE 1=1 "; // To simplify appending conditions

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
                if (decimal.TryParse(txtPriceStart.Text, out decimal minPrice))
                {
                    whereClause += $" AND b.BookPrice >= {minPrice}";
                }
            }

            // If Maximum Price is provided
            if (!string.IsNullOrEmpty(txtPriceEnd.Text))
            {
                if (decimal.TryParse(txtPriceEnd.Text, out decimal maxPrice))
                {
                    whereClause += $" AND b.BookPrice <= {maxPrice}";
                }
            }

            // Finalize the query
            query += whereClause;

            // Execute the query and get the result in a DataTable
            DataTable dt = Con.GetData(query);

            // If there are no results, show the 'no data' message
            if (dt == null || dt.Rows.Count == 0)
            {
                lblNoListDisplay.Visible = true;
                listBooks.DataSource = null;
                listBooks.DataBind();
            }
            else
            {
                // Hide the no data label and bind the ListView
                lblNoListDisplay.Visible = false;
                listBooks.DataSource = dt;
                listBooks.DataBind();
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

        protected void listBooks_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewDetails")
            {
                int bookId = Convert.ToInt32(e.CommandArgument.ToString());
                string encryptedBookId = Common_Functions.Encrypt(bookId.ToString());
                Response.Redirect($"BookDetail_User.aspx?BookId={encryptedBookId}");
            }
            else if (e.CommandName == "AddToCart")
            {
                string commandArg = e.CommandArgument.ToString();
                string[] args = commandArg.Split(';');

                int bookId = Convert.ToInt32(args[0]);
                string bookName = args[1];
                if (!IsBookAdded(bookId, bookName))
                {
                    AddToCart(bookId, bookName);
                }
            }
        }

        protected Boolean IsBookAdded(int bookId, string bookName)
        {
            string query = $"select * from CheckOut where UserId = {userId} and BookId = {bookId}";
            DataTable dt = Con.GetData(query);

            if (!(dt == null || dt.Rows.Count == 0))
            {
                ShowFailureMessage($"{bookName} is already added to your cart.");
                return true;
            }
            return false;
        }
        protected void AddToCart(int bookId, string bookName)
        {
            try
            {
                string Query = $"Insert into CheckOut values('{userId}','{bookId}',{1})";
                Con = new Models.Functions();
                Con.SetData(Query);

                ShowSuccessMessage($"{bookName} is added to your cart successfully!");
            }
            catch (Exception ex)
            {
                //lblError.Text = ex.Message;
                //lblError.Visible = true;
            }
        }

        protected void ShowSuccessMessage(string message)
        {
            lblSuccessContents.Text = message;
            successModal.Style["display"] = "block";
        }

        protected void btnSuccessOK_Click(object sender, EventArgs e)
        {
            successModal.Style["display"] = "none";
        }

        protected void btnGridSearch_Click(object sender, EventArgs e)
        {
            ShowAllBooks();
        }

        protected void ShowFailureMessage(string message)
        {
            lblFailureContents.Text = message;
            failureModal.Style["display"] = "block";
        }

        protected void btnFailureOK_Click(object sender, EventArgs e)
        {
            failureModal.Style["display"] = "none";
        }

        private bool checkSearchCondition()
        {
            bool isError = false;
            if (Int32.Parse(txtPriceStart.Text.Trim()) > Int32.Parse(txtPriceEnd.Text.Trim()))
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