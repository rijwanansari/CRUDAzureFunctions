using AzFnCRUDSample.Domain;
using AzFnCRUDSample.Service.Common.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AzFnCRUDSample.Service.Menu
{
    public interface IMenuService
    {
        Task<ResponseModel<List<ItemType>>> GetItemTypesAsync();
        Task<ResponseModel<ItemType>> CreateItemTypeAsync(ItemType itemType);
        Task<ResponseModel<ItemType>> GetItemTypeAsync(int id);
        Task<ResponseModel<ItemType>> UpdateItemTypeAsync(ItemType itemType);
        Task<ResponseModel<bool>> DeleteItemTypeAsync(int id);
        Task<ResponseModel<List<ItemView>>> GetItemsAsync();
    }
}
