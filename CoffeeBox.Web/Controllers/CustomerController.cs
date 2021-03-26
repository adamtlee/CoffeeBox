using CoffeeBox.Services.Customer;
using CoffeeBox.Web.Serialization;
using CoffeeBox.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeBox.Web.Controllers
{
    public class CustomerController : ControllerBase 
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomerService _customerService; 

        public CustomerController(ILogger<CustomerController> logger, ICustomerService customerService)
        {
            _customerService = customerService;
            _logger = logger;
        }

        [HttpPost("api/customer")]
        public ActionResult CreateCustomer([FromBody] CustomerModel customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _logger.LogInformation("Creating a new Customer");
            customer.CreatedOn = DateTime.UtcNow;
            customer.UpdatedOn = DateTime.UtcNow;
            var customerData = CustomerMapper.SerializeCustomer(customer);
            var newCustomer = _customerService.CreateCustomer(customerData);
            return Ok(newCustomer);
        }

        [HttpGet("/api/customer")]
        public ActionResult GetCustomer([FromBody] CustomerModel customer)
        {
            _logger.LogInformation("Getting Customers");
            var customers = _customerService.GetAllCustomers();

            var customerModels = customers
                .Select(customer => new CustomerModel
                {
                    Id = customer.Id,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    PrimaryAddress = CustomerMapper
                        .MapCustomerAddress(customer.PrimaryAddress),
                    CreatedOn = customer.CreatedOn,
                    UpdatedOn = customer.UpdatedOn
                })
            .OrderByDescending(customer => customer.CreatedOn)
            .ToList(); 
            return Ok(customerModels);
        }

        [HttpDelete("/api/customer/{id}")]
        public ActionResult DeleteCustomer(int id)
        {
            _logger.LogInformation("Deleting a customer");
            var response = _customerService
                .DeleteCustomer(id);
            return Ok(response);
        }
    }
}
