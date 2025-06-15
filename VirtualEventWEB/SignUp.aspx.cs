using System;
using System.Net.Http;
using System.Text;
using System.Web.UI;
using Newtonsoft.Json;
using System.Configuration;

namespace VirtualEventWEB
{
    // Sign-up page to allow new users to register by sending data to the API
    public partial class SignUp : Page
    {
        // Triggered when the "Sign Up" button is clicked
        protected async void btnSignUp_Click(object sender, EventArgs e)
        {
            // Get user input from the form
            string username = txtUsername.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text;

            // Create a request body object
            var signUpData = new
            {
                Username = username,
                Password = password,
                Email = email
            };

            // Convert the object to a JSON string
            string json = JsonConvert.SerializeObject(signUpData);

            // Prepare HTTP content with appropriate content type
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Get the API base URL from Web.config (AppSettings)
            string apiUrl = ConfigurationManager.AppSettings["ApiBaseUrl"]; // Ensure this key exists

            // Initialize HttpClient and send the POST request
            using (var client = new HttpClient())
            {
                try
                {
                    // Send sign-up data to the API endpoint
                    var response = await client.PostAsync($"{apiUrl}/api/signup", content);
                    var result = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        // On success, redirect to Login page
                        Response.Redirect("Login.aspx");
                    }
                    else
                    {
                        // Show error message if sign-up fails
                        lblMessage.Text = $"Sign-up failed: {result}";
                    }
                }
                catch (Exception ex)
                {
                    // Display exception message if something goes wrong
                    lblMessage.Text = $"Error: {ex.Message}";
                }
            }
        }
    }
}
