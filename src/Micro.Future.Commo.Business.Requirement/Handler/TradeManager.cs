using Micro.Future.Commo.Business.Abstraction.BizInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Micro.Future.Commo.Business.Abstraction.BizObject;
using Micro.Future.Business.DataAccess.Commo.CommoObject;
using Micro.Future.Business.DataAccess.Commo.CommonInterface;
using Micro.Future.Business.DataAccess.Commo.CommoHandler;

namespace Micro.Future.Commo.Business.Requirement.Handler
{
    public class TradeManager : ITradeManager
    {
        private ITrade _tradeService = null;
        private IOrder _orderService = null;

        public TradeManager(ITrade tradeService, IOrder orderService)
        {
            _tradeService = tradeService;
            _orderService = orderService;
        }

        public OrderInfo GetOrderInfo(int orderId)
        {
            Order orderObj = _orderService.queryOrder(orderId);
            return CovnertOrderToInfo(orderObj);
        }

        private OrderInfo CovnertOrderToInfo(Order orderObj)
        {
            return new OrderInfo()
            {
                OrderId = orderObj.OrderId,
                CompleteTime = orderObj.CompleteTime,
                CreateTime = orderObj.CreateTime,
                EnterpriseId = orderObj.EnterpriseId,
                EnterpriseName = orderObj.EnterpriseName,
                ExecuteUsername = orderObj.ExecuteUsername,
                ModifyTime = orderObj.ModifyTime,
                OrderState = orderObj.OrderState,
                RequirementFilters = orderObj.RequirementFilters,
                RequirementId = orderObj.RequirementId,
                RequirementRemarks = orderObj.RequirementRemarks,
                RequirementType = orderObj.RequirementType,
                TradeId = orderObj.TradeId,
                TradeSequence = orderObj.TradeSequence,
                UserId = orderObj.UserId,
                UserName = orderObj.UserName
            };
        }

        private Order CovnertOrderToObject(OrderInfo info)
        {
            return new Order()
            {
                OrderId = info.OrderId,
                CompleteTime = info.CompleteTime,
                CreateTime = info.CreateTime,
                EnterpriseId = info.EnterpriseId,
                EnterpriseName = info.EnterpriseName,
                ExecuteUsername = info.ExecuteUsername,
                ModifyTime = info.ModifyTime,
                OrderState = info.OrderState,
                RequirementFilters = info.RequirementFilters,
                RequirementId = info.RequirementId,
                RequirementRemarks = info.RequirementRemarks,
                RequirementType = info.RequirementType,
                TradeId = info.TradeId,
                TradeSequence = info.TradeSequence,
                UserId = info.UserId,
                UserName = info.UserName
            };
        }

        public IList<OrderInfo> GetOrders(int tradeId)
        {
            IList<Order> orders = _orderService.queryTradeOrder(tradeId);
            if (orders == null || orders.Count == 0)
                return null;

            IList<OrderInfo> orderInfoList = new List<OrderInfo>();
            foreach(var order in orders)
            {
                orderInfoList.Add(CovnertOrderToInfo(order));
            }

            return orderInfoList;
        }

        public TradeInfo GetTradeInfo(int tradeId)
        {
            Trade tradeObj = _tradeService.queryTrade(tradeId);
            if (tradeObj == null)
                return null;

            return ConvertTradeToInfo(tradeObj);
        }

        public IList<TradeInfo> GetTrades(string userId)
        {
            IList<Trade> trades = _tradeService.queryAllTrade(userId);
            if (trades == null || trades.Count == 0)
                return null;

            List<TradeInfo> tradeInfos = new List<TradeInfo>();
            TradeInfo tradeInfo = null;
            foreach(var trade in trades)
            {
                tradeInfo = ConvertTradeToInfo(trade);
                tradeInfo.Orders = GetOrders(trade.TradeId);
                tradeInfos.Add(tradeInfo);
            }

            return tradeInfos;
        }

        public bool UpdateTradeState(int tradeId, string state)
        {
            return _tradeService.updateTradeState(tradeId, state);
        }

        private TradeInfo ConvertTradeToInfo(Trade t)
        {
            return new TradeInfo()
            {
                TradeId = t.TradeId,
                TradeTitle = t.TradeTitle,
                CurrentState = t.CurrentState,
                ParticipatorCount = t.ParticipatorCount,
                TradeAmount = t.TradeAmount,
                TradeFee = t.TradeFee,
                TradeQuota = t.TradeQuota,
                TradeSubsidy = t.TradeSubsidy,
                TradeTime = t.TradeTime
            };
        }


        private Trade ConvertTradeInfoToObject(TradeInfo t)
        {
            return new Trade()
            {
                TradeId = t.TradeId,
                TradeTitle = t.TradeTitle,
                CurrentState = t.CurrentState,
                ParticipatorCount = t.ParticipatorCount,
                TradeAmount = t.TradeAmount,
                TradeFee = t.TradeFee,
                TradeQuota = t.TradeQuota,
                TradeSubsidy = t.TradeSubsidy,
                TradeTime = t.TradeTime
            };
        }
    }
}
