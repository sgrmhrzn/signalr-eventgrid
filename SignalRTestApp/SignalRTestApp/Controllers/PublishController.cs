using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SignalRTestApp.Models;

namespace SignalRTestApp.Controllers
{
    [Route("api/publish")]
    [ApiController]
    public class PublishController : ControllerBase
    {
        private static EventGridPublisher _eventGridPublisher;

        public PublishController()
        {
            string topicAccesskey = "2iamR/G4fk2EMr8H8yuPoSc3UwvTs5oZq+St+GUkamc=";
            string topicEndPoint = "https://mytopic.southeastasia-1.eventgrid.azure.net/api/events";
            _eventGridPublisher = new EventGridPublisher(topicAccesskey, topicEndPoint);
        }

        [HttpPost("publishData")]
        public async Task<IActionResult> PublishData([FromBody]EventGridModel eventGridModel)
        {

            await _eventGridPublisher.PublishMessage(eventGridModel);

            return new OkObjectResult(eventGridModel);
            
        }
    }
}