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
        /// 创建一个空链
        /// </summary>
        /// <param name="length">链的长度</param>
        /// <returns></returns>
        RequirementChainInfo CreateEmptyChain(int length = 3);

        /// <summary>
        /// 手动向链中添加一个新需求
        /// </summary>
        /// <param name="newRequirement">新需求</param>
        /// <param name="chainId">链Id</param>
        /// <param name="position">需求位置pos</param>
        /// <returns></returns>
        bool AddChainRequirement(RequirementInfo newRequirement, int chainId, int position);

        /// <summary>
        /// 手动向链中添加一个已创建的需求
        /// </summary>
        /// <param name="requirementId">需求Id</param>
        /// <param name="chainId">链id</param>
        /// <param name="position">需求位置position</param>
        /// <returns></returns>
        bool AddChainRequirement(int requirementId, int chainId, int position);

        /// <summary>
        /// 链中指定一个位置，撮合匹配的需求，返回最有5条结果
        /// </summary>
        /// <param name="chain"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        IList<RequirementInfo> FindChainMatchRequirements(int chain, int position);

        /// <summary>
        /// 撮合空链，自动撮合并补充空缺的需求，返回一条完整撮合链
        /// </summary>
        /// <param name="chainId"></param>
        /// <returns></returns>
        RequirementChainInfo AutoMatchEmptyChain(int chainId);

        #endregion
    }
}
