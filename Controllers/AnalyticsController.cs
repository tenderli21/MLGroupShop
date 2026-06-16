using Microsoft.AspNetCore.Mvc;
using MLGroupShop.Data;

namespace MLGroupShop.Controllers
{
    public class AnalyticsController : Controller
    {
        private readonly AppDbContext _context;

        public AnalyticsController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var orders = _context.Orders.ToList();

            ViewBag.TotalRevenue = orders.Sum(x => x.TotalPrice);

            ViewBag.TotalOrders = orders.Count();

            ViewBag.TotalClients =
                orders.Select(x => x.CustomerName)
                .Distinct()
                .Count();

            ViewBag.AverageCheck =
                orders.Count() > 0
                ? orders.Average(x => x.TotalPrice)
                : 0;

            return View(orders);
        }
    }
}