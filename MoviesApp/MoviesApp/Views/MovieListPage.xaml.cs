using MoviesApp.Models;
using MoviesApp.Utility;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoviesApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MovieListPage : ContentPage
    {
        public MovieListPage()
        {
            InitializeComponent();
            BindingContext = ViewModelManager.MovieListViewModel;
        }

        void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Movie movie = e.CurrentSelection.Count > 0 ? e.CurrentSelection[0] as Movie : null;
            if (movie != null)
            {
                ViewModelManager.MovieListViewModel.GoToMovieDetailCommand.Execute(movie);
                moviesCollectionView.SelectedItem = null;
            }
        }
    }
}