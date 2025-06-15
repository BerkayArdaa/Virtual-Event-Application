using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VirtualEventWEB
{
    // Web Forms page for displaying, filtering, and registering for events
    public partial class Events : System.Web.UI.Page
    {
        private List<EventModel> _allEvents = new List<EventModel>();

        // Page load: checks session and loads events if first load

        protected async void Page_Load(object sender, EventArgs e)
        {
            // Redirect to login if session is invalid
            if (Session["Username"] == null)
            {
                Response.Redirect("Login.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
                return;
            }

            if (!IsPostBack)
            {
                await LoadAndDisplayEvents();
            }
        }
        // Loads events from API and binds them to the grid
        private async Task LoadAndDisplayEvents()
        {
            await LoadEventsFromApi();

            if (_allEvents != null && _allEvents.Any())
            {
                EventsGrid.DataSource = _allEvents;
                EventsGrid.DataBind();
                lblMessage.Text = $"Showing all {_allEvents.Count} events";
                lblMessage.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lblMessage.Text = "No events available";
                lblMessage.ForeColor = System.Drawing.Color.Orange;
                EventsGrid.DataSource = null;
                EventsGrid.DataBind();
            }
        }

        // Checkbox toggles start date filter field
        protected void chkStartDate_CheckedChanged(object sender, EventArgs e)
        {
            txtStartDate.Enabled = chkStartDate.Checked;
            if (!chkStartDate.Checked)
            {
                txtStartDate.Text = "";
            }
        }
        // Applies filters and displays matching results
        protected async void btnFilter_Click(object sender, EventArgs e)
        {
            try
            {
                // First reload all events from API to get fresh data
                await LoadEventsFromApi();

                if (_allEvents == null || !_allEvents.Any())
                {
                    lblMessage.Text = "Events list is empty!";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                var filteredEvents = _allEvents.AsEnumerable();

                // Title filter
                if (!string.IsNullOrWhiteSpace(txtTitle.Text))
                {
                    string titleFilter = txtTitle.Text.Trim().ToLower();
                    filteredEvents = filteredEvents.Where(ev =>
                        ev.Title != null &&
                        ev.Title.ToLower().Contains(titleFilter));
                }

                // Category filter
                if (!string.IsNullOrWhiteSpace(txtCategory.Text))
                {
                    string categoryFilter = txtCategory.Text.Trim().ToLower();
                    filteredEvents = filteredEvents.Where(ev =>
                        ev.Category != null &&
                        ev.Category.ToLower().Contains(categoryFilter));
                }

                // Start Date filter
                if (chkStartDate.Checked && DateTime.TryParse(txtStartDate.Text, out DateTime startDate))
                {
                    filteredEvents = filteredEvents.Where(ev =>
                        ev.StartTime.Date == startDate.Date);
                }

                // Duration filter
                if (!string.IsNullOrWhiteSpace(txtDuration.Text) && int.TryParse(txtDuration.Text, out int duration))
                {
                    filteredEvents = filteredEvents.Where(ev => ev.Duration == duration);
                }

                // Participant Limit filter
                if (!string.IsNullOrWhiteSpace(txtSlots.Text) && long.TryParse(txtSlots.Text, out long slots))
                {
                    filteredEvents = filteredEvents.Where(ev => ev.ParticipantLimit == slots);
                }

                // Apply the filter
                var result = filteredEvents.ToList();
                EventsGrid.DataSource = result;
                EventsGrid.DataBind();

                lblMessage.Text = result.Any()
                    ? $"Showing {result.Count} events"
                    : "No events matched your search criteria.";
                lblMessage.ForeColor = result.Any()
                    ? System.Drawing.Color.Green
                    : System.Drawing.Color.Orange;
            }
            catch (Exception ex)
            {
                lblMessage.Text = $"Error applying filters: {ex.Message}";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected async void btnReset_Click(object sender, EventArgs e)
        {
            // Clear all filters
            txtTitle.Text = "";
            txtCategory.Text = "";
            txtDuration.Text = "";
            txtSlots.Text = "";
            chkStartDate.Checked = false;
            txtStartDate.Text = "";
            txtStartDate.Enabled = false;

            // Reload all events from API
            await LoadEventsFromApi();

            if (_allEvents != null && _allEvents.Any())
            {
                EventsGrid.DataSource = _allEvents;
                EventsGrid.DataBind();
                lblMessage.Text = $"Showing all {_allEvents.Count} events";
                lblMessage.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lblMessage.Text = "No events available";
                lblMessage.ForeColor = System.Drawing.Color.Orange;
            }
        }
        protected void btnNotifications_Click(object sender, EventArgs e)
        {
            Response.Redirect("Notification.aspx");
        }
        protected void btnChangeCounts_Click(object sender, EventArgs e)
        {
            Response.Redirect("ChangeNotification.aspx");
        }
        // Loads events from the API endpoint
        private async Task LoadEventsFromApi()
        {
            string apiUrl = "https://localhost:44393/api/events";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync(apiUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<RootResponse>(jsonString);
                        _allEvents = result?.Events ?? new List<EventModel>();
                    }
                    else
                    {
                        lblMessage.Text = $"API access failed: {response.StatusCode}";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        _allEvents = new List<EventModel>();
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Text = $"Error loading events: {ex.Message}";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    _allEvents = new List<EventModel>();
                }
            }
        }

        protected void EventsGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Register")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                long selectedEventId = Convert.ToInt64(EventsGrid.DataKeys[rowIndex].Value);

                // ❗ userId kaldırıldı çünkü artık API kendisi Username üzerinden bulacak
                RegisterUserToEvent(selectedEventId);
            }
            if (e.CommandName == "Info")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                long selectedEventId = Convert.ToInt64(EventsGrid.DataKeys[rowIndex].Value);

                // EventId'yi Session'a at
                Session["SelectedEventId"] = selectedEventId;

                // Info sayfasına yönlendir
                Response.Redirect("EventInfo.aspx");
            }
        }
        // Registers user to an event using their session-based username
        private async void RegisterUserToEvent(long eventId)
        {
            using (HttpClient client = new HttpClient())
            {
                string username = Session["Username"]?.ToString();

                if (string.IsNullOrEmpty(username))
                {
                    lblMessage.Text = "Kullanıcı oturumu geçersiz.";
                    return;
                }

                client.DefaultRequestHeaders.Add("Username", username);

                var request = new
                {
                    EventId = eventId
                };

                string apiUrl = "https://localhost:44393/api/registration/register";
                var content = new StringContent(JsonConvert.SerializeObject(request), System.Text.Encoding.UTF8, "application/json");

                try
                {
                    var response = await client.PostAsync(apiUrl, content);
                    if (response.IsSuccessStatusCode)
                    {
                        lblMessage.Text = "Etkinliğe başarıyla kayıt olundu.";
                        lblMessage.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        string msg = await response.Content.ReadAsStringAsync();
                        lblMessage.Text = $"Kayıt başarısız: {msg}";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Text = $"API hatası: {ex.Message}";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
        protected void btnMyEvents_Click(object sender, EventArgs e)
        {
            Response.Redirect("MyRegistrations.aspx");
        }

        // Root response structure for API
        public class RootResponse
        {
            public string Message { get; set; }
            public string SolutionPath { get; set; }
            public string DatabasePath { get; set; }
            public List<EventModel> Events { get; set; }
        }
        // Event structure used in API communication
        public class EventModel
        {
            public long EventId { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string Category { get; set; }
            public DateTime StartTime { get; set; }
            public int Duration { get; set; }
            public long ParticipantLimit { get; set; }
            public DateTime RegistrationDeadline { get; set; }
            public DateTime CreatedAt { get; set; }
        }
    }
}
