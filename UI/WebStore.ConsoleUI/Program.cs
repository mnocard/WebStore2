using Microsoft.Extensions.Configuration;
using System;
using WebStore.Clients.Products;

namespace WebStore.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ReadKey();
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            var clients = new ProductsClient(builder);

            var product = clients.GetProducts();
            foreach (var item in product)
            {
                Console.WriteLine($"{item.Name} - {item.Price}");
            }
            Console.ReadKey();
        }
    }
}
