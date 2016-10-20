using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Commo.Business.Abstraction.BizObject
{
    public class BizServiceOptions
    {
        public string ConnectionString { get; set; }

        /// <summary>
        /// 连接串
        /// </summary>
        public string MongoDBConnectionString { get; set; }

        /// <summary>
        /// Production
        /// </summary>
        public string MongoDBName { get; set; }
    }
}
