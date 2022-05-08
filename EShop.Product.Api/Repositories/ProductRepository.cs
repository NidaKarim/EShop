using EShop.Infrastructure.Commands.Product;
using EShop.Infrastructure.Events.Product;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Product.Api.Repositories
{
   public class ProductRepository : IProductRepository
   {
      private IMongoDatabase _database;
      private IMongoCollection<CreateProduct> _collection;
       
      public ProductRepository(IMongoDatabase database)
      {
         _database = database;
         _collection = _database.GetCollection<CreateProduct>("Product");
      }
      public async Task<ProductCreated> AddProduct(CreateProduct Product)
      {
         await _collection.InsertOneAsync(Product);
         return new ProductCreated { ProductName = Product.ProductName, ProductID = Product.ProductId , CreatedDt = DateTime.Now};
      }

      public async Task<ProductCreated> GetProduct(string ProductId)
      {
         var Product =  _collection.AsQueryable().Where(x => x.ProductId == ProductId).FirstOrDefault();
         if(Product == null)
         {
            throw new Exception("Product Not ound");
         }
         return new ProductCreated { ProductName = Product.ProductName, ProductID = Product.ProductId, CreatedDt = DateTime.Now };

      }
   }
}
