namespace CrissCargoApp.Models
{
    public class Quotation
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string? Details { get; set; }
        public int TotalAmount { get;}
        public string? OrderStatus { get; set; }
        public DateTime QuotationDate { get; set; }
    }
}
