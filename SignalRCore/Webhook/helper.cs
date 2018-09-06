
using System;
using Newtonsoft.Json.Linq;

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