using System.Threading.Tasks;

namespace Pickler.Interfaces
{
    public interface IFileLoader
    {
        Task<string> OpenAsync(string filePath);
    }
}
