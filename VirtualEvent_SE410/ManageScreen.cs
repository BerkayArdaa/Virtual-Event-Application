using System;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;
using System.Linq;

namespace VirtualEvent_SE410
{
    public partial class ManageScreen : Form
    { 
        // Table to hold all event data
        private DataTable eventsTable;

        public ManageScreen()
        {
            InitializeComponent();

            // Trigger Load method when form is shown
            this.Load += ManageScreen_Load;

            // Set default state for optional date filters
            eventStartDatePicker.Value = DateTime.Now;
            eventStartDatePicker.Checked = false;
            deadlineDatePicker.Checked = false;
        }

        private void ManageScreen_Load(object sender, EventArgs e)
        {

            // Setup for DataGridView behavior
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            // Attach double-click event to open edit screen
            dataGridView1.CellDoubleClick += DataGridView1_CellDoubleClick;

            // Load all event data into the grid
            LoadEvents();
        }

        // When a cell is double-clicked, trigger the edit
        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                EditSelectedEvent();
            }
        }

        // Opens edit form for the selected event
        private void EditSelectedEvent()
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an event to edit.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            // Clone the selected row to edit it safely
            DataRowView rowView = (DataRowView)dataGridView1.SelectedRows[0].DataBoundItem;
            DataRow rowToEdit = eventsTable.NewRow();

            foreach (DataColumn col in eventsTable.Columns)
            {
                rowToEdit[col.ColumnName] = rowView[col.ColumnName];
            }
            // Open edit form
            using (var editForm = new EditEventForm(rowToEdit))
            {
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    // Update the original row with edited data
                    foreach (DataColumn col in eventsTable.Columns)
                    {
                        rowView[col.ColumnName] = editForm.EditedRow[col.ColumnName];
                    }

                    UpdateEvent(rowView.Row);
                    LoadEvents();// Refresh table

                    MessageBox.Show("Event updated successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        // Loads all events from the database into DataGridView, including participant user IDs
        private void LoadEvents()
        {
            string solutionRoot = Directory.GetParent(Application.StartupPath).Parent.Parent.Parent.Parent.FullName;
            string dbFilePath = Path.Combine(solutionRoot, "SharedData", "VirtualEvent.db");

            if (!File.Exists(dbFilePath))
            {
                MessageBox.Show("Veritabanı bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var connection = new SQLiteConnection($"Data Source={dbFilePath};Version=3;"))
            {
                connection.Open();

                string query = @"SELECT event_id, title, description, category, start_time, 
                         duration, participant_limit, registration_deadline, created_at
                         FROM Events";

                using (var adapter = new SQLiteDataAdapter(query, connection))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Add Participants column
                    dt.Columns.Add("Participants", typeof(string));

                    foreach (DataRow row in dt.Rows)
                    {
                        int eventId = Convert.ToInt32(row["event_id"]);

                        // Get user_ids of registered participants
                        string participantQuery = "SELECT user_id FROM Registrations WHERE event_id = @eventId";

                        using (var cmd = new SQLiteCommand(participantQuery, connection))
                        {
                            cmd.Parameters.AddWithValue("@eventId", eventId);
                            using (SQLiteDataReader reader = cmd.ExecuteReader())
                            {
                                List<string> userIds = new List<string>();

                                while (reader.Read())
                                {
                                    userIds.Add(reader["user_id"].ToString());
                                }

                                row["Participants"] = string.Join(", ", userIds);
                            }
                        }
                    }

                    eventsTable = dt;
                    dataGridView1.DataSource = eventsTable;
                    dataGridView1.Columns["Participants"].ReadOnly = true;
                }
            }
        }

        // Handles deleting selected event from database and UI
        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete the selected event?",
                                                             "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (dialogResult == DialogResult.Yes)
                {
                    long eventId = Convert.ToInt64(dataGridView1.SelectedRows[0].Cells["event_id"].Value);

                    string solutionRoot = Directory.GetParent(Application.StartupPath).Parent.Parent.Parent.Parent.FullName;
                    string dbFilePath = Path.Combine(solutionRoot, "SharedData", "VirtualEvent.db");

                    if (!File.Exists(dbFilePath))
                    {
                        MessageBox.Show("Veritabanı bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    // Delete event from database
                    using (var connection = new SQLiteConnection($"Data Source={dbFilePath};Version=3;"))
                    {
                        connection.Open();

                        string query = "DELETE FROM Events WHERE event_id = @eventId";

                        using (var command = new SQLiteCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@eventId", eventId);
                            command.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Event deleted successfully!");
                    LoadEvents();
                }
            }
            else
            {
                MessageBox.Show("Please select an event to delete.");
            }
        }
        // Handles manual event editing using the edit button
        private void editButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select an event to edit.", "Information",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                // Get selected event ID
                long selectedEventId = (from DataGridViewRow row in dataGridView1.SelectedRows
                                        select Convert.ToInt64(row.Cells["event_id"].Value)).FirstOrDefault();

                var rowToEdit = (from DataRow row in eventsTable.Rows
                                 where Convert.ToInt64(row["event_id"]) == selectedEventId
                                 select row).FirstOrDefault();

                if (rowToEdit == null)
                {
                    MessageBox.Show("Selected event not found in database.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DataRow editRow = eventsTable.NewRow();
                editRow.ItemArray = rowToEdit.ItemArray;

                using (var editForm = new EditEventForm(editRow))
                {
                    if (editForm.ShowDialog() == DialogResult.OK)
                    {
                        // Update the row with edited values
                        var rowToUpdate = (from DataRow row in eventsTable.Rows
                                           where Convert.ToInt64(row["event_id"]) == selectedEventId
                                           select row).FirstOrDefault();

                        if (rowToUpdate != null)
                        {
                            foreach (DataColumn column in eventsTable.Columns)
                            {
                                rowToUpdate[column] = editForm.EditedRow[column];
                            }

                            UpdateEvent(rowToUpdate);
                            // Optional: Sort events by date after update
                            var sortedEvents = from DataRow row in eventsTable.Rows
                                               orderby Convert.ToDateTime(row["start_time"])
                                               select row;

                            dataGridView1.DataSource = sortedEvents.CopyToDataTable();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error editing event: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Updates an event record in the database using data from a DataRow
        private void UpdateEvent(DataRow row)
        {
            string solutionRoot = Directory.GetParent(Application.StartupPath).Parent.Parent.Parent.Parent.FullName;
            string dbFilePath = Path.Combine(solutionRoot, "SharedData", "VirtualEvent.db");
            if (!File.Exists(dbFilePath))
            {
                MessageBox.Show("Database not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var connection = new SQLiteConnection($"Data Source={dbFilePath};Version=3;"))
            {
                connection.Open();

                string query = @"UPDATE Events 
                        SET title = @title, 
                            description = @description, 
                            category = @category, 
                            start_time = @start_time, 
                            duration = @duration, 
                            participant_limit = @participant_limit, 
                            registration_deadline = @registration_deadline
                        WHERE event_id = @event_id";

                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@title", row["title"]);
                    command.Parameters.AddWithValue("@description", row["description"]);
                    command.Parameters.AddWithValue("@category", row["category"]);
                    command.Parameters.AddWithValue("@start_time", row["start_time"]);
                    command.Parameters.AddWithValue("@duration", row["duration"]);
                    command.Parameters.AddWithValue("@participant_limit", row["participant_limit"]);
                    command.Parameters.AddWithValue("@registration_deadline", row["registration_deadline"]);
                    command.Parameters.AddWithValue("@event_id", row["event_id"]);
                    command.ExecuteNonQuery();
                }
            }
        }
        // Return to main menu
        private void backToMenuButton_Click(object sender, EventArgs e)
        {
            Form2 mainMenu = new Form2();
            mainMenu.Show();
            this.Hide();
        }
        // Filter events by user input (title, category, date, etc.) with LINQ Queries
        // Text fields are trimmed and converted to lowercase for case-insensitive search
        private void filterButton_Click(object sender, EventArgs e)
        {
            if (eventsTable == null || eventsTable.Rows.Count == 0)
            {
                MessageBox.Show("Events table is empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Copies the events table 
            DataTable originalTable = eventsTable.Copy();
            var query = eventsTable.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(titleText.Text))
            {
                string titleFilter = titleText.Text.Trim().ToLower();
                query = query.Where(row =>
                    row["title"] != DBNull.Value &&
                    row.Field<string>("title").ToLower().Contains(titleFilter));
            }

            if (!string.IsNullOrWhiteSpace(categoryText.Text))
            {
                string categoryFilter = categoryText.Text.Trim().ToLower();
                query = query.Where(row =>
                    row["category"] != DBNull.Value &&
                    row.Field<string>("category").ToLower().Contains(categoryFilter));
            }

            if (eventStartDatePicker.Checked)
            {
                DateTime filterDate = eventStartDatePicker.Value.Date;
                query = query.Where(row =>
                    row.Field<DateTime>("start_time").Date == filterDate);
            }

            if (!string.IsNullOrWhiteSpace(durationText.Text) && int.TryParse(durationText.Text, out int duration))
            {
                query = query.Where(row =>
                    row.Field<int>("duration") == duration);
            }

            if (!string.IsNullOrWhiteSpace(slotNumText.Text) && long.TryParse(slotNumText.Text, out long slots))
            {
                query = query.Where(row =>
                    row["participant_limit"] != DBNull.Value &&
                    row.Field<long>("participant_limit") == slots);
            }

            if (deadlineDatePicker.Checked)
            {
                DateTime filterDate = deadlineDatePicker.Value.Date;
                query = query.Where(row =>
                    row["registration_deadline"] != DBNull.Value &&
                    row.Field<DateTime>("registration_deadline").Date == filterDate);
            }
            // Apply filter, add all the filters into a list or show full table if no match
            var filteredList = query.ToList();
            if (filteredList.Any())
            {
                dataGridView1.DataSource = filteredList.CopyToDataTable();
            }
            else
            {
                MessageBox.Show("No events matched your search.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.DataSource = originalTable;
                titleText.Text = "";
                categoryText.Text = "";
                durationText.Text = "";
                slotNumText.Text = "";
                eventStartDatePicker.Checked = false;
                deadlineDatePicker.Checked = false;
            }
        }
        // Reset all filters and reload original data
        private void resetFilterButton_Click(object sender, EventArgs e)
        {
            titleText.Text = "";
            categoryText.Text = "";
            durationText.Text = "";
            slotNumText.Text = "";
            eventStartDatePicker.Checked = false;
            deadlineDatePicker.Checked = false;

            if (eventsTable != null)
            {
                dataGridView1.DataSource = eventsTable;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        // Calculate total available slots for a given category with LINQ Queries
        private void totalFinderButton_Click(object sender, EventArgs e)
        {
            if (eventsTable == null || eventsTable.Rows.Count == 0)
            {
                MessageBox.Show("The Events table is empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string category = textBox1.Text.Trim().ToLower();

            if (string.IsNullOrWhiteSpace(category))
            {
                MessageBox.Show("Please enter a category.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            var matchingRows = eventsTable.AsEnumerable()
                .Where(row =>
                    row["category"] != DBNull.Value &&
                    row["participant_limit"] != DBNull.Value &&
                    row.Field<string>("category").ToLower() == category);

            if (!matchingRows.Any())
            {
                MessageBox.Show("No events found for the specified category. Please make sure you entered it correctly.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var totalSlots = matchingRows.Sum(row => Convert.ToInt64(row["participant_limit"]));

            MessageBox.Show($"Total Slot Count for '{category}': {totalSlots}", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
