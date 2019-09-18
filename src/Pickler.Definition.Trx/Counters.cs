using System.Xml.Serialization;

namespace Pickler.Definition.Trx
{
    public class Counters
    {
        [XmlAttribute("total")]
        public long Total { get; set; }

        [XmlAttribute("executed")]
        public long Executed { get; set; }

        [XmlAttribute("passed")]
        public long Passed { get; set; }

        [XmlAttribute("failed")]
        public long Failed { get; set; }

        [XmlAttribute("error")]
        public long Error { get; set; }

        [XmlAttribute("timeout")]
        public long Timeout { get; set; }

        [XmlAttribute("aborted")]
        public long Aborted { get; set; }

        [XmlAttribute("inconclusive")]
        public long Inconclusive { get; set; }

        [XmlAttribute("passedButRunAborted")]
        public long PassedButRunAborted { get; set; }

        [XmlAttribute("notRunnable")]
        public long NotRunnable { get; set; }

        [XmlAttribute("notExecuted")]
        public long NotExecuted { get; set; }

        [XmlAttribute("disconnected")]
        public long Disconnected { get; set; }

        [XmlAttribute("warning")]
        public long Warning { get; set; }

        [XmlAttribute("completed")]
        public long Completed { get; set; }

        [XmlAttribute("inProgress")]
        public long InProgress { get; set; }

        [XmlAttribute("pending")]
        public long Pending { get; set; }
    }
}
