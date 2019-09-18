using System;
using System.Xml.Serialization;

namespace Pickler.Definition.Trx
{
    public class UnitTest
    {
        [XmlAttribute("id")]
        public Guid Id { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("storage")]
        public string Storage { get; set; }

        [XmlAttribute("computerName")]
        public string ComputerName { get; set; }

        public Execution Execution { get; set; }

        public TestMethod TestMethod { get; set; }
    }
}
