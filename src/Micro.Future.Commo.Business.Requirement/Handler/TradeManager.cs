using Micro.Future.Commo.Business.Abstraction.BizInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Micro.Future.Commo.Business.Abstraction.BizObject;
using Micro.Future.Business.DataAccess.Commo.CommoObject;
using Micro.Future.Business.DataAccess.Commo.CommonInterface;
using Micro.Future.Business.DataAccess.Commo.CommoHandler;
using Micro.Future.Commo.Business.Abstraction.BizObject.Enums;
using Micro.Future.Business.Common;

namespace Micro.Future.Commo.Business.Requirement.Handler
{
    public class TradeManager : ITradeManager
    {
        private ITrade _tradeService = null;
        private IOrder _orderService = null;
        private IOrderImage _orderImageService = null;

        public TradeManager(ITrade tradeService, IOrder orderService, IOrderImage orderImageService)
        {
            _tradeService = tradeService;
            _orderService = orderService;
            _orderImageService = orderImageService;
        }

        public OrderInfo GetOrderInfo(int orderId)
        {
            Order orderObj = _orderService.queryOrder(orderId);
            var orderInfo =  CovnertOrderObjectToInfo(orderObj);
            if (orderInfo != null)
                orderInfo.OrderImages = QueryOrderImages(orderInfo.OrderId);
            return orderInfo;
        }

