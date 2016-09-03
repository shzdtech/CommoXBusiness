using Micro.Future.Business.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Commo.Business.Abstraction.BizObject
{
    public class CommoBizTResult<TResult> : BizTResult<TResult>
    {
        public CommoBizTResult(TResult result, BizException bizExeption): base(result, bizExeption)
        {
        }
        public CommoBizTResult(TResult result) : base(result)
        {
        }
        public CommoBizTResult(BizException bizExeption) : base(bizExeption)
        {
        }

        /// <summary>
        /// 耗时(毫秒)
        /// </summary>
        public long ElapsedTime { get; set; }
    }
}
