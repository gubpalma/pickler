using System;
using System.Collections.Generic;
using System.Linq;

namespace Pickler.Definition.Gherkin
{
    public class Scenario : IEquatable<Scenario>, IComparable<Scenario>
    {
        public string Name { get; set; }

        public IEnumerable<Parameter> Parameters { get; set; }

        public IEnumerable<Step> Steps { get; set; }

        public bool Equals(Scenario other)
        {
            var equal = true;

            equal &=
                string
                    .Equals(other?.Name, Name, StringComparison.InvariantCultureIgnoreCase);

            equal &=
                Steps
                    .OrderBy(i => i)
                    .SequenceEqual(
                        (other?.Steps ?? new List<Step>())
                            .OrderBy(i => i));

            return equal;
        }

        public int CompareTo(Scenario other)
        {
            return
                string
                    .Compare(Name, other?.Name, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
