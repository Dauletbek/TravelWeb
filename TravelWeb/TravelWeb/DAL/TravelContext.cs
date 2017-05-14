using TravelWeb.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace TravelWeb.DAL
{
    public class TravelContext:DbContext
    {
        public TravelContext()
            : base("Travel1")
        {
        }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Guider> Guides { get; set; }
        public DbSet<TravelType> TravelTypes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Travel> Travels { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<TravelPlan> Plans { get; set; }
        public DbSet<Day> Days { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentHistory> PaymentHistory { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
