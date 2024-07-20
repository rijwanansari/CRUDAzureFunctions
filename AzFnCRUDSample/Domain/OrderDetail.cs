using AzFnCRUDSample.Domain.Common;
using System;

namespace AzFnCRUDSample.Domain
{
    public class OrderDetail: BaseEntity<long>
    { 
        public long OrderKeyId { get; set; }
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
        public DateTime Created { get; set; }
    }
}
