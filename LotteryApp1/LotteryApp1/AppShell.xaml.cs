using LotteryApp1.ViewModels;
using LotteryApp1.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace LotteryApp1
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
        }

    }
}
