using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace VirtualEventWEB
{
    // Login page that authenticates users via Web API and stores their username in session
    public partial class Login : Page
    {
        // Triggered when the "Login" button is clicked
        protected async void btnLogin_Click(object sender, EventArgs e)
        {
            // Initialize HttpClient for sending HTTP requests
            using (var client = new HttpClient())
            {
                // Prepare the login data from user input
                var loginData = new
                {
                    Username = txtUsername.Text.Trim(),
                    Password = txtPassword.Text.Trim()
                };

                // Serialize login data to JSON
                string json = JsonConvert.SerializeObject(loginData);

                // Create the request content with proper headers
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Get base API URL from web.config (AppSettings)
                string apiUrl = ConfigurationManager.AppSettings["ApiBaseUrl"];

                // Send POST request to the API login endpoint
                var response = await client.PostAsync($"{apiUrl}/api/auth/login", content);

                // Debugging output (optional for local testing)
                Console.WriteLine(response);
                Console.WriteLine(response.IsSuccessStatusCode);

                // If login is successful
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();

                    // Store the username in session
                    Session["Username"] = txtUsername.Text;

                    // Redirect to Events page (do not terminate the current request immediately)
                    Response.Redirect("Events.aspx", false);
                    Context.ApplicationInstance.CompleteRequest(); // Prevent further processing
                }
                else
                {
                    // Login failed - show API response message
                    var result = await response.Content.ReadAsStringAsync();
                    lblMessage.Text = $"Login failed. {response}";
                }
            }
        }
    }
}
