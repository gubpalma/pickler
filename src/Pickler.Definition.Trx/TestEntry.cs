using System;
using System.Xml.Serialization;

namespace Pickler.Definition.Trx
{
    public class TestEntry
    {
        [XmlAttribute("testId")]
        public Guid TestId { get; set; }

        [XmlAttribute("executionId")]
        public Guid ExecutionId { get; set; }

        [XmlAttribute("testListId")]
        public Guid TestListId { get; set; }
    }
}
