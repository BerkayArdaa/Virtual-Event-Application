using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VirtualEventWEB
{
    // This page displays upcoming event notifications (e.g., events happening tomorrow)
    public partial class Notification : Page
    {
        // Called when the page is loaded
        protected void Page_Load(object sender, EventArgs e)
        {
            // Load notifications only on initial load, not on postbacks (e.g., button clicks)
            if (!IsPostBack)
            {
                // Check if the user is logged in via session
                if (Session["Username"] == null)
                {
                    Response.Redirect("Login.aspx");
                    return;
                }

                // Register asynchronous task to load notifications
                RegisterAsyncTask(new PageAsyncTask(LoadNotifications));
            }
        }

        // Async method that calls the API and displays upcoming notifications
        private async Task LoadNotifications()
        {
            string apiUrl = "https://localhost:44393/api/notification/upcoming";
            string username = Session["Username"]?.ToString(); // Get username from session

            using (HttpClient client = new HttpClient())
            {
                // Attach username to request headers (for user-specific notifications)
                client.DefaultRequestHeaders.Add("Username", username);

                try
                {
                    // Send GET request to fetch upcoming event notifications
                    var response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        // Read and deserialize the response
                        string json = await response.Content.ReadAsStringAsync();
                        var notifications = JsonConvert.DeserializeObject<List<NotificationModel>>(json);

                        // Loop through notifications and dynamically generate labels to show them
                        foreach (var note in notifications)
                        {
                            var label = new Label
                            {
                                Text = $"<div class='card'><b>{note.Title}</b><br/>Starts at: {note.StartTime}</div>",
                                EnableViewState = false
                            };

                            pnlNotifications.Controls.Add(label); // Add to panel on the page
                        }

                        // Show a message if there are no notifications
                        if (notifications.Count == 0)
                        {
                            pnlNotifications.Controls.Add(new Literal
                            {
                                Text = "<i>No upcoming events for tomorrow.</i>"
                            });
                        }
                    }
                    else
                    {
                        // If API call fails, show error message
                        pnlNotifications.Controls.Add(new Literal
                        {
                            Text = "<span style='color:red'>Error loading notifications.</span>"
                        });
                    }
                }
                catch (Exception ex)
                {
                    // Show exception message if the call crashes
                    pnlNotifications.Controls.Add(new Literal
                    {
                        Text = $"<span style='color:red'>Hata: {ex.Message}</span>"
                    });
                }
            }
        }

        // Represents the model received from the API for notifications
        public class NotificationModel
        {
            public string Title { get; set; }         // Title of the event
            public DateTime StartTime { get; set; }   // Start time of the event
        }
    }
}
