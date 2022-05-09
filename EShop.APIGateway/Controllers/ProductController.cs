using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShop.Infrastructure.Commands.Product;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShop.APIGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

      private IBusControl _bus;

      public ProductController(IBusControl bus)
      {
         _bus = bus;
      }
      [HttpGet]
      public async Task<IActionResult> Get([FromForm] CreateProduct Product)
      {
         await Task.CompletedTask;
         return Accepted("Get product method called");
      }

      [HttpPost]
      public async Task<IActionResult> Add(CreateProduct Product)
      {
         var Uri = new Uri("rabbitmq://localhost/create_product");
         var endPoint = await _bus.GetSendEndpoint(Uri);
         await endPoint.Send(Product);
         await Task.CompletedTask;
         return Accepted("Product Created");
      }
    }
}