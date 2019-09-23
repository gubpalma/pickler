using System.Collections.Generic;

namespace Pickler.Infrastructure.Parsing.Gherkin.Definition
{
    public class ParsedBlock
    {
        public IEnumerable<string> TagLines { get; set; }

        public IEnumerable<string> CommentLines { get; set; }

        public string Data { get; set; }
    }
}
