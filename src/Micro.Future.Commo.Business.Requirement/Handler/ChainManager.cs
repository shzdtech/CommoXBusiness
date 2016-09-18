using Micro.Future.Commo.Business.Abstraction.BizInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Micro.Future.Commo.Business.Abstraction.BizObject;
using Micro.Future.Business.DataAccess.Commo.CommoObject;
using Micro.Future.Business.DataAccess.Commo.CommonInterface;
using Micro.Future.Business.DataAccess.Commo.CommoHandler;
using Micro.Future.Business.Common;
using Micro.Future.Business.MongoDB.Commo.BizObjects;

using mongoDB = Micro.Future.Business.MongoDB.Commo;
using Micro.Future.Business.MongoDB.Commo.MongoInterface;
using Micro.Future.Business.MongoDB.Commo.Handler;
using Micro.Future.Commo.Business.Abstraction.Handler;

namespace Micro.Future.Commo.Business.Requirement.Handler
{
    public class ChainManager : BaseBizHandler, IChainManager
    {
        private IChainDAL _chainService = null;
        private ITrade _tradeService = null;
        private IOrder _orderService = null;
        private IMatcher _matcherService = null;
        private IOperationRecord _operatorService = null;


        public ChainManager(IChainDAL chainService, 
            ITrade tradeService, 
            IOrder orderService, 
            IMatcher matcherService,
            IOperationRecord operatorService)
        {
            _chainService = chainService;
            _tradeService = tradeService;
            _orderService = orderService;
            _matcherService = matcherService;
            _operatorService = operatorService;
        }
        

        public RequirementChainInfo GetChainInfo(int chainId)
        {
            ChainObject chainObj = _chainService.GetChain(chainId);
            if (chainObj == null)
                return null;
            return ConvertChainObjectToInfo(chainObj);
        }

        private RequirementChainInfo ConvertChainObjectToInfo(ChainObject chainObj)
        {
            RequirementChainInfo info = new RequirementChainInfo();
            info.ChainId = chainObj.ChainId;
            info.CreateTime = chainObj.CreateTime;
            info.ModifyTime = chainObj.ModifyTime;
            info.IsDeleted = chainObj.Deleted;
            info.Version = chainObj.Version;
            info.ChainStatus = (ChainStatusType)chainObj.ChainStateId;
            
            if(chainObj.RequirementIdChain!=null && chainObj.RequirementIdChain.Count > 0)
            {
                info.Requirements = new List<RequirementInfo>();

                foreach (var reqId in chainObj.RequirementIdChain)
                {
                    var requirement = _matcherService.QueryRequirementInfo(reqId);
                    if (requirement == null)
                        continue;
                    
                    info.Requirements.Add(RequirementManager.ConvertToRequirementInfo(requirement));
                }
            }

            return info;
        }

        public bool LockChain(string userId, int chainId)
        {
            bool result = _matcherService.LockMatcherChain(chainId,userId);
            if (result)
            {
                OperationRecord record = new OperationRecord();
                record.UserId = userId;
                record.ObjectType = "Chain";
                record.ObjectValue = chainId.ToString();
                record.Operation = "1";
                _operatorService.AddOperationRecord(record);
            }
           
            return result;
        }

        public bool UnlockChain(string userId, int chainId)
        {
            bool result = _matcherService.UnLockMatcherChain(chainId,userId);
            if (result)
            {
                OperationRecord record = new OperationRecord();
                record.UserId = userId;
                record.ObjectType = "Chain";
                record.ObjectValue = chainId.ToString();
                record.Operation = "2";
                _operatorService.AddOperationRecord(record);
            }
            return result;
        }

