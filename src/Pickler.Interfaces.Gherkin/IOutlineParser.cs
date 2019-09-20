using System.Collections.Generic;
using Pickler.Definition.Gherkin;

namespace Pickler.Interfaces.Gherkin
{
    public interface IOutlineParser
    {
        IEnumerable<Parameter> Parse(string data);
    }
}
