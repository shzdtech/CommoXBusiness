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
        public bool ConfirmRequirement(int chainId, int requirementId, out int tradeId)
        {
            tradeId = 0;

            IChainDAL chainDAL = new ChainDAL();

            bool isAllConfirmed = false;
            bool isConfirmed = chainDAL.ConfirmChainRequirement(chainId, requirementId, out isAllConfirmed);
            if(isConfirmed && isAllConfirmed)
            {
                //生成订单
                RequirementChainInfo chain = GetChainInfo(chainId);

                ITrade tradeHandler = new TradeHandler();
                IOrder orderHander = new OrderHandler();

                Trade trade = new Trade();
                trade.TradeTime = DateTime.Now;

                Trade newTrade = tradeHandler.submitTrade(trade);
                if (newTrade == null || newTrade.TradeId <= 0)
                {
                    throw new BizException("生产订单失败！");
                }

                tradeId = newTrade.TradeId;

                Order order = null;
                foreach(RequirementInfo requirement in chain.Requirements)
                {
                    order = new Order();
                    order.TradeId = tradeId;
                    order.UserId = requirement.UserId;
                    order.RequirementType = requirement.Type.ToString();
                    order.CreateTime = trade.TradeTime;
                    order.EnterpriseId = requirement.EnterpriseId;
                    order.ModifyTime = trade.TradeTime;
                    orderHander.submitOrder(order);
                }
            }

            return isConfirmed;
        }

        public RequirementChainInfo GetChainInfo(int chainId)
        {
            IChainDAL chainDAL = new ChainDAL();
            ChainObject chainObj = chainDAL.GetChain(chainId);
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
            IChainDAL chainDAL = new ChainDAL();
            IList<RequirementObject> requirementObjects = chainDAL.GetChainRequirements(chainId);
            if (requirementObjects == null || requirementObjects.Count == 0)
                return null;

            List<RequirementInfo> infoList = new List<RequirementInfo>();
            foreach(var requirementObj in requirementObjects)
            {
                infoList.Add(RequirementManager.ConvertToRequirementInfo(requirementObj));
            }

            return infoList;
        }

        public IList<RequirementChainInfo> QueryChains(int userId)
        {
            IChainDAL chainDAL = new ChainDAL();
            IList<ChainObject> chainList = chainDAL.QueryChains(userId);
            if (chainList == null || chainList.Count == 0)
                return null;

            IList<RequirementChainInfo> infoList = new List<RequirementChainInfo>();
            foreach(var chainObj in chainList)
            {
                infoList.Add(ConvertChainObjectToInfo(chainObj));
            }
            return infoList;
        }
    }
}
