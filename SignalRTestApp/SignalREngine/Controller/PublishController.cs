using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SignalREngine.Models;

namespace SignalREngine.Controllers
{
    [Route("api/publish")]
    public class PublishController : ControllerBase
    {
        private static EventGridPublisher _eventGridPublisher;

        public PublishController()
        {
            string topicAccesskey = "your-event-grid-topic-sas-key";
            string topicEndPoint = "your-event-grid-endpoint";
            _eventGridPublisher = new EventGridPublisher(topicAccesskey, topicEndPoint);
        }

        [HttpPost]
        public async Task<IActionResult> PublishData([FromBody]EventGridModel eventGridModel)
        {
            var data = new ActivityMetaModel();
            data.UserActivityType = UserActivityType.UserPresenceStatusChanged;
            data.UserId = eventGridModel.UserId;
            data.ActivityMetadata = JsonConvert.SerializeObject(new
            {
                NotificationType = 19,
                TenantId = eventGridModel.TenantId,
                NotificationMetadata = JsonConvert.SerializeObject(new
                {
                    EventDateTime = DateTime.UtcNow,
                    UserId = eventGridModel.UserId,
                    PresenceState = PresenceState.Chatting
                }),
            });
            eventGridModel.Metadata = JsonConvert.SerializeObject(data);
            await _eventGridPublisher.PublishMessage(eventGridModel);

            return new OkObjectResult(eventGridModel);

        }
    }
}