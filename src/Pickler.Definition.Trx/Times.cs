using System;
using System.Xml.Serialization;

namespace Pickler.Definition.Trx
{
    public class Times
    {
        [XmlAttribute("creation")]
        public DateTime Creation { get; set; }

        [XmlAttribute("queuing")]
        public DateTime Queuing { get; set; }

        [XmlAttribute("start")]        
        public DateTime Start { get; set; }

        [XmlAttribute("finish")]
        public DateTime Finish { get; set; }
    }
}
