﻿using CoffeeBox.Services.Customer;
using CoffeeBox.Services.Order;
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
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderService _orderService;
        private readonly ICustomerService _customerService;

        public OrderController(
            ILogger<OrderController> logger,
            IOrderService orderService, 
            ICustomerService customerService)
        {
            _logger = logger;
            _orderService = orderService;
            _customerService = customerService; 
        }

        [HttpPost("/api/invoice")]
        public ActionResult GenerateNewOrder([FromBody]InvoiceModel invoice)
        {
            _logger.LogInformation("Generating invoice");
            var order = OrderMapper.SerializeInvoiceToOrder(invoice);
            order.Customer = _customerService.GetById(invoice.CustomerId);
            _orderService.GenerateOpenOrder(order);
            return Ok();
        }
    }
}
