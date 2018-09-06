using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRTestApp.Models
{
    public class EventGridModel
    {
        public NotificationCommandType NotificationCommandType { get; set; }

        public string TenantId { get; set; }
        public string AgentId { get; set; }
        public string ConversationId { get; set; }
        public string Data { get; set; }
    }

    public enum NotificationCommandType
    {
        NotifySupervisors,
        SendNotifyAgentAboutEnd,
        NotifyAssignmentToUser,
        NotifyTransitionToUsers,
        NotifySupervisorsAboutEnd,
        NotifyVoiceMailBoxUsers,
        SendPresenceStatus,
        DropNotificationToNotificationHub
    }
}
