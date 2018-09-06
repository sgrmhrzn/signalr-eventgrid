using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNetCore.Http;
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
            string topicAccesskey = "/Tj/cDru5mn2g/6tfo/cyDaBSlkw5CE2G2SxIyEb8lY=";
            string topicEndPoint = "https://signalrtest-test.southeastasia-1.eventgrid.azure.net/api/events";
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