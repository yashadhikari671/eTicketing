using eTicketing.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eTicketing.Data.Services
{
    public interface IOrdersService 
    {
        Task StoreOrderAsync(List<ShoppingCartItem> items, string UserId, string UserEmailAddress);
        Task<List<Order>> GetOrdersByIdAndRoleAsync(string UserId, string UserRole);
    }
}
