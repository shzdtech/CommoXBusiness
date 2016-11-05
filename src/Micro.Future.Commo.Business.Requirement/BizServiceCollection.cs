using Micro.Future.Business.DataAccess.Commo;
using Micro.Future.Business.DataAccess.Commo.CommoHandler;
using Micro.Future.Business.DataAccess.Commo.CommonInterface;
using Micro.Future.Business.MatchMaker.Abstraction.Models;
using Micro.Future.Business.MatchMaker.Commo.Models;
using Micro.Future.Business.MongoDB.Commo.Config;
using Micro.Future.Business.MongoDB.Commo.Handler;
using Micro.Future.Business.MongoDB.Commo.MongoInterface;
using Micro.Future.Commo.Business.Abstraction.BizObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
            services.AddTransient<IEmailVerifyCode, EmailVerifyCodeHandler>();
            services.AddTransient<IOperationRecord, OperationRecordHandler>();
            services.AddTransient<IFinancialProduct, FinancialProductHandler>();

            services.AddTransient<IAcceptance, AcceptanceHandler>();

            //mongodb
            services.AddTransient<IChainDAL, ChainDAL>();
            services.AddTransient<IMatcher, MatcherHandler>();
            services.AddTransient<BaseMatchMaker, RankingMatchMaker>();
            services.AddTransient<IDataVisual, DataVisualHandler>();

            return services;
        }
    }
}
