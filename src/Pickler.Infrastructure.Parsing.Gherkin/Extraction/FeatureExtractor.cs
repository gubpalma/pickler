using Pickler.Definition.Gherkin;
using Pickler.Interfaces.Gherkin;

namespace Pickler.Infrastructure.Parsing.Gherkin.Extraction
{
    public class FeatureExtractor : IFeatureExtractor
    {
        private readonly IFeatureParser _featureParser;
        private readonly IScenarioExtractor _scenarioExtractor;

        public FeatureExtractor(
            IFeatureParser featureParser, 
            IScenarioExtractor scenarioExtractor)
        {
            _featureParser = featureParser;
            _scenarioExtractor = scenarioExtractor;
        }

        public Feature Extract(string data)
        {
            var result = 
                _featureParser
                    .Parse(data);

            result.Scenarios =
                _scenarioExtractor
                    .Extract(data);

            return result;
        }
    }
}

