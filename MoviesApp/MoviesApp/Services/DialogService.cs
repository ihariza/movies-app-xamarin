using System.Threading.Tasks;
using Xamarin.Forms;

namespace MoviesApp.Services
{
    public class DialogService : IDialogService
    {
        public async Task ShowError()
        {
            await Application.Current.MainPage.DisplayAlert(
                AppResources.CommonErrorTitle, AppResources.CommonErrorSubtitle, AppResources.CommonRetry);
        }
    }
}
