using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRCore.Enum
{
    public enum NotificationType
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
