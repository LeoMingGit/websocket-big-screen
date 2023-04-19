using JwtWebApiDemo.Common;
using JwtWebApiDemo.Filters;
using JwtWebApiDemo.Logic;
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

namespace JwtWebApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private readonly IUserService _userService;

        public LoginController(IConfiguration configuration,IUserService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }

        [VerifyAttribute]
        [HttpGet]
        [Route("GetHelloMsg")]
        public ActionResult<string> GetHelloMsg()
        {
            return "hello";
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginUser loginDto, [FromServices] IOptions<JWTTokenOptions> jwtOptions)
        {
            //User Authentication
            if (ModelState.IsValid == false)
            {
                return BadRequest("UserId or Password can not be empty");
            }
            //先要在数据库中根据用户的名称和密码比一下，如果匹配上了，则发放令牌
            // 此处省略 ...
            var  login_user_obj = _userService.GetUser(loginDto.UserId, loginDto.PassWord);
            if(login_user_obj==null) return Forbid();
            //to do :发放令牌
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, login_user_obj.name));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, login_user_obj.user_id.ToString()));
            claims.Add(new Claim(ClaimTypes.Role,login_user_obj.role.ToString()));
            string jwtToken = BuildToken(claims, jwtOptions.Value);
            return Ok(jwtToken);
        }
        /// <summary>
        /// 生成secretkey
        /// </summary>
        /// <returns></returns>
        [Route("GenerateSecretKey")]
        [HttpGet]
        public string GenerateSecretKey()
        {
            var key = new byte[32];
            RandomNumberGenerator.Create().GetBytes(key);
            var base64Secret = UrlBase64.Encode(key);
            return base64Secret;
        }
     

        private static string BuildToken(IEnumerable<Claim> claims, JWTTokenOptions options)
        {
            DateTime expires = DateTime.Now.AddSeconds(options.ExpireSeconds);
            byte[] keyBytes = Encoding.UTF8.GetBytes(options.SigningKey);
            var secKey = new SymmetricSecurityKey(keyBytes);
            var credentials = new SigningCredentials(secKey,
                SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new JwtSecurityToken(expires: expires,
                signingCredentials: credentials, claims: claims);
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
       
    }
}
