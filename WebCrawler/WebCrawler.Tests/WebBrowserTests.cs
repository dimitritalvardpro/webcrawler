namespace WebCrawler.Tests
{
    [TestClass]
    public sealed class WebBrowserTests
    {
        [TestMethod]
        public void GetHtml_NotExistingFile_ShouldReturns_Null()
        {
            WebBrowser webBrowser = new();

            string content = webBrowser.GetHtml("C:\\TestHtml\\xddl.html");

            Assert.IsNull(content);
        }

        [TestMethod]
        public void GetHtml_ExistingFile_ShouldReturns_Content()
        {
            WebBrowser webBrowser = new();

            string content = webBrowser.GetHtml("C:\\TestHtml\\index.html");

            Assert.IsNotNull(content);
            StringAssert.Contains(content, "<h1>INDEX</h1>");
        }

        [TestMethod]
        public void ResolvePath_RelativePath_ShouldReturns_AbsolutePAthWithParentBase()
        {
            WebBrowser webBrowser = new();

            var resolvedPath = webBrowser.ResolvePath("C:\\Test\\Parent.html", ".\\Child.html");

            Assert.AreEqual("C:\\Test\\Child.html", resolvedPath);
        }

        [TestMethod]
        public void ResolvePath_AbsolutePath_ShouldReturns_AbsolutePath()
        {
            WebBrowser webBrowser = new();

            var resolvedPath = webBrowser.ResolvePath("C:\\Test\\Parent.html", "C:\\Test\\ChildPages\\Child.html");

            Assert.AreEqual("C:\\Test\\ChildPages\\Child.html", resolvedPath);
        }
    }
}
