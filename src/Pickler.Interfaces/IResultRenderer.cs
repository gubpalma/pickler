using Pickler.Definition.Gherkin;
using Pickler.Definition.Trx;
using System.Threading.Tasks;

namespace Pickler.Interfaces
{
    public interface IResultRenderer
    {
        Task RenderAsync(Feature featureData, TestRun trxData, string filePath);
    }
}
