using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace JwtWebApiDemo.Model
{
    /// <summary>
    /// 登录用户信息存储
    /// </summary>
    public class LoginUser
    {
        [Required]
        public string UserId { get; set; }
        [Required]

        public string  PassWord { get; set; }

    }
}
