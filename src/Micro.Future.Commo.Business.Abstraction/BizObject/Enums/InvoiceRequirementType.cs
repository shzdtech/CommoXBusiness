using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Commo.Business.Abstraction.BizObject.Enums
{
    public enum InvoiceRequirementType
    {
        /// <summary>
        /// 不限
        /// </summary>
        None = 0,

        /// <summary>
        /// 款货后票
        /// </summary>
        GoodsFirst = 1,

        /// <summary>
        /// 款货（实提数）后票
        /// </summary>
        GoodsFirstLift = 2,
    }
}
