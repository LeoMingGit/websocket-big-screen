using JwtWebApiDemo.Config;
using JwtWebApiDemo.Service;
using JwtWebApiDemo.Model;

namespace WebApi.Service.Impl
{
    public class UserServiceImpl : IUserService
    {
        public user GetUser(string userid, string pwd)
        {
            var db = SqlSugarHelper.Db;
            var res = db.Queryable<user>().Where(p => p.user_id.ToString() == userid && p.pwd == pwd).ToList().FirstOrDefault();
            return res;
        }
    }
}
