using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Commo.Business.Abstraction.BizObject
{
    public enum EnterpriseStateType
    {
        /// <summary>
        /// 未提交审核
        /// </summary>
        UNSUBMITED = 0,

        /// <summary>
        /// 未审核
        /// </summary>
        UNAPPROVED = 1,

        /// <summary>
        /// 已审核
        /// </summary>
        APPROVED = 2,

        /// <summary>
        /// 审核失败
        /// </summary>
        REJECTED = 3
    }
}
