using System;
using System.Data;
using System.Windows.Forms;
using System.ComponentModel;

namespace VirtualEvent_SE410
{
    public partial class EditEventForm : Form
    {
        // This property stores the DataRow that is being edited
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DataRow EditedRow { get; private set; }
        // DataTable to bind to the DataGridView for editing
        private DataTable editTable;

        // Constructor: initializes form, stores original DataRow, and sets up grid and data
        public EditEventForm(DataRow row)
        {
            InitializeComponent();

            EditedRow = row;

            InitializeDataGridView();
            LoadEventData(row);
        }

        // Set up the DataGridView's properties
        private void InitializeDataGridView()
        {
            editDataGridView.AutoGenerateColumns = true;
            editDataGridView.AllowUserToAddRows = false;
            editDataGridView.AllowUserToDeleteRows = false;
            editDataGridView.EditMode = DataGridViewEditMode.EditOnEnter;
            editDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            editDataGridView.CellValidating += EditDataGridView_CellValidating;
        }

        // Load the provided DataRow into a new DataTable and bind it to the grid
        private void LoadEventData(DataRow row)
        {
            try
            {
                editTable = new DataTable();

                foreach (DataColumn column in row.Table.Columns)
                {
                    // Important: Preserve correct data types
                    editTable.Columns.Add(column.ColumnName, column.DataType);
                }

                DataRow newRow = editTable.NewRow();
                foreach (DataColumn column in editTable.Columns)
                {
                    newRow[column.ColumnName] = row[column.ColumnName];
                }

                editTable.Rows.Add(newRow);
                editDataGridView.DataSource = editTable;
                // Make 'event_id' read-only if it exists
                if (editDataGridView.Columns.Contains("event_id"))
                    editDataGridView.Columns["event_id"].ReadOnly = true;

                //Make 'Participants' read-only if it exists
                if (editDataGridView.Columns.Contains("Participants"))
                {
                    editDataGridView.Columns["Participants"].ReadOnly = true;
                }

                // Configure DateTime formatting for relevant columns
                ConfigureDateTimeColumn("start_time");
                ConfigureDateTimeColumn("registration_deadline");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading event data: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }
        
        // Set display and data types for DateTime columns
        private void ConfigureDateTimeColumn(string columnName)
        {
            if (editDataGridView.Columns.Contains(columnName))
            {
                editDataGridView.Columns[columnName].ValueType = typeof(DateTime);
                editDataGridView.Columns[columnName].DefaultCellStyle.Format = "g";
            }
        }
        // Validate cells when editing to ensure correct format and non-empty fields
        private void EditDataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (editDataGridView.Rows[e.RowIndex].IsNewRow ||
                editDataGridView.Columns[e.ColumnIndex].ReadOnly)
                return;

            string columnName = editDataGridView.Columns[e.ColumnIndex].Name;
            string value = e.FormattedValue.ToString().Trim();

            if (string.IsNullOrWhiteSpace(value))
            {
                editDataGridView.Rows[e.RowIndex].ErrorText = "This field cannot be empty";
                e.Cancel = true;
                return;
            }
            // Validate date format
            if (columnName == "start_time" || columnName == "registration_deadline")
            {
                if (!DateTime.TryParse(value, out _))
                {
                    editDataGridView.Rows[e.RowIndex].ErrorText = "Invalid date format";
                    e.Cancel = true;
                }
            }
            // Validate numeric fields
            if (columnName == "duration" || columnName == "participant_limit")
            {
                if (!long.TryParse(value, out _)) // long olarak kontrol et
                {
                    editDataGridView.Rows[e.RowIndex].ErrorText = "Must be a number";
                    e.Cancel = true;
                }
            }
        }

        // Save button: validates input, updates EditedRow, and closes the form
        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                editDataGridView.EndEdit();

                foreach (DataGridViewRow row in editDataGridView.Rows)
                {
                    if (!row.IsNewRow && !string.IsNullOrEmpty(row.ErrorText))
                    {
                        MessageBox.Show("Please fix validation errors before saving.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                if (editTable != null && editTable.Rows.Count > 0)
                {
                    DataRow newValues = editTable.Rows[0];
                    long eventId = Convert.ToInt64(EditedRow["event_id"]);
                    // Prevent saving if there are validation errors
                    foreach (DataColumn column in editTable.Columns)
                    {
                        string columnName = column.ColumnName;
                        object oldVal = EditedRow[columnName];
                        object newVal = newValues[columnName];

                        if (!Equals(oldVal, newVal))
                        {
                            // Log the change in the database
                            SaveChangeToDatabase(eventId, columnName, oldVal?.ToString(), newVal?.ToString());
                        }

                        // Update the original DataRow with new value
                        EditedRow[columnName] = newVal;
                    }
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving changes: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Saves the field change to a ChangeEvents table in the database
        private void SaveChangeToDatabase(long eventId, string field, string oldVal, string newVal)
        {
            try
            {
                // Relative path to the shared database
                string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\SharedData\VirtualEvent.db");
                string connStr = $"Data Source={dbPath};Version=3;";

                using (var conn = new System.Data.SQLite.SQLiteConnection(connStr))
                {
                    conn.Open();
                    string insertQuery = @"
                INSERT INTO ChangeEvents (event_id, field_changed, old_value, new_value)
                VALUES (@eventId, @field, @oldVal, @newVal)";

                    using (var cmd = new System.Data.SQLite.SQLiteCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@eventId", eventId);
                        cmd.Parameters.AddWithValue("@field", field);
                        cmd.Parameters.AddWithValue("@oldVal", oldVal);
                        cmd.Parameters.AddWithValue("@newVal", newVal);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Change logging failed: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }





        // Alias click handler for save button
        private void saveButton_Click_1(object sender, EventArgs e)
        {
            saveButton_Click(sender, e);
        }
        // Cancel button: discard changes and close form
        private void cancelButton_Click_1(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
