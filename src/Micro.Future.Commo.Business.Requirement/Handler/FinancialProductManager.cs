using Micro.Future.Commo.Business.Abstraction.BizInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Micro.Future.Commo.Business.Abstraction.BizObject;

namespace Micro.Future.Commo.Business.Requirement.Handler
{
    public class FinancialProductManager : IFinancialProductManager
    {
        public int CreateFinancialProduct(FinancialProductInfo productInfo)
        {
            throw new NotImplementedException();
        }

        public bool DeleteFinancialProduct(int productId)
        {
            throw new NotImplementedException();
        }

        public IList<FinancialProductInfo> QueryAllFinancialProducts()
        {
            throw new NotImplementedException();
        }

        public bool UpdateFinancialProduct(FinancialProductInfo productInfo)
        {
            throw new NotImplementedException();
        }
    }
}
