using Pickler.Definition.Trx;
using Pickler.Interfaces;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Pickler.Infrastructure.Parsing.Trx
{
    public class TrxResultsParser : ITrxResultsParser
    {
        public TestRun Build(string data)
        {
            var stream = new MemoryStream(Encoding.Default.GetBytes(data));

            var reader = new XmlTextReader(stream);

            reader.Namespaces = false;

            var serializer = new XmlSerializer(typeof(TestRun));

            var result = serializer.Deserialize(reader) as TestRun;

            return result;
        }
    }
}
