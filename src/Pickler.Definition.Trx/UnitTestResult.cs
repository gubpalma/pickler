using System;
using System.Xml.Serialization;

namespace Pickler.Definition.Trx
{
    public class UnitTestResult
    {
        [XmlAttribute("executionId")]
        public Guid ExecutionId { get; set; }

        [XmlAttribute("testId")]
        public Guid TestId { get; set; }

        [XmlAttribute("testName")]
        public string TestName { get; set; }

        [XmlAttribute("computerName")]
        public string ComputerName { get; set; }

        [XmlAttribute("duration")]
        public string Duration { get; set; }

        [XmlAttribute("startTime")]
        public DateTime StartTime { get; set; }

        [XmlAttribute("endTime")]
        public DateTime EndTime { get; set; }

        [XmlAttribute("testType")]
        public Guid TestType { get; set; }

        [XmlAttribute("outcome")]
        public string Outcome { get; set; }

        [XmlAttribute("testListId")]
        public Guid TestListId { get; set; }

        [XmlAttribute("relativeResultsDirectory")]
        public Guid RelativeResultsDirectory { get; set; }

        public Output Output { get; set; }

    }
}
