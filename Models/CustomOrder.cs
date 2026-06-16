using System.ComponentModel.DataAnnotations;

namespace MLGroupShop.Models
{
    public class CustomOrder
    {
        public int Id { get; set; }

        [Required]
        public string CustomerName { get; set; }

        [Required]
        public string Phone { get; set; }

        public string ProductType { get; set; }

        public string Size { get; set; }

        public string Color { get; set; }

        public string Description { get; set; }

        public string Status { get; set; } = "На рассмотрении";
        public decimal Price { get; set; }
        public bool IsCustom { get; set; }
    }
}