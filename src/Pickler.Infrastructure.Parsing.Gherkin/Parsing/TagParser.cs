using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Pickler.Interfaces.Gherkin;

namespace Pickler.Infrastructure.Parsing.Gherkin.Parsing
{
    public class TagParser : ITagParser
    {
        private static readonly string TagRegex = "(\\@\\w+)";

        public IEnumerable<string> Parse(string data)
        {
            var tags =
                new Regex(TagRegex)
                    .Split(data)
                    .Where(t => t.StartsWith("@"));

            tags =
                tags
                    .Select(t => t.Remove(0, 1))
                    .ToList();

            return tags;
        }
    }
}
