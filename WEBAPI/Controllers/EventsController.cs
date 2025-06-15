using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace VirtualEventAPI.Controllers
{
    // API controller responsible for retrieving event-related data
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly string _dbPath;         // Full path to the SQLite database
        private readonly string _solutionRoot;   // Root directory of the solution

        // Constructor initializes file path to the database
        public EventsController()
        {
            _solutionRoot = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
            _dbPath = Path.Combine(_solutionRoot, "SharedData", "VirtualEvent.db");
        }

        // GET: api/events/description/{eventId}
        // Returns only the description of a specific event by ID
        [HttpGet("description/{eventId}")]
        public IActionResult GetEventDescription(long eventId)
        {
            if (!System.IO.File.Exists(_dbPath))
                return NotFound("Database not found.");

            using (var conn = new SQLiteConnection($"Data Source={_dbPath};Version=3;"))
            {
                conn.Open();
                var cmd = new SQLiteCommand("SELECT description FROM Events WHERE event_id = @event_id", conn);
                cmd.Parameters.AddWithValue("@event_id", eventId);
                var result = cmd.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                    return Ok(new { description = result.ToString() });
                else
                    return NotFound("Description not found.");
            }
        }

        // GET: api/events/info/{eventId}
        // Returns detailed information including available slots for a given event
        [HttpGet("info/{eventId}")]
        public IActionResult GetEventInfo(long eventId)
        {
            if (!System.IO.File.Exists(_dbPath))
                return NotFound("Database not found.");

            using (var conn = new SQLiteConnection($"Data Source={_dbPath};Version=3;"))
            {
                conn.Open();

                var cmd = new SQLiteCommand(@"
                    SELECT 
                        e.description,
                        e.participant_limit,
                        (SELECT COUNT(*) FROM Registrations WHERE event_id = e.event_id) as current_registrations
                    FROM Events e
                    WHERE e.event_id = @event_id", conn);

                cmd.Parameters.AddWithValue("@event_id", eventId);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string description = reader.IsDBNull(0) ? "No description" : reader.GetString(0);
                        long participantLimit = reader.IsDBNull(1) ? 0 : reader.GetInt64(1);
                        long currentRegistrations = reader.IsDBNull(2) ? 0 : reader.GetInt64(2);
                        long availableSlots = participantLimit > 0 ? (participantLimit - currentRegistrations) : -1;

                        return Ok(new
                        {
                            description,
                            availableSlots = availableSlots >= 0 ? availableSlots : (long?)null
                        });
                    }
                    else
                    {
                        return NotFound("Event not found.");
                    }
                }
            }
        }

        // GET: api/events
        // Returns a list of all events from the database
        [HttpGet]
        public IActionResult GetEvents()
        {
            if (!System.IO.File.Exists(_dbPath))
            {
                return NotFound($"Database not found.\nSolution path: {_solutionRoot}");
            }

            List<EventModel> events = new List<EventModel>();

            using (var conn = new SQLiteConnection($"Data Source={_dbPath};Version=3;"))
            {
                conn.Open();
                string query = @"
                    SELECT event_id, title, description, category, start_time, 
                           duration, participant_limit, registration_deadline, created_at 
                    FROM Events";

                using (var cmd = new SQLiteCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        events.Add(new EventModel
                        {
                            EventId = reader.GetInt64(0),
                            Title = reader.GetString(1),
                            Description = reader.GetString(2),
                            Category = reader.GetString(3),
                            StartTime = reader.GetDateTime(4),
                            Duration = reader.GetInt32(5),
                            ParticipantLimit = reader.GetInt64(6),
                            RegistrationDeadline = reader.GetDateTime(7),
                            CreatedAt = reader.GetDateTime(8)
                        });
                    }
                }
            }

            return Ok(new
            {
                Message = "Events retrieved successfully.",
                SolutionPath = _solutionRoot,
                DatabasePath = _dbPath,
                Events = events
            });
        }
    }

    // Data transfer model for events
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
