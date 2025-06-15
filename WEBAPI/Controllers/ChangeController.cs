using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace WEBAPI.Controllers
{
    // API controller to fetch change history of events from the database
    [Route("api/[controller]")]
    [ApiController]
    public class ChangeController : ControllerBase
    {
        private readonly string _dbPath;         // Path to the SQLite database file
        private readonly string _solutionRoot;   // Root directory of the solution

        // Constructor: sets up the database file path dynamically
        public ChangeController()
        {
            _solutionRoot = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
            _dbPath = Path.Combine(_solutionRoot, "SharedData", "VirtualEvent.db");
        }

        // Model representing a single change record
        public class ChangeModel
        {
            public long EventId { get; set; }         // ID of the event that was changed
            public string Title { get; set; }         // Title of the event
            public string FieldChanged { get; set; }  // Name of the changed field
            public string OldValue { get; set; }      // Value before the change
            public string NewValue { get; set; }      // Value after the change
            public DateTime ChangeTime { get; set; }  // When the change occurred
        }

        // GET: api/change/changed
        // Returns a list of event field changes, ordered by most recent
        [HttpGet("changed")]
        public IActionResult GetChangedEvents()
        {
            // Return 404 if the database file does not exist
            if (!System.IO.File.Exists(_dbPath))
                return NotFound("Veritabanı bulunamadı.");

            // List to store results
            List<ChangeModel> changedEvents = new List<ChangeModel>();

            // Open SQLite connection and execute SQL query
            using (var conn = new SQLiteConnection($"Data Source={_dbPath};Version=3;"))
            {
                conn.Open();

                // Query to join Events and ChangeEvents, and retrieve change logs
                string query = @"
                    SELECT 
                        ce.event_id, 
                        e.title, 
                        ce.field_changed, 
                        ce.old_value, 
                        ce.new_value, 
                        ce.change_time
                    FROM ChangeEvents ce
                    JOIN Events e ON ce.event_id = e.event_id
                    ORDER BY ce.change_time DESC"; // Most recent changes first

                using (var cmd = new SQLiteCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    // Read each record and map it to the ChangeModel
                    while (reader.Read())
                    {
                        changedEvents.Add(new ChangeModel
                        {
                            EventId = reader.GetInt64(0),
                            Title = reader.GetString(1),
                            FieldChanged = reader.GetString(2),
                            OldValue = reader.GetString(3),
                            NewValue = reader.GetString(4),
                            ChangeTime = reader.GetDateTime(5)
                        });
                    }
                }
            }

            // Return the list of changes as a JSON response
            return Ok(changedEvents);
        }
    }
}
