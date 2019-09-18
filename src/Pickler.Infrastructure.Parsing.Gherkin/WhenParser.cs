using Pickler.Definition.Gherkin;
using Pickler.Interfaces;
using System.Collections.Generic;

namespace Pickler.Infrastructure.Parsing.Gherkin
{
    public class WhenParser : BaseStepParser<When>, IStepParser<When>
    {
        public IEnumerable<When> Parse(string data) => Parse(data, () => new When());
    }
}
