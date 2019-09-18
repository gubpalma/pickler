using System.Collections.Generic;

namespace Pickler.Definition.Gherkin
{
    public class Parameter
    {
        public string Name { get; set; }

        public IEnumerable<string> Examples { get; set; }
    }
}
