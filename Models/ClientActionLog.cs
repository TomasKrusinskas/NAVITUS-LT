using System;

namespace ClientManagementAPI.Models
{
    public class ClientActionLog
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string Action { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
