using CoffeeBox.Data;
using CoffeeBox.Data.Models;
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
        [Fact]
        public void ProductService_CreatesProduct_GivenNewProductObject()
        {
            var options = new DbContextOptionsBuilder<CoffeeBoxDbContext>()
                .UseInMemoryDatabase("Archives_product").Options;

            using var context = new CoffeeBoxDbContext(options);
            var sut = new ProductService(context);

            sut.CreateProduct(new Product { Id = 7689 });
            context.Products.Single().Id.Should().Be(7689);
        }
        [Fact]
        public void ProductService_ArchivesProduct_GivenNewProductObject()
        {
            var options = new DbContextOptionsBuilder<CoffeeBoxDbContext>()
                .UseInMemoryDatabase("Add_writes_to_database").Options;

            using var context = new CoffeeBoxDbContext(options);
            var sut = new ProductService(context);

            sut.CreateProduct(new Product { Id = 7 });
            sut.ArchiveProduct(7);
            var archivedProduct = sut.GetProductById(7);
            archivedProduct.IsArchived.Should().Be(true);
        }

        [Fact]
        public void ProductService_GetProductById_GivenTheyExist()
        {
            var options = new DbContextOptionsBuilder<CoffeeBoxDbContext>()
                .UseInMemoryDatabase("gets_one").Options;

            using var context = new CoffeeBoxDbContext(options);
            var sut = new ProductService(context);

            sut.CreateProduct(new Product { Id = 7 });
            sut.CreateProduct(new Product { Id = 24 });

            var getProductWithId = sut.GetProductById(24);
            getProductWithId.Id.Should().Be(24);
        }
    }
}
