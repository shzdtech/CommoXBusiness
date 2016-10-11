using Micro.Future.Commo.Business.Abstraction.BizObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Commo.Business.Abstraction.BizInterface
{
    public interface ICommonManager
    {
        IList<BankInfo> QueryAllBanks();

        BankInfo QueryBankInfo(int bankId);

        IList<PaymentMethodInfo> QueryAllPaymentMethods();

        IList<EnterpriseTypeInfo> QueryAllEnterpriseTypes();
    }
}
