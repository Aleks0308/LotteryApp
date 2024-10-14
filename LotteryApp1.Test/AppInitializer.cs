using System;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace LotteryApp1.Test
{
    public class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            if (platform == Platform.Android)
            {
                return ConfigureApp.Android.InstalledApp("com.companyname.lotteryapp1").StartApp();
            }

            return ConfigureApp.iOS.StartApp();
        }
    }
}