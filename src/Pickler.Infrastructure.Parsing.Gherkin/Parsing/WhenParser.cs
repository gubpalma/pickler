using System.Collections.Generic;
using Pickler.Definition.Gherkin;
using Pickler.Interfaces.Gherkin;

namespace Pickler.Infrastructure.Parsing.Gherkin.Parsing
{
    public class WhenParser : BaseStepParser<When>, IStepParser<When>
    {
        public IEnumerable<When> Parse(string data) => Parse(data, () => new When());
    }
}
