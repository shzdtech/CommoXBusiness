using Micro.Future.Commo.Business.Abstraction.BizInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Micro.Future.Commo.Business.Abstraction.BizObject;
using Micro.Future.Business.DataAccess.Commo.CommoHandler;
using Micro.Future.Business.DataAccess.Commo.CommoObject;
using Micro.Future.Business.Common;

namespace Micro.Future.Commo.Business.Requirement.Handler
{
    public class UserManager : IUserManager
    {
        public Micro.Future.Business.DataAccess.Commo.CommonInterface.IUserManager userStore = null;

        public UserManager()
        {
            this.userStore = new UserManagerHandler();
        }


        public UserManager(Micro.Future.Business.DataAccess.Commo.CommonInterface.IUserManager userManager)
        {
            this.userStore = userManager;
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public UserInfo GetUserById(int userId)
        {
            User findUser = userStore.GetUserById(userId);
            return UserToUserInfo(findUser);
        }

        /// <summary>
        /// 根据用户Id，UserName，Phone，Email获取用户信息
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public UserInfo GetUserInfo(UserInfo userInfo)
        {
            User user = new User();
            user.UserId = userInfo.UserId;
            user.UserName = userInfo.UserName;
            user.Phone = userInfo.Phone;
            var findUser = userStore.GetUser(user);
            return UserToUserInfo(findUser);
        }
        public UserInfo GetUserByLoginName(string name, LoginNameType type)
        {
            if(type == LoginNameType.ALL)
            {
                var findUser = GetUserByUserName(name);
                if (findUser == null)
                    findUser = GetUserByPhone(name);

                return findUser;
            }
            else if (type == LoginNameType.UserName)
            {
                return GetUserByUserName(name);
            }
            else if (type == LoginNameType.Phone)
            {
                return GetUserByPhone(name);
            }

            return null;
        }

        private UserInfo GetUserByUserName(string userName)
        {
            return GetUserInfo(new UserInfo() { Name = userName });
        }

        private UserInfo GetUserByPhone(string phone)
        {
            return GetUserInfo(new UserInfo() { Phone = phone });
        }


        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public BizTResult<bool> Register(UserInfo userInfo)
        {
            var findUserInfo = GetUserByUserName(userInfo.UserName);
            if (findUserInfo != null)
            {
                return new BizTResult<bool>(false, new BizException("用户名已注册！"));
            }

            findUserInfo = GetUserByPhone(userInfo.Phone);
            if (findUserInfo != null)
            {
                return new BizTResult<bool>(false, new BizException("手机号码已注册！"));
            }

            var newUser = UserInfoToUser(userInfo);
            bool regSuccess = this.userStore.userRegister(newUser);
            
            if(!regSuccess)
            {
                return new BizTResult<bool>(regSuccess, new BizException("注册失败！"));
            }
            return new BizTResult<bool>(regSuccess);
        }

        public BizTResult<bool> SignIn(UserInfo userInfo)
        {
            var user = UserInfoToUser(userInfo);
            bool signInSuccess = this.userStore.userLogin(user);
            if (!signInSuccess)
            {
                return new BizTResult<bool>(signInSuccess, new BizException("登录失败！"));
            }
            return new BizTResult<bool>(signInSuccess);
        }

        public bool UpdateUserInfo(UserInfo user)
        {
            throw new NotImplementedException();
        }




        private UserInfo UserToUserInfo(User user)
        {
            if (user == null)
                return null;

            UserInfo userInfo = new UserInfo();
            userInfo.UserId = user.UserId;
            userInfo.Name = user.Name;
            userInfo.Phone = user.Phone;
            userInfo.UserName = user.UserName;
            userInfo.EnterpriseId = user.EnterpriseId;
            userInfo.LastLoginTime = user.LastLoginTime;
            userInfo.RoleId = user.RoleId;
            userInfo.StateId = user.StateId;
            return userInfo;
        }

        private User UserInfoToUser(UserInfo userInfo)
        {
            User user = new User();
            user.UserId = userInfo.UserId;
            user.Name = userInfo.Name;
            user.Password = userInfo.Password;
            user.Phone = userInfo.Phone;
            user.UserName = userInfo.UserName;
            user.EnterpriseId = userInfo.EnterpriseId;
            user.RoleId = userInfo.RoleId;
            user.StateId = userInfo.StateId;
            return user;
        }
    }
}
