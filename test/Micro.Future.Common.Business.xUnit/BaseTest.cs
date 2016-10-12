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

            serviceProvider = services.BuildServiceProvider();
        }
    }
}