        public bool ComfirmChain(string userId, int chainId,out int tradeId)
        {
            tradeId = 0;
            bool isConfirmed = _matcherService.ConfirmMatcherChain(chainId,userId);
            if(isConfirmed)
            {
                OperationRecord record = new OperationRecord();
                record.UserId = userId;
                record.ObjectType = "Chain";
                record.ObjectValue = chainId.ToString();
                record.Operation = "3";
                _operatorService.AddOperationRecord(record);

                //生成订单
                RequirementChainInfo chain = GetChainInfo(chainId);

                Trade trade = new Trade();
                trade.TradeTime = DateTime.Now;

                Trade newTrade = _tradeService.submitTrade(trade);
                if (newTrade == null || newTrade.TradeId <= 0)
                {
                    throw new BizException("生产订单失败！");
                }

                tradeId = newTrade.TradeId;

                Order order = null;
                int sequence = 0;
                foreach (RequirementInfo requirement in chain.Requirements)
                {
                    sequence += 1;
                    order = new Order()
                    {
                        BusinessRange = requirement.BusinessRange,
                        CreateTime = trade.TradeTime,
                        EnterpriseId = requirement.EnterpriseId,
                        EnterpriseType = requirement.EnterpriseType,
                        InvoiceIssueDateTime = requirement.InvoiceIssueDateTime,
                        InvoiceTransferMode = requirement.InvoiceTransferMode,
                        InvoiceValue = requirement.InvoiceValue,
                        ModifyTime = DateTime.Now,
                        OrderStateId = (int)OrderStatusType.WAITING,
                        PaymentAmount = requirement.PaymentAmount,
                        PaymentDateTime = requirement.PaymentDateTime,
                        PaymentType = requirement.PaymentType,
                        ProductName = requirement.ProductName,
                        ProductPrice = requirement.ProductPrice,
                        ProductQuantity = requirement.ProductQuantity,
                        ProductSpecification = requirement.ProductSpecification,
                        ProductType = requirement.ProductType,
                        ProductUnit = requirement.ProductUnit,
                        RequirementId = requirement.RequirementId,
                        RequirementRemarks = requirement.RequirementRemarks,
                        RequirementStateId = (int)requirement.State,
                        RequirementTypeId = (int)requirement.Type,
                        Subsidies = requirement.Subsidies,
                        TradeAmount = requirement.TradeAmount,
                        TradeId = tradeId,
                        TradeProfit = requirement.TradeProfit,
                        UserId = requirement.UserId,
                        WarehouseAccount = requirement.WarehouseAccount,
                        WarehouseAddress1 = requirement.WarehouseAddress1,
                        WarehouseAddress2 = requirement.WarehouseAddress2,
                        WarehouseCity = requirement.WarehouseCity,
                        WarehouseState = requirement.WarehouseState,
                        TradeSequence = sequence
                    };

                    order.CreateTime = trade.TradeTime;
                    order.ModifyTime = trade.TradeTime;

                    _orderService.submitOrder(order);
                }

            }
            return isConfirmed;
        }

        public CommoBizTResult<IList<RequirementChainInfo>> QueryChainsByRequirementId(int requirementId, ChainStatusType type)
        {
            _stopwatch.Restart();

            IList<RequirementChainInfo> chainInfoList = null;
            BizException bizException = null;


            bool latestVersion = type == ChainStatusType.OPEN ? true : false;
            IList<ChainObject> chainList = _matcherService.GetMatcherChainsByRequirementId(requirementId, (ChainStatus)type, latestVersion);

            foreach(var chainObject in chainList)
            {
                if (chainObject.RequirementIdChain == null || chainObject.RequirementIdChain.Count == 0)
                    continue;

                int totalRequirements = chainObject.RequirementIdChain.Count;
                int findRequirementPos = chainObject.RequirementIdChain.FindIndex(f => f == requirementId);

                int upstreamIndex = findRequirementPos - 1;
                int downstreamIndex = findRequirementPos + 1;

                List<int> newRequirementIds = new List<int>();
                List<string> newUserIds = new List<string>();

                if (upstreamIndex >= 0)
                {
                    newRequirementIds.Add(chainObject.RequirementIdChain[upstreamIndex]);
                    newUserIds.Add(chainObject.UserIdChain[upstreamIndex]);
                }

                newRequirementIds.Add(chainObject.RequirementIdChain[findRequirementPos]);
                newUserIds.Add(chainObject.UserIdChain[findRequirementPos]);

                if (downstreamIndex <= totalRequirements-1)
                {
                    newRequirementIds.Add(chainObject.RequirementIdChain[downstreamIndex]);
                    newUserIds.Add(chainObject.UserIdChain[downstreamIndex]);
                }

                chainObject.RequirementIdChain = newRequirementIds;
                chainObject.UserIdChain = newUserIds;
            }

            chainInfoList = ConvertChainObjectsToChainInfoList(chainList);

            _stopwatch.Stop();

            CommoBizTResult<IList<RequirementChainInfo>> bizResult = new CommoBizTResult<IList<RequirementChainInfo>>(chainInfoList, bizException);
            bizResult.ElapsedTime = _stopwatch.ElapsedMilliseconds;

            return bizResult;
        }

