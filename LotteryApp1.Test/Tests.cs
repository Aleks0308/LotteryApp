using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace LotteryApp1.Test
{
    [TestFixture(Platform.Android)]
    public class Tests
    {
        IApp app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);

        }

        [Test]
        public void InitialScreenIsDisplayed()
        {
            AppResult[] results = app.WaitForElement(c => c.Marked("infoLabel"));
            app.Screenshot("Initial screen.");

            Assert.IsTrue(results.Any());
        }
    }
}
