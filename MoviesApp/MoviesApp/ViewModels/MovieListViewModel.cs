using MoviesApp.Models;
using MoviesApp.Services;
using MoviesApp.Utility;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Extended;

namespace MoviesApp.ViewModels
{
    class MovieListViewModel : BaseViewModel
    {
        private const int PageStart = 1;
        private const int PageSize = 20;
        private readonly INavigationService _navigationService;
        private readonly IMovieDataService _movieDataService;
        private readonly IDialogService _dialogService;

        private bool _isLoading = true;
        public bool IsLoading
        {
            get => _isLoading;
            set { _isLoading = value; OnPropertyChanged(); }
        }
        public InfiniteScrollCollection<Movie> Movies { get; set; }

        public ICommand GoToMovieDetailCommand { private set; get; }
        public MovieListViewModel(
            INavigationService navigation, 
            IMovieDataService movieDataService,
            IDialogService dialogService)
        {
            _navigationService = navigation;
            _movieDataService = movieDataService;
            _dialogService = dialogService;

            GoToMovieDetailCommand = new Command<Movie>(GoToMovieDetail);

            Movies = new InfiniteScrollCollection<Movie>()
            {
                OnLoadMore = async () =>
                {
                    // load the next page
                    var page = (Movies.Count / PageSize) + 1;
                    var movies = await GetMovies(page);
                    return movies;
                }
            };

            GetMoviesStart();
        }

        private async void GetMoviesStart()
        {
            var movies = await GetMovies(PageStart);
            Movies.AddRange(movies);
        }

        private async Task<List<Movie>> GetMovies(int page)
        {
            List<Movie> movies = new List<Movie>();
            var dataWrapper = await _movieDataService.GetMovies(page);
            if (dataWrapper.Success)
            {
                movies.AddRange(dataWrapper.Result);
            } else
            {
                ShowError(page);
            }
            return movies;
        }

        private async void ShowError(int pageToRetry)
        {
            if (pageToRetry == PageStart)
            {
                await _dialogService.ShowError();
                GetMoviesStart();
            }
        }

        void GoToMovieDetail(Movie movie)
        {
            _navigationService.NavigateTo(ViewNames.MovieDetailPage, movie);
        }
    }
}