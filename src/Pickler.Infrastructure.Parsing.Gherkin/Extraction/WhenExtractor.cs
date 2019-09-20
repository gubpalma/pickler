using Pickler.Definition.Gherkin;
using Pickler.Interfaces.Gherkin;

namespace Pickler.Infrastructure.Parsing.Gherkin.Extraction
{
    public class WhenExtractor : BaseSectionExtractor<When>, ISectionExtractor<When>
    {
        private static readonly string WhenSectionRegex = "(When)(.|\n)*?(And)*?(?=(Then))";

        public WhenExtractor(IStepParser<When> parser) : base(parser, WhenSectionRegex) { }
    }
}