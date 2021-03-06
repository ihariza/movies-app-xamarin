using MoviesApp.ViewModels;


namespace MoviesApp.Utility
{
    class ViewModelManager
    {
        public static MovieListViewModel MovieListViewModel { get; set; } =
            new MovieListViewModel(App.NavigationService, App.MovieDataService, App.DialogService);
        public static MovieDetailViewModel MovieDetailViewModel { get; set; } = new MovieDetailViewModel();
    }
}
