﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Commo.Business.Abstraction.BizObject
{
    /// <summary>
    /// 撮合链
    /// </summary>
    public class RequirementChainInfo
    {
        /// <summary>
        /// 撮合链ID
        /// </summary>
        public int ChainId { get; set; }
        
        /// <summary>
        /// 撮合链中的需求
        /// </summary>
        public List<RequirementInfo> Requirements { get; set; }

        /// <summary>
        /// 撮合链创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 撮合链最后一次修改时间
        /// </summary>
        public DateTime ModifyTime { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}