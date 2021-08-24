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

        void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item != null && ViewModelManager.MovieListViewModel.GoToMovieDetailCommand.CanExecute(e))
            {
                ViewModelManager.MovieListViewModel.GoToMovieDetailCommand.Execute(e.Item);
            }
        }

        void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            ViewModelManager.MovieListViewModel.PerformSearch.Execute(e.NewTextValue);
        }
    }
}