using Micro.Future.Commo.Business.Abstraction.BizObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Commo.Business.Abstraction.BizInterface
{
    public interface IChainManager
    {
        /// <summary>
        /// 查询撮合链详情
        /// </summary>
        /// <param name="chainId"></param>
        /// <returns></returns>
        RequirementChainInfo GetChainInfo(int chainId);

        /// <summary>
        /// 获取某一个用户下面的所有需求的撮合连
        /// 根据 ChainStatusType=LOCKED 可以获取已锁定的
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        CommoBizTResult<IList<RequirementChainInfo>> QueryChainsByUserId(string userId, ChainStatusType type);

        /// <summary>
        /// 获取某一个用户下面的所有需求的撮合连
        /// 根据 ChainStatusType=LOCKED 可以获取已锁定的
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        CommoBizTResult<IList<RequirementChainInfo>> QueryChainsByEnterpriseId(int enterpriseId, ChainStatusType type);

        /// <summary>
        /// 获取某一个需求的撮合连
        /// 根据 ChainStatusType=LOCKED 可以仅获取已锁定的
        /// </summary>
        /// <param name="requirementId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        CommoBizTResult<IList<RequirementChainInfo>> QueryChainsByRequirementId(int requirementId, ChainStatusType type);

        /// <summary>
        /// 查询所有撮合链，不分用户，不分需求
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        CommoBizTResult<IList<RequirementChainInfo>> QueryAllChains(ChainStatusType type);

        /// <summary>
        /// 锁定撮合连
        /// </summary>
        /// <param name="chainId"></param>
        /// <returns></returns>
        bool LockChain(int chainId);

        /// <summary>
        /// 解锁撮合连
        /// </summary>
        /// <param name="chainId"></param>
        /// <returns></returns>
        bool UnlockChain(int chainId);

        /// <summary>
        /// 确认撮合连，生成订单并且返回tradeId
        /// </summary>
        /// <param name="chainId"></param>
        /// <param name="tradeId"></param>
        /// <returns></returns>
        bool ComfirmChain(int chainId, out int tradeId);
    }
}
