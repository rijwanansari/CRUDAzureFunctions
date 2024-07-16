using AzFnCRUDSample.Domain.Common;
using System;

namespace AzFnCRUDSample.Domain;

public class ItemType: BaseEntity<int>
{
    public string TypeName { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }

}
