using Micro.Future.Commo.Business.Abstraction.BizObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Commo.Business.Abstraction.BizInterface
{
    public interface IAcceptanceBillManager
    {
        /// <summary>
        /// 查询所有承兑汇票
        /// </summary>
        /// <returns></returns>
        IList<AcceptanceBillInfo> QueryAllAcceptances(int enterpriseId);

        /// <summary>
        /// 查询单个承兑汇票
        /// </summary>
        /// <param name="acceptanceId"></param>
        /// <returns></returns>
        AcceptanceBillInfo QueryAcceptance(int acceptanceId);

        /// <summary>
        /// 新增承兑汇票
        /// </summary>
        /// <param name="acceptance"></param>
        /// <returns></returns>
        AcceptanceBillInfo CreateAcceptance(AcceptanceBillInfo acceptance);

        /// <summary>
        /// 更新承兑汇票信息
        /// </summary>
        /// <param name="acceptance"></param>
        /// <returns></returns>
        bool UpdateAcceptance(AcceptanceBillInfo acceptance);

        /// <summary>
        /// 删除承兑汇票
        /// </summary>
        /// <param name="acceptanceId"></param>
        /// <returns></returns>
        bool DeleteAcceptance(int acceptanceId);
    }
}
