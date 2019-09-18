using System.Xml.Serialization;

namespace Pickler.Definition.Trx
{
    public class ResultSummary
    {
        [XmlAttribute("outcome")]
        public string Outcome { get; set; }

        public Counters Counters { get; set; }

        public Output Output { get; set; }

        public RunInfo[] RunInfos { get; set; }
    }
}
