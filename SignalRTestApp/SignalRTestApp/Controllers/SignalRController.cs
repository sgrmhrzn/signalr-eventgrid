using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SignalRTestApp.Common;
using SignalRTestApp.Models;

namespace SignalRTestApp.Controllers
{
    [Route("api/signalr")]
    [ApiController]
    public class SignalRController : ControllerBase
    {
        private static HubConnection _hub;
        private static IHubProxy _proxy;

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
                        var hubURL = "http://localhost:34543/";
                        _hub = new HubConnection(hubURL);
                    }
                    if (_hub.State != ConnectionState.Connected)
                    {
                        _proxy = _hub.CreateHubProxy("NotificationHub");
                        await _hub.Start();
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
                            await _proxy.Invoke("SendMessage", "", eventGridUserStatModel.Data);

                            //await _proxy.Invoke("NotifySupervisors", eventGridUserStatModel.Data);
                            break;
                        case NotificationCommandType.NotifyAgentAboutEnd:
                            await _proxy.Invoke("C", eventGridUserStatModel.AgentId,eventGridUserStatModel.ConversationId,eventGridUserStatModel.TenantId);
                            break;
                        case NotificationCommandType.NotifyAssignmentToUser:
                            await _proxy.Invoke("VoiceMailAssignmentNotification", eventGridUserStatModel.Data);
                            break;
                        case NotificationCommandType.NotifyTransitionToUsers:
                            await _proxy.Invoke("VoiceMailNotification", eventGridUserStatModel.Data);
                            break;
                        case NotificationCommandType.NotifySupervisorsAboutEnd:
                            await _proxy.Invoke("NotifySupervisorsAboutEnd", eventGridUserStatModel.Data);
                            break;
                        case NotificationCommandType.NotifyVoiceMailBoxUsers:
                            await _proxy.Invoke("VoiceMailNotification", eventGridUserStatModel.Data);
                            break;
                        case NotificationCommandType.SendPresenceStatus:
                            await _proxy.Invoke("SendAgentStatus", eventGridUserStatModel.Data);
                            break;
                        case NotificationCommandType.DropNotificationToNotificationHub:
                            await _proxy.Invoke("NotificationReceived", eventGridUserStatModel.Data);
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