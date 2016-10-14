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
        bool LockChain(string userId, int chainId);

        /// <summary>
        /// 解锁撮合连
        /// </summary>
        /// <param name="chainId"></param>
        /// <returns></returns>
        bool UnlockChain(string userId, int chainId);

        /// <summary>
        /// 确认撮合连，生成订单并且返回tradeId
        /// </summary>
        /// <param name="chainId"></param>
        /// <param name="tradeId"></param>
        /// <returns></returns>
        bool ComfirmChain(string userId, int chainId, out int tradeId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="chainId"></param>
        /// <param name="replacedNodeIndex"></param>
        /// <param name="topN"></param>
        /// <returns></returns>
        IList<RequirementInfo> FindReplacedRequirementsForChain(int chainId, int replacedNodeIndex, int topN = 5);

        /// <summary>
        /// Replace Requirements for chain
        /// </summary>
        /// 给定的Chain必须为锁定状态
        /// <param name="chainId"></param>
        /// <param name="replacedNodeIndexArr"></param>
        /// <param name="replacingRequirementIds"></param>
        /// <returns></returns>
        bool ReplaceRequirementsForChain(int chainId, IList<int> replacedNodeIndexes, IList<int> replacingRequirementIds);


        #region 手动撮合

        /// <summary>
        /// 指定几个需求，撮合一条需求列表
        /// </summary>
        /// <param name="requirementIds">
        /// 指定的一些需求id列表，不能为空，或者全是0。
        /// 固定位置时，留空的需求Id填0.
        /// </param>
        /// <param name="fixedLength">传值表示固定长度</param>
        /// <param name="isPositionFixed">已有的需求是否固定位置。 true表示固定位置；false表示不固定 </param>
        /// <returns></returns>
        IList<RequirementInfo> AutoMatchRequirements(IList<int> requirementIds, int? fixedLength, bool isPositionFixed = false);

        /// <summary>
        /// 指定需求列表，直接生成一条链
        /// </summary>
        /// <param name="requirementids">需求列表，长度必须大于等于3，所有Id不能为0</param>
        /// <param name="opUserId">操作员</param>
        /// <returns></returns>
        RequirementChainInfo CreateChain(IList<int> requirementids, string opUserId);

        #endregion
    }
}
