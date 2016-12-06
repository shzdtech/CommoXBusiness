using Micro.Future.Business.DataAccess.Commo.CommoHandler;
using Micro.Future.Business.DataAccess.Commo.CommonInterface;
using Micro.Future.Business.MongoDB.Commo.Config;
using Micro.Future.Commo.Business.Abstraction.BizInterface;
using Micro.Future.Commo.Business.Abstraction.BizObject;
using Micro.Future.Commo.Business.Requirement;
using Micro.Future.Commo.Business.Requirement.Handler;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Common.Business.xUnit
{
    public class BaseTest
    {
        protected IServiceProvider serviceProvider = null;
        protected IServiceCollection services = null;

        private static readonly string connectionString = "Server=114.55.54.144;UID=sa;password=shzdtech!123;database=Commo-test;";

        public BaseTest()
        {

            MongoDBConfig.mongoAddr = "mongodb://root:Xhmz372701@114.55.54.144:3718";
            MongoDBConfig.DATABASE = "releaseTest1";

            services = new ServiceCollection();

            BizServiceOptions options = new BizServiceOptions()
            {
                ConnectionString = connectionString
            };

            services.AddBizServices(options);

            //for testing use
            services.AddTransient<IChainManager, ChainManager>();
            services.AddTransient<IEnterpriseManager, EnterpriseManager>();
            services.AddTransient<ITradeManager, TradeManager>();
            services.AddTransient<IMatchMakerManager, MatchMakerManager>();
            services.AddTransient<IRequirementManager, RequirementManager>();

            services.AddTransient<IAcceptanceManager, AcceptanceManager>();
            services.AddTransient<IFinancialProductManager, FinancialProductManager>();

            services.AddTransient<IAcceptanceBankManager, AcceptanceBankManager>();

            serviceProvider = services.BuildServiceProvider();
        }
    }
}
