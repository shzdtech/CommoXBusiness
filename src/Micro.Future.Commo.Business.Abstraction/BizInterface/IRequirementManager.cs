using Micro.Future.Business.Common;
using Micro.Future.Commo.Business.Abstraction.BizObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Commo.Business.Abstraction.BizInterface
{
    public interface IRequirementManager
    {
        event Action<IList<RequirementChainInfo>> OnChainChanged;

        BizTResult<IEnumerable<RequirementInfo>> QueryAllRequirements();

        BizTResult<IEnumerable<RequirementInfo>> QueryRequirements(string userId);

        

        BizTResult<RequirementInfo> QueryRequirementInfo(int requirementId);

        BizTResult<RequirementInfo> AddRequirementInfo(RequirementInfo requirement);

        BizTResult<IEnumerable<RequirementChainInfo>> QueryRequirementChains(int requirementId);

        /// <summary>
        /// 查询需求，根据RequirementType、ProductName、ProductType等信息查询
        /// </summary>
        /// <param name="searchCriteria">查询参数</param>
        /// <returns></returns>
        SearchResult<RequirementInfo> SearchRequirements(RequirementSearchCriteria searchCriteria);

        /// <summary>
        /// 根据企业Id查询需求
        /// </summary>
        /// <param name="enterpriseId"></param>
        /// <returns></returns>
        IList<RequirementInfo> QueryRequirementsByEnterpriseId(int enterpriseId, RequirementState state);
    }
}
