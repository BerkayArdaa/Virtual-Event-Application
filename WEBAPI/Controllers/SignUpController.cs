using Microsoft.AspNetCore.Mvc;
using System.Data.SQLite;
using System.IO;

namespace VirtualEventAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignUpController : ControllerBase
    {
        private readonly string _dbPath;
        private readonly string _solutionRoot;

        public SignUpController()
        {
            _solutionRoot = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
            _dbPath = Path.Combine(_solutionRoot, "SharedData", "VirtualEvent.db");
        }

        // Request model from client
        public class SignUpRequest
        {
            public string Username { get; set; }
            public string Password { get; set; }
            public string Email { get; set; }
        }

        [HttpPost]
        public IActionResult Register([FromBody] SignUpRequest request)
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(request.Username) ||
                string.IsNullOrWhiteSpace(request.Password) ||
                string.IsNullOrWhiteSpace(request.Email))
            {
                return BadRequest("All fields are required.");
            }

            // Force default role
            string role = "attendee";

            // Check database exists
            if (!System.IO.File.Exists(_dbPath))
                return NotFound("Database not found.");

            // Connect to SQLite DB
            using (var conn = new SQLiteConnection($"Data Source={_dbPath};Version=3;"))
            {
                conn.Open();

                // Check for duplicate username or email
                using (var check = new SQLiteCommand("SELECT COUNT(*) FROM Users WHERE username = @username OR email = @email", conn))
                {
                    check.Parameters.AddWithValue("@username", request.Username);
                    check.Parameters.AddWithValue("@email", request.Email);
                    long count = (long)check.ExecuteScalar();

                    if (count > 0)
                        return Conflict("Username or email already exists.");
                }

                // Insert new user with plain text password
                using (var insert = new SQLiteCommand("INSERT INTO Users (username, password, role, email) VALUES (@username, @password, @role, @email)", conn))
                {
                    insert.Parameters.AddWithValue("@username", request.Username);
                    insert.Parameters.AddWithValue("@password", request.Password); // plain text (⚠️ not recommended for real apps)
                    insert.Parameters.AddWithValue("@role", role);
                    insert.Parameters.AddWithValue("@email", request.Email);
                    insert.ExecuteNonQuery();
                }

                return Ok("User registered successfully.");
            }
        }
    }
}
