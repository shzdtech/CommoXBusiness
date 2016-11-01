using Micro.Future.Commo.Business.Abstraction.BizInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Micro.Future.Commo.Business.Abstraction.BizObject;

namespace Micro.Future.Commo.Business.Requirement.Handler
{
    public class BasisManager : IBasisManager
    {
        public IList<BasisInfo> QueryBasisInfos(string productCode)
        {
            throw new NotImplementedException();
        }
    }
}
