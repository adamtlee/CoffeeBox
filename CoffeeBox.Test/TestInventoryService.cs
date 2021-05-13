using CoffeeBox.Data;
using CoffeeBox.Services.Inventory;
using CoffeeBox.Services.Product;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CoffeeBox.Test
{
    public class TestInventoryService
    {
        [Fact]
        public void InventoryService_GetsAllInventory_GivenTheyExist()
        {
            var options = new DbContextOptionsBuilder<CoffeeBoxDbContext>()
                .UseInMemoryDatabase("gets_all").Options;

            using var context = new CoffeeBoxDbContext(options);
            var sut = new ProductService(context);
        }
    }
}
