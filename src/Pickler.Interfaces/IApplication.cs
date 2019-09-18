using System.Threading.Tasks;

namespace Pickler.Interfaces
{
    public interface IApplication
    {
        Task RunAsync(string[] args);
    }
}
