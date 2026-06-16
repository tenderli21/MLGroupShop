using Microsoft.AspNetCore.Mvc;
using MLGroupShop.Data;

namespace MLGroupShop.Controllers
{
    public class ClientsController : Controller
    {
        private readonly AppDbContext _context;

        public ClientsController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var clients = _context.Orders
                .GroupBy(x => x.Phone)
                .Select(g => new
                {
                    Name = g.First().CustomerName,
                    Phone = g.First().Phone,
                    Address = g.First().Address,
                    OrdersCount = g.Count(),
                    TotalMoney = g.Sum(x => x.TotalPrice)
                })
                .ToList();

            return View(clients);
        }
    }
}