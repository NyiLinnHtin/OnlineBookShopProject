using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace OnlineBookShopProject.Views.User.CheckOut
{
    public partial class CheckOut_User : System.Web.UI.Page
    {
        int userId;
        Models.Functions Con;
        protected void Page_Load(object sender, EventArgs e)
        {
            Con = new Models.Functions();
            userId = Convert.ToInt32(Session["UserId"].ToString());

            if (!IsPostBack)
            {
                BindOrderSummary();
            }
        }

        private void BindOrderSummary()
        {
            // Assuming you have a method to get the order summary data (e.g., from CheckOut or Cart)
            string query = string.Format(@"
                    SELECT 
                        ROW_NUMBER() OVER(ORDER BY b.BookId) AS Number,
                        b.BookName,
                        c.Qty,
                        FORMAT(b.BookPrice, 'N2') AS BookPrice
                    FROM 
                        CheckOut c
                    JOIN 
                        Book b ON c.BookId = b.BookId
                    WHERE 
                        c.UserId = '{0}'", userId);

            // Replace this with your method to fetch data from the database
            DataTable dt = Con.GetData(query);

            // Add a "TotalAmount" column to the DataTable (Qty * BookPrice)
            dt.Columns.Add("TotalAmount", typeof(decimal));
            decimal totalAmount = 0.00m;
            foreach (DataRow row in dt.Rows)
            {
                decimal qty = Convert.ToDecimal(row["Qty"]);
                decimal price = Convert.ToDecimal(row["BookPrice"]);
                decimal rowTotalAmount = qty * price;

                row["TotalAmount"] = rowTotalAmount;
                totalAmount += rowTotalAmount;
            }

            // Store the DataTable in ViewState for later use
            ViewState["OrderSummaryData"] = dt;

            rptOrderSummary.DataSource = dt;
            rptOrderSummary.DataBind();

            foreach (DataRow row in dt.Rows)
            {
                totalAmount += Convert.ToDecimal(row["TotalAmount"]);
            }

            // Set the total amount label
            lblTotalAmount.Text = totalAmount.ToString("N2");
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            // Retrieve the DataTable from ViewState
            DataTable dt = ViewState["OrderSummaryData"] as DataTable;

            if (dt != null)
            {
                GenerateOrderSummaryPDF(dt);
            }
            else
            {
                // Handle case where the data is not available
                Response.Write("Order data is not available.");
            }
        }

        private void GenerateOrderSummaryPDF(DataTable dt)
        {
            // Get current datetime to create the filename
            string fileName = $"order_{DateTime.Now:yyyyMMddHHmmss}.pdf";

            // Create a document object
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 20f, 20f);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Bind the PDF writer
                PdfWriter.GetInstance(pdfDoc, memoryStream);
                pdfDoc.Open();

                // Add a title to the document
                Font titleFont = FontFactory.GetFont("Arial", 18, Font.BOLD);
                Paragraph title = new Paragraph("Order Summary", titleFont);
                title.Alignment = Element.ALIGN_CENTER;
                pdfDoc.Add(title);

                // Add space
                pdfDoc.Add(new Paragraph("\n"));

                // Table to display order summary
                PdfPTable table = new PdfPTable(3); // 3 columns: Item Name, Qty, Total
                table.WidthPercentage = 100;

                // Define the column widths
                float[] widths = { 40f, 10f, 20f };
                table.SetWidths(widths);

                // Add headers
                Font headerFont = FontFactory.GetFont("Arial", 14, Font.BOLD);
                PdfPCell cell;
                // For "Item" header
                cell = new PdfPCell(new Phrase("Item", headerFont));
                cell.PaddingTop = 10f; // Top padding to increase the height
                cell.PaddingBottom = 10f; // Bottom padding to increase the height
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell);

                // For "Qty" header
                cell = new PdfPCell(new Phrase("Qty", headerFont));
                cell.PaddingTop = 10f;
                cell.PaddingBottom = 10f;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell);

                // For "Total" header
                cell = new PdfPCell(new Phrase("Total", headerFont));
                cell.PaddingTop = 10f;
                cell.PaddingBottom = 10f;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell);

                // Add data rows(body cells)
                foreach (DataRow row in dt.Rows)
                {
                    // Body: "Item"
                    cell = new PdfPCell(new Phrase(row["BookName"].ToString()));
                    cell.PaddingTop = 8f; // Increase height for body
                    cell.PaddingBottom = 8f;
                    cell.HorizontalAlignment = Element.ALIGN_LEFT; // Align Item column text to the left
                    table.AddCell(cell);

                    // Body: "Qty"
                    cell = new PdfPCell(new Phrase(row["Qty"].ToString()));
                    cell.PaddingTop = 8f;
                    cell.PaddingBottom = 8f;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER; // Center-align the "Qty" column text
                    table.AddCell(cell);

                    // Body: "Total"
                    cell = new PdfPCell(new Phrase("$" + Convert.ToDecimal(row["TotalAmount"]).ToString("N2")));
                    cell.PaddingTop = 8f;
                    cell.PaddingBottom = 8f;
                    cell.HorizontalAlignment = Element.ALIGN_RIGHT; // Right-align the "Total" column text
                    table.AddCell(cell);
                }

                // Add table to PDF document
                pdfDoc.Add(table);

                // Add total and shipping
                pdfDoc.Add(new Paragraph("\n"));
                pdfDoc.Add(new Paragraph($"Shipping: $5.00"));
                decimal totalAmount = Convert.ToDecimal(lblTotalAmount.Text);
                pdfDoc.Add(new Paragraph($"Total Amount: ${totalAmount + 5.00m:N2}", titleFont));

                // Close document
                pdfDoc.Close();

                // Initiate PDF download
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", $"attachment;filename={fileName}");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.BinaryWrite(memoryStream.ToArray());
                Response.End();
            }
        }
    }
}