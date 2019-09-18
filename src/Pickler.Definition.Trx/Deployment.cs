using System.Xml.Serialization;

namespace Pickler.Definition.Trx
{
    public class Deployment
    {
        [XmlAttribute("runDeploymentRoot")]
        public string RunDeploymentRoot { get; set; }
    }
}
