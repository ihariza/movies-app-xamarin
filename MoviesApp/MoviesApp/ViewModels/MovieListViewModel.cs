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

  
        private int currentPage;

        private bool _isLoading = true;
        public bool IsLoading
        {
            get => _isLoading;
            set { _isLoading = value; OnPropertyChanged(); }
        }

        private string _query;
        public string Query
        {
            get => _query;
            set { 
                _query = value;
                currentPage = 0;
                Movies.Clear();
                Movies.LoadMoreAsync();
            }
        }

        public InfiniteScrollCollection<Movie> Movies { get; set; }

        public ICommand GoToMovieDetailCommand => new Command<Movie>(GoToMovieDetail);

        public MovieListViewModel(
            INavigationService navigation, 
            IMovieDataService movieDataService,
            IDialogService dialogService)
        {
            _navigationService = navigation;
            _movieDataService = movieDataService;
            _dialogService = dialogService;

            Movies = new InfiniteScrollCollection<Movie>()
            {
                OnLoadMore = async () =>
                {
                    // load the next page
                    var page = (Movies.Count / PageSize) + 1;
                    List<Movie> movies = null;
                    if (page > currentPage)
                    {
                        currentPage = page;
                        if (string.IsNullOrWhiteSpace(_query))
                        {
                            movies = await GetMovies(currentPage);
                        }
                        else
                        {
                            movies = await SearchMovies(currentPage);
                        }
                    }
                    IsLoading = false;
                    return movies != null && movies.Count > 0 ? movies : null;
                }
            };

            Movies.LoadMoreAsync();
        }

        private async Task<List<Movie>> GetMovies(int page)
        {
            var dataWrapper = await _movieDataService.GetMovies(page);
            return GetMoviesFromDataWrapper(dataWrapper, page);
        }

        private async Task<List<Movie>> SearchMovies(int page)
        {
            var dataWrapper = await _movieDataService.SearchMovies(_query, page);
            return GetMoviesFromDataWrapper(dataWrapper, page);
        }

        private List<Movie> GetMoviesFromDataWrapper(DataWrapper<List<Movie>> dataWrapper, int pageToRetry)
        {
            List<Movie> movies = new List<Movie>();
            if (dataWrapper.Success)
            {
                if (dataWrapper.Result.Count > 0)
                {
                    movies.AddRange(dataWrapper.Result);
                }
            }
            else
            {
                ShowError(pageToRetry);
            }

            return movies;
        }

        private async void ShowError(int pageToRetry)
        {
            if (pageToRetry == PageStart)
            {
                await _dialogService.ShowError();
                Movies.Clear();
                await Movies.LoadMoreAsync();
            }
        }

        void GoToMovieDetail(Movie movie)
        {
            _navigationService.NavigateTo(ViewNames.MovieDetailPage, movie);
        }
    }
}