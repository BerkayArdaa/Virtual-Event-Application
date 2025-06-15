using System;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Web.UI;

namespace VirtualEventWEB
{
    public partial class EditEventForm : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                long eventId;
                if (long.TryParse(Request.QueryString["eventId"], out eventId))
                {
                    LoadChangeLogs(eventId);
                }
                else
                {
                    Response.Write("Invalid event ID.");
                }
            }
        }

        private void LoadChangeLogs(long eventId)
        {
            try
            {
                string dbPath = Path.Combine(Server.MapPath("~/App_Data"), "VirtualEvent.db");
                using (var conn = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
                {
                    conn.Open();
                    string query = @"SELECT column_changed AS 'Field Changed', 
                                            old_value AS 'Old Value', 
                                            new_value AS 'New Value', 
                                            changed_at AS 'Changed At' 
                                     FROM EventChangeLogs 
                                     WHERE event_id = @eventId 
                                     ORDER BY changed_at DESC";

                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@eventId", eventId);
                        SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        GridViewChanges.DataSource = dt;
                        GridViewChanges.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error loading change logs: " + ex.Message);
            }
        }
    }
}
