using System.Threading.Tasks;

namespace LotteryApp1.Services
{
    public interface IFileCacheService
    {
        Task<bool> WriteTextToCache(string filename, string text);

        Task<string> GetTextFromCache(string filename);
    }
}
