using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Pickler.Definition.Gherkin;
using Pickler.Infrastructure.Parsing.Gherkin.Extensions;
using Pickler.Infrastructure.Parsing.Gherkin.Parsing;
using Pickler.Interfaces.Gherkin;

namespace Pickler.Infrastructure.Parsing.Gherkin.Extraction
{
    public class ScenarioExtractor : BaseParser, IScenarioExtractor
    {
        private static readonly string ScenarioBlockRegex = "((@((?!Feature).|\n)*)?)((?:Scenario: |Scenario Outline: ){1}?)(.|\n)*";
        private static readonly string ScenarioNameRegex = "(?<=(Scenario: |Scenario Outline: )).*";

        private readonly ISectionExtractor<Given> _givenExtractor;
        private readonly ISectionExtractor<When> _whenExtractor;
        private readonly ISectionExtractor<Then> _thenExtractor;
        private readonly IOutlineParser _outlineParser;
        private readonly ITagParser _tagParser;

        public ScenarioExtractor(
            ISectionExtractor<Given> givenExtractor, 
            ISectionExtractor<When> whenExtractor, 
            ISectionExtractor<Then> thenExtractor, 
            IOutlineParser outlineParser,
            ITagParser tagParser)
        {
            _givenExtractor = givenExtractor;
            _whenExtractor = whenExtractor;
            _thenExtractor = thenExtractor;
            _outlineParser = outlineParser;
            _tagParser = tagParser;
        }

        public IEnumerable<Scenario> Extract(
            string data,
            IEnumerable<string> tags = null)
        {
            tags = tags ?? new List<string>();

            var scenarioBlock = 
                new Regex(ScenarioBlockRegex)
                    .Match(data)
                    .Value;

            var parsedBlocks =
                ParseBlock(scenarioBlock, "Scenario: ", "Scenario Outline: ");

            var featureScenarios = new List<Scenario>();

            foreach (var parsedBlock in parsedBlocks)
            {
                var scenario = parsedBlock.Data;
                if (string.IsNullOrEmpty(scenario)) continue;

                var parameters = _outlineParser.Parse(scenario);

                var scenarioTags = _tagParser.Parse(string.Join(" ", parsedBlock.TagLines));

                var allTags = new List<string>();

                allTags
                    .AddRange(scenarioTags.Union(tags));

                var scenarioName =
                    new Regex(ScenarioNameRegex)
                        .Match(scenario)
                        .Value;

                if (string.IsNullOrEmpty(scenarioName)) continue;

                var featureScenario = new Scenario
                {
                    Name = scenarioName.ToFormatted(),
                    Tags = allTags
                };

                featureScenarios.Add(featureScenario);

                var scenarioSteps = new List<Step>();

                scenarioSteps.AddRange(_givenExtractor.Extract(scenario));
                scenarioSteps.AddRange(_whenExtractor.Extract(scenario));
                scenarioSteps.AddRange(_thenExtractor.Extract(scenario));

                featureScenario.Steps = scenarioSteps.ToArray();
                featureScenario.Parameters = parameters ?? new List<Parameter>();
            }

            return featureScenarios.ToArray();
        }
    }
}
