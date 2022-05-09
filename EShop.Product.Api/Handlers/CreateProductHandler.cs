using EShop.Infrastructure.Commands.Product;
using EShop.Product.Api.Services;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Product.Api.Handlers
{
   public class CreateProductHandler : IConsumer<CreateProduct>
   {

      private IProductService _service { get; set; }

      public CreateProductHandler(IProductService service)
      {
         _service = service;
      }
      public async Task Consume(ConsumeContext<CreateProduct> context)
      {
        await _service.AddProduct(context.Message);
         await Task.CompletedTask;
      }
   }
}
