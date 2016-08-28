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

namespace Micro.Future.Commo.Business.Requirement.Handler
{
    public class ChainManager : IChainManager
    {
        private IChainDAL _chainService = null;
        private ITrade _tradeService = null;
        private IOrder _orderService = null;
        private IMatcher _matcherService = null;

        public ChainManager(IChainDAL chainService, ITrade tradeService, IOrder orderService, IMatcher matcherService)
        {
            _chainService = chainService;
            _tradeService = tradeService;
            _orderService = orderService;
            _matcherService = matcherService;
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

                RequirementManager mgr = new RequirementManager();
                foreach (var reqId in chainObj.RequirementIdChain)
                {
                    var bizResult = mgr.QueryRequirementInfo(reqId);
                    if (bizResult.HasError || bizResult.Result == null)
                        continue;
                    info.Requirements.Add(bizResult.Result);
                }
            }

            return info;
        }

        public IList<RequirementInfo> GetRequirements(int chainId)
        {
            IList<RequirementObject> requirementObjects = _chainService.GetChainRequirements(chainId);
            if (requirementObjects == null || requirementObjects.Count == 0)
                return null;

            List<RequirementInfo> infoList = new List<RequirementInfo>();
            foreach(var requirementObj in requirementObjects)
            {
                infoList.Add(RequirementManager.ConvertToRequirementInfo(requirementObj));
            }

            return infoList;
        }

        public bool LockChain(int chainId)
        {
            return _matcherService.LockMatcherChain(chainId);
        }

        public bool UnlockChain(int chainId)
        {
            return _matcherService.UnLockMatcherChain(chainId);
        }

        public bool ComfirmChain(int chainId,out int tradeId)
        {
            tradeId = 0;
            bool isConfirmed = _matcherService.ConfirmMatcherChain(chainId);
            if(isConfirmed)
            {
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
                foreach (RequirementInfo requirement in chain.Requirements)
                {
                    order = new Order();
                    order.TradeId = tradeId;
                    order.UserId = requirement.UserId;


                    //order.RequirementType = requirement.Type.ToString();
                    order.CreateTime = trade.TradeTime;
                    order.EnterpriseId = requirement.EnterpriseId;
                    order.ModifyTime = trade.TradeTime;


                    _orderService.submitOrder(order);
                }

            }
            return isConfirmed;
        }

        public IList<RequirementChainInfo> QueryChainsByRequirementId(int requirementId, ChainStatusType type)
        {
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

            return ConvertChainObjectsToChainInfoList(chainList);
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

        public IList<RequirementChainInfo> QueryChainsByUserId(string userId, ChainStatusType type)
        {
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

            return ConvertChainObjectsToChainInfoList(chainList);
        }

        public IList<RequirementChainInfo> QueryAllChains(ChainStatusType type)
        {
            bool latestVersion = type == ChainStatusType.OPEN ? true : false;
            IList<ChainObject> chainList = _matcherService.GetMatcherChains((ChainStatus)type, latestVersion);
            return ConvertChainObjectsToChainInfoList(chainList);
        }
    }
}
