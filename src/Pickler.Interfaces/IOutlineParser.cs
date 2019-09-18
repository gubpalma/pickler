using Pickler.Definition.Gherkin;
using System.Collections.Generic;

namespace Pickler.Interfaces
{
    public interface IOutlineParser
    {
        IEnumerable<Parameter> ParseOutline(string data);
    }
}
