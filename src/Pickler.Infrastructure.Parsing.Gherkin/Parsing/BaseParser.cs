using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pickler.Infrastructure.Parsing.Gherkin.Definition;

namespace Pickler.Infrastructure.Parsing.Gherkin.Parsing
{
    public class BaseParser
    {
        public static readonly string CommentDirective = "#";
        public static readonly string TagDirective = "@";

        protected static ParsedBlock[] ParseBlock(string data, params string[] sectionDelimiters)
        {
            ParsedBlock currentBlock = null;

            var results = new List<ParsedBlock>();

            var lines =
                data
                    .Split(new [] {'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries);

            var blockData = new StringBuilder();
            var comments = new List<string>();
            var tags = new List<string>();

            for (var i = 0; i < lines.Length; i++)
            {
                // Delimiter found - work backwards for comments and tags
                if (sectionDelimiters.Any(d => lines[i].TrimStart().StartsWith(d)))
                {
                    if (currentBlock != null)
                    {
                        currentBlock.CommentLines = comments;
                        currentBlock.TagLines = tags;
                        currentBlock.Data = blockData.ToString();
                        results.Add(currentBlock);
                    }
                    currentBlock = new ParsedBlock();
                    comments = new List<string>();
                    tags = new List<string>();
                    blockData = new StringBuilder();
                    blockData.AppendLine(lines[i]);

                    for (var j = Math.Max(0, i - 1); j >= 0; j--)
                    {
                        if (lines[j].TrimStart().StartsWith(CommentDirective)) comments.Add(lines[j]);
                        else if (lines[j].TrimStart().StartsWith(TagDirective)) tags.Add(lines[j]);
                        else if (string.IsNullOrEmpty(lines[j].Trim())) { }
                        else break;
                    }
                }
                else if (i == lines.Length - 1)
                {
                    if (currentBlock != null)
                    {
                        blockData.AppendLine(lines[i]);
                        currentBlock.CommentLines = comments;
                        currentBlock.TagLines = tags;
                        currentBlock.Data = blockData.ToString();
                        results.Add(currentBlock);
                    }
                }
                else
                {
                    if (lines[i].TrimStart().StartsWith(CommentDirective)) { }
                    else if (lines[i].TrimStart().StartsWith(TagDirective)) { }
                    else blockData.AppendLine(lines[i]);
                }
            }

            return results.ToArray();
        }
    }
}
