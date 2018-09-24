using Engine.Services.Intefaces;
using Engine.Utilities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Engine.APIs
{
    [Route("api/[controller]")]
    public class RTCController : Controller
    {
        private static IRTCService _rtcService;
        public RTCController(IRTCService rtcService)
        {
            _rtcService = rtcService;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            _rtcService.get();
            return new List<string> { "ASP.NET", "Docker", "Windows Containers" };
        }

        [HttpPost]
        public Task<ActionResult> Post([FromBody]object realTimeData)
        {
            try
            {
                var messages = JsonConvert.DeserializeObject<JArray>(realTimeData.ToString());

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
                    Notification eventGridModel = JsonConvert.DeserializeObject<Notification>(message["Data"].ToString());
                    await _hubContext.Clients.All.SendAsync("ReceiveMessage", eventGridModel);
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
    }
}
