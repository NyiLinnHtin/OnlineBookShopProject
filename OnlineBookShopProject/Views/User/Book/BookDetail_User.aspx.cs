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
    public partial class BookDetail_User : System.Web.UI.Page
    {
        Models.Functions Con = new Models.Functions();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetBookData();
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("BookList_User.aspx");
        }

        protected void SetBookData()
        {
            string encryptedBookId = Request.QueryString["BookId"];
            if (!string.IsNullOrEmpty(encryptedBookId))
            {
                int bookId = Convert.ToInt32(Common_Functions.Decrypt(encryptedBookId));
                ViewState["BookId"] = bookId;
                string query = String.Format(@"
                                SELECT 
                                    b.BookImage,
                                    b.BookId,
                                    b.BookName,
                                    b.BookDescription,
                                    b.BookAuthor,
                                    COALESCE(a.AuthorName, 'Unknown') AS AuthorName,
                                    b.BookCategory,
                                    COALESCE(c.Category, 'Unknown') AS Category, 
                                    FORMAT(b.BookQty, 'N0') AS BookQty,    
                                    FORMAT(b.BookPrice, 'N2') AS BookPrice 
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
                    string bookImagePath = dt.Rows[0]["BookImage"].ToString();
                    bookImage.Src = !string.IsNullOrEmpty(bookImagePath)
                        ? bookImagePath
                        : "~/UploadedImages/default-book-image.png";
                    bookImage.Src = dt.Rows[0]["BookImage"].ToString();
                    txtBookName.Text = dt.Rows[0]["BookName"].ToString();
                    txtBookDescription.Text = dt.Rows[0]["BookDescription"].ToString();
                    txtAuthor.Text = dt.Rows[0]["AuthorName"].ToString();
                    txtCategory.Text = dt.Rows[0]["Category"].ToString();
                    txtBookPrice.Value = dt.Rows[0]["BookPrice"].ToString();
                    txtBookQty.Value = dt.Rows[0]["BookQty"].ToString();
                }
                else
                {
                    lblError.Text = "Sorry, Something is wrong. Please Go Back to Book List";
                    lblError.Visible = true;
                }
            }
            else
            {
                lblError.Text = "Sorry, Something is wrong. Please Go Back to Book List";
                lblError.Visible = true;
            }
            
        }
    }
}