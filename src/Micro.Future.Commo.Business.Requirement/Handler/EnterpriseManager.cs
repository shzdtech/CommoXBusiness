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
            List<string> errors = ValidateEnterpriseInfo(enterprise);
            if(errors!=null && errors.Count > 0)
            {
                string errorMsg = string.Join(Environment.NewLine, errors);
                return new BizTResult<int>(-1,
                  new BizException(BizErrorType.BIZ_ERROR, errorMsg));
            }

            bool isValid = _enterpriseService.ValidationEnterpriceRegister(enterprise.Name, enterprise.EmailAddress);
            if (!isValid)
            {
                return new BizTResult<int>(-1,
                 new BizException(BizErrorType.BIZ_ERROR, "企业名称或企业邮箱已注册！"));
            }


            var entity = EnterpriseToEntityObject(enterprise);
            var result = _enterpriseService.AddEnterprise(entity); 

            if(result != null && result.EnterpriseId > 0)
            {
                return new BizTResult<int>(result.EnterpriseId);
            }
            else
            {
                return new BizTResult<int>(-1, new BizException("添加失败！"));
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
            return new BizTResult<bool>(result);
        }

        public bool UpdateEnterpriseState(int enterpriseId, EnterpriseStateType stateType)
        {
            return _enterpriseService.UpdateEnterpriseState(enterpriseId, (int)stateType);
        }


        private List<string> ValidateEnterpriseInfo(EnterpriseInfo enterprise)
        {
            List<string> errors = new List<string>();
            if(string.IsNullOrWhiteSpace(enterprise.Name))
            {
                errors.Add("“企业名称”不能为空！");
            }

            if (string.IsNullOrWhiteSpace(enterprise.EmailAddress))
            {
                errors.Add("“企业邮箱”不能为空！");
            }


            if (string.IsNullOrWhiteSpace(enterprise.Contacts))
            {
                errors.Add("“联系人”不能为空！");
            }

            if (string.IsNullOrWhiteSpace(enterprise.MobilePhone))
            {
                errors.Add("“联系人手机号码”不能为空！");
            }

            return errors;
        }

        private EnterpriseInfo EnterpriseToBizObject(Enterprise entity)
        {
            EnterpriseInfo info = new EnterpriseInfo();
            info.EnterpriseId = entity.EnterpriseId;
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
            info.EnterpriseState = (EnterpriseStateType)entity.StateId;
            info.EmailAddress = entity.EmailAddress;
            info.MobilePhone = entity.MobilePhone;
            info.LicenseImagePath = entity.LicenseImagePath;
            info.Fax = entity.Fax;
            return info;
        }

        private Enterprise EnterpriseToEntityObject(EnterpriseInfo info)
        {
            Enterprise entity = new Enterprise();
            entity.EnterpriseId = info.EnterpriseId;
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
            entity.StateId = (int)info.EnterpriseState;
            entity.EmailAddress = info.EmailAddress;
            entity.MobilePhone = info.MobilePhone;
            entity.LicenseImagePath = info.LicenseImagePath;
            entity.Fax = info.Fax;
            return entity;
        }

        public BizTResult<IList<EnterpriseInfo>> QueryEnterprises(string name, EnterpriseStateType? stateType)
        {
            BizException bizException = null;
            IList<EnterpriseInfo> infoList = null;

            IQueryable<Enterprise> enterpriseList = _enterpriseService.QueryEnterprises(f => IsEnterpriseMatch(f, name, stateType));
            if (enterpriseList == null)
                return new BizTResult<IList<EnterpriseInfo>>(null, bizException);

            infoList = new List<EnterpriseInfo>();
            foreach(var obj in enterpriseList)
            {
                infoList.Add(EnterpriseToBizObject(obj));
            }

            return new BizTResult<IList<EnterpriseInfo>>(infoList, bizException);
        }

        private bool IsEnterpriseMatch(Enterprise enterpriseObj, string name, EnterpriseStateType? stateType)
        {
            if (!string.IsNullOrWhiteSpace(name) && (string.IsNullOrWhiteSpace(enterpriseObj.Name) || !enterpriseObj.Name.Contains(name)))
            {
                return false;
            }

            if(stateType.HasValue && enterpriseObj.StateId != (int)stateType.Value)
            {
                return false;
            }

            return true;
        }

    }
}
