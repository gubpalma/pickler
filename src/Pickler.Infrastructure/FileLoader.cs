using Pickler.Interfaces;
using System.IO;
using System.Threading.Tasks;

namespace Pickler.Infrastructure
{
    public class FileLoader : IFileLoader
    {
        public async Task<string> OpenAsync(string filePath)
        {
            return File.ReadAllText(filePath);
        }
    }
}
