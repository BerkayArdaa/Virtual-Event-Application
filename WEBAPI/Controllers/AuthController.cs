using Microsoft.AspNetCore.Mvc;
using System.Data.SQLite;
using System.IO;
using System.Threading.Tasks;

namespace WEBAPI.Controllers
{
    // API Controller for authentication operations (login endpoint)
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly string _dbPath;         // Path to SQLite database
        private readonly string _solutionRoot;   // Root path of the project solution

        // Constructor sets up the database file path relative to the solution root
        public AuthController()
        {
            _solutionRoot = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
            _dbPath = Path.Combine(_solutionRoot, "SharedData", "VirtualEvent.db");
        }

        // DTO for login requests
        public class LoginDto
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        // Represents a user record in the system
        public class UserModel
        {
            public int UserId { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
            public string Role { get; set; }
            public string Email { get; set; }
            public System.DateTime CreatedAt { get; set; }
        }

        // POST: api/auth/login
        // Authenticates user credentials against the database
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto request)
        {
            // Basic input validation
            if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
                return BadRequest("Username and password are required.");

            UserModel user = null;

            // Manually connect to the SQLite database and run SQL query
            using (var conn = new SQLiteConnection($"Data Source={_dbPath};Version=3;"))
            {
                conn.Open();

                // Query to match username and password
                string query = @"
                    SELECT user_id, username, password, role, email, created_at 
                    FROM Users 
                    WHERE username = @username AND password = @password";

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    // Use parameterized queries to prevent SQL injection
                    cmd.Parameters.AddWithValue("@username", request.Username);
                    cmd.Parameters.AddWithValue("@password", request.Password); // 🔒 Insecure: consider hashing in production

                    // Execute query and check if any matching user exists
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            user = new UserModel
                            {
                                UserId = reader.GetInt32(0),
                                Username = reader.GetString(1),
                                Password = reader.GetString(2),
                                Role = reader.GetString(3),
                                Email = reader.GetString(4),
                                CreatedAt = reader.GetDateTime(5)
                            };
                        }
                    }
                }
            }

            // If no user found, return 401 Unauthorized
            if (user == null)
                return Unauthorized("Invalid credentials.");

            // Return a minimal user object (no password)
            return Ok(new
            {
                user_id = user.UserId,
                username = user.Username,
                role = user.Role,
                email = user.Email,
                created_at = user.CreatedAt
            });
        }
    }
}
