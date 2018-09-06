using Newtonsoft.Json;
using SignalRTestApp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRTestApp.Models
{
    public class EventGridPublisher
    {
        private readonly string _topicendpoint;
        /// <summary>
        /// Initializes a new instance of the HttpClient class
        /// </summary>
        private readonly IHttpClient _client = new StandardHttpClient();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key">Key of the Event Grid Topic</param>
        /// <param name="topicEndpoint">Endpoint of the Event Grid Topic.</param>
        public EventGridPublisher(string key, string topicEndpoint)
        {
            _topicendpoint = topicEndpoint;
            _client.AddHeader("aeg-sas-key", key);
        }

        /// <summary>
        /// Creates event and sends event to Event Grid Topic
        /// </summary>
        /// <param name="message">Initializes a new instance of the UserPacket class</param>
        /// <returns></returns>
        public async Task<string> PublishMessage<T>(T message, string eventType = "")
        {
            if (string.IsNullOrEmpty(eventType))
            {
                eventType = "captureFileCreated";
            }

            List<CustomEvent<T>> events = new List<CustomEvent<T>>();
            if (string.IsNullOrEmpty(eventType))
            {
                eventType = "captureFileCreated";
            }
            var customEvent = new CustomEvent<T>
            {
                EventType = eventType,
                Subject = "/customtopic/message",
                Data = JsonConvert.SerializeObject(message),
                DataVersion = "1.0"
            };
            events.Add(customEvent);

            var httpResponseMessage = await _client.PostAsync(_topicendpoint, events);
            return httpResponseMessage;
        }
    }
}
