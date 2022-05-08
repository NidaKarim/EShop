using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Infrastructure.Events.Product
{
   public class ProductCreated
   {
      public string ProductID { get; set; }

      public string ProductName { get; set; }

      public DateTime CreatedDt { get; set; } 
   }
}
