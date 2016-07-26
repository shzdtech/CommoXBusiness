using Micro.Future.Business.Common;
using Micro.Future.Commo.Business.Abstraction.BizObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Commo.Business.Abstraction.BizInterface
{
    public interface IEnterpriseManager
    {
        EnterpriseInfo QueryEnterpriseInfo(int enterpriseId);

        BizTResult<int> AddEnterprise(EnterpriseInfo enterprise);

        BizTResult<bool> UpdateEnterprise(EnterpriseInfo enterprise);
    }
}
