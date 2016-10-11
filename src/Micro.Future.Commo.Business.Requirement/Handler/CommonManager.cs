using Micro.Future.Commo.Business.Abstraction.BizInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Micro.Future.Commo.Business.Abstraction.BizObject;
using Micro.Future.Business.DataAccess.Commo.CommonInterface;

namespace Micro.Future.Commo.Business.Requirement.Handler
{
    public class CommonManager : ICommonManager
    {
        private ICommon _commonHandler = null;
        public CommonManager(ICommon commonHandler)
        {
            _commonHandler = commonHandler;
        }

        public IList<BankInfo> QueryAllBanks()
        {
            var banks = _commonHandler.queryAllBank();
            if (banks == null || banks.Count == 0)
                return null;

            IList<BankInfo> bankInfos = new List<BankInfo>();
            foreach(var bank in banks)
            {
                bankInfos.Add(new BankInfo()
                {
                    BankAddress = bank.BankAddress,
                    BankId = bank.BankId,
                    BankName = bank.BankName
                });
            }

            return bankInfos;
        }

        public BankInfo QueryBankInfo(int bankId)
        {
            var bank = _commonHandler.queryBank(bankId);
            if (bank == null)
                return null;

            return new BankInfo()
            {
                BankAddress = bank.BankAddress,
                BankId = bank.BankId,
                BankName = bank.BankName
            };
        }

        public IList<EnterpriseTypeInfo> QueryAllEnterpriseTypes()
        {
            var types = _commonHandler.queryAllBusinessType();
            if (types == null || types.Count == 0)
                return null;

            IList<EnterpriseTypeInfo> infoList = new List<EnterpriseTypeInfo>();
            foreach (var t in types)
            {
                infoList.Add(new EnterpriseTypeInfo()
                {
                    BusinessTypeId = t.BusinessTypeId,
                    BusinessTypeName = t.BusinessTypeName,
                    ParentId = t.ParentId,
                    StateId = t.StateId
                });
            }

            return infoList;
        }

        public IList<PaymentMethodInfo> QueryAllPaymentMethods()
        {
            var methods = _commonHandler.queryAllPaymentMethod();
            if (methods == null || methods.Count == 0)
                return null;

            IList<PaymentMethodInfo> infoList = new List<PaymentMethodInfo>();
            foreach (var t in methods)
            {
                infoList.Add(new PaymentMethodInfo()
                {
                    PaymentMethodId = t.PaymentMethodId,
                    PaymentMethodName = t.PaymentMethodName,
                    StateId = t.StateId
                });
            }

            return infoList;
        }
    }
}
