// ---------------------------------------------------------------
// <author>Paul Datsyuk</author>
// <url>https://www.linkedin.com/in/pauldatsyuk/</url>
// ---------------------------------------------------------------

using NUnit.Framework;
using Xamarin.UITest;

namespace TyreKlicker.UITests
{
    [TestFixture(Platform.Android, Category = "Andorid")]
    [TestFixture(Platform.iOS, Category = "iOS")]
    public class Tests
    {
        private IApp app;
        private Platform platform;

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
        public void AppLaunches()
        {
            // Assert
            var results = app.Query(c => c.Marked("MainPageTitleLbl"));
            Assert.AreEqual("Hello Xamarin!", results[0].Text);
        }
    }
}