        private OrderInfo CovnertOrderObjectToInfo(Order orderObj)
        {
            return new OrderInfo()
            {
                OrderId = orderObj.OrderId,
                CompleteTime = orderObj.CompleteTime,
                CreateTime = orderObj.CreateTime,
                EnterpriseId = orderObj.EnterpriseId,
                EnterpriseName = orderObj.EnterpriseName,
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
                OrderStateId = (OrderStateType) orderObj.OrderStateId,
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

        private Order CovnertOrderInfoToObject(OrderInfo info)
        {
            return new Order()
            {
                OrderId = info.OrderId,
                CompleteTime = info.CompleteTime,
                CreateTime = info.CreateTime,
                EnterpriseId = info.EnterpriseId,
                EnterpriseName = info.EnterpriseName,
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
                OrderStateId = (int)info.OrderStateId,
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
                var orderInfo = CovnertOrderObjectToInfo(order);
                orderInfo.OrderImages = QueryOrderImages(order.OrderId);
                orderInfoList.Add(orderInfo);
            }

            return orderInfoList;
        }

        public TradeInfo GetTradeInfo(int tradeId)
        {
            Trade tradeObj = _tradeService.queryTrade(tradeId);
            if (tradeObj == null)
                return null;

            TradeInfo tradeInfo = ConvertTradeToInfo(tradeObj);
            tradeInfo.Orders = GetOrders(tradeId);
            return tradeInfo;
        }

        public IList<TradeInfo> GetTrades(TradeState tradeState)
        {
            int intTradeState = (int)tradeState;

            IList<Trade> trades = _tradeService.queryAllTrade(intTradeState.ToString());
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

        public bool UpdateTradeState(int tradeId, TradeState state)
        {
            IList<Order> orderList = _orderService.queryTradeOrder(tradeId);
            if (orderList == null || orderList.Count == 0)
            {
                throw new BizException("子订单不存在！");
            }

            Order unConfirmedOrder = null;
            int unConfirmedState = 0;

            //修改状态成资金时, 需判断是否所有的order都已经签署合同
            if (state == TradeState.Payment)
            {
                unConfirmedState = (int)OrderStateType.ContractConfirmed;
                unConfirmedOrder = orderList.FirstOrDefault(f => f.OrderStateId != unConfirmedState);
                if (unConfirmedOrder != null)
                {
                    throw new BizException("交易中存在尚未签署合同的企业！");
                }
            }
            //修改状态成支付尾款时，需判断是否所有order都已经开具发票
            else if(state == TradeState.FinalPayment)
            {
                unConfirmedState = (int)OrderStateType.InvoiceConfirmed;
                unConfirmedOrder = orderList.FirstOrDefault(f => f.OrderStateId != unConfirmedState);
                if (unConfirmedOrder != null)
                {
                    throw new BizException("交易中存在尚未开具发票的企业！");
                }
            }

            return _tradeService.updateTradeState(tradeId, ((int)state).ToString());
        }

        private TradeInfo ConvertTradeToInfo(Trade t)
        {
            return new TradeInfo()
            {
                TradeId = t.TradeId,
                TradeTitle = t.TradeTitle,
                CurrentState = (TradeState)int.Parse(t.CurrentState),
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
                CurrentState = ((int)t.CurrentState).ToString(),
                ParticipatorCount = t.ParticipatorCount,
                TradeAmount = t.TradeAmount,
                TradeFee = t.TradeFee,
                TradeQuota = t.TradeQuota,
                TradeSubsidy = t.TradeSubsidy,
                TradeTime = t.TradeTime
            };
        }

        public IList<TradeInfo> QueryTradesByEnterprise(int enterpriseId, TradeState state)
        {
            var trades =  _tradeService.queryTradesByEnterprise(enterpriseId, ((int)state).ToString());
            if (trades == null || trades.Count == 0)
                return null;

            IList<TradeInfo> tradeList = new List<TradeInfo>();

            TradeInfo info = null;
            IList<Order> tradeOrders = null;
            foreach (var trade in trades)
            {
                info = ConvertTradeToInfo(trade);
                tradeOrders = _orderService.queryTradeOrder(trade.TradeId);
                if (tradeOrders == null || tradeOrders.Count == 0)
                    continue;

                //我的订单
                var myOrder = tradeOrders.FirstOrDefault(f => f.EnterpriseId == enterpriseId);
                int myIndex = myOrder.TradeSequence;

                info.Orders = new List<OrderInfo>();

                OrderInfo orderInfo = null;
                //上游
                var upstreamOrder = tradeOrders.FirstOrDefault(f => f.TradeSequence == myIndex - 1);
                if(upstreamOrder!=null)
                {
                    orderInfo = CovnertOrderObjectToInfo(upstreamOrder);
                    orderInfo.OrderImages = QueryOrderImages(orderInfo.OrderId);
                    info.Orders.Add(orderInfo);
                }

                //我自己
                orderInfo = CovnertOrderObjectToInfo(myOrder);
                orderInfo.OrderImages = QueryOrderImages(orderInfo.OrderId);
                info.Orders.Add(CovnertOrderObjectToInfo(myOrder));

                //下游
                var downstreamOrder = tradeOrders.FirstOrDefault(f => f.TradeSequence == myIndex + 1);
                if (downstreamOrder != null)
                {
                    orderInfo = CovnertOrderObjectToInfo(downstreamOrder);
                    orderInfo.OrderImages = QueryOrderImages(orderInfo.OrderId);
                    info.Orders.Add(CovnertOrderObjectToInfo(downstreamOrder));
                }

                tradeList.Add(info);
            }
            return tradeList;
        }

        /// <summary>
        /// 修改子订单状态
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public bool UpdateOrderState(int orderId, OrderStateType state,string opUserId)
        {
            return _orderService.updateOrderState(orderId, opUserId, (int)state);

        }


        #region 图片


        private OrderImageInfo ConvertOrderImageObjectToInfo(OrderImage image)
        {
            return new OrderImageInfo()
            {
                ImageId = image.ImageId,
                CreateTime = image.CreateTime,
                ImagePath = image.ImagePath,
                ImageType = (OrderImageType)image.ImageTypeId,
                OrderId = image.OrderId,
                Position = image.Position,
                TradeId = image.TradeId,
                UpdateTime = image.UpdateTime
            };
        }

        private OrderImage ConvertOrderImageInfoToObject(OrderImageInfo image)
        {
            return new OrderImage()
            {
                ImageId = image.ImageId,
                CreateTime = image.CreateTime,
                ImagePath = image.ImagePath,
                ImageTypeId = (int)image.ImageType,
                OrderId = image.OrderId,
                Position = image.Position,
                TradeId = image.TradeId,
                UpdateTime = image.UpdateTime
            };
        }


        public bool BulkSaveOrderImages(IList<OrderImageInfo> imageList)
        {
            if (imageList == null || imageList.Count == 0)
                return false;

            IList<OrderImage> orderImages = new List<OrderImage>();
            foreach(var img in imageList)
            {
                orderImages.Add(ConvertOrderImageInfoToObject(img));
            }

            return _orderImageService.BulkSaveOrderImages(orderImages);
        }

        public IList<OrderImageInfo> QueryOrderImages(int orderId)
        {
            var imageList = _orderImageService.QueryOrderImages(orderId);
            if (imageList == null || imageList.Count == 0)
                return null;

            IList<OrderImageInfo> orderImages = new List<OrderImageInfo>();
            foreach(var img in imageList)
            {
                orderImages.Add(ConvertOrderImageObjectToInfo(img));
            }

            return orderImages;
        }

        public IList<OrderImageInfo> QueryOrderImagesByType(int orderId, OrderImageType imageType)
        {
            var imageList = _orderImageService.QueryOrderImagesByType(orderId, (int)imageType);
            if (imageList == null || imageList.Count == 0)
                return null;

            IList<OrderImageInfo> orderImages = new List<OrderImageInfo>();
            foreach (var img in imageList)
            {
                orderImages.Add(ConvertOrderImageObjectToInfo(img));
            }

            return orderImages;
        }

        public OrderImageInfo QueryOrderImageInfo(int imageId)
        {
            var image = _orderImageService.QueryOrderImageInfo(imageId);
            if (image == null)
                return null;

            return ConvertOrderImageObjectToInfo(image);
        }

        public OrderImageInfo CreateOrderImage(OrderImageInfo newImage)
        {
            var orderImage = ConvertOrderImageInfoToObject(newImage);

            var newOrderImage = _orderImageService.CreateOrderImage(orderImage);
            if (newOrderImage == null)
                return null;

            newImage.ImageId = newOrderImage.ImageId;
            return newImage;
        }

        public bool UpdateOrderImage(OrderImageInfo image)
        {
            var orderImage = ConvertOrderImageInfoToObject(image);
            return _orderImageService.UpdateOrderImage(orderImage);
        }

        public bool DeleteOrderImage(int imageId)
        {
            return _orderImageService.DeleteOrderImage(imageId);
        }

        #endregion
    }
}
