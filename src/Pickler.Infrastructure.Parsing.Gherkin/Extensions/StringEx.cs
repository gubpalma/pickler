using System.Collections.Generic;
using System.Linq;

namespace Pickler.Infrastructure.Parsing.Gherkin.Extensions
{
    internal static class StringEx
    {
        internal static string CommentDelimiter = "#";

        public static string ToFormatted(this string value)
        {
            return value
                .Replace("\n", "")
                .Replace("\r", "")
                .Trim();
        }

        public static string ToHtml(this string value)
        {
            return value
                .Replace("<", "&lt;")
                .Replace(">", "&gt;")
                .Trim();
        }

        public static string ToNamedParameter(this string value)
        {
            return value
                .Replace("\n", "")
                .Replace("\r", "")
                .Replace("<", "")
                .Replace(">", "")
                .Trim();
        }

        public static IEnumerable<string> ToCommentFilteredLines(this string data)
        {
            var split = data.Split('\n');
            split = split.Select(o => o.Trim()).ToArray();
            split = split.Where(o => !string.IsNullOrEmpty(o)).ToArray();
            split = split.Where(o => !o.StartsWith(CommentDelimiter)).ToArray();

            return 
                data
                    .Split('\n')
                    .Select(o => o.Trim())
                    .Where(o => !string.IsNullOrEmpty(o))
                    .Where(o => !o.StartsWith(CommentDelimiter));
        }
    }
}
