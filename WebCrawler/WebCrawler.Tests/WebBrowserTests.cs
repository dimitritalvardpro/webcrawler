namespace WebCrawler.Tests
{
    [TestClass]
    public sealed class WebBrowserTests
    {
        [TestMethod]
        public void GetHtml_ShouldReturns_Null()
        {
            WebBrowser webBrowser = new();

            string content = webBrowser.GetHtml("C:\\TestHtml\\xddl.html");

            Assert.IsNull(content);
        }

        [TestMethod]
        public void GetHtml_ShouldReturns_Content()
        {
            WebBrowser webBrowser = new();

            string content = webBrowser.GetHtml("C:\\TestHtml\\index.html");

            Assert.IsNotNull(content);
            StringAssert.Contains(content, "<h1>INDEX</h1>");
        }
    }
}
