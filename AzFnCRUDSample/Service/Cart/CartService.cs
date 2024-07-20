using AzFnCRUDSample.Domain;
using AzFnCRUDSample.Service.Cart.Dto;
using AzFnCRUDSample.Service.Common.Interface;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;
using AzFnCRUDSample.Service.Common.Model;
using System.Linq;

namespace AzFnCRUDSample.Service.Cart;

internal class CartService(IUnitOfWork unitOfWork, ILogger<CartService> logger) : ICartService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILogger<CartService> _logger = logger;
    public async Task<ResponseModel<CartOrderRequest>> CreateCartOrder(CartOrderRequest cartOrderRequest)
    {
        try
        {
            var orderKey = new OrderKey
            {
                OrderId = cartOrderRequest.OrderId,
                OrderDateTime = cartOrderRequest.OrderDateTime,
                OrderTotal = cartOrderRequest.OrderTotal,
                UserId = cartOrderRequest.UserId,
                TaxAmount = cartOrderRequest.TaxAmount,
                IsConfirmed = cartOrderRequest.IsConfirmed,
                IsCompleted = cartOrderRequest.IsCompleted,
                FutureOrderDateTime = cartOrderRequest.FutureOrderDateTime,
                UserRemark = cartOrderRequest.UserRemark,
                TotalPrice = cartOrderRequest.TotalPrice,
                Floor = cartOrderRequest.Floor,
                Building = cartOrderRequest.Building
            };
            _unitOfWork.BeginTransaction();
            var orderKeyAdded = await _unitOfWork.Repository<OrderKey>().AddAsync(orderKey);
            await _unitOfWork.SaveAsync();
            foreach (var orderDetail in cartOrderRequest.OrderDetails)
            {
                var orderDetailEntity = new OrderDetail
                {
                    OrderKeyId = orderKeyAdded.Id,
                    ItemType = orderDetail.ItemType,
                    ItemName = orderDetail.ItemName,
                    SubItem = orderDetail.SubItem,
                    Size = orderDetail.Size,
                    Topping = orderDetail.Topping,
                    ItemDescription = orderDetail.ItemDescription,
                    OrderPriority = orderDetail.OrderPriority,
                    Comment = orderDetail.Comment,
                    Quantity = orderDetail.Quantity,
                    Price = orderDetail.Price,
                    Tax = orderDetail.Tax
                };
                await _unitOfWork.Repository<OrderDetail>().AddAsync(orderDetailEntity);
            }
            await _unitOfWork.SaveAsync();
            _unitOfWork.CommitTransaction();

            return ResponseModel<CartOrderRequest>.SuccessResponse("Order Created Successfully", cartOrderRequest);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in CreateCartOrder");
            return ResponseModel<CartOrderRequest>.FailureResponse("Internal Server Error");
        }
    }

    public async Task<ResponseModel<CartOrderRequest>> GetCartOrder(long id)
        {
        try
        {
            var orderKey = await _unitOfWork.Repository<OrderKey>().Get(id);
            var orderDetails = _unitOfWork.Repository<OrderDetail>().GetMany(x=> x.OrderKeyId == id).ToList();
            var cartOrderRequest = new CartOrderRequest
            {
                Id = orderKey.Id,
                OrderId = orderKey.OrderId,
                OrderDateTime = orderKey.OrderDateTime,
                OrderTotal = orderKey.OrderTotal,
                UserId = orderKey.UserId,
                TaxAmount = orderKey.TaxAmount,
                IsConfirmed = orderKey.IsConfirmed,
                IsCompleted = orderKey.IsCompleted,
                FutureOrderDateTime = orderKey.FutureOrderDateTime,
                UserRemark = orderKey.UserRemark,
                TotalPrice = orderKey.TotalPrice,
                Floor = orderKey.Floor,
                Building = orderKey.Building,
                OrderDetails = orderDetails.Select(x => new CardOrderDetails
                {
                    Id = x.Id,
                    ItemType = x.ItemType,
                    ItemName = x.ItemName,
                    SubItem = x.SubItem,
                    Size = x.Size,
                    Topping = x.Topping,
                    ItemDescription = x.ItemDescription,
                    OrderPriority = x.OrderPriority,
                    Comment = x.Comment,
                    Quantity = x.Quantity,
                    Price = x.Price,
                    Tax = x.Tax
                }).ToList()
            };
            return ResponseModel<CartOrderRequest>.SuccessResponse("Success", cartOrderRequest);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetOrderKeyById");
            return ResponseModel<CartOrderRequest>.FailureResponse("Internal Server Error");
        }
    }
    

}
