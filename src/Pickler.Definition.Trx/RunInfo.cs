using System.Xml.Serialization;

namespace Pickler.Definition.Trx
{
    public class RunInfo
    {
        [XmlAttribute("computerName")]
        public string ComputerName { get; set; }

        [XmlAttribute("outcome")]
        public string Outcome { get; set; }

        [XmlAttribute("timestamp")]
        public string Timestamp { get; set; }

        public string Text { get; set; }
    }
}
