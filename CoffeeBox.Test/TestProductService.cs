using CoffeeBox.Data;
using CoffeeBox.Services.Product;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CoffeeBox.Test
{
    public class TestProductService
    {
        [Fact]
        public void ProductService_GetsAllProducts_GivenTheyExist()
        {
            var options = new DbContextOptionsBuilder<CoffeeBoxDbContext>()
                .UseInMemoryDatabase("gets_all").Options;

            using var context = new CoffeeBoxDbContext(options);

            var sut = new ProductService(context);

            sut.CreateProduct(new Data.Models.Product { Id = 1 });
            sut.CreateProduct(new Data.Models.Product { Id = 2 });

            var allProducts = sut.GetAllProducts();

            allProducts.Count.Should().Be(2); 


        }
    }
}
