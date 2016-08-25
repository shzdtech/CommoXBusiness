using Micro.Future.Business.DataAccess.Commo;
using Micro.Future.Business.DataAccess.Commo.CommoHandler;
using Micro.Future.Business.DataAccess.Commo.CommonInterface;
using Micro.Future.Business.MongoDB.Commo.Handler;
using Micro.Future.Business.MongoDB.Commo.MongoInterface;
using Micro.Future.Commo.Business.Abstraction.BizObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Commo.Business.Requirement
{
    public static class BizServiceCollection
    {
        public static IServiceCollection AddBizServices(this IServiceCollection services, BizServiceOptions bizOptions)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            //sql server
            services.AddDbContext<CommoXContext>(options => options.UseSqlServer(bizOptions.ConnectionString));

            services.AddTransient<ICommon, CommonHandler>();
            services.AddTransient<IProduct, ProductHandler>();
            services.AddTransient<IEnterprise, EnterpriseHandler>();
            services.AddTransient<ITrade, TradeHandler>();
            services.AddTransient<IOrder, OrderHandler>();
            services.AddTransient<IRequirement, RequirementHandler>();

            //mongodb
            services.AddTransient<IChainDAL, ChainDAL>();
            services.AddTransient<IMatcher, MatcherHandler>();

            return services;
        }
    }
}
