using eTicketing.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTicketing.Data.Services
{
    public class OrderService : IOrdersService
    {
        private readonly AppDbcontext _context;
        public OrderService(AppDbcontext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetOrdersByIdAsync(string UserId)
        {
            var orders = await _context.Orders.Include(n => n.OrderItems).ThenInclude(n => n.Movie).Where(n => n.UserId ==
            UserId).ToListAsync();
            return orders;
        }

        public async Task StoreOrderAsync(List<ShoppingCartItem> items, string UserId, string UserEmailAddress)
        {
            var order = new Order()
            {
                UserId = UserId,
                Email = UserEmailAddress
            };
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            
            foreach(var item in items)
            {
                var orderitem = new OrderItem()
                {
                    Amount = item.Amount,
                    MovieId = item.Movie.Id,
                    OrderId = order.Id,
                    Price = item.Movie.Price,
                };
                 await _context.OrderItems.AddAsync(orderitem);
            }
            await _context.SaveChangesAsync();
        }
    }
}
