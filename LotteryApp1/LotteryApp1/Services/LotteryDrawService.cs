using LotteryApp1.Models;
using LotteryApp1.Services;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LotteryApp.Services
{
    public class LotteryDrawService : ILotteryDrawService
    {
        private readonly IFileCacheService fileCacheService;
        private readonly ILotteryDrawMockDataService lotteryDrawMockDataService;

        public LotteryDrawService(IFileCacheService fileCacheService, ILotteryDrawMockDataService lotteryDrawMockDataService)
        {
            this.fileCacheService = fileCacheService;
            this.lotteryDrawMockDataService = lotteryDrawMockDataService;
        }

        public LotteryDrawService()
        {
            fileCacheService = DependencyService.Get<IFileCacheService>();
            lotteryDrawMockDataService = DependencyService.Get<ILotteryDrawMockDataService>();
        }

        public async Task<LotteryDrawResponse> GetLotteryDraws(LotteryDrawRequestType requestType, bool cached = false)
        {
            if (cached)
            {
                var cachedText = await fileCacheService.GetTextFromCache(GetLotteryDrawCacheFileName(requestType));
                if (!string.IsNullOrEmpty(cachedText))
                {
                    return JsonConvert.DeserializeObject<LotteryDrawResponse>(cachedText);
                }
            }

            LotteryDrawResponse lotteryDrawResponse = lotteryDrawMockDataService.GetMockLotteryDrawData(requestType);

            await fileCacheService.WriteTextToCache(GetLotteryDrawCacheFileName(requestType), JsonConvert.SerializeObject(lotteryDrawResponse));

            return lotteryDrawResponse;
        }

        private static string GetLotteryDrawCacheFileName(LotteryDrawRequestType requestType)
        {
            return $"lottery-draws-{requestType}-cached.json";
        }
    }
}