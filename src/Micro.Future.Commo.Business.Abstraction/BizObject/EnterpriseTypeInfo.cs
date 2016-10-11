using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Commo.Business.Abstraction.BizObject
{
    public class EnterpriseTypeInfo
    {
        public int BusinessTypeId { get; set; }
        /// <summary>
        /// 企业类型
        /// </summary>
        public string BusinessTypeName { get; set; }
        /// <summary>
        /// 大类ID
        /// </summary>
        public int ParentId { get; set; }
        public int StateId { get; set; }
    }
}
