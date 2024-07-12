using System.Data.SQLite;
using Dapper;

namespace ClientManagementAPI.Data
{
    public class ClientDbContext
    {
        private readonly string _connectionString = "Data Source=clientmanagement.db";

        public ClientDbContext()
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Execute(@"
                    CREATE TABLE IF NOT EXISTS Clients (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Name TEXT NOT NULL,
                        Age INTEGER NOT NULL,
                        Comment TEXT
                    );

                    CREATE TABLE IF NOT EXISTS ClientActionLogs (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        ClientId INTEGER NOT NULL,
                        Action TEXT NOT NULL,
                        Timestamp DATETIME NOT NULL,
                        FOREIGN KEY (ClientId) REFERENCES Clients(Id)
                    );
                ");
            }
        }

        public SQLiteConnection CreateConnection() => new SQLiteConnection(_connectionString);
    }
}
