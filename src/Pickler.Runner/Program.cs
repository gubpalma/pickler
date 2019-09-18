using Microsoft.Extensions.DependencyInjection;
using Pickler.Interfaces;
using System;
using System.Threading.Tasks;

namespace Pickler.Runner
{
    internal static class Program
    {
        static async Task Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.UseStartup();

            var serviceProvider =
                serviceCollection
                .BuildServiceProvider();

            var app = serviceProvider.GetRequiredService<IApplication>();

            await app.RunAsync(args);

            Console.WriteLine("Completed");
        }
    }
}
