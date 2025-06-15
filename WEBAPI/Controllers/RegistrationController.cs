using Microsoft.AspNetCore.Mvc;
using System;
using System.Data.SQLite;
using System.IO;
using System.Collections.Generic;

namespace VirtualEventAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly string _dbPath;
        private readonly string _solutionRoot;

        // Constructor to set database path
        public RegistrationController()
        {
            _solutionRoot = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
            _dbPath = Path.Combine(_solutionRoot, "SharedData", "VirtualEvent.db");
        }

        // DTO for registration request
        public class RegisterRequest
        {
            public long EventId { get; set; }
        }

        // POST: api/registration/register
        // Registers a user for a specific event after checking availability and time conflicts
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequest request)
        {
            if (!System.IO.File.Exists(_dbPath))
                return NotFound($"Database not found.\nSolution path: {_solutionRoot}");

            try
            {
                using (var conn = new SQLiteConnection($"Data Source={_dbPath};Version=3;"))
                {
                    conn.Open();

                    // Get username from request header
                    string username = Request.Headers["Username"].ToString();
                    if (string.IsNullOrEmpty(username))
                        return BadRequest("Username header is missing.");

                    // Get user ID from database
                    var userCmd = new SQLiteCommand("SELECT user_id FROM Users WHERE username = @username", conn);
                    userCmd.Parameters.AddWithValue("@username", username);
                    var userIdObj = userCmd.ExecuteScalar();

                    if (userIdObj == null)
                        return NotFound("User not found.");

                    long userId = Convert.ToInt64(userIdObj);

                    // Get the new event's time and capacity
                    var eventTimeCmd = new SQLiteCommand(@"
                        SELECT start_time, duration, participant_limit
                        FROM Events
                        WHERE event_id = @event_id", conn);
                    eventTimeCmd.Parameters.AddWithValue("@event_id", request.EventId);

                    DateTime newStartTime;
                    int newDuration;
                    long participantLimit = 0;

                    using (var reader = eventTimeCmd.ExecuteReader())
                    {
                        if (!reader.Read())
                            return NotFound("Event not found.");

                        newStartTime = reader.GetDateTime(0);
                        newDuration = reader.GetInt32(1);
                        if (!reader.IsDBNull(2))
                            participantLimit = reader.GetInt64(2);
                    }

                    DateTime newEndTime = newStartTime.AddMinutes(newDuration);

                    // Check for time conflicts with existing registrations
                    var conflictCmd = new SQLiteCommand(@"
                        SELECT e.title, e.start_time, e.duration
                        FROM Events e
                        JOIN Registrations r ON e.event_id = r.event_id
                        WHERE r.user_id = @user_id", conn);
                    conflictCmd.Parameters.AddWithValue("@user_id", userId);

                    using (var reader = conflictCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string existingTitle = reader.GetString(0);
                            DateTime existingStart = reader.GetDateTime(1);
                            int existingDuration = reader.GetInt32(2);
                            DateTime existingEnd = existingStart.AddMinutes(existingDuration);

                            // Check for time overlap
                            if (newStartTime < existingEnd && existingStart < newEndTime)
                            {
                                return BadRequest($"Time conflict with event '{existingTitle}'.");
                            }
                        }
                    }

                    // Check if participant limit has been reached
                    if (participantLimit > 0)
                    {
                        var countCmd = new SQLiteCommand("SELECT COUNT(*) FROM Registrations WHERE event_id = @event_id", conn);
                        countCmd.Parameters.AddWithValue("@event_id", request.EventId);
                        long currentRegistrations = (long)(countCmd.ExecuteScalar() ?? 0);

                        if (currentRegistrations >= participantLimit)
                            return BadRequest("This event has reached its participant limit.");
                    }

                    // Perform the registration
                    var insertCmd = new SQLiteCommand(@"
                        INSERT INTO Registrations (user_id, event_id, registration_time, status)
                        VALUES (@user_id, @event_id, @registration_time, @status)", conn);
                    insertCmd.Parameters.AddWithValue("@user_id", userId);
                    insertCmd.Parameters.AddWithValue("@event_id", request.EventId);
                    insertCmd.Parameters.AddWithValue("@registration_time", DateTime.Now);
                    insertCmd.Parameters.AddWithValue("@status", "registered");

                    int affected = insertCmd.ExecuteNonQuery();

                    if (affected > 0)
                        return Ok(new { message = "Registration successful." });
                    else
                        return BadRequest("Registration failed.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Server error: {ex.Message}");
            }
        }

        // GET: api/registration/myregistrations
        // Returns a list of events the current user is registered for
        [HttpGet("myregistrations")]
        public IActionResult GetMyRegistrations()
        {
            if (!System.IO.File.Exists(_dbPath))
                return NotFound("Database not found.");

            string username = Request.Headers["Username"].ToString();
            if (string.IsNullOrEmpty(username))
                return BadRequest("Username header is missing.");

            List<EventModel> events = new List<EventModel>();

            using (var conn = new SQLiteConnection($"Data Source={_dbPath};Version=3;"))
            {
                conn.Open();

                // Get user ID
                var userCmd = new SQLiteCommand("SELECT user_id FROM Users WHERE username = @username", conn);
                userCmd.Parameters.AddWithValue("@username", username);
                var userIdObj = userCmd.ExecuteScalar();

                if (userIdObj == null)
                    return NotFound("User not found.");

                long userId = Convert.ToInt64(userIdObj);

                // Get events the user is registered for
                var cmd = new SQLiteCommand(@"
                    SELECT e.event_id, e.title, e.category, e.start_time
                    FROM Events e
                    JOIN Registrations r ON e.event_id = r.event_id
                    WHERE r.user_id = @user_id", conn);

                cmd.Parameters.AddWithValue("@user_id", userId);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        events.Add(new EventModel
                        {
                            EventId = reader.GetInt64(0),
                            Title = reader.GetString(1),
                            Category = reader.GetString(2),
                            StartTime = reader.GetDateTime(3)
                        });
                    }
                }
            }

            return Ok(events);
        }

        // DELETE: api/registration/unregister/{eventId}
        // Removes a user's registration for a specific event
        [HttpDelete("unregister/{eventId}")]
        public IActionResult Unregister(long eventId)
        {
            if (!System.IO.File.Exists(_dbPath))
                return NotFound("Database not found.");

            string username = Request.Headers["Username"].ToString();
            if (string.IsNullOrEmpty(username))
                return BadRequest("Username header is missing.");

            try
            {
                using (var conn = new SQLiteConnection($"Data Source={_dbPath};Version=3;"))
                {
                    conn.Open();

                    // Get user ID
                    var userCmd = new SQLiteCommand("SELECT user_id FROM Users WHERE username = @username", conn);
                    userCmd.Parameters.AddWithValue("@username", username);
                    var userIdObj = userCmd.ExecuteScalar();

                    if (userIdObj == null)
                        return NotFound("User not found.");

                    long userId = Convert.ToInt64(userIdObj);

                    // Perform unregistration
                    var cmd = new SQLiteCommand(
                        "DELETE FROM Registrations WHERE user_id = @user_id AND event_id = @event_id", conn);
                    cmd.Parameters.AddWithValue("@user_id", userId);
                    cmd.Parameters.AddWithValue("@event_id", eventId);

                    int affected = cmd.ExecuteNonQuery();

                    if (affected > 0)
                        return Ok(new { message = "Successfully unregistered." });
                    else
                        return NotFound("Registration not found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Server error: {ex.Message}");
            }
        }

        // Data transfer model for returning registration events
        public class EventModel
        {
            public long EventId { get; set; }
            public string Title { get; set; }
            public string Category { get; set; }
            public DateTime StartTime { get; set; }
        }
    }
}
