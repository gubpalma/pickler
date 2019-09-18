using Pickler.Definition.Trx;

namespace Pickler.Interfaces
{
    public interface ITrxResultsParser
    {
        TestRun Build(string data);
    }
}
