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
using System.Reflection;

namespace Micro.Future.Commo.Business.Requirement.Handler
{
    public class RequirementManager : BaseBizHandler, IRequirementManager
    {
        protected MatcherHandler _matcherService = null;

        public event Action<IList<RequirementChainInfo>> OnChainChanged;

        #region constructor

        public RequirementManager()
        {
            _matcherService = new MatcherHandler();
        }

        public RequirementManager(MatcherHandler requirementHandler)
        {
            _matcherService = requirementHandler;
            _matcherService.OnChainChanged += _matcherService_OnChainChanged;
        }

        private void _matcherService_OnChainChanged(IEnumerable<ChainObject> chains, MatcherHandler.ChainUpdateStatus status)
        {
            if (OnChainChanged != null)
            {
                List<RequirementChainInfo> chainInfoList = new List<RequirementChainInfo>();
                RequirementChainInfo info = null;
                foreach (var chainObj in chains)
                {
                    info = ConvertToRequirementChainInfo(chainObj);
                    chainInfoList.Add(info);
                }

                OnChainChanged(chainInfoList);
            }
        }

        #endregion

        #region implements

        public BizTResult<RequirementInfo> QueryRequirementInfo(int requirementId)
        {
            var findRequirement = this._matcherService.QueryRequirementInfo(requirementId);
            if (findRequirement == null)
                return new BizTResult<RequirementInfo>(null, new BizException(BizErrorType.NOTFOUND_ERROR, "The Requirement not exist!"));
            
            RequirementInfo requirement = ConvertToRequirementInfo(findRequirement);

            return new BizTResult<RequirementInfo>(requirement);
        }

        public BizTResult<IEnumerable<RequirementInfo>> QueryAllRequirements()
        {
            var findRequirements = this._matcherService.QueryAllRequirements();
            if (findRequirements == null)
                return new BizTResult<IEnumerable<RequirementInfo>>(null, null);

            var requirements = ConvertToRequirementInfos(findRequirements);
            return new BizTResult<IEnumerable<RequirementInfo>>(requirements);
        }

        public BizTResult<IEnumerable<RequirementInfo>> QueryRequirements(string userId)
        {
            var findRequirements = this._matcherService.QueryRequirements(userId);
            if (findRequirements == null)
                return new BizTResult<IEnumerable<RequirementInfo>>(null, null);

            var requirements = ConvertToRequirementInfos(findRequirements);
            return new BizTResult<IEnumerable<RequirementInfo>>(requirements);
        }

        public BizTResult<RequirementInfo> AddRequirementInfo(RequirementInfo requirement)
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
                int requirementId = _matcherService.AddRequirement(dto);
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

