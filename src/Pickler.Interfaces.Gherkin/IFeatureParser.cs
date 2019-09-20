using Pickler.Definition.Gherkin;

namespace Pickler.Interfaces.Gherkin
{
    public interface IFeatureParser
    {
        Feature Parse(string data);
    }
}
