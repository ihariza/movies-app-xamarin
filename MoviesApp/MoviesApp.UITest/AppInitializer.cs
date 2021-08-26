using Xamarin.UITest;

namespace MoviesApp.UITest
{
    public class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            if (platform == Platform.Android)
            {
                return ConfigureApp.Android.InstalledApp("io.nhariza.moviesapp").StartApp();

            }

            return ConfigureApp.iOS.StartApp();
        }
    }
}