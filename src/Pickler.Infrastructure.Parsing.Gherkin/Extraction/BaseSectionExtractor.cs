using System.Collections.Generic;
using System.Text.RegularExpressions;
using Pickler.Definition.Gherkin;
using Pickler.Interfaces.Gherkin;

namespace Pickler.Infrastructure.Parsing.Gherkin.Extraction
{
    public class BaseSectionExtractor<T> where T : Step
    {
        private readonly string _sectionRegex;

        private readonly IStepParser<T> _stepParser;

        public BaseSectionExtractor(
            IStepParser<T> stepParser,
            string sectionRegex)
        {
            _stepParser = stepParser;
            _sectionRegex = sectionRegex;
        }

        public IEnumerable<T> Extract(string scenario)
        {
            var section =
                new Regex(_sectionRegex)
                    .Match(scenario)
                    .Value;

            return _stepParser.Parse(section);
        }
    }
}