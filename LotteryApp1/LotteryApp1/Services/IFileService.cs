using System.Threading.Tasks;

namespace LotteryApp.Services
{
    public interface IFileService
    {
        Task WriteAllText(string subfolder, string fileName, string text);

        Task<string> ReadAllText(string subfolder, string fileName);
    }
}
