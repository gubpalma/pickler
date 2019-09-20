using Microsoft.Extensions.DependencyInjection;
using Pickler.Application;
using Pickler.Definition.Gherkin;
using Pickler.Infrastructure;
using Pickler.Infrastructure.Parsing.Gherkin.Extraction;
using Pickler.Infrastructure.Parsing.Gherkin.Parsing;
using Pickler.Infrastructure.Parsing.Trx;
using Pickler.Interfaces;
using Pickler.Interfaces.Gherkin;
using Pickler.Interfaces.Trx;

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
                .AddTransient<IFeatureExtractor, FeatureExtractor>()
                .AddTransient<IScenarioExtractor, ScenarioExtractor>()
                .AddScoped<ISectionExtractor<Given>, GivenExtractor>()
                .AddScoped<ISectionExtractor<When>, WhenExtractor>()
                .AddScoped<ISectionExtractor<Then>, ThenExtractor>()
                .AddScoped<IStepParser<Given>, GivenParser>()
                .AddScoped<IStepParser<When>, WhenParser>()
                .AddScoped<IStepParser<Then>, ThenParser>()
                .AddTransient<IOutlineParser, OutlineParser>();
        }
    }
}
