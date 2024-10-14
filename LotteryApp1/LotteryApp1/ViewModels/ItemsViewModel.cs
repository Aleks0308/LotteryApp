using LotteryApp1.Models;
using LotteryApp1.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LotteryApp1.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private Item _selectedItem;

        public ObservableCollection<Item> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command ToggleRandomCommand { get; }
        public Command ToggleCachedCommand { get; }
        public Command<Item> ItemTapped { get; }

        bool cachedEnabled = false;
        public bool CachedEnabled
        {
            get { return cachedEnabled; }
            set { SetProperty(ref cachedEnabled, value); }
        }

        bool randomEnabled = false;
        public bool RandomEnabled
        {
            get { return randomEnabled; }
            set { SetProperty(ref randomEnabled, value); }
        }

        public ItemsViewModel()
        {
            Title = "Lottery Draws";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<Item>(OnItemSelected);

            ToggleCachedCommand = new Command(async () => await ToggleCached());
            ToggleRandomCommand= new Command(async () => await ToggleRandom());
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                await ReloadItems();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task ReloadItems()
        {
            Items.Clear();
            await DataStore.DeleteItemsAsync();

            LotteryDrawRequestType lotteryDrawRequestType = RandomEnabled ? LotteryDrawRequestType.Random : LotteryDrawRequestType.Latest;
            LotteryDrawResponse lotteryDrawResponse = await LotteryDrawService.GetLotteryDraws(lotteryDrawRequestType, CachedEnabled);
            await DataStore.AddItemsAsync(lotteryDrawResponse.draws);

            foreach (var item in lotteryDrawResponse.draws)
            {
                Items.Add(new Item
                {
                    Id = item.Id,
                    Description = $"[{item.Id}] - {item.TopPrize?.ToString("C0", CultureInfo.GetCultureInfo("en-GB"))}",
                    Text = item.DrawDate?.ToShortDateString()
                });
            }
        }

        public void OnAppearing()
        {
            if(Items.Count == 0)
            {
                IsBusy = true;
            }
            SelectedItem = null;
        }

        public Item SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        private async Task ToggleRandom()
        {
            RandomEnabled = !RandomEnabled;
            await ReloadItems();
        }

        private async Task ToggleCached()
        {
            CachedEnabled = !CachedEnabled;
            await ReloadItems();
        }

        async void OnItemSelected(Item item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }
    }
}