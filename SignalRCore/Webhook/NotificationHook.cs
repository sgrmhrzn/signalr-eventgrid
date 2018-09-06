using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.EventGrid;
using Microsoft.Azure.EventGrid.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SignalRChat.Hubs;
using SignalRCore.Enum;
using SignalRCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Controllers
{
    [Route("api/signalr")]
    [ApiController]
    public class NotificationHook : ControllerBase
    {
        public NotificationHook()
        {
            // string topicEndpoint = "https://<topic-name>.<region>-1.eventgrid.azure.net/api/events";
            // string topicKey = "<topic-key>";
            // string topicHostname = new Uri(topicEndpoint).Host;

            // TopicCredentials topicCredentials = new TopicCredentials(topicKey);
            // EventGridClient client = new EventGridClient(topicCredentials);

            // client.PublishEventsAsync(topicHostname, GetEventsList()).GetAwaiter().GetResult();
            // Console.Write("Published events to Event Grid.");


        }
        [HttpGet]
        public string GetById()
        {
            return "test";
        }

        [HttpPost("notification")]
        public async Task<IActionResult> Post([FromBody]object SignalRData)
        {
            try
            {
                var messages = JsonConvert.DeserializeObject<JArray>(SignalRData.ToString());

                // If the request is for subscription validation, send back the validation code.
                var eventGridValidation = messages.ValidateMessageForEventGrid();
                if (eventGridValidation.IsValidationMessage)
                {
                    return new OkObjectResult(new
                    {
                        validationResponse = eventGridValidation.ValidationCode
                    });
                }


                foreach (var message in messages)
                {
                    EventGridModel eventGridModel = JsonConvert.DeserializeObject<EventGridModel>(message["Data"].ToString());

                    //if (_hub == null)
                    //{
                    //    var hubURL = "";
                    //    _hub = new HubConnection(hubURL);
                    //}
                    //if (_hub.State != ConnectionState.Connected)
                    //{
                    //    proxy = hub.CreateHubProxy("SignalRHub");
                    //    await _hub.Start();
                    //}
                    //await _proxy.Invoke("MethodName", SignalRData);
                    NotificationHub hub = new NotificationHub();

                    switch (eventGridModel.NotificationType)
                    {
                        case NotificationType.NotifySupervisors:
                            {
                                await hub.SendMessage(eventGridModel.AgentId,eventGridModel.ConversationId);
                                break;
                            }
                        //case NotificationType.NotifySupervisors:
                        //    {
                        //        await hub.SendMessage(eventGridModel.Data);
                        //        break;
                        //    }
                        //case NotificationType.NotifySupervisorsAboutEnd:
                        //    {
                        //        await hub.NotifySupervisorsAboutEnd(eventGridModel.Data);
                        //        break;
                        //    }
                        //case NotificationType.NotifyAssignmentToUser:
                        //    {
                        //        await hub.VoiceMailAssignmentNotification(eventGridModel.Data);
                        //        break;
                        //    }
                        //case NotificationType.NotifyVoiceMailBoxUsers:
                        //    {
                        //        await hub.VoiceMailNotification(eventGridModel.Data);
                        //        break;
                        //    }
                        //case NotificationType.SendPresenceStatus:
                        //    {
                        //        await hub.SendAgentStatus(eventGridModel.Data);
                        //        break;
                        //    }
                        //case NotificationType.DropNotificationToNotificationHub:
                        //    {await hub.NotificationReceived(eventGridModel.Data);
                        //        break;
                        //    }
                        default:
                            {
                                break;
                            }
                    }
                }

                //switch()
                return new OkObjectResult(new
                {
                    validationResponse = eventGridValidation.ValidationCode
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // static IList<EventGridEvent> GetEventsList()
        // {
        //     List<EventGridEvent> eventsList = new List<EventGridEvent>();
        //     for (int i = 0; i < 1; i++)
        //     {
        //         eventsList.Add(new EventGridEvent()
        //         {
        //             Id = Guid.NewGuid().ToString(),
        //             EventType = "Contoso.Items.ItemReceivedEvent",
        //             // Data = new ContosoItemReceivedEventData()
        //             // {
        //             //     ItemUri = "ContosoSuperItemUri"
        //             // },

        //             EventTime = DateTime.Now,
        //             Subject = "Door1",
        //             DataVersion = "2.0"
        //         });
        //     }
        //     return eventsList;
        // }
    }


}