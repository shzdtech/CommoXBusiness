using Micro.Future.Commo.Business.Abstraction.BizInterface;
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
using mongodbObjects = Micro.Future.Business.MongoDB.Commo.BizObjects;

namespace Micro.Future.Commo.Business.Requirement.Handler
{
    public class RequirementManager : AbstractRequirementManager
    {
        public event RequirementChainChangedEventHandler OnRequirementChainChanged;

        protected MatcherHandler mongDBRequirementHandler = null;

        #region constructor

        public RequirementManager() : base()
        {
            mongDBRequirementHandler = new MatcherHandler();
        }

        public RequirementManager(MatcherHandler requirementHandler)
        {
            mongDBRequirementHandler = requirementHandler;
            //mongDBRequirementHandler.OnChainChanged += MongDBRequirementHandler_OnChainChanged;
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

        public override BizTResult<IEnumerable<RequirementInfo>> QueryAllRequirements()
        {
            var findRequirements = this.mongDBRequirementHandler.QueryAllRequirements();
            if (findRequirements == null)
                return new BizTResult<IEnumerable<RequirementInfo>>(null, null);

            var requirements = ConvertToRequirementInfos(findRequirements);
            return new BizTResult<IEnumerable<RequirementInfo>>(requirements);
        }

        public override BizTResult<IEnumerable<RequirementInfo>> QueryRequirements(string userId)
        {
            var findRequirements = this.mongDBRequirementHandler.QueryRequirements(userId);
            if (findRequirements == null)
                return new BizTResult<IEnumerable<RequirementInfo>>(null, null);

            var requirements = ConvertToRequirementInfos(findRequirements);
            return new BizTResult<IEnumerable<RequirementInfo>>(requirements);
        }

        public override BizTResult<RequirementInfo> AddRequirementInfo(RequirementInfo requirement)
        {
            if (requirement == null)
                return new BizTResult<RequirementInfo>(null, new BizException(BizErrorType.BIZ_ERROR, "RequirementInfo is null"));

            List<string> errors = null;
            RequirementObject dto = ConvertToRequirementDto(requirement, out errors);

            List<RequirementFilter> filters = ConvertToRequirementFilterDTOs(requirement.Rules, ref errors);

            if (errors != null && errors.Count > 0)
            {
                var errorMsg = string.Join(Environment.NewLine, errors);
                return new BizTResult<RequirementInfo>(null,
                   new BizException(BizErrorType.BIZ_ERROR, errorMsg));
            }

            //bool saveSuccess = false;
            try
            {
                int requirementId = mongDBRequirementHandler.AddRequirement(dto);
                requirement.RequirementId = requirementId;
                //if(requirementId > 0)
                //{
                //    saveSuccess = true;
                //}
                //else
                //{
                //    saveSuccess = false;
                //}

                ////save filters
                //if (saveSuccess && filters != null && filters.Count > 0)
                //{
                //    foreach (var filter in filters)
                //    {
                //        filter.RequirementId = requirementId;
                //        //mongDBRequirementHandler.AddRequirementFilter(filter);
                //    }
                //}

            }
            catch(Exception ex)
            {
                return new BizTResult<RequirementInfo>(null, new BizException(BizErrorType.DATABASE_ERROR, ex.Message));
            }

            return new BizTResult<RequirementInfo>(requirement);
        }

        public override BizTResult<IEnumerable<RequirementChainInfo>> QueryRequirementChains(int requirementId)
        {
            var findChainObjects = this.mongDBRequirementHandler.GetMatcherChainsByRequirementId(requirementId, ChainStatus.OPEN);//.QueryRequirementChains(requirementId);
            if (findChainObjects == null)
                return new BizTResult<IEnumerable<RequirementChainInfo>>(null, null);

            var chains = ConvertToRequirementChainInfos(findChainObjects);
            return new BizTResult<IEnumerable<RequirementChainInfo>>(chains);
        }
        
        private RequirementInfo GetRequirementInfo(int requirementId)
        {
            var bizResult = QueryRequirementInfo(requirementId);
            if (bizResult.HasError || bizResult.Result == null)
                return null;

            return bizResult.Result;
        }

        private IEnumerable<RequirementInfo> GetAllRequirement()
        {
            var bizResult = QueryAllRequirements();
            if (bizResult.HasError || bizResult.Result == null)
                return null;

            DateTime datetime = DateTime.Parse("2016/8/10");

            return bizResult.Result.Where(f => DateTime.Compare(f.CreateTime, datetime) > 0).ToList();

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

        public static IEnumerable<RequirementInfo> ConvertToRequirementInfos(IEnumerable<RequirementObject> dtoList)
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


        public static RequirementInfo ConvertToRequirementInfo(RequirementObject dto)
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

            requirement.ProductName = dto.ProductName;
            requirement.ProductType = dto.ProductType;
            requirement.ProductSpecification = dto.ProductSpecification;
            requirement.ProductQuantity = dto.ProductQuantity;
            requirement.ProductUnit = dto.ProductUnit;


            requirement.WarehouseState = dto.WarehouseState;
            requirement.WarehouseCity = dto.WarehouseCity;
            requirement.WarehouseAddress1 = dto.WarehouseAddress1;
            requirement.WarehouseAddress2 = dto.WarehouseAddress2;

            requirement.TradeAmount = dto.TradeAmount;
            requirement.Subsidies = dto.Subsidies;

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
            if (string.IsNullOrEmpty(requirement.UserId))
            {
                errors.Add("UserId is required.");
            }
            dto.UserId = requirement.UserId;

            if (requirement.Type ==  Abstraction.BizObject.RequirementType.None)
            {
                errors.Add("RequirementType is required.");
            }
            dto.RequirementTypeId = (mongodbObjects.RequirementType)requirement.Type;

            dto.RequirementId = requirement.RequirementId;
            dto.EnterpriseId = requirement.EnterpriseId;
            dto.ProductPrice = (int)requirement.ProductPrice;
            dto.CreateTime = requirement.CreateTime;
            dto.ModifyTime = requirement.ModifyTime;
            dto.RequirementStateId = (mongodbObjects.RequirementStatus)requirement.State;
            dto.RequirementTypeId = (mongodbObjects.RequirementType)requirement.Type;


            dto.ProductName = requirement.ProductName;
            dto.ProductType = requirement.ProductType;
            dto.ProductSpecification = requirement.ProductSpecification;
            dto.ProductQuantity = requirement.ProductQuantity;
            dto.ProductUnit = requirement.ProductUnit;


            dto.WarehouseState = requirement.WarehouseState;
            dto.WarehouseCity = requirement.WarehouseCity;
            dto.WarehouseAddress1 = requirement.WarehouseAddress1;
            dto.WarehouseAddress2 = requirement.WarehouseAddress2;

            dto.TradeAmount = requirement.TradeAmount;
            dto.Subsidies = requirement.Subsidies;

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
