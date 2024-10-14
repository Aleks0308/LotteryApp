using LotteryApp1.Models;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LotteryApp1.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : BaseViewModel
    {
        public Command LuckyDipCommand { get; }

        private string itemId;
        private string drawDate;
        private string numbers;
        private string bonusBall;
        private string topPrize;
        private LotteryDraw luckyDip;
        private LotteryDraw currentLotteryDraw;
        private string luckyDipMessage;
        private string luckyDipNumbers;
        private bool showLuckyDip;

        public ItemDetailViewModel()
        {
            LuckyDipCommand = new Command(async () => await GenerateLuckyDip());
        }

        public string Id { get; set; }

        public string ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadItemId(value);
            }
        }

        public string DrawDate
        {
            get => drawDate;
            set => SetProperty(ref drawDate, value);
        }

        public string Numbers
        {
            get => numbers;
            set => SetProperty(ref numbers, value);
        }

        public string LuckyDipNumbers
        {
            get => luckyDipNumbers;
            set => SetProperty(ref luckyDipNumbers, value);
        }

        public string BonusBall
        {
            get => bonusBall;
            set => SetProperty(ref bonusBall, value);
        }

        public string TopPrize
        {
            get => topPrize;
            set => SetProperty(ref topPrize, value);
        }

        public string LuckyDipMessage
        {
            get => luckyDipMessage;
            set => SetProperty(ref luckyDipMessage, value);
        }

        public LotteryDraw LuckyDip
        {
            get => luckyDip;
            set => SetProperty(ref luckyDip, value);
        }

        public bool ShowLuckyDip
        {
            get => showLuckyDip;
            set => SetProperty(ref showLuckyDip, value);
        }

        public async void LoadItemId(string itemId)
        {
            try
            {
                LotteryDraw item = await DataStore.GetItemAsync(itemId);
                currentLotteryDraw = item;
                Id = item.Id;
                DrawDate = item.DrawDate?.ToShortDateString();
                Numbers = $"{item.Number1} {item.Number2} {item.Number3} {item.Number4} {item.Number5} {item.Number6}";
                BonusBall = item.BonusBall?.ToString();
                TopPrize = item.TopPrize?.ToString("C0", CultureInfo.GetCultureInfo("en-GB"));
                Title = item.Id;
                ShowLuckyDip = false;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        private async Task GenerateLuckyDip()
        {
            LotteryDrawResponse lotteryDrawResponse = await LotteryDrawService.GetLotteryDraws(LotteryDrawRequestType.LuckyDip);
            LotteryDraw lotteryDraw = lotteryDrawResponse.draws.FirstOrDefault();

            var luckyDipNumbers = new int?[]
            {
                lotteryDraw.Number1,
                lotteryDraw.Number2,
                lotteryDraw.Number3,
                lotteryDraw.Number4,
                lotteryDraw.Number5,
                lotteryDraw.Number6
            };

            var currentNumbers = new int?[]
            {
                currentLotteryDraw.Number1,
                currentLotteryDraw.Number2,
                currentLotteryDraw.Number3,
                currentLotteryDraw.Number4,
                currentLotteryDraw.Number5,
                currentLotteryDraw.Number6
            };

            var winningNumbers = luckyDipNumbers.Where(x => currentNumbers.Contains(x));
            bool bonusBallWin = winningNumbers.Count() == 5 && lotteryDraw.BonusBall == currentLotteryDraw.BonusBall;

            if (bonusBallWin || winningNumbers.Count() == 6)
            {
                LuckyDipMessage = "Congratulations! You win!";
            }
            else
            {
                LuckyDipMessage = "Better luck next time!";
            }

            LuckyDipNumbers = string.Join(" ", luckyDipNumbers);
            LuckyDip = lotteryDraw;
            ShowLuckyDip = true;
        }
    }
}
