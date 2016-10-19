using Micro.Future.Commo.Business.Abstraction.BizInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Micro.Future.Commo.Business.Abstraction.BizObject;

namespace Micro.Future.Commo.Business.Requirement.Handler
{
    public class UserCommonManager : IUserCommon
    {
        public int CreateUserInvoiceInfo(UserInvoiceInfo info)
        {
            throw new NotImplementedException();
        }

        public int CreateUserPaymentInfo(UserPaymentInfo info)
        {
            throw new NotImplementedException();
        }

        public bool DeleteUserInvoiceInfo(int infoId)
        {
            throw new NotImplementedException();
        }

        public bool DeleteUserPaymentInfo(int infoId)
        {
            throw new NotImplementedException();
        }

        public IList<UserInvoiceInfo> QueryAllUserInvoiceInfo()
        {
            throw new NotImplementedException();
        }

        public IList<UserPaymentInfo> QueryAllUserPaymentInfo()
        {
            throw new NotImplementedException();
        }

        public UserInvoiceInfo QueryUserInvoiceInfo(int infoId)
        {
            throw new NotImplementedException();
        }

        public UserPaymentInfo QueryUserPaymentInfo(int infoId)
        {
            throw new NotImplementedException();
        }

        public bool UpdateUserInvoiceInfo(UserInvoiceInfo info)
        {
            throw new NotImplementedException();
        }

        public bool UpdateUserPaymentInfo(UserPaymentInfo info)
        {
            throw new NotImplementedException();
        }
    }
}
