using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Infrastructure.EventBus
{
   public static class Extension
   {

      public static IServiceCollection AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
      {
         var rabbitMQ = new RabbitMqOption();
         configuration.GetSection("rabbitmq").Bind(rabbitMQ);

         services.AddMassTransit(x =>
         {
            x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
             
               cfg.Host(new Uri(rabbitMQ.ConnectionString), hostcfg =>
               {
                  hostcfg.Username(rabbitMQ.UserName);
                  hostcfg.Password(rabbitMQ.Password);
               });
               cfg.ConfigureEndpoints(provider);
            }));

         });



         return services;
      }
   }

}