using System.Collections.Generic;
using Pickler.Definition.Gherkin;

namespace Pickler.Interfaces.Gherkin
{
    public interface IScenarioExtractor
    {
        IEnumerable<Scenario> Extract(string data, IEnumerable<string> tags);
    }
}
