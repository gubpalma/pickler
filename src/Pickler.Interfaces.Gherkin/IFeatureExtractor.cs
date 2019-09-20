using Pickler.Definition.Gherkin;

namespace Pickler.Interfaces.Gherkin
{
    public interface IFeatureExtractor
    {
        Feature Extract(string data);
    }
}
