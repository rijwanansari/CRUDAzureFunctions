using AzFnCRUDSample.Domain.Common;

namespace AzFnCRUDSample.Domain
{
    public class ItemView : BaseEntity<long>
    {
        public string TypeName { get; set; }
        public string ItemName { get; set; }
        public string Size { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public string TypeDescription { get; set; }
    }
}
