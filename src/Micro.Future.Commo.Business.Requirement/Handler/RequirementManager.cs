﻿using Micro.Future.Commo.Business.Abstraction.BizInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Micro.Future.Business.Common;
using Micro.Future.Commo.Business.Abstraction.BizObject;
using Micro.Future.Business.DataAccess.Commo;
using Micro.Future.Business.MongoDB.Commo.Handler;
using Micro.Future.Business.MongoDB.Commo.BizObjects;
using Micro.Future.Commo.Business.Abstraction.Handler;

namespace Micro.Future.Commo.Business.Requirement.Handler
{
    public class RequirementManager : AbstractRequirementManager
    {
        public event RequirementChainChangedEventHandler OnRequirementChainChanged;

        protected RequirementHandler mongDBRequirementHandler = null;

        #region constructor

        public RequirementManager() : base()
        {
            mongDBRequirementHandler = new RequirementHandler();
        }

        public RequirementManager(RequirementHandler requirementHandler)
        {
            mongDBRequirementHandler = requirementHandler;
            mongDBRequirementHandler.OnChainChanged += MongDBRequirementHandler_OnChainChanged;
        }


        private void MongDBRequirementHandler_OnChainChanged(IEnumerable<ChainObject> chains)
        {
            if (OnRequirementChainChanged != null)
            {
                List<RequirementChainInfo> chainInfoList = new List<RequirementChainInfo>();
                RequirementChainInfo info = null;
                foreach (var chainObj in chains)
                {
                    info = ConvertToRequirementChainInfo(chainObj);
                    chainInfoList.Add(info);
                }

                OnRequirementChainChanged(chainInfoList);
            }
        }


        #endregion

        #region implements

        public override BizTResult<RequirementInfo> QueryRequirementInfo(int requirementId)
        {
            var findRequirement = this.mongDBRequirementHandler.QueryRequirementInfo(requirementId);
            if (findRequirement == null)
                return new BizTResult<RequirementInfo>(null, new BizException(BizErrorType.NOTFOUND_ERROR, "The Requirement not exist!"));
            
            RequirementInfo requirement = ConvertToRequirementInfo(findRequirement);

            return new BizTResult<RequirementInfo>(requirement);
        }

        public override BizTResult<IEnumerable<RequirementInfo>> QueryRequirements(int userId)
        {
            var findRequirements = this.mongDBRequirementHandler.QueryRequirements(userId.ToString());
            if (findRequirements == null)
                return new BizTResult<IEnumerable<RequirementInfo>>(null, null);

            var requirements = ConvertToRequirementInfos(findRequirements);
            return new BizTResult<IEnumerable<RequirementInfo>>(requirements);
        }

        public override BizTResult<bool> AddRequirementInfo(RequirementInfo requirement)
        {
            if (requirement == null)
                return new BizTResult<bool>(false, new BizException(BizErrorType.BIZ_ERROR, "RequirementInfo is null"));

            List<string> errors = null;
            RequirementObject dto = ConvertToRequirementDto(requirement, out errors);

            List<RequirementFilter> filters = ConvertToRequirementFilterDTOs(requirement.Rules, ref errors);

            if (errors != null && errors.Count > 0)
            {
                var errorMsg = string.Join(Environment.NewLine, errors);
                return new BizTResult<bool>(false,
                   new BizException(BizErrorType.BIZ_ERROR, errorMsg));
            }

            bool saveSuccess = false;
            try
            {
                int requirementId = mongDBRequirementHandler.AddRequirement(dto);
                if(requirementId > 0)
                {
                    saveSuccess = true;
                }
                else
                {
                    saveSuccess = false;
                }

                //save filters
                if (saveSuccess && filters != null && filters.Count > 0)
                {
                    foreach (var filter in filters)
                    {
                        filter.RequirementId = requirementId;
                        //mongDBRequirementHandler.AddRequirementFilter(filter);
                    }
                }

            }
            catch(Exception ex)
            {
                return new BizTResult<bool>(false, new BizException(BizErrorType.DATABASE_ERROR, ex.Message));
            }

            return new BizTResult<bool>(saveSuccess);
        }

        public override BizTResult<bool> UpdateRequirementInfo(RequirementInfo requirement)
        {
            if (requirement == null)
                return new BizTResult<bool>(false, new BizException(BizErrorType.BIZ_ERROR, "RequirementInfo is null"));

            List<string> errors = null;
            RequirementObject dto = ConvertToRequirementDto(requirement, out errors);

            if (errors != null && errors.Count > 0)
            {
                var errorMsg = string.Join(Environment.NewLine, errors);
                return new BizTResult<bool>(false,
                   new BizException(BizErrorType.BIZ_ERROR, errorMsg));
            }

            bool saveSuccess = false;
            try
            {
                saveSuccess = false;//this.mongDBRequirementHandler.UpdateRequirement(dto);
            }
            catch (Exception ex)
            {
                return new BizTResult<bool>(false, new BizException(BizErrorType.DATABASE_ERROR, ex.Message));
            }

            return new BizTResult<bool>(saveSuccess);
        }

