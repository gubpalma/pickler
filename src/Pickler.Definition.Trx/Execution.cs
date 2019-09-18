using System;
using System.Xml.Serialization;

namespace Pickler.Definition.Trx
{
    public class Execution
    {
        [XmlAttribute("id")]
        public Guid Id { get; set; }
    }
}
