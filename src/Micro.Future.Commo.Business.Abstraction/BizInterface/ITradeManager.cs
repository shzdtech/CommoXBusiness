using Micro.Future.Commo.Business.Abstraction.BizObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Commo.Business.Abstraction.BizInterface
{
    public interface ITradeManager
    {
        TradeInfo GetTradeInfo(int tradeId);

        IList<TradeInfo> GetTrades(int userId);

        IList<OrderInfo> GetOrders(int tradeId);

        OrderInfo GetOrderInfo(int orderId);

        bool UpdateTradeState(int tradeId, string state);
    }
}
