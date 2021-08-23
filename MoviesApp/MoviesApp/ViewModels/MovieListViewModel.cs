using MoviesApp.Models;
using MoviesApp.Services;
using MoviesApp.Utility;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Extended;

namespace MoviesApp.ViewModels
{
    class MovieListViewModel : BaseViewModel
    {
        private const int PageSize = 20;
        private readonly INavigationService _navigationService;
        private readonly IMovieDataService _movieDataService;

        private bool _isLoading = true;
        public bool IsLoading
        {
            get => _isLoading;
            set { _isLoading = value; OnPropertyChanged(); }
        }
        public InfiniteScrollCollection<Movie> Movies { get; set; }

        public ICommand GoToMovieDetailCommand { private set; get; }
        public MovieListViewModel(INavigationService navigation, IMovieDataService movieDataService)
        {
            _navigationService = navigation;
            _movieDataService = movieDataService;

            GoToMovieDetailCommand = new Command<Movie>(GoToMovieDetail);

            Movies = new InfiniteScrollCollection<Movie>()
            {
                OnLoadMore = async () =>
                {
                    // load the next page
                    var page = (Movies.Count / PageSize) + 1;
                    var movies = await _movieDataService.GetMovies(page);
                    return movies;
                }
            };

            GetMovies();
        }

        private async void GetMovies()
        {
            var movies = await _movieDataService.GetMovies(1);
            Movies.AddRange(movies);
        }

        void GoToMovieDetail(Movie movie)
        {
            _navigationService.NavigateTo(ViewNames.MovieDetailPage, movie);
        }
    }
}