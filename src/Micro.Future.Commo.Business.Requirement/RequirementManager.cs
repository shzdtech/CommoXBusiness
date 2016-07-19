using Micro.Future.Commo.Business.Abstraction.BizInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Micro.Future.Business.Common;
using Micro.Future.Commo.Business.Abstraction.BizObject;
using Micro.Future.Business.DataAccess.Commo.CommonInterface;
using Micro.Future.Business.DataAccess.Commo.CommoHandler;
using Micro.Future.Business.DataAccess.Commo;

namespace Micro.Future.Commo.Business.Requirement
{
    public class RequirementManager : IRequirementManager
    {
        private IRequirement requirementHandler = null;

        public RequirementManager()
        {
            requirementHandler = new RequirementHandler();
        }

        public RequirementManager(IRequirement requirementHandler)
        {
            this.requirementHandler = requirementHandler;
        }

        public BizTResult<RequirementInfo> QueryRequirementInfo(int requirementId)
        {
            var findRequirement = this.requirementHandler.queryRequirementInfo(requirementId);
            if (findRequirement == null)
                return new BizTResult<RequirementInfo>(null, new BizException(BizErrorType.NOTFOUND_ERROR, "The Requirement not exist!"));

            var filters = this.requirementHandler.queryRequirementFilters(requirementId);
            RequirementInfo requirement = ConvertToRequirementInfo(findRequirement);
            requirement.Rules = ConvertToRequirementRules(filters);
            return new BizTResult<RequirementInfo>(requirement);
        }

        private IEnumerable<RequirementInfo> ConvertToRequirementInfos(IEnumerable<Micro.Future.Business.DataAccess.Commo.Requirement> dtoList)
        {
            if (dtoList == null)
                return null;

            List<RequirementInfo> requirements = new List<RequirementInfo>();
            foreach(var dto in dtoList)
            {
                requirements.Add(ConvertToRequirementInfo(dto));
            }
            return requirements;
        }

        private RequirementInfo ConvertToRequirementInfo(Micro.Future.Business.DataAccess.Commo.Requirement dto)
        {
            RequirementInfo requirement = new RequirementInfo();
            requirement.RequirementId = dto.RequirementId;
            requirement.UserId = dto.UserId;
            requirement.EnterpriseId = dto.EnterpriseId;
            requirement.ProductId = dto.ProductId;
            requirement.ProductPrice = dto.ProductPrice;
            requirement.ProductQuota = dto.ProductQuota;
            requirement.CreateTime = dto.CreateTime;
            requirement.ModifyTime = dto.ModifyTime;
            requirement.State = (Abstraction.BizObject.RequirementState)dto.RequirementStateId;
            requirement.Type = (Abstraction.BizObject.RequirementType)dto.RequirementTypeId;
            return requirement;
        }

        public BizTResult<IEnumerable<RequirementInfo>> QueryRequirements(int userId)
        {
            var findRequirements = this.requirementHandler.queryRequirements(userId);
            if (findRequirements == null)
                return new BizTResult<IEnumerable<RequirementInfo>>(null, null);

            var requirements = ConvertToRequirementInfos(findRequirements);
            return new BizTResult<IEnumerable<RequirementInfo>>(requirements);
        }

        public BizTResult<bool> SaveRequirement(RequirementInfo requirement)
        {
            if (requirement == null)
                return new BizTResult<bool>(false, new BizException(BizErrorType.BIZ_ERROR, "RequirementInfo is null"));

            List<string> errors = null;
            Micro.Future.Business.DataAccess.Commo.Requirement dto = ConvertToRequirementDto(requirement, out errors);

            List<Filter> filters = ConvertToRequirementFilterDTOs(requirement.Rules, ref errors);

            if (errors != null && errors.Count > 0)
            {
                var errorMsg = string.Join(Environment.NewLine, errors);
                return new BizTResult<bool>(false,
                   new BizException(BizErrorType.BIZ_ERROR, errorMsg));
            }

            bool saveSuccess = false;
            try
            {
                saveSuccess = this.requirementHandler.saveRequirement(dto);

                //save filters
            }
            catch(Exception ex)
            {
                return new BizTResult<bool>(false, new BizException(BizErrorType.DATABASE_ERROR, ex.Message));
            }

            return new BizTResult<bool>(saveSuccess);
        }

        private List<Filter> ConvertToRequirementFilterDTOs(IEnumerable<RequirementRuleInfo> rules, ref List<string> errors)
        {
            if (rules == null)
            {
                errors.Add("Rules are required.");
                return null;
            }

            List<Filter> filters = new List<Filter>();
            Filter f = null;
            foreach (var rule in rules)
            {
                f = new Filter();
                f.FilterId = rule.RuleId;
                f.FilterKey = rule.Key;
                f.FilterValue = rule.Value;
                f.OperationId = (int)rule.OperationType;
                f.StateId = (int)rule.State;
                filters.Add(f);
            }
            return filters;
        }

        private Micro.Future.Business.DataAccess.Commo.Requirement ConvertToRequirementDto(RequirementInfo requirement, out List<string> errors)
        {
            Future.Business.DataAccess.Commo.Requirement dto = new Future.Business.DataAccess.Commo.Requirement();

            errors = new List<string>();

            //user id
            if (requirement.UserId == 0)
            {
                errors.Add("UserId is required.");
            }
            dto.UserId = requirement.UserId;

            if (requirement.EnterpriseId == 0)
            {
                errors.Add("EnterpriseId is required.");
            }
            dto.EnterpriseId = requirement.EnterpriseId;

            if (requirement.Type ==  Abstraction.BizObject.RequirementType.NONE)
            {
                errors.Add("RequirementType is required.");
            }
            dto.RequirementTypeId = (int)requirement.Type;

            return dto;
        }

        private IEnumerable<RequirementRuleInfo> ConvertToRequirementRules(IEnumerable<Filter> filters)
        {
            if (filters == null)
                return null;

            List<RequirementRuleInfo> rules = new List<RequirementRuleInfo>();
            foreach (var filter in filters)
            {
                rules.Add(ConvertToRequirementRule(filter));
            }
            return rules;
        }

        private RequirementRuleInfo ConvertToRequirementRule(Filter filter)
        {
            RequirementRuleInfo rule = new RequirementRuleInfo();
            rule.RuleId = filter.FilterId;
            rule.Key = filter.FilterKey;
            rule.Value = filter.FilterValue;
            rule.OperationType = (RequirementRuleOperation)filter.OperationId;
            rule.State = (RequirementRuleState)filter.StateId;
            return rule;
        }
    }
}
