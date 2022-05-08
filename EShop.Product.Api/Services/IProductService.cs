using EShop.Infrastructure.Commands.Product;
using EShop.Infrastructure.Events.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Product.Api.Services
{
   public interface IProductService
   {
      Task<ProductCreated> GetProduct(string ProductId);
      Task<ProductCreated> AddProduct(CreateProduct ProductId);
   }
}
