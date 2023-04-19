using SqlSugar;

namespace JwtWebApiDemo.Config
{
    public class SqlSugarHelper
    {
        public static SqlSugarScope Db = new SqlSugarScope(new ConnectionConfig()
        {
            ConnectionString = "Server=127.0.0.1;Port=3306;Database=test;Uid=root;Pwd=123456;CharSet=utf8;pooling=true;SslMode=None;Allow User Variables=true;Convert Zero Datetime=True;Allow Zero Datetime=True;",//连接符字串
            DbType = DbType.MySql,//数据库类型
            IsAutoCloseConnection = true //不设成true要手动close
        },
        db =>
        {
            //(A)全局生效配置点，一般AOP和程序启动的配置扔这里面 ，所有上下文生效
            //调试SQL事件，可以删掉
            db.Aop.OnLogExecuting = (sql, pars) =>
            {
                Console.WriteLine(sql);//输出sql,查看执行sql 性能无影响


                //5.0.8.2 获取无参数化 SQL  对性能有影响，特别大的SQL参数多的，调试使用
                //UtilMethods.GetSqlString(DbType.SqlServer,sql,pars)
            };

            //多个配置就写下面
            //db.Ado.IsDisableMasterSlaveSeparation=true;
        });
    }
}
