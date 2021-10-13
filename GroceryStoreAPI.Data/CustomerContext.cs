using GroceryStoreAPI.Data.Configurations;
using GroceryStoreAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GroceryStoreAPI.Data
{
    public class CustomerContext : DbContext
    {
        Customer Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Filename=GroceryStoreCustomers.sqlite3");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
        }
    }
}