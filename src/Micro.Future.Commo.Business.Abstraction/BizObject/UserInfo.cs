using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Commo.Business.Abstraction.BizObject
{
    public class UserInfo
    {
        public string UserId { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 户名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 联系手机
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public int RoleId { get; set; }
        /// <summary>
        /// 企业Id
        /// </summary>
        public int EnterpriseId { get; set; }
        /// <summary>
        /// 用户状态
        /// </summary>
        public int StateId { get; set; }
        /// <summary>
        /// 上次登录时间
        /// </summary>
        public DateTime LastLoginTime { get; set; }
    }
}