        public override BizTResult<IEnumerable<RequirementChainInfo>> QueryRequirementChains(int requirementId)
        {
            var findChainObjects = this.mongDBRequirementHandler.QueryRequirementChains(requirementId);
            if (findChainObjects == null)
                return new BizTResult<IEnumerable<RequirementChainInfo>>(null, null);

            var chains = ConvertToRequirementChainInfos(findChainObjects);
            return new BizTResult<IEnumerable<RequirementChainInfo>>(chains);
        }

        #endregion

        #region private


        protected IEnumerable<RequirementChainInfo> ConvertToRequirementChainInfos(IEnumerable<ChainObject> findChainObjects)
        {
            if (findChainObjects == null)
                return null;

            List<RequirementChainInfo> chains = new List<RequirementChainInfo>();
            foreach (var chainObj in findChainObjects)
            {
                chains.Add(ConvertToRequirementChainInfo(chainObj));
            }
            return chains;
        }

        protected RequirementChainInfo ConvertToRequirementChainInfo(ChainObject chainObj)
        {
            RequirementChainInfo info = new RequirementChainInfo();
            info.ChainId = chainObj.ChainId;

            if(chainObj.RequirementIdChain != null && chainObj.RequirementIdChain.Count > 0)
            {
                info.Requirements = new List<RequirementInfo>();
                foreach(var requirementId in chainObj.RequirementIdChain)
                {
                    var bizResult = QueryRequirementInfo(requirementId);
                    if (bizResult.HasError || bizResult.Result == null)
                        continue;

                    info.Requirements.Add(bizResult.Result);
                }
            }

            info.CreateTime = chainObj.CreateTime;
            info.ModifyTime = chainObj.ModifyTime;
            info.IsDeleted = chainObj.Deleted;

            return info;
        }

        private IEnumerable<RequirementInfo> ConvertToRequirementInfos(IEnumerable<RequirementObject> dtoList)
        {
            if (dtoList == null)
                return null;

            List<RequirementInfo> requirements = new List<RequirementInfo>();
            foreach (var dto in dtoList)
            {
                requirements.Add(ConvertToRequirementInfo(dto));
            }
            return requirements;
        }


        private RequirementInfo ConvertToRequirementInfo(RequirementObject dto)
        {
            RequirementInfo requirement = new RequirementInfo();
            requirement.RequirementId = dto.RequirementId;
            requirement.UserId = dto.UserId;
            requirement.EnterpriseId = dto.EnterpriseId;
            requirement.ProductPrice = dto.ProductPrice;
            requirement.CreateTime = dto.CreateTime;
            requirement.ModifyTime = dto.ModifyTime;
            requirement.State = (Abstraction.BizObject.RequirementState)dto.RequirementStateId;
            requirement.Type = (Abstraction.BizObject.RequirementType)dto.RequirementTypeId;
            return requirement;
        }

        private List<RequirementFilter> ConvertToRequirementFilterDTOs(IEnumerable<RequirementRuleInfo> rules, ref List<string> errors)
        {
            if (rules == null)
            {
                errors.Add("Rules are required.");
                return null;
            }

            List<RequirementFilter> filters = new List<RequirementFilter>();
            RequirementFilter f = null;
            foreach (var rule in rules)
            {
                f = new RequirementFilter();
                f.FilterId = rule.RuleId;
                f.FilterKey = rule.Key;
                f.FilterValue = rule.Value;
                f.OperationId = (int)rule.OperationType;
                f.StateId = (int)rule.State;
                filters.Add(f);
            }
            return filters;
        }

        private RequirementObject ConvertToRequirementDto(RequirementInfo requirement, out List<string> errors)
        {
            RequirementObject dto = new RequirementObject();

            errors = new List<string>();

            //user id
            if (requirement.UserId == 0)
            {
                errors.Add("UserId is required.");
            }
            dto.UserId = requirement.UserId;

            if (requirement.Type ==  Abstraction.BizObject.RequirementType.None)
            {
                errors.Add("RequirementType is required.");
            }
            dto.RequirementTypeId = (int)requirement.Type;

            dto.RequirementId = requirement.RequirementId;
            dto.EnterpriseId = requirement.EnterpriseId;
            dto.ProductPrice = (int)requirement.ProductPrice;
            dto.CreateTime = requirement.CreateTime;
            dto.ModifyTime = requirement.ModifyTime;
            dto.RequirementStateId = (int)requirement.State;
            dto.RequirementTypeId = (int)requirement.Type;

            return dto;
        }

        private IEnumerable<RequirementRuleInfo> ConvertToRequirementRules(IEnumerable<RequirementFilter> filters)
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

        private RequirementRuleInfo ConvertToRequirementRule(RequirementFilter filter)
        {
            RequirementRuleInfo rule = new RequirementRuleInfo();
            rule.RuleId = filter.FilterId;
            rule.Key = filter.FilterKey;
            rule.Value = filter.FilterValue;
            rule.OperationType = (RequirementRuleOperation)filter.OperationId;
            rule.State = (RequirementRuleState)filter.StateId;
            return rule;
        }

        #endregion
    }
}