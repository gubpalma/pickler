using System;
using System.Xml.Serialization;

namespace Pickler.Definition.Trx
{
    public class TestList
    {
        [XmlAttribute("id")]
        public Guid Id { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }
    }
}
