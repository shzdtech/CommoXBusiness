using Micro.Future.Commo.Business.Abstraction.BizObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Commo.Business.Abstraction.BizInterface
{
    public interface IProductManager
    {
        /// <summary>
        /// 产品提交
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        ProductInfo AddProduct(ProductInfo product);

        /// <summary>
        /// 产品根据productId查询
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        ProductInfo GetProductInfo(int productId);

        /// <summary>
        /// 查询所有的产品列表
        /// </summary>
        /// <returns></returns>
        IList<ProductInfo> GetProduct(string userId, string productType);

        bool UpdateProductInfo(ProductInfo product);
    }
}
