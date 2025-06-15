using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace VirtualEventAPI.Controllers
{
    // Controller for sending upcoming event notifications to users
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly string _dbPath;         // Path to SQLite database file
        private readonly string _solutionRoot;   // Root directory of the solution

        // Constructor to initialize paths
        public NotificationController()
        {
            _solutionRoot = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
            _dbPath = Path.Combine(_solutionRoot, "SharedData", "VirtualEvent.db");
        }

        // GET: api/notification/upcoming
        // Returns a list of events that are 1 day away for the current user
        [HttpGet("upcoming")]
        public IActionResult GetUpcomingNotifications()
        {
            // Ensure the database file exists
            if (!System.IO.File.Exists(_dbPath))
                return NotFound("Database not found.");

            // Retrieve the username from the request header
            string username = Request.Headers["Username"].ToString();

            if (string.IsNullOrEmpty(username))
                return BadRequest("Username header is missing.");

            List<NotificationModel> notifications = new List<NotificationModel>();

            using (var conn = new SQLiteConnection($"Data Source={_dbPath};Version=3;"))
            {
                conn.Open();

                // Retrieve user ID by username
                var userCmd = new SQLiteCommand("SELECT user_id FROM Users WHERE username = @username", conn);
                userCmd.Parameters.AddWithValue("@username", username);
                var userIdObj = userCmd.ExecuteScalar();

                if (userIdObj == null)
                    return NotFound("User not found.");

                long userId = Convert.ToInt64(userIdObj);

                // Retrieve events the user is registered for
                var cmd = new SQLiteCommand(@"
                    SELECT e.title, e.start_time
                    FROM Events e
                    JOIN Registrations r ON e.event_id = r.event_id
                    WHERE r.user_id = @user_id", conn);

                cmd.Parameters.AddWithValue("@user_id", userId);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DateTime startTime = reader.GetDateTime(1);
                        TimeSpan timeRemaining = startTime.Date - DateTime.Now.Date;

                        // If the event is exactly 1 day away, add it to the notification list
                        if (timeRemaining.TotalDays == 1)
                        {
                            notifications.Add(new NotificationModel
                            {
                                Title = reader.GetString(0),
                                StartTime = startTime
                            });
                        }
                    }
                }
            }

            // Return the list of upcoming event notifications
            return Ok(notifications);
        }

        // Model representing a notification item
        public class NotificationModel
        {
            public string Title { get; set; }         // Event title
            public DateTime StartTime { get; set; }   // Event start time
        }
    }
}
