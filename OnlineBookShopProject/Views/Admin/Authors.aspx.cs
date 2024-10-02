using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace OnlineBookShopProject.Views.Admin
{
    public partial class Authors : System.Web.UI.Page
    {
        Models.Functions Con;
        protected void Page_Load(object sender, EventArgs e)
        {
            Con = new Models.Functions();
            ShowAllAuthors();
            if (!IsPostBack)
            {
                BindCountries();
            }
        }

        private void ShowAllAuthors()
        {
            string Query = "Select * from Author";
            gridAuthors.DataSource = Con.GetData(Query);
            gridAuthors.DataBind();
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {

        }

        private async void BindCountries()
        {
            try
            {
                string apiUrl = "https://restcountries.com/v3.1/all";

                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(apiUrl);
                    response.EnsureSuccessStatusCode();

                    var jsonString = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("API Response: " + jsonString); // Check the API response

                    // Deserialize directly into a list of dynamic objects
                    var countries = JsonConvert.DeserializeObject<List<dynamic>>(jsonString);

                    // Create a list for country names and sort them alphabetically
                    var countryNames = countries
                        .Select(c => new { Name = c.name.common.ToString() })
                        .OrderBy(c => c.Name) // Sort alphabetically
                        .ToList();

                    // Bind the list to the dropdown
                    ddlAuthorCountry.DataSource = countryNames;
                    ddlAuthorCountry.DataTextField = "Name"; // Use the property directly
                    ddlAuthorCountry.DataValueField = ""; // Set to empty string
                    ddlAuthorCountry.DataBind();
                    ddlAuthorCountry.Items.Insert(0, new ListItem("-- Select Country --", ""));
                }
            }
            catch (Exception ex)
            {
                // Handle errors gracefully
                Console.WriteLine("Error fetching countries: " + ex.Message);
            }
        }


    }
}
