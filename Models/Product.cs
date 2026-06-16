using System.ComponentModel.DataAnnotations;

namespace MLGroupShop.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Display(Name = "Название")]
        public string Name { get; set; }

        [Display(Name = "Цена")]
        public decimal Price { get; set; }

        [Display(Name = "Количество")]
        public int Quantity { get; set; }

        public string? ImagePath { get; set; }
    }
}