using Newtonsoft.Json;
using Pickler.Definition.Gherkin;
using Pickler.Infrastructure.Parsing.Gherkin;
using Pickler.Interfaces;
using Xunit;

namespace Pickler.Tests.Integration
{
    public class FeatureParserTests
    {
        private readonly TestContext _testContext;

        public FeatureParserTests()
        {
            _testContext = new TestContext();
        }

        [Theory]
        [InlineData("Basic.feature", "Basic.json")]
        [InlineData("Example.feature", "Example.json")]
        public void TestParseSteps(string featureFile, string jsonFile)
        {
            _testContext.ArrangeFeature(featureFile);
            _testContext.ActParseFeature();
            _testContext.AssertFeature(jsonFile);
        }

        private class TestContext : BaseTestContext
        {
            private readonly IFeatureParser _sut;
            private string _stepData;
            private Feature _result;

            public TestContext()
            {
                var givenParser = new GivenParser();
                var whenParser = new WhenParser();
                var thenParser = new ThenParser();
                var outlineParser = new OutlineParser();

                _sut = new FeatureParser(givenParser, whenParser, thenParser, outlineParser);
            }

            public void ArrangeFeature(string featureFileName)
            {
                _stepData = LoadGherkinFeature(featureFileName);
            }

            public void ActParseFeature() => _result = _sut.ParseFeature(_stepData);

            public void AssertFeature(string expectedFeatureFileName)
            {
                var settings =
                    new JsonSerializerSettings
                        {TypeNameHandling = TypeNameHandling.Auto};

                //delete this
                var serialized = JsonConvert.SerializeObject(_result, settings);

                var expectedData = LoadExpectedFeature(expectedFeatureFileName);

                var expectedFeature = JsonConvert.DeserializeObject<Feature>(expectedData, settings);

                Assert.Equal(expectedFeature, _result);
            }

        }
    }
}