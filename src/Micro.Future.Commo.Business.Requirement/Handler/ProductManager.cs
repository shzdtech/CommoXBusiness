using Micro.Future.Commo.Business.Abstraction.BizInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Micro.Future.Commo.Business.Abstraction.BizObject;
using Micro.Future.Business.DataAccess.Commo.CommonInterface;
using Micro.Future.Business.DataAccess.Commo.CommoHandler;
using Micro.Future.Business.DataAccess.Commo.CommoObject;

namespace Micro.Future.Commo.Business.Requirement.Handler
{
    public class ProductManager : IProductManager
    {
        public ProductInfo AddProduct(ProductInfo product)
        {
            if (product == null || product.Validate().HasError)
                return null;

            IProduct productHandler = new ProductHandler();
            Product p = ConvertProductInfoToObject(product);
            var newProduct = productHandler.saveProduct(p);
            if (newProduct != null && newProduct.ProductId > 0)
            {
                product.ProductId = newProduct.ProductId;
            }

            return product;
        }

        public IList<ProductInfo> GetProduct(int userId, string productType)
        {
            throw new NotImplementedException();
        }

        public ProductInfo GetProductInfo(int productId)
        {
            IProduct handler = new ProductHandler();
            Product p = handler.queryProduct(productId);
            if (p == null)
                return null;

            return ConvertProductToInfo(p);
        }

        public bool UpdateProductInfo(ProductInfo product)
        {
            IProduct handler = new ProductHandler();
            Product p = ConvertProductInfoToObject(product);
            Product newProd = handler.updateProduct(p);
            return newProd != null;
        }

        private Product ConvertProductInfoToObject(ProductInfo info)
        {
            return new Product()
            {
                LimitedQuota = info.LimitedQuota,
                Price = info.Price,
                ProductId = info.ProductId,
                ProductName = info.ProductName,
                ProductTypeId = info.ProductTypeId,
                StateId = info.StateId
            };
        }

        private ProductInfo ConvertProductToInfo(Product product)
        {
            return new ProductInfo()
            {
                LimitedQuota = product.LimitedQuota,
                Price = product.Price,
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                ProductTypeId = product.ProductTypeId,
                StateId = product.StateId
            };
        }
    }
}