        private IList<RequirementChainInfo> ConvertChainObjectsToChainInfoList(IList<ChainObject> chainObjects)
        {
            if (chainObjects == null || chainObjects.Count == 0)
                return null;

            IList<RequirementChainInfo> infoList = new List<RequirementChainInfo>();
            foreach (var chainObject in chainObjects)
            {
                infoList.Add(ConvertChainObjectToInfo(chainObject));
            }

            return infoList;
        }

        public CommoBizTResult<IList<RequirementChainInfo>> QueryChainsByUserId(string userId, ChainStatusType type)
        {
            _stopwatch.Restart();

            IList<RequirementChainInfo> chainInfoList = null;
            BizException bizException = null;


            bool latestVersion = type == ChainStatusType.OPEN ? true : false;
            IList<ChainObject> chainList = _matcherService.GetMatcherChainsByUserId(userId, (ChainStatus)type, latestVersion);

            foreach (var chainObject in chainList)
            {
                if (chainObject.UserIdChain == null || chainObject.UserIdChain.Count == 0)
                    continue;

                int totalUsers = chainObject.UserIdChain.Count;
                int findUserIndex = chainObject.UserIdChain.FindIndex(f => f == userId);

                int upstreamIndex = findUserIndex - 1;
                int downstreamIndex = findUserIndex + 1;

                List<int> newRequirementIds = new List<int>();
                List<string> newUserIds = new List<string>();

                if (upstreamIndex >= 0)
                {
                    newRequirementIds.Add(chainObject.RequirementIdChain[upstreamIndex]);
                    newUserIds.Add(chainObject.UserIdChain[upstreamIndex]);
                }

                newRequirementIds.Add(chainObject.RequirementIdChain[findUserIndex]);
                newUserIds.Add(chainObject.UserIdChain[findUserIndex]);

                if (downstreamIndex <= totalUsers - 1)
                {
                    newRequirementIds.Add(chainObject.RequirementIdChain[downstreamIndex]);
                    newUserIds.Add(chainObject.UserIdChain[downstreamIndex]);
                }

                chainObject.RequirementIdChain = newRequirementIds;
                chainObject.UserIdChain = newUserIds;
            }

            chainInfoList = ConvertChainObjectsToChainInfoList(chainList);


            _stopwatch.Stop();

            CommoBizTResult<IList<RequirementChainInfo>> bizResult = new CommoBizTResult<IList<RequirementChainInfo>>(chainInfoList, bizException);
            bizResult.ElapsedTime = _stopwatch.ElapsedMilliseconds;

            return bizResult;
        }

        public CommoBizTResult<IList<RequirementChainInfo>> QueryAllChains(ChainStatusType type)
        {
            _stopwatch.Restart();

            IList<RequirementChainInfo> chainInfoList = null;
            BizException bizException = null;

            bool latestVersion = type == ChainStatusType.OPEN ? true : false;
            IList<ChainObject> chainList = _matcherService.GetMatcherChains((ChainStatus)type, latestVersion);
            chainInfoList = ConvertChainObjectsToChainInfoList(chainList);

            _stopwatch.Stop();

            CommoBizTResult<IList<RequirementChainInfo>> bizResult = new CommoBizTResult<IList<RequirementChainInfo>>(chainInfoList, bizException);
            bizResult.ElapsedTime = _stopwatch.ElapsedMilliseconds;

            return bizResult;
        }

        public CommoBizTResult<IList<RequirementChainInfo>> QueryChainsByEnterpriseId(int enterpriseId, ChainStatusType type)
        {

            bool latestVersion = type == ChainStatusType.OPEN ? true : false;

            _stopwatch.Restart();

            IList<RequirementChainInfo> chainInfoList = null;
            BizException bizException = null;

            IList<ChainObject> chainList = _matcherService.GetMatcherChainsByRequirementId(enterpriseId,(ChainStatus)type, latestVersion);
            chainInfoList = ConvertChainObjectsToChainInfoList(chainList);

            _stopwatch.Stop();

            CommoBizTResult<IList<RequirementChainInfo>> bizResult = new CommoBizTResult<IList<RequirementChainInfo>>(chainInfoList, bizException);
            bizResult.ElapsedTime = _stopwatch.ElapsedMilliseconds;

            return bizResult;
        }
    }
}
