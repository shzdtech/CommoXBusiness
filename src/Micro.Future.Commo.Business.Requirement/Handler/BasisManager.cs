using Micro.Future.Commo.Business.Abstraction.BizInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Micro.Future.Commo.Business.Abstraction.BizObject;
using Micro.Future.Business.MongoDB.Commo.MongoInterface;

namespace Micro.Future.Commo.Business.Requirement.Handler
{
    public class BasisManager : IBasisManager
    {
        private IDataVisual _dataVisualHandler = null;
        public BasisManager(IDataVisual dataVisualHandler)
        {
            this._dataVisualHandler = dataVisualHandler;
        }

        public string QueryBasisInfos(string exchange, string productCode, string startDateTime, string endDateTime = null)
        {
            return _dataVisualHandler.getJsonData(exchange, productCode, startDateTime, endDateTime);
        }
    }
}
