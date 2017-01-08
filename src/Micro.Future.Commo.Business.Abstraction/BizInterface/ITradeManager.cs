using Micro.Future.Commo.Business.Abstraction.BizObject;
using Micro.Future.Commo.Business.Abstraction.BizObject.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Commo.Business.Abstraction.BizInterface
{
    public interface ITradeManager
    {
        TradeInfo GetTradeInfo(int tradeId);

        IList<TradeInfo> GetTrades(TradeState tradeState);

        IList<OrderInfo> GetOrders(int tradeId);

        OrderInfo GetOrderInfo(int orderId);

        bool UpdateTradeState(int tradeId, TradeState state);

        /// <summary>
        /// 修改子订单状态
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        bool UpdateOrderState(int orderId, OrderStateType state, string opUserId);

        IList<TradeInfo> QueryTradesByEnterprise(int enterpriseId, TradeState state);

        #region 

        IList<OrderImageInfo> BulkSaveOrderImages(IList<OrderImageInfo> imageList);

        IList<OrderImageInfo> QueryOrderImages(int orderId);

        IList<OrderImageInfo> QueryOrderImagesByType(int orderId, OrderFileType imageType);

        OrderImageInfo QueryOrderImageInfo(int imageId);

        OrderImageInfo CreateOrderImage(OrderImageInfo newImage);

        bool UpdateOrderImage(OrderImageInfo image);

        bool DeleteOrderImage(int imageId);

        #endregion
    }
}
