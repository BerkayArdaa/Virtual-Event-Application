    using Newtonsoft.Json;
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.UI;

    namespace VirtualEventWEB
    {
    // Web Forms page for displaying detailed information about a selected event
    public partial class EventInfo : System.Web.UI.Page
        {

            protected void Page_Load(object sender, EventArgs e)
            {
                if (!IsPostBack && Session["SelectedEventId"] != null)
                {
                    long eventId = Convert.ToInt64(Session["SelectedEventId"]);

                    RegisterAsyncTask(new PageAsyncTask(() => LoadEventInfoFromApi(eventId)));
                }
            }

        // Asynchronous method to fetch event info from the API
        private async Task LoadEventInfoFromApi(long eventId)
            {
                string apiUrl = $"https://localhost:44393/api/events/info/{eventId}";

                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        var response = await client.GetAsync(apiUrl);
                        if (response.IsSuccessStatusCode)
                        {
                            var json = await response.Content.ReadAsStringAsync();
                            dynamic data = JsonConvert.DeserializeObject(json);

                            string description = data.description;
                            string slotsInfo = data.availableSlots != null
                                ? $"Available Slots: {data.availableSlots}"
                                : "Available Slots: Unlimited";

                            lblDescription.Text = $"{description}<br /><br /><b>{slotsInfo}</b>";
                        }
                        else
                        {
                            lblDescription.Text = "Etkinlik bilgisi getirilemedi.";
                        }
                    }
                    catch (Exception ex)
                    {
                        lblDescription.Text = $"Hata: {ex.Message}";
                    }
                }
            }


        // Handles the back button click event and redirects to the Events page
        protected void btnBack_Click(object sender, EventArgs e)
            {
                Response.Redirect("Events.aspx");
            }
        }
    }
