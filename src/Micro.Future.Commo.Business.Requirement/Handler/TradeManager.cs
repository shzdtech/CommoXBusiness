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
        public OrderInfo GetOrderInfo(int orderId)
        {
            IOrder orderHandler = new OrderHandler();
            Order orderObj = orderHandler.queryOrder(orderId);
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

        private Order CovnertOrderToInfo(OrderInfo info)
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
            IOrder orderHandler = new OrderHandler();
            throw new NotImplementedException();
        }

        public TradeInfo GetTradeInfo(int tradeId)
        {
            ITrade tradeHandler = new TradeHandler();
            Trade tradeObj = tradeHandler.queryTrade(tradeId);
            if (tradeObj == null)
                return null;

            return ConvertTradeToInfo(tradeObj);
        }

        public IList<TradeInfo> GetTrades(int userId)
        {
            ITrade tradeHandler = new TradeHandler();
            throw new NotImplementedException();
        }

        public bool UpdateTradeState(int tradeId, string state)
        {
            ITrade tradeHandler = new TradeHandler();
            return tradeHandler.updateTradeState(tradeId, state);
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
