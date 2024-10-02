using System;
using OnlineBookShopProject.Assets.Common;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace OnlineBookShopProject.Views.Admin.Categories
{
    public partial class CategoryDetail : System.Web.UI.Page
    {
        Models.Functions Con;
        int categoryId, errorCd;
        string mode;
        string encryptedMode, encryptedCategoryId;

        protected void Page_Load(object sender, EventArgs e)
        {
            Con = new Models.Functions();
            if (!IsPostBack)
            {
                encryptedMode = Request.QueryString["Mode"];

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
                        txtCategory.Enabled = false;
                        txtDescription.Disabled = true;
                    }
                    else
                    {
                        btnRegister.Visible = false;
                        btnUpdate.Visible = false;
                        btnDelete.Visible = false;

                        DisplayValues();
                        txtCategory.Enabled = false;
                        txtDescription.Disabled = true;
                    }
                }
                else
                {
                    lblError.Text = "Invalid or missing parameters.";
                    lblError.Visible = true;
                }
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("CategoryLists.aspx");
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            CheckError();

            if (errorCd == 0)
            {
                ShowConfirmMessage("Are you sure you want to register new Category?");
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ShowConfirmMessage($"Are you sure you want to Delete [{txtCategory.Text}] Category?");
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

        protected void ShowConfirmMessage(string message)
        {
            lblConfirmContents.Text = message;
            confirmationModal.Style["display"] = "block";
        }


        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            CheckError();

            if (errorCd == 0)
            {
                ShowConfirmMessage("Are you sure you want to Update?");
            }
        }

        private void DisplayValues()
        {
            encryptedCategoryId = Request.QueryString["CategoryId"];
            categoryId = Convert.ToInt32(Common_Functions.Decrypt(encryptedCategoryId));
            ViewState["CategoryId"] = categoryId;
            string Query = $"SELECT * FROM Category where CategoryId = {categoryId}";

            DataTable dt = Con.GetData(Query);

            if (dt.Rows.Count > 0)
            {
                txtCategory.Text = dt.Rows[0]["Category"].ToString();
                txtDescription.Value = dt.Rows[0]["CategoryDescription"].ToString();
            }
            else
            {
                lblError.Text = "No data found for the given category.";
                lblError.Visible = true;
            }
        }

        private void CheckError()
        {
            errorCd = 0;
            if (string.IsNullOrEmpty(txtCategory.Text))
            {
                txtCategory.CssClass = "form-control border-error";
                lblError.Text = "Please Fill all Required Fields";
                lblError.Visible = true;
                errorCd = 1;
            }
            else
            {
                txtCategory.CssClass = "form-control border-black";
                lblError.Visible = false;
            }

        }

        private void UpdateData()
        {
            mode = ViewState["Mode"].ToString();
            //Update Function
            if (mode == Common_Datas.Mode.Edit)
            {
                try
                {
                    categoryId = Convert.ToInt32(ViewState["CategoryId"]);
                    string Query = $"UPDATE Category SET Category = '{txtCategory.Text}', CategoryDescription = '{txtDescription.Value}' WHERE CategoryId = {categoryId}";
                    Con.SetData(Query);

                    ShowSuccessMessage("Category updated successfully!");
                }
                catch (Exception ex)
                {
                    lblError.Text = ex.Message;
                    lblError.Visible = true;
                }

            }

            //Register Method
            else if (mode == Common_Datas.Mode.Add)
            {
                try
                {
                    string Query = $"insert into Category values('{txtCategory.Text}','{txtDescription.Value}')";
                    Con = new Models.Functions();
                    Con.SetData(Query);

                    ShowSuccessMessage("Category registered successfully!");
                }
                catch (Exception ex)
                {
                    lblError.Text = ex.Message;
                    lblError.Visible = true;
                }
            }

            //Delete Function
            else if (mode == Common_Datas.Mode.Delete)
            {
                try
                {
                    categoryId = Convert.ToInt32(ViewState["CategoryId"]);
                    string Query = $"DELETE FROM Category WHERE CategoryId = {categoryId}";
                    Con = new Models.Functions();
                    Con.SetData(Query);

                    ShowSuccessMessage("Category deleted successfully!");
                }
                catch (Exception ex)
                {
                    lblError.Text = ex.Message;
                    lblError.Visible = true;
                }
            }
        }
    }
}