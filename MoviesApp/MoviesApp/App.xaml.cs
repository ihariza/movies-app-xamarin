using MoviesApp.Services;
using MoviesApp.Utility;
using MoviesApp.Views;
using Xamarin.Forms;

namespace MoviesApp
{
    public partial class App : Application
    {

        public static NavigationService NavigationService { get; } = new NavigationService();
        public static MovieDataService MovieDataService { get; } = new MovieDataService();
        public static DialogService DialogService { get; } = new DialogService();
        public App()
        {
            InitializeComponent();

            NavigationService.Configure(ViewNames.MovieListPage, typeof(MovieListPage));
            NavigationService.Configure(ViewNames.MovieDetailPage, typeof(MovieDetailPage));

            MainPage = new NavigationPage(new MovieListPage());
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
