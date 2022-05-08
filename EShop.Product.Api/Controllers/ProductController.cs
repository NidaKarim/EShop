using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShop.Infrastructure.Commands.Product;
using EShop.Product.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Product.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

      public IProductService _service { get; }
      public ProductController(IProductService service)
      {

         _service = service;
      }

      public async Task<IActionResult> Get(string ProductId)
      {
        var product =  await _service.GetProduct(ProductId);
         return Ok(product);
      }

      [HttpPost]
      public async Task<IActionResult> Add(CreateProduct Product)
      {
         var product = await _service.AddProduct(Product);
         return Ok(product);
      }
   }
}