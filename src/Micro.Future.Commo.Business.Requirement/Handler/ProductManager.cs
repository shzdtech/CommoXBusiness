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
        private IProduct _productService = null;

        public ProductManager(IProduct productService)
        {
            _productService = productService;
        }

        public ProductInfo AddProduct(ProductInfo product)
        {
            if (product == null || product.Validate().HasError)
                return null;
            
            Product p = ConvertProductInfoToObject(product);
            var newProduct = _productService.saveProduct(p);
            if (newProduct != null && newProduct.ProductId > 0)
            {
                product.ProductId = newProduct.ProductId;
            }

            return product;
        }

        public IList<ProductInfo> GetAllProducts()
        {
            IList<Product> products = _productService.queryAllProduct();
            if (products == null || products.Count == 0)
                return null;

            List<ProductInfo> productInfoList = new List<ProductInfo>();
            foreach(var p in products)
            {
                productInfoList.Add(ConvertProductToInfo(p));
            }

            return productInfoList;
        }

        public IList<ProductTypeInfo> GetAllProductTypes()
        {
            IList<ProductType> productTypes =  _productService.queryAllProductType();
            if (productTypes == null || productTypes.Count == 0)
                return null;

            IList<ProductTypeInfo> typeInfoList = new List<ProductTypeInfo>();
            foreach(ProductType pType in productTypes)
            {
                ProductTypeInfo typeInfo = new ProductTypeInfo();
                typeInfo.ProductTypeId = pType.ProductTypeId;
                typeInfo.ProductTypeName = pType.ProductTypeName;
                typeInfo.ParentId = pType.ParentId;
                typeInfoList.Add(typeInfo);
            }

            return typeInfoList;
        }

        public ProductInfo GetProductInfo(int productId)
        {
            Product p = _productService.queryProduct(productId);
            if (p == null)
                return null;

            return ConvertProductToInfo(p);
        }

        public IList<ProductInfo> GetProductsByType(int productTypeId)
        {
            IList<Product> products = _productService.queryProductByType(productTypeId);
            if (products == null || products.Count == 0)
                return null;

            List<ProductInfo> productInfoList = new List<ProductInfo>();
            foreach (var p in products)
            {
                productInfoList.Add(ConvertProductToInfo(p));
            }

            return productInfoList;
        }

        public bool UpdateProductInfo(ProductInfo product)
        {
            Product p = ConvertProductInfoToObject(product);
            Product newProd = _productService.updateProduct(p);
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
