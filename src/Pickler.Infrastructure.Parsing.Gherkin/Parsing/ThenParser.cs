using System.Collections.Generic;
using Pickler.Definition.Gherkin;
using Pickler.Interfaces.Gherkin;

namespace Pickler.Infrastructure.Parsing.Gherkin.Parsing
{
    public class ThenParser : BaseStepParser<Then>, IStepParser<Then>
    {
        public IEnumerable<Then> Parse(string data) => Parse(data, () => new Then());
    }
}
