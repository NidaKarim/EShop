using EShop.Infrastructure.Commands.Product;
using EShop.Infrastructure.Events.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Product.Api.Repositories
{
   public interface IProductRepository
   {
      Task<ProductCreated> GetProduct(string ProductId);
      Task<ProductCreated> AddProduct(CreateProduct ProductId);
   }
}
