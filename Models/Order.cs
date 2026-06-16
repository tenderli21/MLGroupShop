namespace MLGroupShop.Models
{
    public class Order
    {
        public int Id { get; set; }

        public string ProductName { get; set; } = "";

        public int Quantity { get; set; }

        public decimal TotalPrice { get; set; }

        public string CustomerName { get; set; } = "";

        public string Phone { get; set; } = "";

        public string Address { get; set; } = "";

        public string Status { get; set; } = "";
        public bool IsCustom { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;
    }
}