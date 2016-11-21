using Micro.Future.Commo.Business.Abstraction.BizInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Micro.Future.Commo.Business.Abstraction.BizObject;
using Micro.Future.Business.DataAccess.Commo.CommoObject;
using Micro.Future.Business.DataAccess.Commo.CommonInterface;

namespace Micro.Future.Commo.Business.Requirement.Handler
{
    public class AcceptanceBankManager : IAcceptanceBankManager
    {
        private IAcceptanceBank _bankHandler = null;

        public AcceptanceBankManager(IAcceptanceBank bankHandler)
        {
            _bankHandler = bankHandler;
        }


        public AcceptanceBankInfo CreateBank(AcceptanceBankInfo newBankInfo)
        {
            AcceptanceBank bank = ConvertInfoToObject(newBankInfo);
            var newBank = _bankHandler.CreateBank(bank);
            if (newBank == null)
                return null;

            newBankInfo.BankId = newBank.BankId;
            return newBankInfo;
        }


        private AcceptanceBank ConvertInfoToObject(AcceptanceBankInfo info)
        {
            return new AcceptanceBank()
            {
                AcceptanceType = info.AcceptanceType,
                BankId = info.BankId,
                BankName = info.BankName,
                BankPrice = info.BankPrice,
                BankType = info.BankType,
                CreateTime = info.CreateTime,
                UpdateTime = info.UpdateTime
            };
        }

        private AcceptanceBankInfo ConvertObjectToInfo(AcceptanceBank info)
        {
            return new AcceptanceBankInfo()
            {
                AcceptanceType = info.AcceptanceType,
                BankId = info.BankId,
                BankName = info.BankName,
                BankPrice = info.BankPrice,
                BankType = info.BankType,
                CreateTime = info.CreateTime,
                UpdateTime = info.UpdateTime
            };
        }



        public bool DeleteBank(int bankId)
        {
            return _bankHandler.DeleteBank(bankId);
        }

        public IList<AcceptanceBankInfo> QueryAllBanks()
        {
            var banks = _bankHandler.QueryAllBanks();
            if (banks == null || banks.Count == 0)
                return null;

            IList<AcceptanceBankInfo> infos = new List<AcceptanceBankInfo>();
            foreach(var bank in banks)
            {
                infos.Add(ConvertObjectToInfo(bank));
            }
            return infos;
        }

        public AcceptanceBankInfo QueryBankInfo(int bankId)
        {
            var bank = _bankHandler.QueryBankInfo(bankId);
            if (bank == null)
                return null;
            return ConvertObjectToInfo(bank);
        }

        public bool UpdateBank(AcceptanceBankInfo bankInfo)
        {
            var bank = ConvertInfoToObject(bankInfo);
            return _bankHandler.UpdateBank(bank);
        }
    }
}
