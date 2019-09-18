using Microsoft.Extensions.DependencyInjection;
using Pickler.Application;
using Pickler.Definition.Gherkin;
using Pickler.Infrastructure;
using Pickler.Infrastructure.Parsing.Gherkin;
using Pickler.Infrastructure.Parsing.Trx;
using Pickler.Interfaces;

namespace Pickler.Runner
{
    internal static class Startup
    {
        public static void UseStartup(this IServiceCollection services)
        {
            services
                .AddTransient<IArgumentParser, ArgumentParser>()
                .AddTransient<IFileLoader, FileLoader>()
                .AddTransient<IResultRenderer, ResultRenderer>()
                .AddTransient<IApplication, PicklerApplication>();

            services
                .AddTransient<IOutcomeParser, OutcomeParser>()
                .AddTransient<ITrxResultsParser, TrxResultsParser>();

            services
                .AddTransient<IFeatureParser, FeatureParser>()
                .AddScoped<IStepParser<Given>, GivenParser>()
                .AddScoped<IStepParser<When>, WhenParser>()
                .AddScoped<IStepParser<Then>, ThenParser>()
                .AddTransient<IOutlineParser, OutlineParser>();
        }
    }
}
