using System;
using System.Collections.Generic;
using System.Linq;

namespace Pickler.Definition.Gherkin
{
    public class Feature : IEquatable<Feature>
    {
        public string Name { get; set; }

        public string Summary { get; set; }

        public Background Background { get; set; }

        public IEnumerable<Scenario> Scenarios { get; set; }

        public bool Equals(Feature other)
        {
            var equal = true;

            equal &=
                string
                    .Equals(other?.Name, Name, StringComparison.InvariantCultureIgnoreCase);

            equal &=
                Scenarios
                    .OrderBy(i => i)
                    .SequenceEqual(
                        (other?.Scenarios ?? new List<Scenario>())
                        .OrderBy(i => i));

            return equal;
        }
    }
}
