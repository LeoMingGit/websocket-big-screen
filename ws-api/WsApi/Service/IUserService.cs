using JwtWebApiDemo.Model;

namespace JwtWebApiDemo.Service
{
    public interface IUserService
    {
        user GetUser(string userid,string pwd);
    }
}
