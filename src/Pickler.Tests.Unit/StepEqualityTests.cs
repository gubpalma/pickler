using Pickler.Definition.Gherkin;
using Xunit;

namespace Pickler.Tests.Unit
{
    public class StepEqualityTests
    {
        private readonly TestContext _testContext;

        public StepEqualityTests()
        {
            _testContext = new TestContext();
        }

        [Fact]
        public void TestEqualTypesAndEqualDefinitions()
        {
            _testContext.ArrangeTwoEqualSteps();
            _testContext.ActTestEquality();
            _testContext.AssertEqualSteps();
        }

        [Fact]
        public void TestEqualTypesAndEqualDefinitionsCased()
        {
            _testContext.ArrangeTwoEqualStepsDifferentCase();
            _testContext.ActTestEquality();
            _testContext.AssertEqualSteps();
        }

        [Fact]
        public void TestUnequalDefinitions()
        {
            _testContext.ArrangeTwoNonEqualStepsByDefinition();
            _testContext.ActTestEquality();
            _testContext.AssertUnequalSteps();
        }

        [Fact]
        public void TestUnequalTypes()
        {
            _testContext.ArrangeTwoNonEqualStepsByType();
            _testContext.ActTestEquality();
            _testContext.AssertUnequalSteps();
        }

        private class TestContext
        {
            private Step _stepOne;
            private Step _stepTwo;
            private bool _result;

            public void ArrangeTwoEqualSteps()
            {
                _stepOne = new Given { Definition = "Given this description"};
                _stepTwo = new Given { Definition = "Given this description" };
            }

            public void ArrangeTwoEqualStepsDifferentCase()
            {
                _stepOne = new Given { Definition = "Given this description" };
                _stepTwo = new Given { Definition = "GivEn THIS descRiptIon" };
            }

            public void ArrangeTwoNonEqualStepsByDefinition()
            {
                _stepOne = new Given { Definition = "Given this description\r\n" };
                _stepTwo = new Given { Definition = "Given this description" };
            }

            public void ArrangeTwoNonEqualStepsByType()
            {
                _stepOne = new Given { Definition = "Given this description" };
                _stepTwo = new Then { Definition = "Given this description" };
            }

            public void ActTestEquality() => _result = _stepOne.Equals(_stepTwo);

            public void AssertEqualSteps() => Assert.True(_result);

            public void AssertUnequalSteps() => Assert.False(_result);
        }
    }
}
