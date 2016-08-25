using Micro.Future.Commo.Business.Abstraction.BizInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Micro.Future.Business.Common;
using Micro.Future.Commo.Business.Abstraction.BizObject;
using Micro.Future.Business.DataAccess.Commo.CommonInterface;
using Micro.Future.Business.DataAccess.Commo.CommoHandler;
using Micro.Future.Business.DataAccess.Commo.CommoObject;

namespace Micro.Future.Commo.Business.Requirement.Handler
{
    public class EnterpriseManager : IEnterpriseManager
    {
        private IEnterprise _enterpriseService = null;

        public EnterpriseManager(IEnterprise enterpriseService)
        {
            _enterpriseService = enterpriseService;
        }

        public BizTResult<int> AddEnterprise(EnterpriseInfo enterprise)
        {
            var entity = EnterpriseToEntityObject(enterprise);
            var result = _enterpriseService.AddEnterprise(entity); 

            if(result != null && result.EnterpriseId > 0)
            {
                return new BizTResult<int>(result.EnterpriseId);
            }
            else
            {
                return new BizTResult<int>(0, new BizException("添加失败！"));
            }
        }

        public EnterpriseInfo QueryEnterpriseInfo(int enterpriseId)
        {
            var entity = _enterpriseService.QueryEnterpriseInfo(enterpriseId);
            return EnterpriseToBizObject(entity);
        }

        public BizTResult<bool> UpdateEnterprise(EnterpriseInfo enterprise)
        {
            var entity = EnterpriseToEntityObject(enterprise);
            var result = _enterpriseService.UpdateEnterprise(entity);
            if (result != null && result.EnterpriseId > 0)
            {
                return new BizTResult<bool>(true);
            }
            else
            {
                return new BizTResult<bool>(false, new BizException("保存失败！"));
            }
        }

        private EnterpriseInfo EnterpriseToBizObject(Enterprise entity)
        {
            EnterpriseInfo info = new EnterpriseInfo();
            info.Address = entity.Address;
            info.AnnualInspection = entity.AnnualInspection;
            info.BusinessRange = entity.BusinessRange;
            info.BusinessTypeId = entity.BusinessTypeId;
            info.Contacts = entity.Contacts;
            info.InvoicedQuantity = entity.InvoicedQuantity;
            info.LegalRepresentative = entity.LegalRepresentative;
            info.Name = entity.Name;
            info.PaymentMethodId = entity.PaymentMethodId;
            info.PreviousProfit = entity.PreviousProfit;
            info.PreviousSales = entity.PreviousSales;
            info.RegisterAccount = entity.RegisterAccount;
            info.RegisterAddress = entity.RegisterAddress;
            info.RegisterBankId = entity.RegisterBankId;
            info.RegisterCapital = entity.RegisterCapital;
            info.RegisterNumber = entity.RegisterNumber;
            info.RegisterTime = entity.RegisterTime;
            info.ReputationGrade = entity.ReputationGrade;
            info.StateId = entity.StateId;
            return info;
        }

        private Enterprise EnterpriseToEntityObject(EnterpriseInfo info)
        {
            Enterprise entity = new Enterprise();
            entity.Address = info.Address;
            entity.AnnualInspection = info.AnnualInspection;
            entity.BusinessRange = info.BusinessRange;
            entity.BusinessTypeId = info.BusinessTypeId;
            entity.Contacts = info.Contacts;
            entity.InvoicedQuantity = info.InvoicedQuantity;
            entity.LegalRepresentative = info.LegalRepresentative;
            entity.Name = info.Name;
            entity.PaymentMethodId = info.PaymentMethodId;
            entity.PreviousProfit = info.PreviousProfit;
            entity.PreviousSales = info.PreviousSales;
            entity.RegisterAccount = info.RegisterAccount;
            entity.RegisterAddress = info.RegisterAddress;
            entity.RegisterBankId = info.RegisterBankId;
            entity.RegisterCapital = info.RegisterCapital;
            entity.RegisterNumber = info.RegisterNumber;
            entity.RegisterTime = info.RegisterTime;
            entity.ReputationGrade = info.ReputationGrade;
            entity.StateId = info.StateId;
            return entity;
        }
    }
}
