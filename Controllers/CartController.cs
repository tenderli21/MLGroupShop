using Microsoft.AspNetCore.Mvc;
using MLGroupShop.Data;
using MLGroupShop.Models;

namespace MLGroupShop.Controllers
{
    public class CartController : Controller
    {
        private readonly AppDbContext _context;

        public CartController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var cart = _context.CartItems.ToList();

            ViewBag.Total = cart.Sum(x => x.Price * x.Quantity);

            return View(cart);
        }

        public IActionResult Add(int id)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == id);

            if (product != null)
            {
                CartItem item = new CartItem
                {
                    ProductName = product.Name,
                    Price = product.Price,
                    ImagePath = product.ImagePath,
                    Quantity = 1
                };

                _context.CartItems.Add(item);

                _context.SaveChanges();
            }

            return RedirectToAction("Index", "Products");
        }

        public IActionResult Increase(int id)
        {
            var item = _context.CartItems.FirstOrDefault(x => x.Id == id);

            if (item != null)
            {
                item.Quantity++;

                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public IActionResult Decrease(int id)
        {
            var item = _context.CartItems.FirstOrDefault(x => x.Id == id);

            if (item != null)
            {
                if (item.Quantity > 1)
                {
                    item.Quantity--;
                }

                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Checkout(
            List<int> selectedItems,
            string customerName,
            string phone,
            string address)
        {
            var cart = _context.CartItems
                .Where(x => selectedItems.Contains(x.Id))
                .ToList();

            foreach (var item in cart)
            {
                Order order = new Order
                {
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                    TotalPrice = item.Price * item.Quantity,

                    CustomerName = customerName,
                    Phone = phone,
                    Address = address,

                    Status = "На рассмотрении"
                };

                _context.Orders.Add(order);
            }

            _context.CartItems.RemoveRange(cart);

            _context.SaveChanges();

            TempData["Message"] = "Спасибо за заказ!";

            return RedirectToAction("Index");
        }

        public IActionResult Remove(int id)
        {
            var item = _context.CartItems.FirstOrDefault(x => x.Id == id);

            if (item != null)
            {
                _context.CartItems.Remove(item);

                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}