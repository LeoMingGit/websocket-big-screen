using SqlSugar;

namespace JwtWebApiDemo.Model
{

    [SugarTable("user")]//当和数据库名称不一样可以设置表别名 指定表明

    public class user
    {
        public user()
        {
        }
        ///<summary>
        ///主键ID
        ///</summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]//数据库是自增才配自增 
        public long user_id { get; set; }
        ///<summary>
        ///租户ID
        ///</summary>
        public long tenant_id { get; set; }
        ///<summary>
        ///名称
        ///</summary>
        public string name { get; set; }
        ///<summary>
        ///年龄
        ///</summary>
        public int? age { get; set; }
        ///<summary>
        ///测试下划线字段命名类型
        ///</summary>
        public int? test_type { get; set; }
        ///<summary>
        ///日期
        ///</summary>
        public DateTime? test_date { get; set; }
        ///<summary>
        ///测试
        ///</summary>
        public long? role { get; set; }
        ///<summary>
        ///手机号码
        ///</summary>
        public string phone { get; set; }
        ///<summary>
        ///密码
        ///</summary>
        public string pwd { get; set; }
    }
}
