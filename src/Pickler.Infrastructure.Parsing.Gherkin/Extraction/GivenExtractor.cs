using Pickler.Definition.Gherkin;
using Pickler.Interfaces.Gherkin;

namespace Pickler.Infrastructure.Parsing.Gherkin.Extraction
{
    public class GivenExtractor : BaseSectionExtractor<Given>, ISectionExtractor<Given>
    {
        private static readonly string GivenSectionRegex = "(Given)(.|\n)*?(And)*?(?=(When|Then))";

        public GivenExtractor(IStepParser<Given> parser) : base(parser, GivenSectionRegex) { }
    }
}
