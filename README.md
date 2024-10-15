# LotteryApp

Technologies used in the solution are Xamarin.Forms with PCLStorage for local cache. This type of project offered good boilerplate solution to rapidly expand it to achieve the tasks.

## Essential tasks completed:
1.  ✅ Parse JSON Data https://github.com/Aleks0308/LotteryApp/blob/13d2dcae97918f6886120113463a81aa0529f192/LotteryApp1/LotteryApp1/Services/LotteryDrawMockDataService.cs#L17
2.  ✅ Display Lottery Draws [ItemsViewModel](https://github.com/Aleks0308/LotteryApp/blob/13d2dcae97918f6886120113463a81aa0529f192/LotteryApp1/LotteryApp1/ViewModels/ItemsViewModel.cs) / [ItemsPage](https://github.com/Aleks0308/LotteryApp/blob/13d2dcae97918f6886120113463a81aa0529f192/LotteryApp1/LotteryApp1/Views/ItemsPage.xaml)
3.  ✅ Unit Testing [LotteryDrawDataStoreTests](https://github.com/Aleks0308/LotteryApp/blob/13d2dcae97918f6886120113463a81aa0529f192/LotteryApp.UnitTests/LotteryDrawDataStoreTests.cs) / [LotteryDrawServiceTests](https://github.com/Aleks0308/LotteryApp/blob/13d2dcae97918f6886120113463a81aa0529f192/LotteryApp.UnitTests/LotteryDrawServiceTests.cs)
4.  ❔ Integration testing: WaitForElement in UITests always times out

## Additional tasks completed
1. ✅ Detail View for Each Draw: Implement a detail view for each lottery draw, showing
all numbers and the bonus ball.  \
[ItemDetailViewModel](https://github.com/Aleks0308/LotteryApp/blob/13d2dcae97918f6886120113463a81aa0529f192/LotteryApp1/LotteryApp1/ViewModels/ItemDetailViewModel.cs) / [ItemDetailPage](https://github.com/Aleks0308/LotteryApp/blob/13d2dcae97918f6886120113463a81aa0529f192/LotteryApp1/LotteryApp1/Views/ItemDetailPage.xaml)
3. ✅ Lottery Tickets: Implement a &#39;ticket view&#39; that shows some randomly generated
lottery tickets and if they have won or not. \
[Randomly generated list of lottery draws](https://github.com/Aleks0308/LotteryApp/blob/13d2dcae97918f6886120113463a81aa0529f192/LotteryApp1/LotteryApp1/Services/LotteryDrawMockDataService.cs#L18) / [Lucky Dip button in draw details](https://github.com/Aleks0308/LotteryApp/blob/13d2dcae97918f6886120113463a81aa0529f192/LotteryApp1/LotteryApp1/ViewModels/ItemDetailViewModel.cs#L115C19-L115C20)
4. ✅ Navigation: Add basic navigation from the main list view to the detail views of each
draw.
6. ✅ Local Storage: Cache the lottery draws locally and allow the app to display the
cached data when offline. https://github.com/Aleks0308/LotteryApp/blob/13d2dcae97918f6886120113463a81aa0529f192/LotteryApp1/LotteryApp1/Services/LotteryDrawService.cs#L30
   
   

<img src="https://github.com/user-attachments/assets/43f331a7-8084-43cf-8dc1-f90ca27b5da3">
<img src="https://github.com/user-attachments/assets/44030012-4623-45c8-8c8c-aaee6bc0f0a2" height="450">

## What more could be done
4. ❔ Additional Tests: Add more thorough testing and improve test coverage.  \
   There is some known bug which does not let the UITests find any elements when using WaitForElement. With more time, the root cause would be found.  \
   Also more unit tests for other services could be created.
6. ❔ Interactive Navigation: Implement swipe gestures to navigate between different
draw details.  \
  A GestureRecognizer could be used to navigate between ItemsPage and ItemDetailPage 
