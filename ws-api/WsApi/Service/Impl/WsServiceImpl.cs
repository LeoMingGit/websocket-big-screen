namespace WebApi.Service.Impl
{
    public class WsServiceImpl: IWsService
    {

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string SayHello()
        {
            return $"hello world  {DateTime.Now.ToString()}";
        }

    }
}
