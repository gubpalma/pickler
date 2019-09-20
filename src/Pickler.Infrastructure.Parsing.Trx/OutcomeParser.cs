using Pickler.Definition.Trx;
using Pickler.Definition.Trx.Enum;
using System.Collections.Generic;
using Pickler.Interfaces.Trx;

namespace Pickler.Infrastructure.Parsing.Trx
{
    public class OutcomeParser : IOutcomeParser
    {
        private readonly Dictionary<string, OutcomeEnum> _outcomes =
            new Dictionary<string, OutcomeEnum>
            {
                { "PASSED", OutcomeEnum.Pass },
                { "FAILED", OutcomeEnum.Fail },
                { "ABORTED", OutcomeEnum.Aborted },
                { "INCONCLUSIVE", OutcomeEnum.Inconclusive }
            };

        public OutcomeEnum Parse(UnitTestResult result) => Parse(result?.Outcome?.ToUpper());

        private OutcomeEnum Parse(string result)
        {
            if (_outcomes.ContainsKey(result))
                return _outcomes[result];

            return OutcomeEnum.NotDefined;
        }
    }
}
