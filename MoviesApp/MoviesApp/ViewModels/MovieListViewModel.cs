using MoviesApp.Models;
using MoviesApp.Services;
using MoviesApp.Utility;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MoviesApp.ViewModels
{
    class MovieListViewModel : BaseViewModel
    {
        private const int PageStart = 1;
        private const int PageSize = 20;
        private const int ItemsMaxThreshold = 5;
        private readonly INavigationService _navigationService;
        private readonly IMovieDataService _movieDataService;
        private readonly IDialogService _dialogService;


        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set { _isLoading = value; OnPropertyChanged(); }
        }

        private int _itemsThreshold;
        public int ItemsThreshold
        {
            get => _itemsThreshold;
            set { _itemsThreshold = value; OnPropertyChanged(); }
        }

        private string _query;
        public string Query
        {
            get => _query;
            set { 
                _query = value;
                Movies.Clear();
                ItemsThreshold = ItemsMaxThreshold;
                LoadMovies();
            }
        }

        private ObservableCollection<Movie> _movies;
        public ObservableCollection<Movie> Movies
        {
            get => _movies;
            set { _movies = value; OnPropertyChanged(); }
        }

        public ICommand GoToMovieDetailCommand => new Command<Movie>(GoToMovieDetail);
        public ICommand ItemTresholdReachedCommand => new Command(LoadMovies);

        public MovieListViewModel(
            INavigationService navigation, 
            IMovieDataService movieDataService,
            IDialogService dialogService)
        {
            _navigationService = navigation;
            _movieDataService = movieDataService;
            _dialogService = dialogService;

            Movies = new ObservableCollection<Movie>();
            ItemsThreshold = ItemsMaxThreshold;

            LoadMovies();
        }

        private async void LoadMovies()
        {
            if (IsLoading)
            {
                return;
            }
            IsLoading = true;
            int page = (Movies.Count / PageSize) + 1;
            List<Movie> movies = new List<Movie>();

            movies = string.IsNullOrWhiteSpace(_query) ? await GetMovies(page) : await SearchMovies(page);
            movies.ForEach(movie =>
            {
                Movies.Add(movie);
            });

            if (movies.Count < PageSize)
            {
                ItemsThreshold = -1;
            }
            IsLoading = false;
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
                LoadMovies();
            }
        }

        void GoToMovieDetail(Movie movie)
        {
            _navigationService.NavigateTo(ViewNames.MovieDetailPage, movie);
        }
    }
}