﻿using System;
using System.Text.RegularExpressions;
using Pickler.Definition.Gherkin;
using Pickler.Infrastructure.Parsing.Gherkin.Extensions;
using Pickler.Interfaces.Gherkin;

namespace Pickler.Infrastructure.Parsing.Gherkin.Parsing
{
    public class FeatureParser : IFeatureParser
    {
        private static readonly string FeatureNameRegex = "(?<=Feature:).*\n";
        private static readonly string SummaryRegex = "(?<=(Feature:).*\n)(.|\n)*?((?=(Scenario))|$)";

        public Feature Parse(string data)
        {
            var result = new Feature();

            var name =
                new Regex(FeatureNameRegex)
                .Match(data)
                .Value;

            if (string.IsNullOrEmpty(name))
                throw new Exception("Could not find feature title.");

            result.Name = name.ToFormatted();

            var summary =
                string.Join(
                    " \r\n",
                    new Regex(SummaryRegex)
                        .Match(data)
                        .Value
                        .ToCommentFilteredLines());

            result.Summary = summary.ToFormatted();

            return result;
        }
    }
}
