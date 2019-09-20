using System.Collections.Generic;
using Pickler.Definition.Gherkin;

namespace Pickler.Interfaces.Gherkin
{
    public interface ISectionExtractor<T> where T : Step
    {
        IEnumerable<T> Extract(string scenario);
    }
}