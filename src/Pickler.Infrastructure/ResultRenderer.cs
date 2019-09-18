using Pickler.Definition.Gherkin;
using Pickler.Definition.Trx;
using Pickler.Definition.Trx.Enum;
using Pickler.Interfaces;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Pickler.Infrastructure
{
    public class ResultRenderer : IResultRenderer
    {
        private static string[] _keywords = { "Given", "When", "Then", "And" };

        private readonly IOutcomeParser _outcomeParser;

        public ResultRenderer(IOutcomeParser outcomeParser)
        {
            _outcomeParser = outcomeParser;
        }

        //TODO: convert results to HTML report and write file to path
        public Task RenderAsync(
            Feature featureData, 
            TestRun trxData, 
            string filePath)
        {
            var writer = new StringWriter();

            writer.WriteLine("<html>");
            writer.WriteLine("<head>");
            RenderStylesheet(writer);
            writer.WriteLine("</head>");
            writer.WriteLine("<body>");

            writer.WriteLine("<div class=\"container\">");
            writer.WriteLine("<div class=\"row\">");
            writer.WriteLine("<div class=\"lg-12\">");
            RenderFeatureHeader(writer, featureData);

            foreach(var scenario in featureData.Scenarios)
            {
                var outcome = OutcomeEnum.NotDefined;
                var matchingResult = trxData.Results.FirstOrDefault(r => r.TestName == scenario.Name);
                if (matchingResult != null)
                    outcome = _outcomeParser.Parse(matchingResult);

                RenderFeatureScenario(writer, scenario, trxData, outcome);
            }

            writer.WriteLine("</div>");
            writer.WriteLine("</div>");
            writer.WriteLine("</div>");

            RenderScripts(writer);
            writer.WriteLine("</body>");
            writer.WriteLine("</html>");

            File.WriteAllText(filePath, writer.ToString());

            return Task.CompletedTask;
        }

        private static void RenderStylesheet(StringWriter writer)
        {
            writer.WriteLine("<link rel=\"stylesheet\" href=\"https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css\" />");
        }

        private static void RenderScripts(StringWriter writer)
        {
            writer.WriteLine("<script src=\"https://code.jquery.com/jquery-3.2.1.slim.min.js\"></script>");
            writer.WriteLine("<script src=\"https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.9/umd/popper.min.js\"></script>");
            writer.WriteLine("<script src=\"https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js\"></script>");
        }

        private static void RenderFeatureHeader(StringWriter writer, Feature feature)
        {
            writer.WriteLine($"<h1>{feature.Name}</h1>");
            writer.WriteLine($"<small>{feature.Background}</small>");
        }

        private void RenderFeatureScenario(
            StringWriter writer, 
            Scenario scenario,
            TestRun trxData,
            OutcomeEnum outcome = OutcomeEnum.NotDefined)
        {
            writer.WriteLine("<div class=\"card card-body bg-light\">");
            writer.WriteLine("<table>");
            writer.WriteLine("<tr>");
            writer.WriteLine("<td>");
            writer.WriteLine($"<h4>{scenario.Name}</h4>");
            writer.WriteLine("</td>");
            writer.WriteLine("<td>");
            RenderOutcome(writer, outcome);
            writer.WriteLine("</td>");
            writer.WriteLine("</tr>");
            writer.WriteLine("</table>");

            writer.WriteLine("<div style=\"margin-left:2em;\">");
            foreach(var step in scenario.Steps)
            {
                RenderDefinition(writer, step);
            }
            writer.WriteLine("</div>");

            if (scenario.Parameters.Any())
            {
                writer.WriteLine("<table class=\"table table-striped table-bordered table-hover\">");
                writer.WriteLine("<tr>");

                foreach(var parameter in scenario.Parameters)
                    writer.WriteLine($"<th>{parameter.Name}</th>");

                writer.WriteLine("<th>Result</th>");
                writer.WriteLine("</tr>");

                for (var i=0; i < scenario.Parameters.ElementAt(0).Examples.Count(); i++)
                {
                    var matchingResults =
                        trxData
                        .Results
                        .Where(r => new Regex($"({scenario.Name})\\(.*\\)").IsMatch(r.TestName));

                    writer.WriteLine("<tr>");
                    for (var j=0; j < scenario.Parameters.Count(); j++)
                    {
                        var parameter = scenario.Parameters.ElementAt(j);
                        var value = parameter.Examples.ElementAt(i);
                        writer.WriteLine($"<td>{value}</td>");
                        matchingResults =
                            matchingResults
                            .Where(r => new Regex($"{parameter.Name}\\: \"{value}\"").IsMatch(r.TestName));
                    }

                    var matchingResult =
                        matchingResults
                        .GroupBy(r => r.TestName)
                        .Select(r => r.First())
                        .FirstOrDefault();

                    if (matchingResult != null)
                    {
                        var exampleOutcome = _outcomeParser.Parse(matchingResult);
                        writer.WriteLine("<td>");
                        RenderOutcome(writer, outcome);
                        writer.WriteLine("</td>");
                    }

                    writer.WriteLine("</tr>");
                }

                writer.WriteLine("</table>");
            }

            writer.WriteLine("</div>");

            writer.WriteLine("<br/>");
        }

        private static void RenderDefinition(StringWriter writer, Step step)
        {
            var stepDefinition = step.Definition;

            foreach (var keyword in _keywords)
                stepDefinition = stepDefinition.Replace(keyword, $"<b><i>{keyword}</i></b>");

            writer.WriteLine($"<p>{stepDefinition}</p>");
        }

        private static void RenderOutcome(StringWriter writer, OutcomeEnum outcome)
        {
            if (outcome != OutcomeEnum.NotDefined)
            {
                if (outcome == OutcomeEnum.Pass)
                    writer.WriteLine($"<div style=\"color:#1e7e34\"><small><b>Passed</b></small></div>");
                else if (outcome == OutcomeEnum.Fail)
                    writer.WriteLine($"<div style=\"color:#bd2130\"><small><b>Failed</b></small></div>");
            }
            else
                writer.WriteLine($"<div style=\"color:#d39e00\"><small><b>Inconclusive</b></small></div>");
        }
    }
}
