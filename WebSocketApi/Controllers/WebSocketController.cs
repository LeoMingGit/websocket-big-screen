using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System.Web;
using WebSocketApi.Common;
using WebSocketApi.Service;
using WebSocketApi.Service.Impl;

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
        public WebSocketController()
        {
            _webSocketService = WebSocketServiceImpl.Instance;
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
