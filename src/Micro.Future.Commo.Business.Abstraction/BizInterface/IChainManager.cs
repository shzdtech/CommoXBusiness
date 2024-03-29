﻿using Micro.Future.Commo.Business.Abstraction.BizObject;
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
    }
}
