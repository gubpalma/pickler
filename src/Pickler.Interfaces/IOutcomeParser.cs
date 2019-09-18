using Pickler.Definition.Trx;
using Pickler.Definition.Trx.Enum;

namespace Pickler.Interfaces
{
    public interface IOutcomeParser
    {
        OutcomeEnum Parse(UnitTestResult result);
    }
}
