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

        public override BizTResult<IEnumerable<RequirementInfo>> QueryAllRequirements()
        {
            var findRequirements = this.mongDBRequirementHandler.QueryAllRequirements();
            if (findRequirements == null)
                return new BizTResult<IEnumerable<RequirementInfo>>(null, null);

            var requirements = ConvertToRequirementInfos(findRequirements);
            return new BizTResult<IEnumerable<RequirementInfo>>(requirements);
        }

        public override BizTResult<IEnumerable<RequirementInfo>> QueryRequirements(int userId)
        {
            var findRequirements = this.mongDBRequirementHandler.QueryRequirements(userId.ToString());
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
            IEnumerable<RequirementChainInfo> chains = FakeRequirementChains(requirementId);
            return new BizTResult<IEnumerable<RequirementChainInfo>>(chains);


            //var findChainObjects = this.mongDBRequirementHandler.QueryRequirementChains(requirementId);
            //if (findChainObjects == null)
            //    return new BizTResult<IEnumerable<RequirementChainInfo>>(null, null);

            //var chains = ConvertToRequirementChainInfos(findChainObjects);
            //return new BizTResult<IEnumerable<RequirementChainInfo>>(chains);
        }

        private IEnumerable<RequirementChainInfo> FakeRequirementChains(int requirementId)
        {
            RequirementInfo myRequirement = GetRequirementInfo(requirementId);
            if (myRequirement == null)
                return null;

            var findRequirements = GetAllRequirement();
            if (findRequirements == null)
                return null;

            var allRequirementList = findRequirements.ToList();
            if (allRequirementList == null)
                return null;

            List<RequirementType> targetTypes = new List<RequirementType>();
            if (myRequirement.Type == RequirementType.Sale)
            {
                targetTypes.Add(RequirementType.Subsidy);
                targetTypes.Add(RequirementType.Buy);
                
            }
            else if(myRequirement.Type == RequirementType.Buy)
            {
                targetTypes.Add(RequirementType.Subsidy);
                targetTypes.Add(RequirementType.Sale);
               
            }
            else if (myRequirement.Type == RequirementType.Subsidy)
            {
                targetTypes.Add(RequirementType.Sale);
                targetTypes.Add(RequirementType.Buy);
            }

            allRequirementList.RemoveAll(f => f.UserId == myRequirement.UserId);
            List<RequirementInfo> firstTypeRequirements = allRequirementList.Where(f => f.Type == targetTypes[0]).ToList();
            List<RequirementInfo> secondTypeRequirements = allRequirementList.Where(f => f.Type == targetTypes[1]).ToList();

            if (firstTypeRequirements == null || firstTypeRequirements.Count == 0)
                return null;

            if (secondTypeRequirements == null || secondTypeRequirements.Count == 0)
                return null;

            List<string> chianId = new List<string>();
            

            int length = firstTypeRequirements.Count;
            if (secondTypeRequirements.Count < length)
                length = secondTypeRequirements.Count;

            List<RequirementChainInfo> chainList = new List<RequirementChainInfo>();

            RequirementChainInfo chain = null;
            for (int i = 0; i < length; i++)
            {
                chain = new RequirementChainInfo();
                chain.ChainId = i;
                chain.CreateTime = DateTime.Now.AddMinutes(-50);
                chain.IsDeleted = false;
                chain.Requirements = new List<RequirementInfo>();
                chain.Requirements.Add(myRequirement);
                chain.Requirements.Add(firstTypeRequirements[i]);
                chain.Requirements.Add(secondTypeRequirements[i]);
                chain.ModifyTime = DateTime.Now.AddMinutes(-30);

                chainList.Add(chain);

                if (i >= 20)
                    break;
            }

            return chainList;

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

        private IEnumerable<RequirementInfo> ConvertToRequirementInfos(IEnumerable<RequirementObject> dtoList)
        {
            if (dtoList == null)
                return null;

            List<RequirementInfo> requirements = new List<RequirementInfo>();
            foreach (var dto in dtoList)
            {
                if (string.IsNullOrWhiteSpace(dto.ProductName))
                    continue;

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
