using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SignalREngine.Common;
using SignalREngine.Models;

namespace SignalRTestApp.Controllers
{
    [Route("api/signalr")]
    public class SignalRController : ControllerBase
    {
        private static HubConnection _hub;
        //private static IHubProxy _proxy;

        public SignalRController()
        {

        }

        [HttpPost("notification")]
        public async Task<IActionResult> NotificationData([FromBody]object SignalRData)
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
                    EventGridModel eventGridUserStatModel = JsonConvert.DeserializeObject<EventGridModel>(message["Data"].ToString());

                    if (_hub == null)
                    {
                        var hubURL = "http://localhost:34543/chatHub";

                        _hub = new HubConnectionBuilder()
                        .WithUrl(hubURL)
                        .Build();
                    }
                    {
                        await _hub.StartAsync();
                        //_proxy.On("Echo", value =>
                        //{
                        //    new Thread(() =>
                        //    {
                        //        _proxy.Invoke("Echo", value + 1).Wait();
                        //    }).Start();
                        //});
                        //await _hub.Start();
                        //if (eventGridUserStatModel.NotificationCommandType == NotificationCommandType.SendPresenceStatus)
                        //{
                        //    _proxy = _hub.CreateHubProxy("AgentStatusHub");                            
                        //}
                        //else
                        //{
                        //    _proxy = _hub.CreateHubProxy("NotificationHub");

                        //}
                        //await _hub.Start();
                    }

                    switch (eventGridUserStatModel.NotificationCommandType)
                    {
                        case NotificationCommandType.NotifySupervisors:
                            //await _proxy.Invoke("SendMessage", "", eventGridUserStatModel.Data);
                            {
                                new Thread(() =>
                                {
                                    // do work here
                                    //_hub.InvokeAsync("SendMessage", "", eventGridUserStatModel.Data).Wait();
                                }).Start();
                                //await _proxy.Invoke("NotifySupervisors", eventGridUserStatModel.Data);
                                break;
                            }
                        case NotificationCommandType.NotifyAgentAboutEnd:
                            //await _hub.InvokeAsync("C", eventGridUserStatModel.UserId, eventGridUserStatModel.UserId, eventGridUserStatModel.TenantId);
                            break;
                        case NotificationCommandType.NotifyAssignmentToUser:
                            //await _hub.InvokeAsync("VoiceMailAssignmentNotification", eventGridUserStatModel.Data);
                            break;
                        case NotificationCommandType.NotifyTransitionToUsers:
                            //await _hub.InvokeAsync("VoiceMailNotification", eventGridUserStatModel.Data);
                            break;
                        case NotificationCommandType.NotifySupervisorsAboutEnd:
                            //await _hub.InvokeAsync("NotifySupervisorsAboutEnd", eventGridUserStatModel.Data);
                            break;
                        case NotificationCommandType.NotifyVoiceMailBoxUsers:
                            //await _hub.InvokeAsync("VoiceMailNotification", eventGridUserStatModel.Data);
                            break;
                        case NotificationCommandType.SendPresenceStatus:
                            //await _hub.InvokeAsync("SendAgentStatus", eventGridUserStatModel.Data);
                            break;
                        case NotificationCommandType.DropNotificationToNotificationHub:
                            //await _hub.InvokeAsync("NotificationReceived", eventGridUserStatModel.Data);
                            break;
                        default:
                            break;
                    }

                }
                return new OkObjectResult(new
                {
                    validationResponse = eventGridValidation.ValidationCode
                });
            }
            catch (Exception ex)
            {
                throw new Exception("Exception Occured");
            }
        }
    }
}