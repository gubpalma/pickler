using Newtonsoft.Json;
using Pickler.Definition.Gherkin;
using Pickler.Infrastructure.Parsing.Gherkin.Extraction;
using Pickler.Infrastructure.Parsing.Gherkin.Parsing;
using Pickler.Interfaces.Gherkin;
using Xunit;

namespace Pickler.Tests.Integration
{
    public class FeatureExtractorTests
    {
        private readonly TestContext _testContext;

        public FeatureExtractorTests()
        {
            _testContext = new TestContext();
        }

        [Theory]
        [InlineData("Basic.feature", "Basic.json")]
        [InlineData("Example.feature", "Example.json")]
        public void TestParseSteps(string featureFile, string jsonFile)
        {
            _testContext.ArrangeFeature(featureFile);
            _testContext.ActExtractFeature();
            _testContext.AssertFeature(jsonFile);
        }

        private class TestContext : BaseTestContext
        {
            private readonly IFeatureExtractor _sut;
            private string _stepData;
            private Feature _result;

            public TestContext()
            {
                var givenSectionExtractor = new GivenExtractor(new GivenParser());
                var whenSectionExtractor = new WhenExtractor(new WhenParser());
                var thenSectionExtractor = new ThenExtractor(new ThenParser());
                var outlineParser = new OutlineParser();
                var scenarioExtractor = new ScenarioExtractor(
                    givenSectionExtractor,
                    whenSectionExtractor,
                    thenSectionExtractor,
                    outlineParser);

                _sut = new FeatureExtractor(
                    new FeatureParser(),
                    scenarioExtractor);
            }

            public void ArrangeFeature(string featureFileName)
            {
                _stepData = LoadGherkinFeature(featureFileName);
            }

            public void ActExtractFeature() => _result = _sut.Extract(_stepData);

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