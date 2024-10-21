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
            try
            {
                // Decrypt the BookId from query string and store it in ViewState
                encryptedBookId = Request.QueryString["BookId"];
                bookId = Convert.ToInt32(Common_Functions.Decrypt(encryptedBookId));
                ViewState["BookId"] = bookId;

                // SQL Query to get book details
                string query = string.Format(@"
            SELECT 
                b.BookImage,
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

                // Get data from database
                DataTable dt = Con.GetData(query);

                if (dt.Rows.Count > 0)
                {
                    // Display the book image (use a default image if none exists)
                    string bookImagePath = dt.Rows[0]["BookImage"].ToString();
                    bookImage.Src = !string.IsNullOrEmpty(bookImagePath)
                        ? bookImagePath
                        : "~/UploadedImages/default-book-image.png";

                    // Display book details
                    txtBookName.Text = dt.Rows[0]["BookName"]?.ToString() ?? string.Empty;
                    txtBookDescription.Text = dt.Rows[0]["BookDescription"]?.ToString() ?? string.Empty;

                    // Hidden field for BookAuthor
                    HDAuthorID.Value = dt.Rows[0]["BookAuthor"]?.ToString() ?? string.Empty;

                    // Textbox for Author Name (show 'Unknown' if not available)
                    txtAuthor.Text = dt.Rows[0]["AuthorName"]?.ToString() ?? "Unknown";

                    // Select the category in the dropdown (make sure the value exists in the dropdown)
                    string bookCategory = dt.Rows[0]["BookCategory"].ToString();
                    if (DDLCategory.Items.FindByValue(bookCategory) != null)
                    {
                        DDLCategory.SelectedValue = bookCategory;
                    }
                    else
                    {
                        DDLCategory.SelectedIndex = 0; // Default to first item if the category is missing
                    }

                    // Display book price and quantity
                    txtBookPrice.Value = dt.Rows[0]["BookPrice"]?.ToString() ?? "0.00";
                    txtBookQty.Value = dt.Rows[0]["BookQty"]?.ToString() ?? "0";
                }
                else
                {
                    lblError.Text = "No data found for the given book.";
                    lblError.Visible = true;
                }
            }
            catch (Exception ex)
            {
                // Handle any unexpected errors
                lblError.Text = "Error loading book details: " + ex.Message;
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
            Response.Redirect("BookLists.aspx");
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

            // Update function when in Edit mode
            if (mode == Common_Datas.Mode.Edit)
            {
                UpdateBookData();
            }

            //Register Method
            else if (mode == Common_Datas.Mode.Add)
            {
                InsertBookData();
            }

            //Delete Function
            else if (mode == Common_Datas.Mode.Delete)
            {
                DeleteBookData();
            }
        }

        private void InsertBookData()
        {
            try
            {
                // Step 1: Insert into the database without the image
                string query = string.Format(@"
                    INSERT INTO Book (BookName, BookDescription, BookAuthor, BookCategory, BookQty, BookPrice) 
                    VALUES ('{0}', '{1}', '{2}', '{3}', {4}, {5});
                    SELECT SCOPE_IDENTITY();",
                    txtBookName.Text,
                    txtBookDescription.Text,
                    HDAuthorID.Value,
                    DDLCategory.SelectedValue,
                    Convert.ToInt32(txtBookQty.Value),
                    Convert.ToDouble(txtBookPrice.Value)
                );

                // Execute the query and retrieve the new BookId
                Con = new Models.Functions();
                object result = Con.GetScalar(query);
                int newBookId = Convert.ToInt32(result);  // Assuming this returns the new BookId

                // Step 2: Save the image to the folder (if there's a valid image in the hidden field)
                if (!string.IsNullOrEmpty(hiddenImagePath.Value))
                {
                    SaveImageFromBase64(hiddenImagePath.Value, newBookId);
                }
                ShowSuccessMessage("Book inserted successfully!");
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                lblError.Visible = true;
            }
        }

        private void UpdateBookData()
        {
            try
            {
                // Get bookId from ViewState
                bookId = Convert.ToInt32(ViewState["BookId"]);

                // Call the function to save the image and get the image path
                string imagePath = SaveImageFromBase64(hiddenImagePath.Value, bookId);

                // Only update the image path if a valid image was uploaded
                string query = string.Format(@"UPDATE Book 
                        SET BookImage = '{1}',
                            BookName = '{2}', 
                            BookDescription = '{3}',
                            BookAuthor = '{4}',
                            BookCategory = '{5}',
                            BookQty = '{6}',
                            BookPrice = '{7}'
                        WHERE BookId = {0}",
                    bookId,
                    imagePath,
                    txtBookName.Text,
                    txtBookDescription.Text,
                    HDAuthorID.Value,
                    DDLCategory.SelectedValue,
                    Convert.ToInt32(txtBookQty.Value),
                    Convert.ToDouble(txtBookPrice.Value)
                );

                // Execute the query to update the database
                Con.SetData(query);

                // Show success message
                ShowSuccessMessage("Book updated successfully!");
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                lblError.Visible = true;
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            CheckError();

            if (errorCd == 0)
            {
                ShowConfirmMessage("Are you sure you want to Update?");
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ShowConfirmMessage("Are you sure you want to Delete?");
        }

        private void DeleteBookData()
        {
            try
            {
                // Get bookId from ViewState
                bookId = Convert.ToInt32(ViewState["BookId"]);

                // Step 1: Retrieve the current image path for the book
                string query = string.Format(@"
                            SELECT BookImage 
                            FROM Book 
                            WHERE BookId = {0}", bookId);

                string imagePath = Con.GetScalar(query)?.ToString(); // Fetch the image path

                // Step 2: Delete the book from the database
                string deleteQuery = string.Format(@"
            DELETE FROM Book 
            WHERE BookId = {0}", bookId);

                Con.SetData(deleteQuery);

                // Step 3: If the image exists, delete the physical file
                if (!string.IsNullOrEmpty(imagePath))
                {
                    string fullPath = Server.MapPath(imagePath); // Get the full path on the server
                    if (File.Exists(fullPath))
                    {
                        File.Delete(fullPath); // Delete the file
                    }
                }

                ShowSuccessMessage("Book deleted successfully!");
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                lblError.Visible = true;
            }
        }

        private string SaveImageFromBase64(string base64Image, int bookId)
        {
            try
            {
                // Step 1: Check if base64 string is valid and starts with image data
                if (!string.IsNullOrEmpty(base64Image) && base64Image.StartsWith("data:image"))
                {
                    // Extract the image type from the base64 string
                    string[] parts = base64Image.Split(',');
                    if (parts.Length < 2)
                    {
                        lblError.Text = "Invalid Base64 image data.";
                        lblError.Visible = true;
                        return string.Empty;
                    }

                    string imageData = parts[1];
                    string imageType = parts[0].Split(';')[0].Split('/')[1]; // Get image type (e.g., png, jpeg, gif)

                    // Validate the Base64 string (it should only contain valid Base-64 characters)
                    if (Common_Functions.IsBase64String(imageData))
                    {
                        string folderPath = Server.MapPath("~/UploadedImages/");

                        // Check if the folder exists, if not, create it
                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }

                        // Use the BookId to generate the image file name with the correct extension
                        string fileName = $"image_{bookId}.{imageType}"; // Use the extracted image type for the file extension
                        string savePath = Path.Combine(folderPath, fileName);

                        // Save the base64 image data as a file
                        byte[] imageBytes = Convert.FromBase64String(imageData);
                        File.WriteAllBytes(savePath, imageBytes);

                        // Step 3: Update the database with the image path
                        string imageUpdateQuery = string.Format(@"
                        UPDATE Book 
                        SET BookImage = '{0}'
                        WHERE BookId = {1}",
                                "~/UploadedImages/" + fileName,
                                bookId
                        );

                        // Execute the update query
                        Con.SetData(imageUpdateQuery);
                        // Return the relative path to be stored in the database
                        return "~/UploadedImages/" + fileName;
                    }
                    else
                    {
                        lblError.Text = "Invalid Base64 string.";
                        lblError.Visible = true;
                        return string.Empty;
                    }
                }

                // If no image was uploaded or it's invalid, return an empty string
                return string.Empty;
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the image saving process
                lblError.Text = "Error saving image: " + ex.Message;
                lblError.Visible = true;
                return string.Empty;
            }
        }
    }
}