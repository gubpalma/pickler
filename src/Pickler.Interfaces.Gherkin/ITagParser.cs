using System.Collections.Generic;

namespace Pickler.Interfaces.Gherkin
{
    public interface ITagParser
    {
        IEnumerable<string> Parse(string data);
    }
}
