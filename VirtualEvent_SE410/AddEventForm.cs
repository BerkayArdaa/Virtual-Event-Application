using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;


namespace VirtualEvent_SE410
{
    public partial class AddEventForm : Form
    {
        // Define a static event
        public static event EventAddedHandler OnEventAdded;

        // Constructor initializes the form and subscribes to the OnEventAdded event
        public AddEventForm()
        {
            InitializeComponent();
            OnEventAdded += AddEventForm_OnEventAdded;
        }

        // This method is triggered
        private void AddEventForm_OnEventAdded(Event newEvent)
        {
            MessageBox.Show($"New event '{newEvent.Title}' added successfully!");
        }

        // Triggered when the "Add Event" button is clicked
        private void addEventButton_Click(object sender, EventArgs e)
        {
            //None should be empty
            if (string.IsNullOrWhiteSpace(titleText.Text) ||
                string.IsNullOrWhiteSpace(descriptionBox.Text) ||
                string.IsNullOrWhiteSpace(categoryText.Text) ||
                string.IsNullOrWhiteSpace(slotText.Text) ||
                string.IsNullOrWhiteSpace(durationText.Text)) // duration boş mu kontrolü
            {
                MessageBox.Show("Invalid attempt! Some fields are empty.");
                return;
            }

            // Validate slot input
            if (!int.TryParse(slotText.Text, out int slots))
            {
                MessageBox.Show("Invalid attempt! Slot number must be a valid number.");
                return;
            }

            // Validate duration input
            if (!int.TryParse(durationText.Text, out int duration))
            {
                MessageBox.Show("Invalid attempt! Duration must be a valid number (in minutes).");
                return;
            }

            if (duration <= 0)
            {
                MessageBox.Show("Invalid attempt! Duration must be greater than 0.");
                return;
            }

            // Combine date and time from two DateTimePickers
            DateTime selectedDate = timePicker.Value.Date;
            DateTime selectedTime = time2Picker.Value;

            DateTime startTime = new DateTime(
                selectedDate.Year, selectedDate.Month, selectedDate.Day,
                selectedTime.Hour, selectedTime.Minute, selectedTime.Second
            );
            // Combine deadline date and time
            DateTime selectedDeadlineDate = deadlinePicker.Value.Date;
            DateTime selectedDeadlineTime = deadline2Picker.Value;

            DateTime registrationDeadline = new DateTime(
                selectedDeadlineDate.Year, selectedDeadlineDate.Month, selectedDeadlineDate.Day,
                selectedDeadlineTime.Hour, selectedDeadlineTime.Minute, selectedDeadlineTime.Second
            );

            // Check the registration deadline is before the event start date
            if (registrationDeadline.Date >= startTime.Date)
            {
                MessageBox.Show("Invalid attempt! Registration deadline must be before the event date.");
                return;
            }
            // Create a new Event object with user-provided details
            Event newEvent = new Event
            {
                Title = titleText.Text.Trim(),
                Description = descriptionBox.Text.Trim(),
                Category = categoryText.Text.Trim(),
                StartTime = startTime,
                RegistrationDeadline = registrationDeadline,
                AvailableSlots = slots,
                Duration = duration
            };

            // Database connection  
            // Database connection  
            string solutionRoot = Directory.GetParent(Application.StartupPath).Parent.Parent.Parent.Parent.FullName;
            string dbFilePath = Path.Combine(solutionRoot, "SharedData", "VirtualEvent.db");
            string connectionString = $"Data Source={dbFilePath};Version=3;";
            // Insert the new event into the Events table in SQLite database
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                string query = @"INSERT INTO Events 
                (title, description, category, start_time, participant_limit, registration_deadline, created_at, duration)
                VALUES 
                (@title, @description, @category, @start_time, @participant_limit, @registration_deadline, @created_at, @duration);";

                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    // Set parameters for SQL command
                    cmd.Parameters.AddWithValue("@title", newEvent.Title);
                    cmd.Parameters.AddWithValue("@description", newEvent.Description);
                    cmd.Parameters.AddWithValue("@category", newEvent.Category);
                    cmd.Parameters.AddWithValue("@start_time", newEvent.StartTime);
                    cmd.Parameters.AddWithValue("@participant_limit", newEvent.AvailableSlots);
                    cmd.Parameters.AddWithValue("@registration_deadline", newEvent.RegistrationDeadline);
                    cmd.Parameters.AddWithValue("@created_at", DateTime.Now);
                    cmd.Parameters.AddWithValue("@duration", newEvent.Duration);

                    cmd.ExecuteNonQuery();
                }
            }

            // Notify subscribers that a new event was added
            OnEventAdded?.Invoke(newEvent);

            // Navigate to Form2
            Form2 f2 = new Form2();
            f2.Show();
            this.Hide();
        }

        private void descriptionBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
