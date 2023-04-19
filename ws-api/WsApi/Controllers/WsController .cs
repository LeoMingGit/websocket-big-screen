using JwtWebApiDemo.Common;
using JwtWebApiDemo.Filters;
using JwtWebApiDemo.Service;
using JwtWebApiDemo.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SqlSugar;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WebApi.Service;

namespace JwtWebApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WsController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private readonly IWsService _wsService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="wsService"></param>
        public WsController(IConfiguration configuration, IWsService wsService)
        {
            _configuration = configuration;
            _wsService = wsService;
        }

        [Route("say-hello")]
        [HttpGet]
        public  string SayHello()
        {
            return _wsService.SayHello();
        }

      
    }
}
