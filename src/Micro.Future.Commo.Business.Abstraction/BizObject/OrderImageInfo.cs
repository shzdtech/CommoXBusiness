using Micro.Future.Commo.Business.Abstraction.BizObject.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Commo.Business.Abstraction.BizObject
{
    public class OrderImageInfo
    {
        public int ImageId { get; set; }

        public int OrderId { get; set; }

        public int TradeId { get; set; }

        /// <summary>
        /// 图片类型
        /// </summary>
        public OrderFileType ImageType { get; set; }

        /// <summary>
        /// 图片路径
        /// </summary>
        public string ImagePath { get; set; }

        /// <summary>
        /// 第几张
        /// </summary>
        public int Position { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }
    }
}
