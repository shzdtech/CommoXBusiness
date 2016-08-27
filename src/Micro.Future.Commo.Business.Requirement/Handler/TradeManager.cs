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
                ModifyTime = orderObj.ModifyTime,
                RequirementId = orderObj.RequirementId,
                RequirementRemarks = orderObj.RequirementRemarks,
                TradeId = orderObj.TradeId,
                TradeSequence = orderObj.TradeSequence,
                UserId = orderObj.UserId,
                BusinessRange = orderObj.BusinessRange,
                EnterpriseType = orderObj.EnterpriseType,
                ExecuteUserId = orderObj.ExecuteUserId,
                InvoiceIssueDateTime = orderObj.InvoiceIssueDateTime,
                InvoiceTransferMode = orderObj.InvoiceTransferMode,
                InvoiceValue = orderObj.InvoiceValue,
                OrderStateId = orderObj.OrderStateId,
                PaymentAmount = orderObj.PaymentAmount,
                PaymentDateTime = orderObj.PaymentDateTime,
                PaymentType = orderObj.PaymentType,
                ProductName = orderObj.ProductName,
                ProductPrice = orderObj.ProductPrice,
                ProductQuantity = orderObj.ProductQuantity,
                ProductSpecification = orderObj.ProductSpecification,
                ProductType = orderObj.ProductType,
                ProductUnit = orderObj.ProductUnit,
                RequirementStateId = orderObj.RequirementStateId,
                RequirementTypeId = orderObj.RequirementTypeId,
                Subsidies = orderObj.Subsidies,
                TradeAmount = orderObj.TradeAmount,
                TradeProfit = orderObj.TradeProfit,
                WarehouseAccount = orderObj.WarehouseAccount,
                WarehouseAddress1 = orderObj.WarehouseAddress1,
                WarehouseAddress2 = orderObj.WarehouseAddress2,
                WarehouseCity = orderObj.WarehouseCity,
                WarehouseState = orderObj.WarehouseState
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
                ModifyTime = info.ModifyTime,
                RequirementId = info.RequirementId,
                RequirementRemarks = info.RequirementRemarks,
                TradeId = info.TradeId,
                TradeSequence = info.TradeSequence,
                UserId = info.UserId,
                BusinessRange = info.BusinessRange,
                EnterpriseType = info.EnterpriseType,
                ExecuteUserId = info.ExecuteUserId,
                InvoiceIssueDateTime = info.InvoiceIssueDateTime,
                InvoiceTransferMode = info.InvoiceTransferMode,
                InvoiceValue = info.InvoiceValue,
                OrderStateId = info.OrderStateId,
                PaymentAmount = info.PaymentAmount,
                PaymentDateTime = info.PaymentDateTime,
                PaymentType = info.PaymentType,
                ProductName = info.ProductName,
                ProductPrice = info.ProductPrice,
                ProductQuantity = info.ProductQuantity,
                ProductSpecification = info.ProductSpecification,
                ProductType = info.ProductType,
                ProductUnit = info.ProductUnit,
                RequirementStateId = info.RequirementStateId,
                RequirementTypeId = info.RequirementTypeId,
                Subsidies = info.Subsidies,
                TradeAmount = info.TradeAmount,
                TradeProfit = info.TradeProfit,
                WarehouseAccount = info.WarehouseAccount,
                WarehouseAddress1 = info.WarehouseAddress1,
                WarehouseAddress2 = info.WarehouseAddress2,
                WarehouseCity = info.WarehouseCity,
                WarehouseState = info.WarehouseState
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
