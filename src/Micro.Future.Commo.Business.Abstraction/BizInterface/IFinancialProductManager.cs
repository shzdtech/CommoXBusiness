using Micro.Future.Commo.Business.Abstraction.BizObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Commo.Business.Abstraction.BizInterface
{
    /// <summary>
    /// 理财产品管理
    /// </summary>
    public interface IFinancialProductManager
    {
        /// <summary>
        /// 查询所有理财产品
        /// </summary>
        /// <returns></returns>
        IList<FinancialProductInfo> QueryAllFinancialProducts();

        /// <summary>
        /// 新增理财产品
        /// </summary>
        /// <param name="productInfo"></param>
        /// <returns></returns>
        int CreateFinancialProduct(FinancialProductInfo productInfo);

        /// <summary>
        /// 更新理财产品信息
        /// </summary>
        /// <param name="productInfo"></param>
        /// <returns></returns>
        bool UpdateFinancialProduct(FinancialProductInfo productInfo);

        /// <summary>
        /// 删除理财产品
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        bool DeleteFinancialProduct(int productId);
    }
}
