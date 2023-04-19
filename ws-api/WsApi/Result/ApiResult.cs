using Newtonsoft.Json;
using System.ComponentModel;

namespace JwtWebApiDemo.Result
{


    public class HttpStatus
    {
        /// <summary>
        /// 操作成功
        /// </summary>
        public static readonly int SUCCESS = 200;
        /// <summary>
        /// 对象创建成功
        /// </summary>
        public static readonly int CREATED = 201;

        /// <summary>
        /// 请求已经被接受
        /// </summary>
        public static readonly int ACCEPTED = 202;

        /// <summary>
        /// 操作已经执行成功，但是没有返回数据
        /// </summary>
        public static readonly int NO_CONTENT = 204;

        /// <summary>
        /// 资源已被移除
        /// </summary>
        public static readonly int MOVED_PERM = 301;

        /// <summary>
        /// 重定向
        /// </summary>
        public static readonly int SEE_OTHER = 303;

        /// <summary>
        /// 资源没有被修改
        /// </summary>
        public static readonly int NOT_MODIFIED = 304;

        /**
         * 参数列表错误（缺少，格式不匹配）
         */
        public static readonly int BAD_REQUEST = 400;

        /// <summary>
        /// 未授权
        /// </summary>
        public static readonly int UNAUTHORIZED = 401;

        /**
         * 访问受限，授权过期
         */
        public static readonly int FORBIDDEN = 403;

        /**
         * 资源，服务未找到
         */
        public static readonly int NOT_FOUND = 404;

        /**
         * 不允许的http方法
         */
        public static readonly int BAD_METHOD = 405;

        /**
         * 资源冲突，或者资源被锁
         */
        public static readonly int CONFLICT = 409;

        /**
         * 不支持的数据，媒体类型
         */
        public static readonly int UNSUPPORTED_TYPE = 415;

        /**
         * 系统内部错误
         */
        public static readonly int ERROR = 500;

        /**
         * 接口未实现
         */
        public static readonly int NOT_IMPLEMENTED = 501;
    }

    public enum ResultCode
    {
        [Description("success")]
        SUCCESS = 200,

        [Description("参数错误")]
        PARAM_ERROR = 101,

        [Description("验证码错误")]
        CAPTCHA_ERROR = 103,

        [Description("登录错误")]
        LOGIN_ERROR = 105,

        [Description("操作失败")]
        FAIL = 1,

        [Description("服务端出错啦")]
        GLOBAL_ERROR = 500,

        [Description("自定义异常")]
        CUSTOM_ERROR = 110,

        [Description("非法请求")]
        INVALID_REQUEST = 116,

        [Description("授权失败")]
        OAUTH_FAIL = 201,

        [Description("未授权")]
        DENY = 401,

        [Description("授权访问失败")]
        FORBIDDEN = 403,

        [Description("Bad Request")]
        BAD_REQUEST = 400
    }
    public class ApiResult
    {
        public int Code { get; set; }
        public string Msg { get; set; }
        /// <summary>
        /// 如果data值为null，则忽略序列化将不会返回data字段
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public object Data { get; set; }

        /// <summary>
        /// 初始化一个新创建的APIResult对象，使其表示一个空消息
        /// </summary>
        public ApiResult()
        {
        }

        /// <summary>
        /// 初始化一个新创建的 ApiResult 对象
        /// </summary>
        /// <param name="code"></param>
        /// <param name="msg"></param>
        public ApiResult(int code, string msg)
        {
            Code = code;
            Msg = msg;
        }

        /// <summary>
        /// 初始化一个新创建的 ApiResult 对象
        /// </summary>
        /// <param name="code"></param>
        /// <param name="msg"></param>
        public ApiResult(int code, string msg, object data)
        {
            Code = code;
            Msg = msg;
            if (data != null)
            {
                Data = data;
            }
        }

        /// <summary>
        /// 返回成功消息
        /// </summary>
        /// <returns></returns>
        public ApiResult Success()
        {
            Code = (int)ResultCode.SUCCESS;
            Msg = "success";
            return this;
        }

        /// <summary>
        /// 返回成功消息
        /// </summary>
        /// <param name="data">数据对象</param>
        /// <returns>成功消息</returns>
        public static ApiResult Success(object data) { return new ApiResult(HttpStatus.SUCCESS, "success", data); }

        /// <summary>
        /// 返回成功消息
        /// </summary>
        /// <param name="msg">返回内容</param>
        /// <returns>成功消息</returns>
        public static ApiResult Success(string msg) { return new ApiResult(HttpStatus.SUCCESS, msg, null); }

        /// <summary>
        /// 返回成功消息
        /// </summary>
        /// <param name="msg">返回内容</param>
        /// <param name="data">数据对象</param>
        /// <returns>成功消息</returns>
        public static ApiResult Success(string msg, object data) { return new ApiResult(HttpStatus.SUCCESS, msg, data); }

        /// <summary>
        /// 访问被拒
        /// </summary>
        /// <param name="apiResult"></param>
        /// <returns></returns>
        public ApiResult On401()
        {
            Code = (int)ResultCode.DENY;
            Msg = "access denyed";
            return this;
        }
        public ApiResult Error(ResultCode resultCode, string msg = "")
        {
            Code = (int)resultCode;
            Msg = msg;
            return this;
        }

        /// <summary>
        /// 返回失败消息
        /// </summary>
        /// <param name="code"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static ApiResult Error(int code, string msg) { return new ApiResult(code, msg); }

        /// <summary>
        /// 返回失败消息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static ApiResult Error(string msg) { return new ApiResult((int)ResultCode.CUSTOM_ERROR, msg); }

        public override string ToString()
        {
            return $"msg={Msg},data={Data}";
        }
    }

    public class ApiResult<T> : ApiResult
    {
        public T Result { get; set; }
    }
}
