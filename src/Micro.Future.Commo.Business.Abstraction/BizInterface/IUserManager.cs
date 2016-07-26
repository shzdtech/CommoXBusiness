using Micro.Future.Business.Common;
using Micro.Future.Commo.Business.Abstraction.BizObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Commo.Business.Abstraction.BizInterface
{
    public interface IUserManager
    {
        UserInfo GetUserById(int userId);

        /// <summary>
        /// 根据名称获取用户信息
        /// </summary>
        /// <param name="name">可取值userName，phone，email</param>
        /// <returns></returns>
        UserInfo GetUserInfo(UserInfo user);

        UserInfo GetUserByLoginName(string name, LoginNameType loginNameType);

        /// <summary>
        /// 用户注册， PASSWORD要加密
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        BizTResult<bool> Register(UserInfo user);

        bool UpdateUserInfo(UserInfo user);

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        BizTResult<bool> SignIn(UserInfo user);
    }
}
