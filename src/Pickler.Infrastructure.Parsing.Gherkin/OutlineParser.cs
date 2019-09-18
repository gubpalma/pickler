﻿using Pickler.Definition.Gherkin;
using Pickler.Infrastructure.Parsing.Gherkin.Extensions;
using Pickler.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Pickler.Infrastructure.Parsing.Gherkin
{
    public class OutlineParser : IOutlineParser
    {
        private readonly string _parameterExtractor = "(\\<(\\w| )*\\>)";
        private readonly string _examplesExtractor = "(?<=Examples:)(.|\n)*\\|";

        public IEnumerable<Parameter> ParseOutline(string data)
        {
            var parameters = new List<Parameter>();

            if (string.IsNullOrEmpty(data)) return parameters;

            var foundParameters =
                new Regex(_parameterExtractor)
                .Matches(data);

            if (foundParameters.Count == 0) return parameters;

            foreach(var foundParameter in foundParameters)
            {
                parameters.Add(
                    new Parameter
                    {
                        Name = foundParameter
                        .ToString()
                        .ToNamedParameter()
                    });
            }

            var exampleTable =
                new Regex(_examplesExtractor)
                .Match(data)?.Value;

            if (string.IsNullOrEmpty(exampleTable))
                throw new Exception("Parameters were defined but no examples were specified.");

            var exampleRows = exampleTable.Split('\n');

            var exampleColumns = new Dictionary<int, string>();

            foreach(var exampleRow in exampleRows.Select(r => r.ToFormatted()))
            {
                if (string.IsNullOrEmpty(exampleRow)) continue;

                var rowData =
                    exampleRow
                    .Split('|')
                    .Select(c => c.ToFormatted())
                    .ToList();

                if (!exampleColumns.Any())
                {
                    foreach(var rowDataColumn in rowData)
                    {
                        if (string.IsNullOrEmpty(rowDataColumn)) continue;

                        var matchedColumn =
                            parameters
                            .FirstOrDefault(p => p.Name == rowDataColumn);

                        if (matchedColumn == null)
                            throw new Exception($"Could not find scenario outline column {rowDataColumn}.");

                        exampleColumns
                            .Add(rowData.IndexOf(rowDataColumn), rowDataColumn);
                    }
                }
                else
                {
                    for (var i=0; i < rowData.Count; i++)
                    {
                        if (exampleColumns.ContainsKey(i))
                        {
                            var matchedColumn =
                                parameters
                                .FirstOrDefault(p => p.Name == exampleColumns[i]);

                            matchedColumn.Examples = matchedColumn.Examples.Add(rowData[i]);
                        }
                    }
                }
            }

            return parameters;
        }
    }
}
