using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Commo.Business.Abstraction.BizObject.Enums
{
    /// <summary>
    /// 货物交接方式， 1=入库过户； 2=委托过户；3=提单背书
    /// </summary>
    public enum ProductTransferType
    {
        /// <summary>
        /// 入库过户
        /// </summary>
        Instock = 1,

        /// <summary>
        /// 委托过户
        /// </summary>
        Entrust = 2,

        /// <summary>
        /// 提单背书
        /// </summary>
        BillEndorse = 3
    }
}
