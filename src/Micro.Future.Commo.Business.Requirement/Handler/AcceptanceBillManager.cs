using Micro.Future.Commo.Business.Abstraction.BizInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Micro.Future.Commo.Business.Abstraction.BizObject;
using Micro.Future.Business.DataAccess.Commo.CommonInterface;
using Micro.Future.Business.DataAccess.Commo.CommoObject;

namespace Micro.Future.Commo.Business.Requirement.Handler
{
    public class AcceptanceBillManager : IAcceptanceBillManager
    {
        private IAcceptanceBill _acceptanceBillHandler = null;
        public AcceptanceBillManager(IAcceptanceBill acceptanceBillHandler)
        {
            _acceptanceBillHandler = acceptanceBillHandler;
        }

        public AcceptanceBillInfo CreateAcceptance(AcceptanceBillInfo acceptance)
        {
            var newBill = _acceptanceBillHandler.CreateAcceptance(new AcceptanceBill()
            {
                AgreementNumber = acceptance.AgreementNumber,
                Amount = acceptance.Amount,
                CreateTime = acceptance.CreateTime,
                DrawerAccount = acceptance.DrawerAccount,
                DrawerBankId = acceptance.DrawerBankId,
                DrawerName = acceptance.DrawerName,
                DueDate = acceptance.DueDate,
                EnterpriseId = acceptance.EnterpriseId,
                PayeeAccount = acceptance.PayeeAccount,
                PayeeBankId = acceptance.PayeeBankId,
                PayeeName = acceptance.PayeeName
            });

            acceptance.AcceptanceBillId = newBill.AcceptanceBillId;
            return acceptance;
        }

        public bool DeleteAcceptance(int acceptanceId)
        {
            return _acceptanceBillHandler.DeleteAcceptance(acceptanceId);
        }

        public AcceptanceBillInfo QueryAcceptance(int acceptanceId)
        {
            var acceptance = _acceptanceBillHandler.QueryAcceptance(acceptanceId);
            return new AcceptanceBillInfo()
            {
                AcceptanceBillId = acceptanceId,
                AgreementNumber = acceptance.AgreementNumber,
                Amount = acceptance.Amount,
                CreateTime = acceptance.CreateTime,
                DrawerAccount = acceptance.DrawerAccount,
                DrawerBankId = acceptance.DrawerBankId,
                DrawerName = acceptance.DrawerName,
                DueDate = acceptance.DueDate,
                EnterpriseId = acceptance.EnterpriseId,
                PayeeAccount = acceptance.PayeeAccount,
                PayeeBankId = acceptance.PayeeBankId,
                PayeeName = acceptance.PayeeName
            };
        }

        public IList<AcceptanceBillInfo> QueryAllAcceptances(int enterpriseId)
        {
            var list = _acceptanceBillHandler.QueryAllAcceptances(enterpriseId);
            if (list == null || list.Count == 0)
                return null;

            IList<AcceptanceBillInfo> results = new List<AcceptanceBillInfo>();
            foreach(var acceptance in list)
            {
                results.Add(new AcceptanceBillInfo()
                {
                    AcceptanceBillId = acceptance.AcceptanceBillId,
                    AgreementNumber = acceptance.AgreementNumber,
                    Amount = acceptance.Amount,
                    CreateTime = acceptance.CreateTime,
                    DrawerAccount = acceptance.DrawerAccount,
                    DrawerBankId = acceptance.DrawerBankId,
                    DrawerName = acceptance.DrawerName,
                    DueDate = acceptance.DueDate,
                    EnterpriseId = acceptance.EnterpriseId,
                    PayeeAccount = acceptance.PayeeAccount,
                    PayeeBankId = acceptance.PayeeBankId,
                    PayeeName = acceptance.PayeeName
                });
            }

            return results;
        }

        public bool UpdateAcceptance(AcceptanceBillInfo acceptance)
        {
           return _acceptanceBillHandler.UpdateAcceptance(new AcceptanceBill()
            {
                AcceptanceBillId = acceptance.AcceptanceBillId,
                AgreementNumber = acceptance.AgreementNumber,
                Amount = acceptance.Amount,
                CreateTime = acceptance.CreateTime,
                DrawerAccount = acceptance.DrawerAccount,
                DrawerBankId = acceptance.DrawerBankId,
                DrawerName = acceptance.DrawerName,
                DueDate = acceptance.DueDate,
                EnterpriseId = acceptance.EnterpriseId,
                PayeeAccount = acceptance.PayeeAccount,
                PayeeBankId = acceptance.PayeeBankId,
                PayeeName = acceptance.PayeeName
            });
        }
    }
}
