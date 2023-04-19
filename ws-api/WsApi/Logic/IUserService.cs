using JwtWebApiDemo.Model;

namespace JwtWebApiDemo.Logic
{
    public interface IUserService
    {
        user GetUser(string userid,string pwd);
    }
}
