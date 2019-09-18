using Pickler.Definition.Gherkin;
using Pickler.Infrastructure.Parsing.Gherkin;
using Pickler.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Pickler.Tests.Unit
{
    public class StepParserTests
    {
        private readonly TestContext _testContext;

        public StepParserTests()
        {
            _testContext = new TestContext();
        }

        [Fact]
        public void TestParseTwoBasicGivens()
        {
            _testContext.ArrangeTwoBasicGivens();
            _testContext.ActParseSteps();
            _testContext.AssertTwoBasicGivens();
        }

        [Fact]
        public void TestParseTwoBasicGivensWithExtendedSpace()
        {
            _testContext.ArrangeTwoBasicGivensWithExtendedSpace();
            _testContext.ActParseSteps();
            _testContext.AssertTwoBasicGivens();
        }

        [Fact]
        public void TestParseTwoBasicGivensWithComment()
        {
            _testContext.ArrangeTwoBasicGivensWithSingleComment();
            _testContext.ActParseSteps();
            _testContext.AssertTwoBasicGivens();
        }

        [Fact]
        public void ArrangeTwoBasicGivensWithInterspersedComments()
        {
            _testContext.ArrangeTwoBasicGivensWithInterspersedComments();
            _testContext.ActParseSteps();
            _testContext.AssertTwoBasicGivens();
        }

        private class TestContext
        {
            private readonly IStepParser<Given> _sut;
            private string _stepData;
            private IEnumerable<Step> _result;

            public TestContext() => _sut = new GivenParser();

            public void ArrangeTwoBasicGivens() => 
                _stepData = 
                    "Given numbers 4 and 5\r\n" +
                    "And calculator has low battery";

            public void ArrangeTwoBasicGivensWithExtendedSpace() =>
                _stepData = "Given numbers 4 and 5\r\n" +
                            "\r\n\r\n\r\n\n\n" + 
                            "And calculator has low battery";

            public void ArrangeTwoBasicGivensWithSingleComment() =>
                _stepData = 
                    "Given numbers 4 and 5\r\n" +
                    "# This is a comment and shouldn't be processed\r\n" +
                    "And calculator has low battery";

            public void ArrangeTwoBasicGivensWithInterspersedComments() =>
                _stepData =
                    "  # This is a comment and shouldn't be processed\r\n" +
                    "\t\t# This is also a comment and shouldn't be processed\r\n" +
                    "Given numbers 4 and 5\r\n" +
                    "\t    # This is another comment and shouldn't be processed\r\n" +
                    "\t    \t# Given When Then And But Keywords\r\n" +
                    "And calculator has low battery";

            public void ActParseSteps() => _result = _sut.Parse(_stepData);

            public void AssertTwoBasicGivens()
            {
                Assert.Equal(2, _result.Count());
                Assert.Equal("Given numbers 4 and 5", _result.ElementAt(0).Definition);
                Assert.Equal("And calculator has low battery", _result.ElementAt(1).Definition);
            }
        }
    }
}
