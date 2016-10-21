using Micro.Future.Commo.Business.Abstraction.BizObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Commo.Business.Abstraction.BizInterface
{
    /// <summary>
    /// 承兑汇票管理
    /// </summary>
    public interface IAcceptanceManager
    {
        /// <summary>
        /// 查询承兑汇票
        /// </summary>
        /// <returns></returns>
        IList<AcceptanceInfo> QueryAllAcceptance();


        /// <summary>
        /// 查询承兑汇票
        /// </summary>
        /// <returns></returns>
        AcceptanceInfo QueryAcceptanceInfo(int infoId);

        /// <summary>
        /// 新增承兑汇票
        /// </summary>
        /// <param name="acceptance"></param>
        /// <returns></returns>
        int CreateAcceptance(AcceptanceInfo acceptance);

        /// <summary>
        /// 更新承兑汇票
        /// </summary>
        /// <param name="acceptance"></param>
        /// <returns></returns>
        bool UpdateAcceptance(AcceptanceInfo acceptance);

        /// <summary>
        /// 删除承兑汇票
        /// </summary>
        /// <param name="acceptanceId"></param>
        /// <returns></returns>
        bool DeleteAcceptance(int acceptanceId);
    }
}