        public BizTResult<IEnumerable<RequirementChainInfo>> QueryRequirementChains(int requirementId)
        {
            var findChainObjects = this._matcherService.GetMatcherChainsByRequirementId(requirementId, ChainStatus.OPEN);//.QueryRequirementChains(requirementId);
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


        private bool AreNotEmptyAndEqual(string source,string target, StringComparison compairison = StringComparison.CurrentCultureIgnoreCase)
        {
            if (string.IsNullOrWhiteSpace(source) || string.IsNullOrWhiteSpace(target))
                return false;

            return string.Equals(source, target, compairison);
        }


        private bool IsRequirementMatchSearchCriteria(RequirementObject requirementObj, RequirementSearchCriteria searchCriteria)
        {
            if (!string.IsNullOrWhiteSpace(searchCriteria.ProductName) && !AreNotEmptyAndEqual(searchCriteria.ProductName, requirementObj.ProductName))
                return false;

            if (!string.IsNullOrWhiteSpace(searchCriteria.ProductType) && !AreNotEmptyAndEqual(searchCriteria.ProductType, requirementObj.ProductType))
                return false;


            if (searchCriteria.RequirementType.HasValue && (int)requirementObj.RequirementTypeId != (int)searchCriteria.RequirementType.Value)
                return false;

            if (searchCriteria.RequirementState.HasValue && (int)requirementObj.RequirementStateId != (int)searchCriteria.RequirementState.Value)
                return false;

            if (searchCriteria.StartTradeAmount > 0m && requirementObj.TradeAmount < searchCriteria.StartTradeAmount.Value)
                return false;

            if (searchCriteria.EndTradeAmount > 0m && requirementObj.TradeAmount > searchCriteria.EndTradeAmount.Value)
                return false;


            return true;

        }

        public static string GetObjectPropertyValue<T>(T t, string propertyname)
        {
            Type type = typeof(T);

            PropertyInfo property = type.GetProperty(propertyname);

            if (property == null) return property.Name;

            object o = property.GetValue(t, null);

            if (o == null) return string.Empty;

            return o.ToString();
        }

        public SearchResult<RequirementInfo> SearchRequirements(RequirementSearchCriteria searchCriteria)
        {
            _stopwatch.Restart();

            SearchResult<RequirementInfo> searchResult = new SearchResult<RequirementInfo>();
            searchResult.PageNo = searchCriteria.PageNo;
            searchResult.PageSize = searchCriteria.PageSize;

            var queryRequirements = _matcherService.QueryRequirementsByLinq(f => IsRequirementMatchSearchCriteria(f, searchCriteria));
            int totalRecords = queryRequirements.Count();

            IList<RequirementInfo> requirementInfoList = null;
            if (totalRecords > 0)
            {
                requirementInfoList = new List<RequirementInfo>();

                if (searchCriteria.OrderByFields != null && searchCriteria.OrderByFields.Count > 0)
                {
                    foreach(var field in searchCriteria.OrderByFields)
                    {
                        if(string.IsNullOrWhiteSpace(field.OrderBy) || string.Equals(field.OrderBy, "asc", StringComparison.CurrentCultureIgnoreCase))
                        {
                            queryRequirements = queryRequirements.OrderBy(f => f.GetType().GetProperty(field.Field).PropertyType);
                        }
                        else
                        {
                            queryRequirements = queryRequirements.OrderByDescending(f => f.GetType().GetProperty(field.Field).PropertyType);
                        }
                    }
                }

                var findRequirements = queryRequirements.Skip((searchCriteria.PageNo - 1) * searchCriteria.PageSize).Take(searchCriteria.PageSize);

                foreach (var reqObj in findRequirements)
                {
                    requirementInfoList.Add(ConvertToRequirementInfo(reqObj));
                }
            }

            searchResult.TotalCount = totalRecords;
            searchResult.Result = requirementInfoList;

            _stopwatch.Stop();
            searchResult.ElapsedTime = _stopwatch.ElapsedMilliseconds;
            return searchResult;
        }


        public CommoBizTResult<IList<RequirementInfo>> QueryRequirementsByEnterpriseId(int enterpriseId, RequirementState? state)
        {
            _stopwatch.Restart();
            IList<RequirementInfo> infoList = null;
            BizException _bizException = null;

            int stateId = -1;
            if (state.HasValue)
                stateId = (int)state.Value;

            IList<RequirementObject> requirements = null;

            if (stateId == -1)
            {
                requirements = _matcherService.QueryRequirementsByEnterpriseId(enterpriseId);
            }
            else
            {
                requirements = _matcherService.QueryRequirementsByEnterpriseId(enterpriseId, (mongodbObjects.RequirementStatus)stateId);
            }

            if (requirements != null && requirements.Count > 0)
            {
                infoList = new List<RequirementInfo>();
                foreach (var requirement in requirements)
                {
                    infoList.Add(ConvertToRequirementInfo(requirement));
                }
            }

            _stopwatch.Stop();

            var bizResult = new CommoBizTResult<IList<RequirementInfo>>(infoList, _bizException);
            bizResult.ElapsedTime = _stopwatch.ElapsedMilliseconds;

            return bizResult;
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



            requirement.PaymentAmount = dto.PaymentAmount;
            requirement.PaymentDateTime = dto.PaymentDateTime;
            requirement.PaymentType = dto.PaymentType;
            requirement.TradeProfit = dto.TradeProfit;
            requirement.EnterpriseType = dto.EnterpriseType;
            requirement.BusinessRange = dto.BusinessRange;
            requirement.WarehouseAccount = dto.WarehouseAccount;
            requirement.InvoiceValue = dto.InvoiceValue;
            requirement.InvoiceIssueDateTime = dto.InvoiceIssueDateTime;
            requirement.InvoiceTransferMode = dto.InvoiceTransferMode;



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
                errors.Add("“UserId”不能为空！");
            }

            dto.UserId = requirement.UserId;

            if (requirement.Type ==  Abstraction.BizObject.RequirementType.None)
            {
                errors.Add("“需求类型” 不能为空！");
            }


            if(requirement.Type == Abstraction.BizObject.RequirementType.Buy || requirement.Type == Abstraction.BizObject.RequirementType.Sale)
            {
                if(string.IsNullOrEmpty(requirement.ProductName))
                {
                    errors.Add("“货物名称”不能为空！");
                }
            }


            dto.RequirementTypeId = (mongodbObjects.RequirementType)requirement.Type;

            dto.RequirementId = requirement.RequirementId;
            dto.EnterpriseId = requirement.EnterpriseId;
            dto.ProductPrice = (int)requirement.ProductPrice;
            dto.CreateTime = requirement.CreateTime;
            dto.ModifyTime = requirement.ModifyTime;
            dto.RequirementStateId = (mongodbObjects.RequirementStatus)requirement.State;
            dto.RequirementTypeId = (mongodbObjects.RequirementType)requirement.Type;

            if(requirement.Type == Abstraction.BizObject.RequirementType.Sale)
            {
                if(requirement.ProductPrice<=0)
                {
                    errors.Add("“货物单价”必须大于0！");
                }

                if(requirement.ProductQuantity<=0)
                {
                    errors.Add("“货物数量”必须大于0！");
                }


                if (requirement.TradeAmount == 0)
                    dto.TradeAmount = requirement.ProductPrice * requirement.ProductQuantity;
                else
                    dto.TradeAmount = requirement.TradeAmount;

            }
            else if(requirement.Type == Abstraction.BizObject.RequirementType.Buy)
            {
                if (requirement.TradeAmount == 0 && requirement.PaymentAmount != 0)
                    dto.TradeAmount = requirement.PaymentAmount;
                else
                    dto.TradeAmount = requirement.TradeAmount;

                if (dto.TradeAmount <= 0m)
                    errors.Add("“资金金额”必须大于0！");

            }
            else
            {
                dto.TradeAmount = requirement.TradeAmount;
                if (dto.TradeAmount <= 0m)
                    errors.Add("“合同总额”必须大于0！");
            }

            dto.ProductName = requirement.ProductName;
            dto.ProductType = requirement.ProductType;
            dto.ProductSpecification = requirement.ProductSpecification;
            dto.ProductQuantity = requirement.ProductQuantity;
            dto.ProductUnit = requirement.ProductUnit;

            
            dto.PaymentDateTime = requirement.PaymentDateTime;
            dto.PaymentType = requirement.PaymentType;
            dto.TradeProfit = requirement.TradeProfit;
            dto.EnterpriseType = requirement.EnterpriseType;
            dto.BusinessRange = requirement.BusinessRange;
            dto.WarehouseAccount = requirement.WarehouseAccount;
            dto.InvoiceValue = requirement.InvoiceValue;
            dto.InvoiceIssueDateTime = requirement.InvoiceIssueDateTime;
            dto.InvoiceTransferMode = requirement.InvoiceTransferMode;

            dto.WarehouseState = requirement.WarehouseState;
            dto.WarehouseCity = requirement.WarehouseCity;
            dto.WarehouseAddress1 = requirement.WarehouseAddress1;
            dto.WarehouseAddress2 = requirement.WarehouseAddress2;

            //dto.TradeAmount = requirement.TradeAmount;
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
