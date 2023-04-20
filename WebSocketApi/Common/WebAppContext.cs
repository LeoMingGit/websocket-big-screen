namespace WebSocketApi.Common
{
    public class WebAppContext
    {
        public static IServiceCollection Services;
        public static HttpContext Current
        {
            get
            {
                var factory = Services.BuildServiceProvider().GetService(typeof(IHttpContextAccessor));
                return ((HttpContextAccessor)factory).HttpContext;
            }
        }
    }
}
