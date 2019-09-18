using System.IO;
using System.Reflection;

namespace Pickler.Tests.Integration
{
    internal class BaseTestContext
    {
        protected static string LoadGherkinFeature(string featureFileName) =>
            LoadFeature($"//Gherkin//{featureFileName}");

        protected static string LoadExpectedFeature(string expectedFileName) =>
            LoadFeature($"//Expected//{expectedFileName}");

        private static string LoadFeature(string filePath)
        {
            var currentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return File.ReadAllText($"{currentPath}{filePath}");
        }
    }
}
