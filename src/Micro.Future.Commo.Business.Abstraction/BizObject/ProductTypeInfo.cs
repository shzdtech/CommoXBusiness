using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Commo.Business.Abstraction.BizObject
{
    public class ProductTypeInfo
    {
        public int ProductTypeId { get; set; }
        /// <summary>
        /// 产品类型
        /// </summary>
        public string ProductTypeName { get; set; }
        /// <summary>
        /// 产品大类
        /// </summary>
        public int ParentId { get; set; }
    }
}
