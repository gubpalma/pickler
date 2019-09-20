using Pickler.Definition.Gherkin;
using Pickler.Interfaces.Gherkin;

namespace Pickler.Infrastructure.Parsing.Gherkin.Extraction
{
    public class ThenExtractor : BaseSectionExtractor<Then>, ISectionExtractor<Then>
    {
        private static readonly string ThenSectionRegex = "((Then)(.|\n)*?(?=($|Examples|@){1}))";

        public ThenExtractor(IStepParser<Then> parser) : base(parser, ThenSectionRegex) { }
    }
}