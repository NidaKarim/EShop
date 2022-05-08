using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Infrastructure.Mongo
{
  
   public class MongoInitializer : IDatabaseInitializer
   {

      private bool _initialized;
      private IMongoDatabase _database;
      public MongoInitializer(IMongoDatabase database)
      {
         _database = database;
      }
      public async Task InitializeAsync()
      {
         if (_initialized)
            return;

         IConventionPack convetion = new ConventionPack()
         {
           new IgnoreExtraElementsConvention(true),
           new CamelCaseElementNameConvention(),
           new EnumRepresentationConvention(MongoDB.Bson.BsonType.String)


         };

         ConventionRegistry.Register("EShop", convetion, c => true);
         _initialized = true;
          await Task.CompletedTask;

      }
   }
}
