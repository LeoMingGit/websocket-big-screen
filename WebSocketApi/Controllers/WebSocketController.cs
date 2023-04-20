using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System.Web;
using WebSocketApi.Common;
using WebSocketApi.Service;

namespace WebSocketApi.Controllers
{
    [ApiController]
    [Route("api/ws")]
    public class WebSocketController : ControllerBase
    {

        private readonly IWebSocketService _webSocketService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webSocketService"></param>
        public WebSocketController(IWebSocketService webSocketService)
        {
            _webSocketService = webSocketService;
        }

        [HttpGet("hello")]
        public  string SayHello()
        {
            return $"hello {DateTime.Now.ToString()}";
        }

        [HttpPost("pushdata")]
        public async Task<string> PushData()
        {
            var data = await WebAppContext.Current.Request.GetRawBodyAsync();
            _webSocketService.PushData(data.ToString());
            return "OK";
        }
    }
}
