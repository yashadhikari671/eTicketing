using eTicketing.Data.Cart;
using eTicketing.Data.Services;
using eTicketing.Data.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace eTicketing.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IMoviesService _moviesService;
        private readonly ShopingCart _shopingCart;
        private readonly IOrdersService _OrderService;
        public OrdersController(IMoviesService moviesService, ShopingCart shopingCart, IOrdersService orderService)
        {
            _moviesService = moviesService;
            _shopingCart = shopingCart;
            _OrderService = orderService;
        }
        public async Task<IActionResult> Index()
        {
            string userId = "";
            var orders = await _OrderService.GetOrdersByIdAsync(userId);
            return View(orders);
        }
        public IActionResult ShoppingCart()
        {
            var items = _shopingCart.GetShoppingCartItems();
            _shopingCart.ShopingCartItems = items;
            var response = new ShoppingCartVM()
            {
                shopingCart = _shopingCart,
                ShoppingCartTotal = _shopingCart.GetShoppingCartTotal(),
            };
            return View(response);
        }
        public async Task<IActionResult> AddToShippingCart(int id)
        {
            var item = await _moviesService.GetMovieByIdAsync(id);
            if(item != null)
            {
                _shopingCart.AddItemToCard(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }
        public async Task<IActionResult> RemoveFromShippingCart(int id)
        {
            var item = await _moviesService.GetMovieByIdAsync(id);
            if (item != null)
            {
                _shopingCart.Removeitemfromcard(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }
        public async Task<IActionResult> CompleteOrder()
        {
            var items = _shopingCart.GetShoppingCartItems();
            string UserId = "";
            string UserEmailAddress = "";

            await _OrderService.StoreOrderAsync(items, UserId, UserEmailAddress);
            await _shopingCart.ClearShoppingCartAsync();
            return View("OrderCompleted");
        }
    }
}
