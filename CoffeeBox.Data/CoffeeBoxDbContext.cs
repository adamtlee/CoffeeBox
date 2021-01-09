using CoffeeBox.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace CoffeeBox.Data
{
    public class CoffeeBoxDbContext : IdentityDbContext
    {
        public CoffeeBoxDbContext()
        {

        }

        public CoffeeBoxDbContext(DbContextOptions options): base(options)
        {

        } 

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerAddress> CustomerAddresses { get; set; }
    }
}
