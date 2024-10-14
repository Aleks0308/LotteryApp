using LotteryApp.Services;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LotteryApp1.Services
{
    public class FileCacheService : IFileCacheService
    {
        private readonly IFileService fileService = DependencyService.Get<IFileService>();
        public readonly string cacheSubFolder = "cache";

        public async Task<bool> WriteTextToCache(string filename, string text)
        {
            await fileService.WriteAllText(cacheSubFolder, filename, text);

            return await Task.FromResult(true);
        }

        public async Task<string> GetTextFromCache(string filename)
        {
            return await fileService.ReadAllText(cacheSubFolder, filename);
        }
    }
}
