namespace Pickler.Infrastructure.Parsing.Gherkin.Extensions
{
    internal static class StringEx
    {
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
    }
}
