using Pickler.Definition.Trx;

namespace Pickler.Interfaces.Trx
{
    public interface ITrxResultsParser
    {
        TestRun Build(string data);
    }
}
