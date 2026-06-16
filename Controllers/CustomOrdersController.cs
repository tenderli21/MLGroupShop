using Microsoft.AspNetCore.Mvc;
using MLGroupShop.Data;
using MLGroupShop.Models;

namespace MLGroupShop.Controllers
{
    public class CustomOrdersController : Controller
    {

        private readonly AppDbContext _context;

        public CustomOrdersController(AppDbContext context)
        {
            _context = context;
        }

        // Открытие страницы

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Отправка заявки

        [HttpPost]
        public IActionResult Create(CustomOrder model)
        {
            Order order = new Order
            {
                CustomerName = model.CustomerName,

                Phone = model.Phone,

                Address = "Индивидуальный пошив",

                ProductName =
                    model.ProductType +
                    " | Размер: " + model.Size +
                    " | Цвет: " + model.Color,

                Quantity = 1,

                TotalPrice = 0,

                Status = "Индивидуальный пошив",

                IsCustom = true
            };

            _context.Orders.Add(order);

            _context.SaveChanges();

            TempData["Success"] =
                "Заявка успешно отправлена!";

            return RedirectToAction("Create");
        }
    }
}