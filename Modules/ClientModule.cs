using Nancy;
using Nancy.ModelBinding;
using ClientManagementAPI.Models;
using ClientManagementAPI.Repositories;

namespace ClientManagementAPI.Modules
{
    public class ClientModule : NancyModule
    {
        private readonly ClientRepository _repository;

        public ClientModule() : base("/client")
        {
            _repository = new ClientRepository();

            Get("/", _ => GetAllClients());
            Get("/{id:int}", parameters => GetClientById(parameters.id));
            Post("/", _ => AddClient());
            Put("/{id:int}", parameters => UpdateClient(parameters.id));
            Delete("/{id:int}", parameters => DeleteClient(parameters.id));
            Get("/{id:int}/history", parameters => GetClientActionLogs(parameters.id));
        }

        private object GetAllClients()
        {
            var clients = _repository.GetAllClients();
            return Response.AsJson(clients);
        }

        private object GetClientById(int id)
        {
            var client = _repository.GetClientById(id);
            return client != null ? Response.AsJson(client) : HttpStatusCode.NotFound;
        }

        private object AddClient()
        {
            var client = this.Bind<Client>();
            _repository.AddClient(client);
            return HttpStatusCode.Created;
        }

        private object UpdateClient(int id)
        {
            var client = this.Bind<Client>();
            client.Id = id;
            _repository.UpdateClient(client);
            return Response.AsJson(new { message = "Client updated successfully" }, HttpStatusCode.OK);
        }

        private object DeleteClient(int id)
        {
            _repository.DeleteClient(id);
            return HttpStatusCode.NoContent;
        }

        private object GetClientActionLogs(int id)
        {
            var logs = _repository.GetClientActionLogs(id);
            return Response.AsJson(logs);
        }
    }
}
