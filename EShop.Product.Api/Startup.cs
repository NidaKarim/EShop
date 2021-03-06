using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShop.Infrastructure.EventBus;
using EShop.Infrastructure.Mongo;
using EShop.Product.Api.Handlers;
using EShop.Product.Api.Repositories;
using EShop.Product.Api.Services;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EShop.Product.Api
{
   public class Startup
   {
      public Startup(IConfiguration configuration)
      {
         Configuration = configuration;
      }

      public IConfiguration Configuration { get; }

      // This method gets called by the runtime. Use this method to add services to the container.
      public void ConfigureServices(IServiceCollection services)
      {
         services.AddControllers();
         services.AddMongoDb(Configuration);
         services.AddScoped<IProductService, ProductService>();
         services.AddScoped<IProductRepository, ProductRepository>();
         services.AddScoped<CreateProductHandler>();

         var rabbitMQ = new RabbitMqOption();
         Configuration.GetSection("rabbitmq").Bind(rabbitMQ);

         services.AddMassTransit(x =>
         {
            x.AddConsumer<CreateProductHandler>();
            x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
               cfg.Host(new Uri(rabbitMQ.ConnectionString), hostcfg =>
               {
                  hostcfg.Username(rabbitMQ.UserName);
                  hostcfg.Password(rabbitMQ.Password);
               });
               cfg.ReceiveEndpoint("create_product", ep => {

                  ep.PrefetchCount = 16;
                  ep.UseMessageRetry(retryConfig => { retryConfig.Interval(2, 100); });
                  ep.ConfigureConsumer<CreateProductHandler>(provider);
               });
            }));

         });
      }

      // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
      public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
      {
         if (env.IsDevelopment())
         {
            app.UseDeveloperExceptionPage();
         }
         var busControl = app.ApplicationServices.GetService<IBusControl>();
         busControl.Start();
         var DBInitializer = app.ApplicationServices.GetService<IDatabaseInitializer>();
         DBInitializer.InitializeAsync();
         app.UseHttpsRedirection();

         app.UseRouting();

         app.UseAuthorization();

         app.UseEndpoints(endpoints =>
         {
            endpoints.MapControllers();
         });
      }
   }
}
