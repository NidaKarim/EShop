using EShop.Infrastructure.Commands.Product;
using EShop.Infrastructure.Events.Product;
using EShop.Product.Api.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Product.Api.Services
{
   public class ProductService : IProductService
   {
      public IProductRepository _repository;

      public ProductService(IProductRepository repository)
      {

         _repository = repository;
      }

      public async Task<ProductCreated> AddProduct(CreateProduct Product)
      {
         
         return await _repository.AddProduct(Product);
         
      }

      public async Task<ProductCreated> GetProduct(string ProductId)
      {
         var product = await _repository.GetProduct(ProductId);
         return product;
      }
   }
}
