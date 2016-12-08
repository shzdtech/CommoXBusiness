using Micro.Future.Commo.Business.Abstraction.BizInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Micro.Future.Commo.Business.Abstraction.BizObject;

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

        ///200
        ///
        [Fact]
        public void Test_QueryTradesByEnterprise()
        {
            var trades = _tradeManager.QueryTradesByEnterprise(202, Commo.Business.Abstraction.BizObject.Enums.TradeState.Contract);
        }

        [Fact]
        public void Test_BulkSaveOrderImages()
        {
            IList<OrderImageInfo> imageList = new List<OrderImageInfo>()
            {
                new OrderImageInfo()
                {
                    ImagePath = "/ddd/ddd/dd.jpg",
                    CreateTime = DateTime.Now,
                    ImageType = Commo.Business.Abstraction.BizObject.Enums.OrderImageType.Contract,
                    OrderId = 1,
                    Position = 1,
                    TradeId = 1,
                    UpdateTime = DateTime.Now

                }
            };

            var newImageList = _tradeManager.BulkSaveOrderImages(imageList);
        }
    }
}
