using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRCore.Model
{
    public class EventGridModel
    {
        public NotificationCommandType NotificationType { get; set; }

        public string TenantId { get; set; }
        public string AgentId { get; set; }
        public string ConversationId { get; set; }
        public string Data { get; set; }
    }
}
