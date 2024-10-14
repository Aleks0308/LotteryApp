using LotteryApp.Services;
using LotteryApp1.Services;
using LotteryApp1.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LotteryApp1
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<LotteryDrawDataStore>();
            DependencyService.Register<IFileService, FileService>();
            DependencyService.Register<IFileCacheService, FileCacheService>();
            DependencyService.Register<ILotteryDrawMockDataService, LotteryDrawMockDataService>();
            DependencyService.Register<ILotteryDrawService, LotteryDrawService>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
