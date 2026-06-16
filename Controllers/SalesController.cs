using Microsoft.AspNetCore.Mvc;
using MLGroupShop.Data;

namespace MLGroupShop.Controllers
{
    public class SalesController : Controller
    {
        private readonly AppDbContext _context;

        public SalesController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var orders = _context.Orders.ToList();

            ViewBag.TotalOrders = orders.Count;

            ViewBag.TotalProducts =
                orders.Sum(x => x.Quantity);

            ViewBag.TotalMoney =
                orders.Sum(x => x.TotalPrice);

            ViewBag.ProductNames =
                orders
                .GroupBy(x => x.ProductName)
                .Select(x => x.Key)
                .ToList();

            ViewBag.ProductCounts =
                orders
                .GroupBy(x => x.ProductName)
                .Select(x => x.Sum(y => y.Quantity))
                .ToList();

            ViewBag.TopClients =
                orders
                .GroupBy(x => new
                {
                    x.CustomerName,
                    x.Phone
                })
                .Select(x => new
                {
                    Name = x.Key.CustomerName,
                    Phone = x.Key.Phone,
                    Count = x.Count()
                })
                .OrderByDescending(x => x.Count)
                .Take(5)
                .ToList();

            return View();
        }
    }
}