using Pickler.Definition.Gherkin;
using Pickler.Interfaces;
using System.Collections.Generic;

namespace Pickler.Infrastructure.Parsing.Gherkin
{
    public class GivenParser : BaseStepParser<Given>, IStepParser<Given>
    {
        public IEnumerable<Given> Parse(string data) => Parse(data, () => new Given());
    }
}
