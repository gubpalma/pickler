using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Pickler.Definition.Gherkin;
using Pickler.Infrastructure.Parsing.Gherkin.Extensions;
using Pickler.Interfaces.Gherkin;

namespace Pickler.Infrastructure.Parsing.Gherkin.Extraction
{
    public class ScenarioExtractor : IScenarioExtractor
    {
        private static readonly string ScenarioSplitterRegex = "(?=(?:Scenario: |Scenario Outline: ){1}?)(?:Scenario: |Scenario Outline: ){1}?";
        private static readonly string ScenarioNameRegex = ".*\n";

        private readonly ISectionExtractor<Given> _givenExtractor;
        private readonly ISectionExtractor<When> _whenExtractor;
        private readonly ISectionExtractor<Then> _thenExtractor;
        private readonly IOutlineParser _outlineParser;

        public ScenarioExtractor(
            ISectionExtractor<Given> givenExtractor, 
            ISectionExtractor<When> whenExtractor, 
            ISectionExtractor<Then> thenExtractor, 
            IOutlineParser outlineParser)
        {
            _givenExtractor = givenExtractor;
            _whenExtractor = whenExtractor;
            _thenExtractor = thenExtractor;
            _outlineParser = outlineParser;
        }

        public IEnumerable<Scenario> Extract(string data)
        {
            var scenarios =
                new Regex(ScenarioSplitterRegex)
                    .Split(data);

            if (scenarios.Length <= 0)
                throw new Exception("Scenarios could not be parsed from the feature file.");

            var featureScenarios = new List<Scenario>();

            foreach (var scenario in scenarios.Skip(1))
            {
                var parameters = _outlineParser.Parse(scenario);

                var scenarioName =
                    new Regex(ScenarioNameRegex)
                        .Match(scenario)
                        .Value;

                if (string.IsNullOrEmpty(scenarioName)) continue;

                var featureScenario = new Scenario();
                featureScenario.Name = scenarioName.ToFormatted();

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
