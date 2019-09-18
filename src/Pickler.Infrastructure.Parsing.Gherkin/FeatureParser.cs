using Pickler.Definition.Gherkin;
using Pickler.Infrastructure.Parsing.Gherkin.Extensions;
using Pickler.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Pickler.Infrastructure.Parsing.Gherkin
{
    public class FeatureParser : IFeatureParser
    {
        private readonly string _nameExtractor = "(?<=Feature:).*\n";
        private readonly string _summaryExtractor = "(?<=(Feature:).*\n)(.|\n)*?((?=(Scenario))|$)";
        private readonly string _scenarioSplitter = "(?=(?:Scenario: |Scenario Outline: ){1}?)(?:Scenario: |Scenario Outline: ){1}?";
        private readonly string _scenarioNameExtractor = ".*\n";

        private readonly string _givenExtractor = "(Given)(.|\n)*?(And)*?(?=(When|Then))";
        private readonly string _whenExtractor = "(When)(.|\n)*?(And)*?(?=(Then))";
        private readonly string _thenExtractor = "((Then)(.|\n)*?(?=($|Examples|@){1}))";

        private readonly IStepParser<Given> _givenParser;
        private readonly IStepParser<When> _whenParser;
        private readonly IStepParser<Then> _thenParser;
        private readonly IOutlineParser _outlineParser;

        public FeatureParser(
            IStepParser<Given> givenParser,
            IStepParser<When> whenParser,
            IStepParser<Then> thenParser,
            IOutlineParser outlineParser)
        {
            _givenParser = givenParser;
            _whenParser = whenParser;
            _thenParser = thenParser;
            _outlineParser = outlineParser;
        }

        public Feature ParseFeature(string data)
        {
            var result = new Feature();

            var name =
                new Regex(_nameExtractor)
                .Match(data)?.Value;

            if (string.IsNullOrEmpty(name))
                throw new Exception("Could not find feature title.");

            result.Name = name.ToFormatted();

            var summary =
                string.Join(
                    " \r\n",
                    new Regex(_summaryExtractor)
                        .Match(data)?.Value
                        .ToCommentFilteredLines());

            result.Summary = (summary ?? string.Empty).ToFormatted();

            var scenarios =
                new Regex(_scenarioSplitter)
                .Split(data);

            if (scenarios.Length <= 0)
                throw new Exception("Scenarios could not be parsed from the feature file.");

            var featureScenarios = new List<Scenario>();

            foreach(var scenario in scenarios.Skip(1))
            {
                var parameters = _outlineParser.ParseOutline(scenario);

                var scenarioName =
                    new Regex(_scenarioNameExtractor)
                    .Match(scenario)?.Value;

                if (string.IsNullOrEmpty(scenarioName)) continue;

                var featureScenario = new Scenario();
                featureScenario.Name = scenarioName.ToFormatted();

                featureScenarios.Add(featureScenario);

                var givenSection =
                    new Regex(_givenExtractor)
                    .Match(scenario)?.Value;

                var whenSection =
                    new Regex(_whenExtractor)
                    .Match(scenario)?.Value;

                var thenSection =
                    new Regex(_thenExtractor)
                    .Match(scenario)?.Value;

                var scenarioSteps = new List<Step>();

                scenarioSteps.AddRange(_givenParser.Parse(givenSection));
                scenarioSteps.AddRange(_whenParser.Parse(whenSection));
                scenarioSteps.AddRange(_thenParser.Parse(thenSection));

                featureScenario.Steps = scenarioSteps.ToArray();
                featureScenario.Parameters = parameters ?? new List<Parameter>();
            }

            result.Scenarios = featureScenarios.ToArray();

            return result;
        }
    }
}
