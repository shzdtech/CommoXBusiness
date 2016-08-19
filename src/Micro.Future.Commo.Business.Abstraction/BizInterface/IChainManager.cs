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
        /// 确认撮合链中的一个需求，当所有需求都已确认时，生成订单并返回订单Id
        /// </summary>
        bool ConfirmRequirement(int chainId, int requirementId, out int tradeId);

        IList<RequirementInfo> GetRequirements(int chainId);

        RequirementChainInfo GetChainInfo(int chainId);

        IList<RequirementChainInfo> QueryChains(string userId);
    }
}
