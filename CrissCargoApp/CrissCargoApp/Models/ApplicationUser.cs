using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CrissCargoApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? Address { get; set; }
        public int ZipCode { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DateCreated { get; set; }
        public string? Name { get; set; }
        //public int customerId { get; internal set; }
    }
}
