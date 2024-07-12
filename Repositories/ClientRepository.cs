using System;
using System.Collections.Generic;
using Dapper;
using ClientManagementAPI.Data;
using ClientManagementAPI.Models;

namespace ClientManagementAPI.Repositories
{
    public class ClientRepository
    {
        private readonly ClientDbContext _dbContext;

        public ClientRepository()
        {
            _dbContext = new ClientDbContext();
        }

        public IEnumerable<Client> GetAllClients()
        {
            using (var connection = _dbContext.CreateConnection())
            {
                return connection.Query<Client>("SELECT * FROM Clients");
            }
        }

        public Client GetClientById(int id)
        {
            using (var connection = _dbContext.CreateConnection())
            {
                return connection.QuerySingleOrDefault<Client>("SELECT * FROM Clients WHERE Id = @Id", new { Id = id });
            }
        }

        public void AddClient(Client client)
        {
            using (var connection = _dbContext.CreateConnection())
            {
                var id = connection.QuerySingle<int>(
                    "INSERT INTO Clients (Name, Age, Comment) VALUES (@Name, @Age, @Comment); SELECT last_insert_rowid();",
                    client);
                LogAction(id, "Registered");
            }
        }

        public void UpdateClient(Client client)
        {
            using (var connection = _dbContext.CreateConnection())
            {
                connection.Execute("UPDATE Clients SET Name = @Name, Age = @Age, Comment = @Comment WHERE Id = @Id", client);
                LogAction(client.Id, "Edited");
            }
        }

        public void DeleteClient(int id)
        {
            using (var connection = _dbContext.CreateConnection())
            {
                connection.Execute("DELETE FROM Clients WHERE Id = @Id", new { Id = id });
                LogAction(id, "Deleted");
            }
        }

        public IEnumerable<ClientActionLog> GetClientActionLogs(int clientId)
        {
            using (var connection = _dbContext.CreateConnection())
            {
                return connection.Query<ClientActionLog>("SELECT * FROM ClientActionLogs WHERE ClientId = @ClientId", new { ClientId = clientId });
            }
        }

        private void LogAction(int clientId, string action)
        {
            using (var connection = _dbContext.CreateConnection())
            {
                connection.Execute("INSERT INTO ClientActionLogs (ClientId, Action, Timestamp) VALUES (@ClientId, @Action, @Timestamp)", new
                {
                    ClientId = clientId,
                    Action = action,
                    Timestamp = DateTime.Now
                });
            }
        }
    }
}
