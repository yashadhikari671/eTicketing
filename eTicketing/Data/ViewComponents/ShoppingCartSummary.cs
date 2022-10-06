using eTicketing.Data.Cart;
using Microsoft.AspNetCore.Mvc;

namespace eTicketing.Data.ViewComponents
{
    public class ShoppingCartSummary : ViewComponent
    {
        private readonly ShopingCart _shopingCart;
        public ShoppingCartSummary(ShopingCart shopingCart)
        {
            _shopingCart = shopingCart; 
        }

        public IViewComponentResult Invoke()
        {
            var items = _shopingCart.GetShoppingCartItems();
            return View(items.Count);
        }
    }
}
