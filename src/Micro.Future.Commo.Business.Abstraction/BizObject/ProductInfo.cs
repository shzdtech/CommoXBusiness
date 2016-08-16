using Micro.Future.Commo.Business.Abstraction.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Commo.Business.Abstraction.BizObject
{
    public class ProductInfo : AbstractValidation
    {
        public int ProductId { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 产品类型
        /// </summary>
        public string ProductTypeId { get; set; }
        /// <summary>
        /// 产品价格
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// 产品限额量
        /// </summary>
        public double LimitedQuota { get; set; }

        public int StateId { get; set; }

        /// <summary>
        /// 提交时间
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// 最后一次修改时间
        /// </summary>
        public DateTime Modified { get; set; }

        public override ValidationResult Validate()
        {
            ValidationResult result = new ValidationResult();

            Dictionary<string, string> dictErrors = new Dictionary<string, string>();

            if(string.IsNullOrWhiteSpace(ProductName))
            {
                dictErrors.Add("ProductName", "产品名称不能为空！");
            }

            if (string.IsNullOrWhiteSpace(ProductTypeId))
            {
                dictErrors.Add("ProductTypeId", "请选择产品类型！");
            }

            if (Price<=0d)
            {
                dictErrors.Add("Price", "产品价格必须大于0！");
            }

            result.FieldsErrors = dictErrors;
            return result;
        }
    }
}
