using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalREngine.Models
{
    public class EventGridModel
    {
        public NotificationCommandType NotificationCommandType { get; set; }

        public string TenantId { get; set; }
        public ActivityType ActivityType { get; set; }
        public string Metadata { get; set; }
        public string UserId { get; set; }
    }

    public enum NotificationCommandType
    {
        NotifySupervisors,
        NotifyAgentAboutEnd,
        NotifyAssignmentToUser,
        NotifyTransitionToUsers,
        NotifySupervisorsAboutEnd,
        NotifyVoiceMailBoxUsers,
        SendPresenceStatus,
        DropNotificationToNotificationHub
    }

    public enum ActivityType
    {
        User,
        System
    }

    public class ActivityMetaModel
    {
        public string TenantId { get; set; }
        public string UserId { get; set; }
        public string ConversationId { get; set; }
        public DateTime EventDate { get; set; }
        public string SkillId { get; set; }
        public double ConversationTookTime { get; set; }
        public UserActivityType UserActivityType { get; set; }
        public string ActivityMetadata { get; set; }
    }
    public enum UserActivityType
    {
        LogIn,
        LogOut,
        ConversationAccepted,
        ConversationRejected,
        ConversationEnd,
        UserPresenceStatusChanged, //Self_PresenceStateChanged,
        VoicemailAssigned,
        VoicemailStateChanged,
        InternalMessage
    }

    public enum PresenceState
    {
        Offline,
        Available,
        Busy,
        WrappingUp,
        Talking,
        Chatting,
        OnBreak,
        Lunch
    }
}
