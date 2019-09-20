using System;
using System.Collections.Generic;
using Pickler.Definition.Gherkin;
using Pickler.Infrastructure.Parsing.Gherkin.Extensions;

namespace Pickler.Infrastructure.Parsing.Gherkin.Parsing
{
    public class BaseStepParser<T> where T : Step
    {
        protected IEnumerable<T> Parse(string data, Func<T> newStep)
        {
            var results = new List<T>();

            if (string.IsNullOrEmpty(data)) return results;

            var steps =
                data
                    .ToCommentFilteredLines();

            foreach(var step in steps)
            {
                var scenarioStep = newStep();

                scenarioStep.Definition =
                    step
                        .ToFormatted()
                        .ToHtml();

                results.Add(scenarioStep);
            }

            return results;
        }
    }
}
