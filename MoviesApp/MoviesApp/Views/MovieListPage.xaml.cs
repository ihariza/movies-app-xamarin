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

        protected override void OnAppearing()
        {
            base.OnAppearing();
            moviesCollectionView.SelectedItem = null;
        }
    }
}