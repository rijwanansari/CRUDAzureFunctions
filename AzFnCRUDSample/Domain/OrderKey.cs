using AzFnCRUDSample.Domain.Common;
using System;

namespace AzFnCRUDSample.Domain
{
    public class OrderKey : BaseEntity<long>
    {
        public string OrderId { get; set; }
        public DateTime OrderDateTime { get; set; }
        public decimal OrderTotal { get; set; }
        public string UserId { get; set; }
        public decimal TaxAmount { get; set; }
        public bool IsCancelled { get; set; }
        public string CancelReason { get; set; }
        public DateTime CancelDateTime { get; set; }
        public bool IsConfirmed { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime FutureOrderDateTime{ get; set; }
        public string UserRemark { get; set; }
        public decimal TotalPrice { get; set; }
        public long Author { get; set; }
        public DateTime Created { get; set; }
        public long Editor { get; set; }
        public DateTime Modified { get; set; }
        public int Floor { get; set; }
        public string Building { get; set; }      


    }
}
