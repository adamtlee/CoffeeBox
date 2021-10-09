using CoffeeBox.Data;
using CoffeeBox.Services.Order;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CoffeeBox.Test
{
    public class TestOrderService
    {
        [Fact]
        public void OrderService_GetsAllProducts_GivenTheyExist()
        {
            var options = new DbContextOptionsBuilder<CoffeeBoxDbContext>()
                .UseInMemoryDatabase("gets_all").Options;

            using var context = new CoffeeBoxDbContext(options);
        }

    }
}
