using System.Xml.Serialization;

namespace Pickler.Definition.Trx
{
    public class TestMethod
    {
        [XmlAttribute("codeBase")]
        public string CodeBase { get; set; }

        [XmlAttribute("adapterTypeName")]
        public string AdapterTypeName { get; set; }

        [XmlAttribute("className")]
        public string ClassName { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }
    }
}
