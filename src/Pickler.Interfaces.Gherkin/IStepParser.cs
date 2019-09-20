using System.Collections.Generic;
using Pickler.Definition.Gherkin;

namespace Pickler.Interfaces.Gherkin
{
    public interface IStepParser<T> where T : Step
    {
        IEnumerable<T> Parse(string data);
    }
}
