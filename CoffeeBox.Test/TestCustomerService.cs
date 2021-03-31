using CoffeeBox.Data;
using CoffeeBox.Data.Models;
using CoffeeBox.Services.Customer;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CoffeeBox.Test
{
    public class TestCustomerService
    {
        [Fact]
        public void CustomerService_GetsAllCustomers_GivenTheyExist()
        {
            var options = new DbContextOptionsBuilder<CoffeeBoxDbContext>()
               .UseInMemoryDatabase("gets_all").Options;

            using var context = new CoffeeBoxDbContext(options);

            var sut = new CustomerService(context);

            sut.CreateCustomer(new Data.Models.Customer { Id = 23134 });
            sut.CreateCustomer(new Data.Models.Customer { Id = -345 });

            var allCustomers = sut.GetAllCustomers();

            allCustomers.Count.Should().Be(2);
        }

        [Fact]
        public void CustomerService_CreatesCustomer_GivenNewCustomerObject()
        {
            var options = new DbContextOptionsBuilder<CoffeeBoxDbContext>()
                .UseInMemoryDatabase("Add_writes_to_database").Options;

            using var context = new CoffeeBoxDbContext(options);
            var sut = new CustomerService(context);

            sut.CreateCustomer(new Customer { Id = 66795 });
            context.Customers.Single().Id.Should().Be(66795);
        }

        [Fact]
        public void CustomerService_DeletesCustomer_GivenId()
        {
            var options = new DbContextOptionsBuilder<CoffeeBoxDbContext>()
                .UseInMemoryDatabase("deletes_one")
                .Options;

            using var context = new CoffeeBoxDbContext(options);
            var sut = new CustomerService(context);

            sut.CreateCustomer(new Customer { Id = 66795 });

            sut.DeleteCustomer(66795);
            var allCustomers = sut.GetAllCustomers();
            allCustomers.Count.Should().Be(0);
        }

        [Fact]
        public void CustomerService_OrdersByLastName_WhenGetAllCustomersInvoked()
        {
            // Arrange
            var data = new List<Customer> {
                new Customer { Id = 123, LastName = "Bob"},
                new Customer { Id = 323, LastName = "Joe"},
                new Customer { Id = -89, LastName = "Jannet"}
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Customer>>();

            mockSet.As<IQueryable<Customer>>()
                .Setup(m => m.Provider)
                .Returns(data.Provider);

            mockSet.As<IQueryable<Customer>>()
                .Setup(m => m.Expression)
                .Returns(data.Expression);

            mockSet.As<IQueryable<Customer>>()
                .Setup(m => m.ElementType)
                .Returns(data.ElementType);

            mockSet.As<IQueryable<Customer>>()
                .Setup(m => m.GetEnumerator())
                .Returns(data.GetEnumerator());

            var mockContext = new Mock<CoffeeBoxDbContext>();

            mockContext.Setup(c => c.Customers)
                .Returns(mockSet.Object);

            // Act
            var sut = new CustomerService(mockContext.Object);
            var customers = sut.GetAllCustomers();

            // Assert
            customers.Count.Should().Be(3);
            customers[0].Id.Should().Be(123);
            customers[1].Id.Should().Be(-89);
            customers[2].Id.Should().Be(323);
        }
    }
}
