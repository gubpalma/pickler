using System;
using System.Xml.Serialization;

namespace Pickler.Definition.Trx
{
    [XmlRoot(ElementName = "TestRun")]
    public class TestRun
    {
        [XmlAttribute("id")]
        public Guid Id { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("runUser")]
        public string RunUser { get; set; }

        public Times Times { get; set; }

        public TestSettings TestSettings { get; set; }

        public UnitTestResult[] Results { get; set; }

        public UnitTest[] TestDefinitions { get; set; }

        public TestEntry[] TestEntries { get; set; }

        public TestList[] TestLists { get; set; }

        public ResultSummary ResultSummary { get; set; }
    }
}
