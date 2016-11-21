using Micro.Future.Commo.Business.Abstraction.BizObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Commo.Business.Abstraction.BizInterface
{
    public interface IAcceptanceBankManager
    {
        IList<AcceptanceBankInfo> QueryAllBanks();

        AcceptanceBankInfo QueryBankInfo(int bankId);

        AcceptanceBankInfo CreateBank(AcceptanceBankInfo newBank);

        bool UpdateBank(AcceptanceBankInfo bank);

        bool DeleteBank(int bankId);
    }
}
