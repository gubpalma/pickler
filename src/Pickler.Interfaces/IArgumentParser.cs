using Pickler.Interfaces.Options;

namespace Pickler.Interfaces
{
    public interface IArgumentParser
    {
        PicklerOptions Parse(string[] args);
    }
}
