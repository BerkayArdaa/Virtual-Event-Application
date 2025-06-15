using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.UI;

namespace VirtualEventWEB
{
    // This Web Forms page displays changes made to events via an API call
    public partial class ChangeNotification : Page
    {
        // This method is called when the page loads
        protected void Page_Load(object sender, EventArgs e)
        {
            // Only load data on the initial load (not on postbacks like button clicks)
            if (!IsPostBack)
            {
                // Register and execute the asynchronous task for fetching change data
                RegisterAsyncTask(new PageAsyncTask(LoadChanges));
            }
        }

        // Handles the click event for the "Back" button, redirects to Events page
        protected void btnBack_Click(object sender, EventArgs e)
        {
            // Change this path if you want to redirect elsewhere
            Response.Redirect("Events.aspx"); // nereye döneceksen o sayfa
        }

        // Asynchronously fetches changed event data from the API and binds it to the grid
        private async Task LoadChanges()
        {
            string url = "https://localhost:44393/api/change/changed";

            using (var client = new HttpClient())
            {
                try
                {
                    // Send GET request to API
                    var response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        // Read the JSON response
                        var json = await response.Content.ReadAsStringAsync();
                        // Deserialize JSON into a list of ChangeModel objects
                        var changes = JsonConvert.DeserializeObject<List<ChangeModel>>(json);
                        // If we received a non-empty list, bind it to the grid
                        if (changes != null && changes.Count > 0)
                        {
                            ChangeGrid.DataSource = changes;
                            ChangeGrid.DataBind();
                        }
                        else
                        {
                            Response.Write("<b>Boş veya eşleşmeyen liste geldi.</b>");
                        }
                    }
                    else
                    {
                        Response.Write("<b>API erişimi başarısız: " + response.StatusCode + "</b>");
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("<b>Hata: " + ex.Message + "</b>");
                }
            }
        }

        // Model that maps to the structure of the JSON response from the change API
        public class ChangeModel
        {
            [JsonProperty("eventId")]
            public long EventId { get; set; }

            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("fieldChanged")]
            public string FieldChanged { get; set; }

            [JsonProperty("oldValue")]
            public string OldValue { get; set; }

            [JsonProperty("newValue")]
            public string NewValue { get; set; }

            [JsonProperty("changeTime")]
            public DateTime ChangeTime { get; set; }
        }



    }
}
