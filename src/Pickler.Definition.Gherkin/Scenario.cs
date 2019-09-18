using System.Collections.Generic;

namespace Pickler.Definition.Gherkin
{
    public class Scenario
    {
        public string Name { get; set; }

        public IEnumerable<Parameter> Parameters { get; set; }

        public IEnumerable<Step> Steps { get; set; }
    }
}
