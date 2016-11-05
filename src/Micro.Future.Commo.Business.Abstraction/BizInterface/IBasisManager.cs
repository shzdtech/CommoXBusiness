using Micro.Future.Commo.Business.Abstraction.BizObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Commo.Business.Abstraction.BizInterface
{
    public interface IBasisManager
    {
        string QueryBasisInfos(string exchange, string productCode, string startDateTime, string endDateTime = null);
    }
}
