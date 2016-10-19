using Micro.Future.Commo.Business.Abstraction.BizInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Micro.Future.Commo.Business.Abstraction.BizObject;
using Micro.Future.Business.DataAccess.Commo.CommonInterface;
using Micro.Future.Business.DataAccess.Commo.CommoObject;

namespace Micro.Future.Commo.Business.Requirement.Handler
{
    public class FinancialProductManager : IFinancialProductManager
    {
        private IFinancialProduct _financialDAO = null;

        public FinancialProductManager(IFinancialProduct financialDAO)
        {
            _financialDAO = financialDAO;
        }

        public int CreateFinancialProduct(FinancialProductInfo productInfo)
        {
            var newProduct = _financialDAO.CreateFinancialProduct(new FinancialProduct()
            {
                BankAddress = productInfo.BankAddress,
                CreatedTime = DateTime.Now,
                IsDeleted = false,
                ProductTerm = productInfo.ProductTerm,
                ProductYield = productInfo.ProductYield,
                UpdatedTime = DateTime.Now
            });

            return newProduct.ProductId;
        }

        public bool DeleteFinancialProduct(int productId)
        {
            return _financialDAO.DeleteFinancialProduct(productId);
        }

        public IList<FinancialProductInfo> QueryAllFinancialProducts()
        {
            var productList = _financialDAO.QueryAllFinancialProducts();
            if (productList == null || productList.Count == 0)
                return null;

            IList<FinancialProductInfo> infoList = new List<FinancialProductInfo>();
            foreach(var p in productList)
            {
                infoList.Add(new FinancialProductInfo()
                {
                    BankAddress = p.BankAddress,
                    CreatedTime = p.CreatedTime,
                    IsDeleted = p.IsDeleted,
                    ProductId = p.ProductId,
                    ProductTerm = p.ProductTerm,
                    ProductYield = p.ProductYield,
                    UpdatedTime = p.UpdatedTime
                });
            }

            return infoList;
        }

        public bool UpdateFinancialProduct(FinancialProductInfo productInfo)
        {
           return _financialDAO.UpdateFinancialProduct(new FinancialProduct()
            {
                ProductId = productInfo.ProductId,
                BankAddress = productInfo.BankAddress,
                IsDeleted = productInfo.IsDeleted,
                ProductTerm = productInfo.ProductTerm,
                ProductYield = productInfo.ProductYield,
                UpdatedTime = DateTime.Now
            });
        }

    }
}
