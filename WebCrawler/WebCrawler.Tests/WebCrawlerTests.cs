namespace WebCrawler.Tests
{
    [TestClass]
    public sealed class WebCrawlerTests
    {
        [TestMethod]
        public void GetEmailsInPageAndChildPages_PageNotExisting_ShouldReturns_Empty()
        {
            WebCrawler webCrawler = new();
            WebBrowser webBrowser = new();

            var emails = webCrawler.GetEmailsInPageAndChildPages(webBrowser, "C:\\TestHtml\\xddl.html", 0);

            Assert.IsEmpty(emails);
        }

        [TestMethod]
        [DataRow("C:\\TestHtml\\index.html", 0, new string[] { "nullepart@mozilla.org" })]
        [DataRow("C:\\TestHtml\\index.html", 1, new string[] { "nullepart@mozilla.org", "ailleurs@mozilla.org" })]
        [DataRow("C:\\TestHtml\\index.html", 2, new string[] { "nullepart@mozilla.org", "ailleurs@mozilla.org", "loin@mozilla.org" })]
        [DataRow("C:\\TestHtml\\index.html", -1, new string[] { "nullepart@mozilla.org", "ailleurs@mozilla.org", "loin@mozilla.org" })]
        public void GetEmailsInPageAndChildPages_ShouldReturns_ExpectedEmails(string url, int depth, string[] resultEmails)
        {
            WebCrawler webCrawler = new();
            WebBrowser webBrowser = new();

            var emails = webCrawler.GetEmailsInPageAndChildPages(webBrowser, url, depth);

            CollectionAssert.AreEquivalent(resultEmails, emails.ToArray());
        }
    }
}
