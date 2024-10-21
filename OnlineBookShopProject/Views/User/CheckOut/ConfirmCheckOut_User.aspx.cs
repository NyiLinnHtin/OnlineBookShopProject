using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineBookShopProject.Views.User.CheckOut
{
    public partial class ConfirmCheckOut_User : System.Web.UI.Page
    {
        int userId;
        Models.Functions Con;
        protected void Page_Load(object sender, EventArgs e)
        {
            Con = new Models.Functions();
            userId = Convert.ToInt32(Session["UserId"].ToString());
            if (!IsPostBack)
            {
                ShowAllBooks();
                UpdateFinalTotalAmount();
            }
        }

        private void ShowAllBooks()
        {
            string query = string.Format(@"
                            SELECT 
                                ROW_NUMBER() OVER(ORDER BY b.BookId) AS Number,
                                b.BookId,
                                b.BookName,
                                c.Qty,
                                FORMAT(b.BookQty, 'N0') AS BookQty,    -- No decimal places, comma-separated
                                FORMAT(b.BookPrice, 'N2') AS BookPrice -- Two decimal places, comma-separated
                            FROM 
                                CheckOut c
                            JOIN 
                                Book b ON c.BookId = b.BookId
                            WHERE 
                                c.UserId = '{0}'", userId);

            DataTable dt = Con.GetData(query);

            // Add a "TotalAmount" column to the DataTable (Qty * BookPrice)
            dt.Columns.Add("TotalAmount", typeof(decimal));
            foreach (DataRow row in dt.Rows)
            {
                decimal qty = Convert.ToDecimal(row["Qty"]);
                decimal price = Convert.ToDecimal(row["BookPrice"]);
                row["TotalAmount"] = qty * price;
            }

            // Store the DataTable in session to manage virtual quantities
            Session["BookData"] = dt;

            // Bind the data to GridView
            if (dt == null || dt.Rows.Count == 0)
            {
                lblNoListDisplay.Visible = true;
                gridBooks.DataSource = null;
            }
            else
            {
                lblNoListDisplay.Visible = false;
                gridBooks.DataSource = dt;
            }

            gridBooks.DataBind();
        }

        protected void gridBooks_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "IncreaseQty" || e.CommandName == "DecreaseQty")
            {
                // Get the DataTable from the session
                DataTable dt = Session["BookData"] as DataTable;

                if (dt != null)
                {
                    // Get the book ID from CommandArgument
                    int bookId = Convert.ToInt32(e.CommandArgument);

                    // Find the row in the DataTable where BookId matches
                    DataRow row = dt.Select($"BookId = {bookId}").FirstOrDefault();

                    if (row != null)
                    {
                        // Get the current quantity
                        int currentQty = Convert.ToInt32(row["Qty"]);
                        int maxQty = Convert.ToInt32(row["BookQty"]);
                        decimal price = Convert.ToDecimal(row["BookPrice"]);

                        // Modify the quantity based on button clicked
                        if (e.CommandName == "IncreaseQty" && currentQty < maxQty)
                        {
                            currentQty++;
                        }
                        else if (e.CommandName == "DecreaseQty" && currentQty > 1)
                        {
                            currentQty--;
                        }

                        // Update the quantity in the DataTable
                        row["Qty"] = currentQty;

                        // Update the total amount for the row (Qty * Price)
                        row["TotalAmount"] = currentQty * price;

                        // Rebind the GridView with the updated DataTable
                        gridBooks.DataSource = dt;
                        gridBooks.DataBind();

                        // Update the session with the modified DataTable
                        Session["BookData"] = dt;

                        // Update the final total amount after quantity change
                        UpdateFinalTotalAmount();
                    }
                }
            }
        }

        // Method to calculate and display the final total amount
        private void UpdateFinalTotalAmount()
        {
            DataTable dt = Session["BookData"] as DataTable;
            if (dt != null)
            {
                decimal finalTotal = 0;

                // Sum the "TotalAmount" column to get the final total
                foreach (DataRow row in dt.Rows)
                {
                    finalTotal += Convert.ToDecimal(row["TotalAmount"]);
                }

                // Display the final total amount
                lblFinalTotal.Text = finalTotal.ToString("N2");
            }
        }

        protected void btnOrderNow_Click(object sender, EventArgs e)
        {
            Response.Redirect($"CheckOut_User.aspx");
        }
    }
}