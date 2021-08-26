using NUnit.Framework;
using System.Linq;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace MoviesApp.UITest
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void AppNameIsDisplayed()
        {
            AppResult[] results = app.WaitForElement(c => c.Marked("MoviesApp"));
            app.Screenshot("Movie List.");

            Assert.IsTrue(results.Any());
        }
    }
}
