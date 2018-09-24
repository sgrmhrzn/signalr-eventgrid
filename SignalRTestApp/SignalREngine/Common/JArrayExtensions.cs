using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalREngine.Common
{
    public static class JArrayExtensions
    {
        public static (bool IsValidationMessage, string ValidationCode) ValidateMessageForEventGrid(this JArray jArrayMessages)
        {
            if (jArrayMessages.Count > 0 && string.Equals((string)jArrayMessages[0]["eventType"],
                    "Microsoft.EventGrid.SubscriptionValidationEvent",
                    StringComparison.OrdinalIgnoreCase))
            {
                return (true, jArrayMessages[0]["data"]["validationCode"].ToString());
            }
            return (false, string.Empty);
        }
    }
}
