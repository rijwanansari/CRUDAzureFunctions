using AzFnCRUDSample.Service.Cart.Dto;
using AzFnCRUDSample.Service.Common.Model;
using System.Threading.Tasks;

namespace AzFnCRUDSample.Service.Cart;

public interface ICartService
{
    Task<ResponseModel<CartOrderRequest>> CreateCartOrder(CartOrderRequest cartOrderRequest);
    Task<ResponseModel<CartOrderRequest>> GetCartOrder(long id);
}
