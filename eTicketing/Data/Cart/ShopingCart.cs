using eTicketing.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTicketing.Data.Cart
{
    public class ShopingCart
    {
        public AppDbcontext _context { get; set; }

        public string ShopingCartId { get; set; }
        public List<ShoppingCartItem> ShopingCartItems { get; set; }
        public ShopingCart(AppDbcontext context)
        {
            _context = context;
        }
        public static ShopingCart GetShopingCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetService<AppDbcontext>();

            string CardId = session.GetString("CardId") ?? Guid.NewGuid().ToString();
            session.SetString("CardId",CardId);

            return new ShopingCart(context) { ShopingCartId = CardId };
        }
        public void AddItemToCard(Movie movie)
        {
            var shoppingcartitem = _context.shoppingCartItems.FirstOrDefault(x => x.Movie.Id == movie.Id && x.ShoppingCartId == ShopingCartId);
            
            if(shoppingcartitem == null)
            {
                shoppingcartitem = new ShoppingCartItem()
                {
                    ShoppingCartId = ShopingCartId,
                    Movie = movie,
                    Amount = 1
                };
                _context.shoppingCartItems.Add(shoppingcartitem);
            }
            else
            {
                shoppingcartitem.Amount++;
            }
            _context.SaveChanges();
        }

        public void Removeitemfromcard(Movie movie)
        {
            var shoppingcartitem = _context.shoppingCartItems.FirstOrDefault(x => x.Movie.Id == movie.Id && x.ShoppingCartId == ShopingCartId);

            if (shoppingcartitem != null)
            {
                if (shoppingcartitem.Amount > 1)
                {
                    shoppingcartitem.Amount--;
                }
                else
                {
                    _context.shoppingCartItems.Remove(shoppingcartitem);
                }
            }
            _context.SaveChanges();

        }
        
        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShopingCartItems ?? (ShopingCartItems = _context.shoppingCartItems.Where(n => n.ShoppingCartId == ShopingCartId).Include(n => n.Movie).ToList());
        }

        public double GetShoppingCartTotal()
        {
            var total = _context.shoppingCartItems.Where(n => n.ShoppingCartId == ShopingCartId).Select(n => n.Movie.Price * n.Amount).Sum();
            return total;
        }
        public async Task ClearShoppingCartAsync()
        {
            var items = await _context.shoppingCartItems.Where(n => n.ShoppingCartId == ShopingCartId).ToListAsync();
             _context.shoppingCartItems.RemoveRange(items);
            await _context.SaveChangesAsync();
        }
    }
}
