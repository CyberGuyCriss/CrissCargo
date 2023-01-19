using CrissCargoApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CrissCargoApp.Database
{
    public class ApplicationDbContext : IdentityDbContext
    {
       public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Procurement> Procurements { get; set; }
        public DbSet<Quotation> Quotations { get; set; }
        public DbSet<OrderPayment> OrderPayments { get; set; }
    }
}
