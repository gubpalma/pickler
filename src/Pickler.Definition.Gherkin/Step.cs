using System;

namespace Pickler.Definition.Gherkin
{
    public class Step : IEquatable<Step>, IComparable<Step>
    {
        public string Definition { get; set; }

        public bool Equals(Step other)
        {
            var equal = true;

            equal &= 
                string
                    .Equals(other?.Definition, Definition, StringComparison.InvariantCultureIgnoreCase);

            equal &= GetType() == other?.GetType();

            return equal;
        }

        public int CompareTo(Step other)
        {
            return
                string
                    .Compare(Definition, other?.Definition, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
