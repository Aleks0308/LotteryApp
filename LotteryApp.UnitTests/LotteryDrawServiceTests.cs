using LotteryApp1.Models;
using LotteryApp1.Services;
using Newtonsoft.Json;
using Moq;
using LotteryApp.Services;

namespace LotteryApp1.Tests
{
    [TestFixture]
    public class LotteryDrawServiceTests
    {
        private Mock<IFileCacheService> mockFileCacheService;
        private Mock<ILotteryDrawMockDataService> mockLotteryDrawMockDataService;
        private LotteryDrawService lotteryDrawService;

        [SetUp]
        public void Setup()
        {
            mockFileCacheService = new Mock<IFileCacheService>();
            mockLotteryDrawMockDataService = new Mock<ILotteryDrawMockDataService>();

            lotteryDrawService = new LotteryDrawService(mockFileCacheService.Object, mockLotteryDrawMockDataService.Object);
        }

        [TestCase(LotteryDrawRequestType.Latest)]
        [TestCase(LotteryDrawRequestType.Random)]
        [TestCase(LotteryDrawRequestType.LuckyDip)]
        public async Task GetLotteryDraws_WhenCalled_GeneratesCorrectCacheFileName(LotteryDrawRequestType requestType)
        {
            // Arrange
            string expectedFileName = $"lottery-draws-{requestType}-cached.json";

            // Act
            await lotteryDrawService.GetLotteryDraws(requestType, cached: false);

            // Assert
            mockFileCacheService.Verify(m => m.WriteTextToCache(expectedFileName, It.IsAny<string>()));
        }

        [Test]
        public async Task GetLotteryDraws_WhenCached_ReturnsCachedData()
        {
            // Arrange
            var expectedResponse = new LotteryDrawResponse
            {
                draws = new List<LotteryDraw> { new LotteryDraw { Id = "102" } }
            };

            mockFileCacheService.Setup(m => m.GetTextFromCache(It.IsAny<string>())).ReturnsAsync(JsonConvert.SerializeObject(expectedResponse));

            // Act
            var response = await lotteryDrawService.GetLotteryDraws(LotteryDrawRequestType.Latest, cached: true);

            // Assert
            Assert.That(JsonConvert.SerializeObject(response), Is.EqualTo(JsonConvert.SerializeObject(expectedResponse)));
        }

        [Test]
        public async Task GetLotteryDraws_WhenNotCached_ReturnsMockData_AndWritesCache()
        {
            // Arrange
            var expectedResponse = new LotteryDrawResponse
            {
                draws = new List<LotteryDraw> { new LotteryDraw { Id = "103" } }
            };

            mockLotteryDrawMockDataService.Setup(m => m.GetMockLotteryDrawData(It.IsAny<LotteryDrawRequestType>())).Returns(expectedResponse);
            mockFileCacheService.Setup(m => m.GetTextFromCache(It.IsAny<string>())).ReturnsAsync("");

            // Act
            var response = await lotteryDrawService.GetLotteryDraws(LotteryDrawRequestType.Latest, cached: false);

            // Assert
            Assert.That(response.draws, Is.EqualTo(expectedResponse.draws));
            mockFileCacheService.Verify(m => m.WriteTextToCache(It.IsAny<string>(), JsonConvert.SerializeObject(expectedResponse)));
        }

        [Test]
        public async Task GetLotteryDraws_WhenCachedDataIsEmpty_ReturnsMockData_AndWritesCache()
        {
            // Arrange
            var expectedResponse = new LotteryDrawResponse
            {
                draws = new List<LotteryDraw> { new LotteryDraw { Id = "104" } }
            };

            mockLotteryDrawMockDataService.Setup(m => m.GetMockLotteryDrawData(It.IsAny<LotteryDrawRequestType>())).Returns(expectedResponse);
            mockFileCacheService.Setup(m => m.GetTextFromCache(It.IsAny<string>())).ReturnsAsync("");

            // Act
            var response = await lotteryDrawService.GetLotteryDraws(LotteryDrawRequestType.Latest, cached: true);

            // Assert
            Assert.That(response.draws, Is.EqualTo(expectedResponse.draws));
            mockFileCacheService.Verify(m => m.WriteTextToCache(It.IsAny<string>(), JsonConvert.SerializeObject(expectedResponse)));
        }
    }
}