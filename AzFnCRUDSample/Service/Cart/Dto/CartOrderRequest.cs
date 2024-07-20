using System;
using System.Collections.Generic;

namespace AzFnCRUDSample.Service.Cart.Dto
{
    public record CartOrderRequest
    {
        public long Id { get; set; }
        public string OrderId { get; set; }
        public DateTime OrderDateTime { get; set; }
        public decimal OrderTotal { get; set; }
        public string UserId { get; set; }
        public decimal TaxAmount { get; set; }
        public bool IsConfirmed { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime FutureOrderDateTime { get; set; }
        public string UserRemark { get; set; }
        public decimal TotalPrice { get; set; }
        public int Floor { get; set; }
        public string Building { get; set; }
        public List<CardOrderDetails> OrderDetails { get; set; }
    }

    public record CardOrderDetails
    {
        public long Id { get; set; }
        public string ItemType { get; set; }
        public string ItemName { get; set; }
        public string SubItem { get; set; }
        public string Size { get; set; }
        public string Topping { get; set; }
        public string ItemDescription { get; set; }
        public int OrderPriority { get; set; }
        public string Comment { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Tax { get; set; }
    }
}
