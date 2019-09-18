using Pickler.Definition.Gherkin;

namespace Pickler.Interfaces
{
    public interface IFeatureParser
    {
        Feature ParseFeature(string data);
    }
}
