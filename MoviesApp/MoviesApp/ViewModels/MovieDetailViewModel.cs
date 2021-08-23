using MoviesApp.Models;

namespace MoviesApp.ViewModels
{
    class MovieDetailViewModel : BaseViewModel
    {
        private Movie _movie;
        public Movie Movie
        {
            get => _movie;
            set { _movie = value; OnPropertyChanged(); }
        }

        public MovieDetailViewModel()
        {
            Movie = new Movie();
        }

        public override void Initialize(object parameter)
        {
            Movie = parameter == null ? new Movie() : parameter as Movie;
        }
    }
}
