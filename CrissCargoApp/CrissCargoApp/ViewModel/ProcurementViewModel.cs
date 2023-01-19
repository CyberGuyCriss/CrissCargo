using CrissCargoApp.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrissCargoApp.ViewModel
{
    public class ProcurementViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public string CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public virtual ApplicationUser? Customers { get; set; }
        public int? OrderNo { get; set; }
        public string? ProductName { get; set; }
        public string? ProductLink { get; set; }
        public string? ProductPicture { get; set; }
        public int? Quantity { get; set; }
        public string? Colour { get; set; }
        public int Size { get; set; }
        public string? MoreDescription { get; set; }
    }
}
