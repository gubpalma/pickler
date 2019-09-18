using System.Collections.Generic;

namespace Pickler.Definition.Gherkin
{
    public class Feature
    {
        public string Name { get; set; }

        public string Summary { get; set; }

        public Background Background { get; set; }

        public IEnumerable<Scenario> Scenarios { get; set; }
    }
}
