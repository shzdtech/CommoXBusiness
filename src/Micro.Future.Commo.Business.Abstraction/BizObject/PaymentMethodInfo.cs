using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Commo.Business.Abstraction.BizObject
{
    public class PaymentMethodInfo
    {
        public int PaymentMethodId { get; set; }
        /// <summary>
        /// 支付方式
        /// </summary>
        public string PaymentMethodName { get; set; }
        public int StateId { get; set; }
    }
}
