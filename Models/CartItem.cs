using System.ComponentModel.DataAnnotations;

namespace MLGroupShop.Models
{
    public class CartItem
    {
        public int Id { get; set; }

        [Display(Name = "Название товара")]
        public string ProductName { get; set; }

        [Display(Name = "Цена")]
        public decimal Price { get; set; }

        [Display(Name = "Фото")]
        public string ImagePath { get; set; }

        [Display(Name = "Количество")]
        public int Quantity { get; set; }
        public bool IsSelected { get; set; }
    }
}