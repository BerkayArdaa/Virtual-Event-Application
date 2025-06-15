using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

namespace VirtualEventWEB
{
    // Page to display the list of events the user has registered for
    public partial class MyRegistrations : System.Web.UI.Page
    {
        // Runs when the page loads
        protected void Page_Load(object sender, EventArgs e)
        {
            // If user is not logged in, redirect to Login page
            if (Session["Username"] == null)
            {
                Response.Redirect("Login.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
                return;
            }

            // On initial load, fetch user's registered events
            if (!IsPostBack)
            {
                RegisterAsyncTask(new PageAsyncTask(LoadMyEventsAsync));
            }
        }

        // Async method to load the events the user is registered for (used on page load and refresh)
        private async Task LoadMyEventsAsync()
        {
            string username = Session["Username"].ToString();
            string apiUrl = $"https://localhost:44393/api/registration/myregistrations";

            using (HttpClient client = new HttpClient())
            {
                // Add username to request header for identification
                client.DefaultRequestHeaders.Add("Username", username);

                try
                {
                    // Send GET request to retrieve user-specific registrations
                    var response = await client.GetAsync(apiUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var events = JsonConvert.DeserializeObject<List<EventModel>>(json);

                        // Bind data to the grid
                        RegisteredEventsGrid.DataSource = events;
                        RegisteredEventsGrid.DataBind();
                    }
                    else
                    {
                        lblMessage.Text = "Etkinlikler alınamadı.";
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "Hata: " + ex.Message;
                }
            }
        }

        //  Duplicate of LoadMyEventsAsync (optional: consider removing it if unused)
        private async void LoadMyEvents()
        {
            string username = Session["Username"].ToString();
            string apiUrl = $"https://localhost:44393/api/registration/myregistrations";

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Username", username);

                try
                {
                    var response = await client.GetAsync(apiUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var events = JsonConvert.DeserializeObject<List<EventModel>>(json);
                        RegisteredEventsGrid.DataSource = events;
                        RegisteredEventsGrid.DataBind();
                    }
                    else
                    {
                        lblMessage.Text = "Etkinlikler alınamadı.";
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "Hata: " + ex.Message;
                }
            }
        }

        // Called when user clicks "Back" button – navigates to Events list
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Events.aspx");
        }

        // Data model to represent a registered event
        public class EventModel
        {
            public long EventId { get; set; }
            public string Title { get; set; }
            public string Category { get; set; }
            public DateTime StartTime { get; set; }
        }

        // Handles commands from each row (e.g., "Unregister" button click)
        protected async void RegisteredEventsGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Unregister")
            {
                // Get the row index from the button command
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                long eventId = Convert.ToInt64(RegisteredEventsGrid.DataKeys[rowIndex].Value);

                // Call API to unregister user from the event
                await UnregisterFromEvent(eventId);

                // Refresh event list after unregistering
                await LoadMyEventsAsync();
            }
        }

        // Sends DELETE request to unregister user from the selected event
        private async Task UnregisterFromEvent(long eventId)
        {
            string username = Session["Username"].ToString();
            string apiUrl = $"https://localhost:44393/api/registration/unregister/{eventId}";

            using (HttpClient client = new HttpClient())
            {
                // Add username to identify user
                client.DefaultRequestHeaders.Add("Username", username);

                try
                {
                    // Send DELETE request to unregister
                    var response = await client.DeleteAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        lblMessage.Text = "Successfully unregistered from the event.";
                    }
                    else
                    {
                        string error = await response.Content.ReadAsStringAsync();
                        lblMessage.Text = $"Error: {error}";
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Text = $"Error: {ex.Message}";
                }
            }
        }
    }
}
