using CrissCargoApp.Enum;
using CrissCargoApp.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrissCargoApp.ViewModel
{
    public class OrderPaymentViewModel
    {
        [Key]
        public int Id { get; set; }
        public string? customersName { get; set; }
        public string? CustomersBank { get; set; }
        public int CustomersAccountNumber { get; set; }

        public string? CompanyAccountPaidTo { get; set; }
        public string? PaymentMethod { get; set; }
        [NotMapped]
        [ForeignKey("PaymentImageFileUrl")]
        public IFormFile? PaymentImageUrl { get; set; }
        public int AmountPaid { get; set; }
        public string? PaymentImageFileUrl { get; set; }
        public string? CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public virtual ApplicationUser? Customers { get; set; }
        public int? OrderNo { get; set; }
        public PaymentStatus? PaymentForProcurement { get; set; }
    }
}
