using Micro.Future.Commo.Business.Abstraction.BizObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Commo.Business.Abstraction.BizInterface
{
    public interface IUserCommon
    {
        IList<UserPaymentInfo> QueryAllUserPaymentInfo();

        UserPaymentInfo QueryUserPaymentInfo(int infoId);

        int CreateUserPaymentInfo(UserPaymentInfo info);

        bool UpdateUserPaymentInfo(UserPaymentInfo info);

        bool DeleteUserPaymentInfo(int infoId);


        IList<UserInvoiceInfo> QueryAllUserInvoiceInfo();

        UserInvoiceInfo QueryUserInvoiceInfo(int infoId);

        int CreateUserInvoiceInfo(UserInvoiceInfo info);

        bool UpdateUserInvoiceInfo(UserInvoiceInfo info);

        bool DeleteUserInvoiceInfo(int infoId);
    }
}
