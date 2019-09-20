using System.Collections.Generic;
using Pickler.Definition.Gherkin;
using Pickler.Interfaces.Gherkin;

namespace Pickler.Infrastructure.Parsing.Gherkin.Parsing
{
    public class GivenParser : BaseStepParser<Given>, IStepParser<Given>
    {
        public IEnumerable<Given> Parse(string data) => Parse(data, () => new Given());
    }
}
