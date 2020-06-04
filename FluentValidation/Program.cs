using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ShoppingCart.Data;

namespace FluentValidation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            SeedData(host);

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        static void SeedData(IHost host)
        {
            using var scope = host.Services.CreateScope();
            using var db = scope.ServiceProvider.GetService<AppDbContext>();

            db.Items.Add(new Models.ShoppingItem()
            {
                Id = 1,
                Name = "Carro Controle Remoto",
                Price = 59.9m
            });

            db.Items.Add(new Models.ShoppingItem()
            {
                Id = 2,
                Name = "Cubo Mágico",
                Price = 45m
            });

            db.Items.Add(new Models.ShoppingItem()
            {
                Id = 3,
                Name = "Mascara Start Wars Darth Vader",
                Price = 29.99m
            });

            db.SaveChanges();
        }
    }
}
