using System.Threading.Tasks;
using Pickler.Definition.Gherkin;
using Pickler.Definition.Trx;

namespace Pickler.Interfaces
{
    public interface IResultRenderer
    {
        Task RenderAsync(Feature featureData, TestRun trxData, string filePath);
    }
}
