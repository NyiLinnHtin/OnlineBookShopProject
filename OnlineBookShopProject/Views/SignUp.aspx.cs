using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Net.Mail;
using System.Web.UI.WebControls;
using System.Net;

namespace OnlineBookShopProject.Views
{
    public partial class SignUp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            divPassword.Visible = false;
            divConfirmPassword.Visible = false;
            divSignUp.Visible = false;
        }

        protected void Code_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();

            if (IsValidEmail(email))
            {
                // Generate a 6-digit code
                string code = GenerateCode();

                // Save the code in ViewState or Session for verification later
                ViewState["VerificationCode"] = code;

                // Send the code to the user's email
                SendVerificationCode(email, code);

                // Show password fields and signup button
                divPassword.Visible = true;
                divConfirmPassword.Visible = true;
                divSignUp.Visible = true;
            }
            else
            {
                lblErrorMessage.Text = "Please enter a valid email address.";
                lblErrorMessage.Visible = true;
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                MailAddress m = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private string GenerateCode()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString();
        }

        public void SendVerificationCode(string email, string code)
        {
            try
            {
                Console.WriteLine("Starting email sending process...");
                var fromAddress = new MailAddress("alphanyilinhtin@gmail.com", "Nyi Linn Htin");
                var toAddress = new MailAddress(email);
                const string fromPassword = "mbwntroqntmgxmow"; // App Password from Google
                const string subject = "Verification Code";
                string body = $"Your verification code is: {code}";

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };

                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    Console.WriteLine("Attempting to send email...");
                    smtp.Send(message);
                    Console.WriteLine("Email sent successfully!");
                }
            }
            catch (SmtpException smtpEx)
            {
                Console.WriteLine("SMTP error: " + smtpEx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("General error: " + ex.Message);
            }
        }


    }
}