using Micro.Future.Commo.Business.Abstraction.BizInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Micro.Future.Common.Business.xUnit
{
    public class TradeTests : BaseTest
    {
        private ITradeManager _tradeManager = null;

        public TradeTests()
        {
            _tradeManager = serviceProvider.GetService<ITradeManager>();
        }

        [Fact]
        public void Test_GetTradeInfo()
        {
            int tradeId = 10073;
            var tradeInfo = _tradeManager.GetTradeInfo(tradeId);
            Assert.NotNull(tradeInfo);
            Assert.Equal<int>(tradeInfo.TradeId, tradeId);
        }

        //[Fact]
        public void Test_GetTradeOrders()
        {
            int tradeId = 10084;
            var orders = _tradeManager.GetOrders(tradeId);
            Assert.NotNull(orders);
            Assert.NotEqual<int>(orders.Count, 0);
        }

        [Fact]
        public void Test_GetTrades()
        {
            var trades = _tradeManager.GetTrades(Commo.Business.Abstraction.BizObject.Enums.TradeState.Contract);
        }

    }
}
