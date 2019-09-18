using Pickler.Definition.Gherkin;
using Pickler.Interfaces;
using System.Collections.Generic;

namespace Pickler.Infrastructure.Parsing.Gherkin
{
    public class ThenParser : BaseStepParser<Then>, IStepParser<Then>
    {
        public IEnumerable<Then> Parse(string data) => Parse(data, () => new Then());
    }
}
