using eTicketing.Data.Cart;
using eTicketing.Data.Services;
using eTicketing.Data.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string UserRole = User.FindFirstValue(ClaimTypes.Role);
            var orders = await _OrderService.GetOrdersByIdAndRoleAsync(userId,UserRole);
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
            string UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string UserEmailAddress = User.FindFirstValue(ClaimTypes.Email);
            await _OrderService.StoreOrderAsync(items, UserId, UserEmailAddress);
            await _shopingCart.ClearShoppingCartAsync();
            return View("OrderCompleted");
        }
    }
}
