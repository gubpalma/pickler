using Pickler.Definition.Gherkin;
using System.Collections.Generic;

namespace Pickler.Interfaces
{
    public interface IStepParser<T> where T : Step
    {
        IEnumerable<T> Parse(string data);
    }
}
