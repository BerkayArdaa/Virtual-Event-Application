using System.Data.SQLite;
using System.Text;
using System.IO;

namespace VirtualEvent_SE410
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            string solutionRoot = Directory.GetParent(Application.StartupPath).Parent.Parent.Parent.Parent.FullName;
            string dbDir = Path.Combine(solutionRoot, "SharedData");
            AppDomain.CurrentDomain.SetData("DataDirectory", dbDir);


            string fullPath = Path.Combine(dbDir, "VirtualEvent.db");
           // MessageBox.Show("Veritabanı yolu:\n" + fullPath);
        }




        // Login button: verifies user credentials and role from the database
        private void button1_Click(object sender, EventArgs e)
        {
            string dbFilePath = Path.Combine(AppDomain.CurrentDomain.GetData("DataDirectory").ToString(), "VirtualEvent.db");

            if (!File.Exists(dbFilePath))
            {
                MessageBox.Show("Veritabanı bulunamadı! Yolu:\n" + dbFilePath, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var connection = new SQLiteConnection("Data Source=|DataDirectory|\\VirtualEvent.db;Version=3;"))
            {
                connection.Open();
                string email = userMailText.Text.Trim();
                string password = passwordText.Text.Trim();

                string query = "SELECT role FROM Users WHERE email = @Email AND password = @Password";

                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", password);

                    var result = command.ExecuteScalar();

                    if (result != null)
                    {
                        string role = result.ToString();
                        if (role == "admin" || role == "staff")
                        {
                            Form2 f2 = new Form2();
                            f2.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("İzniniz bulunmamaktadır. Sadece Admin veya Staff giriş yapabilir.", "Yetkisiz Giriş", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Kullanıcı adı veya şifre yanlış!", "Hatalı Giriş", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }





        private void label1_Click(object sender, EventArgs e)
        {

        }

        // Button to show all table names in the SQLite database
        private void button2_Click(object sender, EventArgs e)
        {
            string dbFilePath = Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..\..\SharedData\VirtualEvent.db"));



            if (!File.Exists(dbFilePath))
            {
                MessageBox.Show("Veritabanı bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var connection = new SQLiteConnection($"Data Source={dbFilePath};Version=3;"))
            {
                connection.Open();
                // Get all table names from SQLite master table
                var command = new SQLiteCommand("SELECT name FROM sqlite_master WHERE type='table';", connection);

                using (var reader = command.ExecuteReader())
                {
                    string tables = "";
                    while (reader.Read())
                    {
                        tables += reader["name"].ToString() + "\n";
                    }

                    if (string.IsNullOrWhiteSpace(tables))
                    {
                        MessageBox.Show("Hiç tablo bulunamadı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Tablolar:\n" + tables, "Veritabanı Tabloları", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void passwordText_TextChanged(object sender, EventArgs e)
        {

        }

        private void userNameL_Click(object sender, EventArgs e)
        {

        }
    }
}
