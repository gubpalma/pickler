using System.Collections.Generic;
using Pickler.Definition.Gherkin;
using Xunit;

namespace Pickler.Tests.Unit
{
    public class ScenarioEqualityTests
    {
        private readonly TestContext _testContext;

        private static readonly Scenario StapleScenario =
            new Scenario
            {
                Name = "Test Scenario",
                Steps = new List<Step>
                {
                    new Given {Definition = "Given this description"},
                    new Then {Definition = "Then this description"}
                },
                Tags = new List<string> { "Calculations", "High Level Concept"}
            };

        public ScenarioEqualityTests()
        {
            _testContext = new TestContext();
        }

        [Fact]
        public void TestEqualByReference()
        {
            _testContext.ArrangeTwoScenariosByReference();
            _testContext.ActTestEquality();
            _testContext.AssertEqualScenarios();
        }

        [Fact]
        public void TestEqualTypesAndEqualDefinitions()
        {
            _testContext.ArrangeTwoDuplicateScenarios();
            _testContext.ActTestEquality();
            _testContext.AssertEqualScenarios();
        }

        [Fact]
        public void TestSameStepsDifferentNames()
        {
            _testContext.ArrangeSameStepsDifferentNames();
            _testContext.ActTestEquality();
            _testContext.AssertUnequalScenarios();
        }

        [Fact]
        public void TestSameStepsDifferentOrders()
        {
            _testContext.ArrangeSameStepsDifferentOrders();
            _testContext.ActTestEquality();
            _testContext.AssertEqualScenarios();
        }

        [Fact]
        public void TestSameNamesDifferentStepCounts()
        {
            _testContext.ArrangeSameNamesDifferentStepCounts();
            _testContext.ActTestEquality();
            _testContext.AssertUnequalScenarios();
        }

        [Fact]
        public void TestSameNamesDifferentTags()
        {
            _testContext.ArrangeSameNamesDifferentTags();
            _testContext.ActTestEquality();
            _testContext.AssertUnequalScenarios();
        }

        private class TestContext
        {
            private Scenario _scenarioOne;
            private Scenario _scenarioTwo;
            private bool _result;

            public void ArrangeTwoScenariosByReference()
            {
                _scenarioOne = StapleScenario;
                _scenarioTwo = _scenarioOne;
            }

            public void ArrangeTwoDuplicateScenarios()
            {
                _scenarioOne = StapleScenario;
                _scenarioTwo =
                    new Scenario
                    {
                        Name = "Test Scenario",
                        Steps = new List<Step>
                        {
                            new Given {Definition = "Given this description"},
                            new Then {Definition = "Then this description"}
                        },
                        Tags = new List<string> { "Calculations", "High Level Concept" }
                    };
            }

            public void ArrangeSameStepsDifferentNames()
            {
                _scenarioOne = StapleScenario;
                _scenarioTwo =
                    new Scenario
                    {
                        Name = "Different Name",
                        Steps = new List<Step>
                        {
                            new Given {Definition = "Given this description"},
                            new Then {Definition = "Then this description"}
                        },
                        Tags = new List<string> { "Calculations", "High Level Concept" }
                    };
            }

            public void ArrangeSameStepsDifferentOrders()
            {
                _scenarioOne = StapleScenario;
                _scenarioTwo =
                    new Scenario
                    {
                        Name = "Test Scenario",
                        Steps = new List<Step>
                        {
                            new Then {Definition = "Then this description"},
                            new Given {Definition = "Given this description"}
                        },
                        Tags = new List<string> { "Calculations", "High Level Concept" }
                    };
            }

            public void ArrangeSameNamesDifferentStepCounts()
            {
                _scenarioOne = StapleScenario;
                _scenarioTwo =
                    new Scenario
                    {
                        Name = "Test Scenario",
                        Steps = new List<Step>
                        {
                            new Given {Definition = "Given this description"}
                        },
                        Tags = new List<string> { "Calculations", "High Level Concept" }
                    };
            }

            public void ArrangeSameNamesDifferentTags()
            {
                _scenarioOne = StapleScenario;
                _scenarioTwo =
                    new Scenario
                    {
                        Name = "Test Scenario",
                        Steps = new List<Step>
                        {
                            new Given {Definition = "Given this description"}
                        },
                        Tags = new List<string> { "Low Level Concept" }
                    };
            }

            public void ActTestEquality() => _result = _scenarioOne.Equals(_scenarioTwo);

            public void AssertEqualScenarios() => Assert.True(_result);

            public void AssertUnequalScenarios() => Assert.False(_result);
        }
    }
}
